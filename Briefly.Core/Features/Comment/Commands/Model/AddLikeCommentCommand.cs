namespace Briefly.Core.Features.Comment.Commands.Model
{
    public class AddLikeCommentCommand:IRequest<Response<string>>
    {
        public int commentId { get; set; }
    }
}
