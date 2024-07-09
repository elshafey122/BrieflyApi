using Briefly.Data.Result;

namespace Briefly.Core.Features.Auth.Commands.Model
{
    public class GoogleSigninCommand : IRequest<Response<JwtResult>>
    {
        public string IdToken { get; set; }
    }
}
