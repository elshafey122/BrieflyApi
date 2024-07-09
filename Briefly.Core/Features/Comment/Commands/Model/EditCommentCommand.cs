namespace Briefly.Core.Features.Comment.Commands.Model
{
    public class EditCommentCommand:IRequest<Response<string>>
    {
        public int userId { get; set; }
        public string updatedtext { get; set; }
        public int CommentId { get; set; }
    }
}
