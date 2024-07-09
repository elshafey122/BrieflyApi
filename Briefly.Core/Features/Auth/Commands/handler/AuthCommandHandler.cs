using Briefly.Data.Identity;
using Briefly.Data.Result;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace Briefly.Core.Features.Auth.Commands.handler
{
    public class ArticleCommandHandler : ResponseHandler,
                                                     IRequestHandler<RegisterCommand, Response<string>>,
                                                     IRequestHandler<LoginCommand, Response<JwtResult>>,
                                                     IRequestHandler<SendResetPasswordCommand, Response<string>>,
                                                     IRequestHandler<ResetPasswordCommand, Response<string>>,
                                                     IRequestHandler<RefreshTokenCommand, Response<JwtResult>>,
                                                     IRequestHandler<GoogleSigninCommand, Response<JwtResult>>
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public ArticleCommandHandler(IAuthService authService, IMapper mapper, UserManager<User> userManager)
        {
            _authService = authService;
            _mapper = mapper;
            _userManager = userManager;
        }


        public async Task<Response<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            //var user = _mapper.Map<User>(request);
            var user = new User()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email
            };
            var result = await _authService.Register(user, request.Password);
             switch (result)
            {
                case "Email is Already Exist":
                    return BadRequest<string>("Email is already exists");
                    break;
                case "failed to add user":
                    return BadRequest<string>("Failed to add user");
                    break;
                case "Success":
                    return Created<string>("Please check your email to confirm your registration.");
                    break;
                default:
                    return BadRequest<string>(result);
            }
        }

        public async Task<Response<JwtResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    return NotFound<JwtResult>( "User not found");
                }
                var userPassword = await _userManager.CheckPasswordAsync(user, request.Password);
                if (!userPassword)
                {
                    return BadRequest<JwtResult>("Password is not correct");
                }
                if (!user.EmailConfirmed)
                {
                    return BadRequest<JwtResult>("Email is not confirmed yet check your gmail to confirm and try again");
                }

                var token = await _authService.GenerateToken(user);
                return Success<JwtResult>(token,"user signin successfully");

            }
            catch (Exception ex)
            {
                return BadRequest<JwtResult>(ex.Message);
            }
        }

        public async Task<Response<string>> Handle(SendResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _authService.SendResetPassword(request.Email);
            switch (result)
            {
                case "notfound":
                    return NotFound<string>("Email is not correct");
                    break;
                case "success":
                    return Success<string>(null,"code sent to email successfully to reset password");
                    break;
                default:
                    return BadRequest<string>("Failed to send code to email");
                    break;
            }
        }

        public async Task<Response<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _authService.ResetPassword(request.Email, request.Password);
            switch (result)
            {
                case "userNotFound":
                    return NotFound<string>("User is not found");
                    break;
                case "success":
                    return Success<string>(null,"Reset password successfully");
                    break;
                default:
                    return BadRequest<string>("Failed to reset password");
                    break;
            }
        }

        public async Task<Response<JwtResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            if (request.Token == null)
            {
                return new Response<JwtResult>(null, HttpStatusCode.NotFound, "Token Not Found", false);
            }
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
            var securityToken = jwtSecurityTokenHandler.ReadJwtToken(request.Token);
            var validateToken = await _authService.GenerateRefreshToken(securityToken, request.Token, request.RefreshToken);
            switch (validateToken)
            {
                case ("AlgoritmIsNotCorrect", null):
                    return BadRequest<JwtResult>("Algorithm is wrong");
                    break;
                case ("tokenIsNotExpireed", null):
                    return BadRequest<JwtResult>("Token is not expireed");
                    break;
                case ("RefreshTokenNotFound", null):
                    return NotFound<JwtResult>("Refresh token is not found");
                    break;
                case ("RefreshTokenIsExpired", null):
                    return BadRequest<JwtResult>("Refresh Token Is Expired");
                    break;
                default:
                    return Success<JwtResult>(validateToken.Item2,"generate token successfully");
            }
        }

        public async Task<Response<JwtResult>> Handle(GoogleSigninCommand request, CancellationToken cancellationToken)
        {
            var token = await _authService.GoogleSignIn(request.IdToken);
            if (token == null)
            {
                return BadRequest<JwtResult>("Failed to login using google");

            }
            return Success<JwtResult>(token,"Login with google Successfully");
        }
    }
}
