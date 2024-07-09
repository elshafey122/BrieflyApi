using Briefly.Core.Features.CommentsArticle.Queires.Model;

namespace Briefly.Core.Features.Rss.Queires.Handler
{
    public class CommentArticleQuery : ResponseHandler,
                                                       IRequestHandler<GetAllCommentsQuery, Response<List<GetAllCommentsDto>>>
    {
        private readonly ICommentService _commentsArticleService;
        private readonly IMapper _mapper;
        public CommentArticleQuery(ICommentService commentsArticleService , IMapper mapper)
        {
            _commentsArticleService = commentsArticleService;
            _mapper = mapper;
        }
        public async Task<Response<List<GetAllCommentsDto>>> Handle(GetAllCommentsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var comments = await _commentsArticleService.GetAllCommentsArticle(request.articleId);
                if (comments == null)
                {
                    return NotFound<List<GetAllCommentsDto>>("Article is not found");
                }
                var commentsMapper = _mapper.Map<List<GetAllCommentsDto>>(comments);
                return Success<List<GetAllCommentsDto>>(commentsMapper);
            }
            catch (Exception ex)
            {
                return BadRequest<List<GetAllCommentsDto>>("Failed to get comments for article");
            }
        }
    }
}