using Briefly.Core.Pagination;

namespace Briefly.Core.Features.Article.Queires.Model
{
    public class GetAllRssArticlesQuery:IRequest<PaginationResponse<ArticleDto>>
    {
        public int RssId { get; set; }
        public int PageNumber { get; set; }
        public int pageSize { get; set; }
    }
}
