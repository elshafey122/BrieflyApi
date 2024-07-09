using Briefly.Data.Result;

namespace Briefly.Core.Features.Auth.Commands.Model
{
    public class LoginCommand : IRequest<Response<JwtResult>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
