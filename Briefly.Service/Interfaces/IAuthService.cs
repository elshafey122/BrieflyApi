using Briefly.Data.Identity;
using Briefly.Data.Result;
using System.IdentityModel.Tokens.Jwt;

namespace Briefly.Service.Interfaces
{
    public interface IAuthService
    {
        Task<string> Register(User user, string password);
        Task<JwtResult> GenerateToken(User user);
        Task<(string, JwtResult)> GenerateRefreshToken(JwtSecurityToken securityToken, string token, string refreshToken);
        Task<bool> CheckValidationToken(string token);

        Task<JwtResult> GoogleSignIn(string IdToken);

        Task<string> ConfirmEmailAsync(int? UserId, string Code);
        Task<string> SendResetPassword(string email);
        Task<string> ConfirmResetPassword(string email, string code);
        Task<string> ResetPassword(string email, string password);
    }
}
