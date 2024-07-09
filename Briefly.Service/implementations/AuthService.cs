using Briefly.Data.Result;
using Briefly.Infrastructure.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace Briefly.Service.implementations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<User> _roleManager;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly JwtSettings _jwtSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUrlHelper _urlHelper;
        private readonly IEmailService _emailService;
        private readonly GoogleAuthSetting _googleAuthConfig;
        public AuthService(UserManager<User> userManager, ApplicationDbContext applicationDbContext, JwtSettings jwtSettings,
            IHttpContextAccessor httpContextAccessor, IUrlHelper urlHelper, IEmailService emailService, GoogleAuthSetting googleAuthConfig)
        {
            _userManager = userManager;
            _applicationDbContext = applicationDbContext;
            _jwtSettings = jwtSettings;
            _httpContextAccessor = httpContextAccessor;
            _urlHelper = urlHelper;
            _emailService = emailService;
            _googleAuthConfig = googleAuthConfig;
        }

        public async Task<string> Register(User user, string password)
        {
            user.UserName = user.Email?.Substring(0, user.Email.IndexOf('@'));
            var trans = await _applicationDbContext.Database.BeginTransactionAsync();
            try
            {
                var CheckUserName = await _userManager.FindByNameAsync(user.UserName);
                if (CheckUserName != null)
                {
                    return "UserName Is Already Exits";
                }
                var CheckUserEmail = await _userManager.FindByEmailAsync(user.Email);
                if (CheckUserEmail != null)
                {
                    return "Email is Already Exist";
                }
                var result = await _userManager.CreateAsync(user, password);
                if (!result.Succeeded)
                {
                    await trans.RollbackAsync();
                    return string.Join(",", result.Errors.Select(x => x.Description).ToList());
                }
                await _userManager.AddToRoleAsync(user, "User");

                // dealing with email request
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var requestAccessor = _httpContextAccessor.HttpContext?.Request;
                var requestUrl = requestAccessor?.Scheme + "://" + requestAccessor.Host + _urlHelper.Action(new UrlActionContext { Action = "ConfirmEmail", Controller = "Auth", Values = new { userId = user.Id, Code = code } });
                var requestMessage = $"to confirm email click link: {requestUrl}";
                var sentResultEmail = await _emailService.SendEmailAsync(user.Email, requestMessage, "Confirm Email");

                await trans.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return "failed to add user";
            }
        }

        public async Task<JwtResult> GenerateToken(User user)
        {
            var (jwtToken, accessToken) = await GetToken(user);
            var RefreshToken = GetrefreshToken();
            var userRefreshToken = new UserRefreshToken()
            {
                UserId = user.Id,
                JwtId = jwtToken.Id,
                Token = accessToken,
                refreshToken = RefreshToken.RefreshTokenString,
                IsUsed = true,
                IsRevoked = false,
                AddedTime = DateTime.Now,
                ExpiryDate = RefreshToken.ExpireAt,
            };
            await _applicationDbContext.RefreshToken.AddAsync(userRefreshToken);
            await _applicationDbContext.SaveChangesAsync();

            var result = new JwtResult()
            {
                Token = accessToken,
                RefreshToken = RefreshToken
            };
            return result;
        }

        private async Task<(JwtSecurityToken, string)> GetToken(User user)
        {
            var userClaims = await GenerateClaims(user);
            JwtSecurityToken jwtToken = new JwtSecurityToken
                (
                   issuer: _jwtSettings.Issuer,
                   audience: _jwtSettings.Audience,
                   claims: userClaims,
                   expires: DateTime.Now.AddDays(_jwtSettings.AccessTokenExpireDate),
                   signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256)
                );
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return (jwtToken, accessToken);
        }
        private async Task<List<Claim>> GenerateClaims(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(nameof(UserClaimTypes.UserId), user.Id.ToString()),
                new Claim(nameof(UserClaimTypes.UserName), user.UserName),
                new Claim(nameof(UserClaimTypes.Email), user.Email),
            };

            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            return claims;
        }

        private RefreshToken GetrefreshToken()
        {
            var refreshToken = new RefreshToken()
            {
                ExpireAt = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
                RefreshTokenString = GenerateRefreshToken()
            };
            return refreshToken;
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            var generateRandom = RandomNumberGenerator.Create();
            generateRandom.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public async Task<string> ConfirmEmailAsync(int? UserId, string Code)
        {
            var user = await _userManager.FindByIdAsync(UserId.ToString());
            if (UserId == null || Code == null)
            {
                return "FailedToConfirmEmail";
            }
            var checkCode = await _userManager.ConfirmEmailAsync(user, Code);
            if (!checkCode.Succeeded)
            {
                return "FailedToConfirmEmail";
            }
            return "ConfirmedSuccessfully";
        }

        public async Task<string> SendResetPassword(string email)
        {
            var trans = await _applicationDbContext.Database.BeginTransactionAsync();
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    return "notfound";
                }

                Random Generator = new Random();
                var randomNumber = Generator.Next(0, 10000).ToString();

                user.Code = randomNumber;
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    return "failed";
                }

                var message = $"code to reset password : {randomNumber}";
                await _emailService.SendEmailAsync(email, message, "ResetPassword");

                await trans.CommitAsync();
                return "success";
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return ex.Message.ToString();
            }
        }

        public async Task<string> ConfirmResetPassword(string email, string code)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return "usernotfound";
            }
            var userCode = user.Code;
            if (userCode != code)
                return "failed";
            return "seccess";
        }

        public async Task<string> ResetPassword(string email, string password)
        {
            var trans = await _applicationDbContext.Database.BeginTransactionAsync();
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    return "userNotFound";
                }
                await _userManager.RemovePasswordAsync(user);
                await _userManager.AddPasswordAsync(user, password);
                await trans.CommitAsync();
                return "success";
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return "failed";
            }
        }

        public async Task<(string, JwtResult)> GenerateRefreshToken(JwtSecurityToken securityToken, string token, string refreshToken)
        {
            if (!securityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256))
            {
                return ("AlgoritmIsNotCorrect", null);
            }

            if (securityToken.ValidTo > DateTime.Now)
            {
                return ("tokenIsNotExpireed", null);
            }

            var userId = securityToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimTypes.UserId)).Value;
            var refreshtoenUser = await _applicationDbContext.RefreshToken.FirstOrDefaultAsync(x => x.UserId == int.Parse(userId)
                                                                                         && x.Token == token && x.refreshToken == refreshToken);
            if (refreshtoenUser == null)
            {
                return ("RefreshTokenNotFound", null);
            }

            if (refreshtoenUser.ExpiryDate < DateTime.Now)
            {
                refreshtoenUser.IsRevoked = true;
                refreshtoenUser.IsUsed = false;
                _applicationDbContext.RefreshToken.Update(refreshtoenUser);
                await _applicationDbContext.SaveChangesAsync();
                return ("RefreshTokenIsExpired", null);
            }

            var user = await _userManager.FindByIdAsync(userId);
            var (jwtToken, accessToken) = await GetToken(user);

            refreshtoenUser.Token = accessToken;
            refreshtoenUser.JwtId = jwtToken.Id;
            _applicationDbContext.RefreshToken.Update(refreshtoenUser);
            await _applicationDbContext.SaveChangesAsync();

            JwtResult jwtResult = new();
            jwtResult.Token = accessToken;
            var refreshtoken = new RefreshToken()
            {
                RefreshTokenString = refreshToken,
                ExpireAt = refreshtoenUser.ExpiryDate
            };
            jwtResult.RefreshToken = refreshtoken;

            return ("", jwtResult);
        }

        public async Task<bool> CheckValidationToken(string token)
        {
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
            var securityToken = jwtSecurityTokenHandler.ReadJwtToken(token);
            if (securityToken.ValidTo < DateTime.Now)
            {
                return false;
            }
            return true;
        }

        public async Task<JwtResult> GoogleSignIn(string IdToken)
        {
            try
            {
                Payload payload = new();
                payload = await ValidateAsync(IdToken, new ValidationSettings
                {
                    Audience = new[] { _googleAuthConfig.ClientId }
                });

                var user = await _userManager.FindByEmailAsync(payload.Email);
                if (user == null)
                {
                    var userToBeCreated = new User
                    {
                        FirstName = payload.GivenName,
                        LastName = payload.FamilyName,
                        Email = payload.Email,
                        UserName = Guid.NewGuid().ToString()
                    };
                    var res = await _userManager.CreateAsync(userToBeCreated);
                    if (!res.Succeeded)
                    {
                        return null;
                    }
                    await _userManager.AddToRoleAsync(userToBeCreated, "User");

                    var UserLoginInfo = new UserLoginInfo("Google", userToBeCreated.Id.ToString(), userToBeCreated.UserName);
                    await _userManager.AddLoginAsync(userToBeCreated, UserLoginInfo);

                    return await GenerateToken(userToBeCreated);
                }

                var providerKey = user.Id.ToString();
                var existingLogin = await _userManager.FindByLoginAsync("Google", providerKey);
                if (existingLogin == null)
                {
                    var UserLoginInformation = new UserLoginInfo("Google", user.Id.ToString(), user.UserName);
                    await _userManager.AddLoginAsync(user, UserLoginInformation);
                }
                return await GenerateToken(user);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
