using Briefly.Data.Result;

namespace Briefly.Core.Features.Auth.Commands.Model
{
    public class RefreshTokenCommand : IRequest<Response<JwtResult>>
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
