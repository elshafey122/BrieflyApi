using Briefly.Core.Features.Article.Queires.Model;
using Briefly.Core.Pagination;
using Briefly.Service.Interfaces;

namespace Briefly.Core.Features.Article.Queires.Handler
{
    public class RssQueryHandler : ResponseHandler, 
                                                   IRequestHandler<GerArticleByldQuery, Response<ArticleByIdDto>>,
                                                   IRequestHandler<GetAllRssArticlesQuery, PaginationResponse<ArticleDto>>,
                                                   IRequestHandler<GetAllSavedUserArticles, PaginationResponse<ArticleDto>>
    {
        private readonly IMapper _mapper;
        private readonly IArticleService _articleService;
        public RssQueryHandler(IMapper mapper, IArticleService articleService)
        {
            _mapper = mapper;
            _articleService = articleService;
        }

        public async Task<Response<ArticleByIdDto>> Handle(GerArticleByldQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _articleService.GetRssArticlesById(request.ArticleId);
                switch (response.Item1)
                {
                    case "ArticleNotFound":
                        return NotFound<ArticleByIdDto>("Article is not found");
                        break;
                    case ("Fail"):
                        return BadRequest<ArticleByIdDto>("Failed to get article");
                        break;
                    default:
                        var ArticleDto = _mapper.Map<ArticleByIdDto>(response.Item2);
                        return Success<ArticleByIdDto>(ArticleDto, "Fetch article succesfully");
                }
            }
            catch (Exception ex)
            {
                return BadRequest<ArticleByIdDto>("Failed to get article");
            }

        }

        public async Task<PaginationResponse<ArticleDto>> Handle(GetAllRssArticlesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _articleService.GetAllRssArtricles(request.RssId);
                switch (response.Item1)
                {
                    case "RssNotFound":
                        return PaginationResponse<ArticleDto>.Fail("rss is not found");
                        break;
                    case ("NoContent"):
                        return PaginationResponse<ArticleDto>.Fail("rss has no articles");
                        break;
                    default:
                        var ArticleDto = _mapper.Map<List<ArticleDto>>(response.Item2);
                        var paginatedArticles = await ArticleDto.PaginateList(request.PageNumber, request.pageSize);
                        return paginatedArticles;
                }
            }
            catch (Exception ex)
            {
                return PaginationResponse<ArticleDto>.Fail("Failed to get articles");
            }
        }

        public async Task<PaginationResponse<ArticleDto>> Handle(GetAllSavedUserArticles request, CancellationToken cancellationToken)
        {
            var userId = int.Parse(request.UserId);
             var response = await _articleService.GetAllSaveUserArtricles(userId);
            if(response.Item1== "ArticleNotFound")
            {
                return PaginationResponse<ArticleDto>.Fail("User has no saved articles");
            }
            var ArticleDto = _mapper.Map<List<ArticleDto>>(response.Item2);
            var paginatedArticles = await ArticleDto.PaginateList(request.pageNumber, request.PageSize);
            return paginatedArticles;
        }
    }
}