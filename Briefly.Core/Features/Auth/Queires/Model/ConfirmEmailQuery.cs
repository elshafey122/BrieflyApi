namespace Briefly.Core.Features.Auth.Queires.Model
{
    public class ConfirmEmailQuery : IRequest<Response<string>>
    {
        public string Code { get; set; }
        public int userId { get; set; }

    }
}
