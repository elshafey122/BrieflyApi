namespace Briefly.Core.Features.Auth.Queires.Model
{
    public class CheckTokenValidationQuery : IRequest<Response<string>>
    {
        public string Token { get; set; }
    }
}
