namespace Briefly.Core.Features.Comment.Commands.Model
{
    public class DeleteCommentCommand :IRequest<Response<string>>
    {
        public int CommentId { get; set; }
        public string UserID { get; set; }
    }
}
