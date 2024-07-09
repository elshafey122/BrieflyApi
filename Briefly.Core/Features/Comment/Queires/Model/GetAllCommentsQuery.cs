namespace Briefly.Core.Features.CommentsArticle.Queires.Model
{
    public class GetAllCommentsQuery :IRequest<Response<List<GetAllCommentsDto>>>
    {
        public int articleId { get; set; }
    }
}
