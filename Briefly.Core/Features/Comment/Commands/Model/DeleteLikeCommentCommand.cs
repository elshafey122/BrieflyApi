namespace Briefly.Core.Features.Comment.Commands.Model
{
    public class DeleteLikeCommentCommand : IRequest<Response<string>>
    {
        public int CommentId { get; set; }
    }
}
