namespace Briefly.Core.Features.Auth.Commands.Model
{
    public class ResetPasswordCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
