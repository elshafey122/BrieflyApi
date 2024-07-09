namespace Briefly.Core.Features.Auth.Commands.Model
{
    public class SendResetPasswordCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }
    }
}
