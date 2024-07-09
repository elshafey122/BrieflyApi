using Briefly.Core.Pagination;

namespace Briefly.Core.Features.Article.Queires.Model
{
    public class GetAllSavedUserArticles : IRequest<PaginationResponse<ArticleDto>>
    {
        public string UserId { get; set; }
        public int pageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
