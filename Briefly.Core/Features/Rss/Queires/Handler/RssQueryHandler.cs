using Briefly.Core.Features.Rss.Queires.Model;
using Briefly.Core.Pagination;
using Briefly.Data.Identity;
using Microsoft.AspNetCore.Identity;

namespace Briefly.Core.Features.Rss.Queires.Handler
{
    public class CommentsArticleQuery : ResponseHandler,
                                                   IRequestHandler<SubscribedRssQuery, PaginationResponse<SubscribrdRssDto>>,
                                                   IRequestHandler<GetAllRssQuery, PaginationResponse<RssDto>>,
                                                   IRequestHandler<GetRssByIdQuery, Response<RssDto>>
    {
        private readonly IRssService _rssService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public CommentsArticleQuery(IRssService rssService, IMapper mapper, UserManager<User> userManager)
        {
            _rssService = rssService;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<PaginationResponse<SubscribrdRssDto>> Handle(SubscribedRssQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId);
                if (user == null)
                {
                    return PaginationResponse<SubscribrdRssDto>.Fail("User not found");
                }
                var result = await _rssService.GetAllSubscribedRss(request.UserId);
                if (result == null)
                {
                    return PaginationResponse<SubscribrdRssDto>.Fail("User not subscribed to any rss");
                }
                var resultMapper = _mapper.Map<List<SubscribrdRssDto>>(result);
                var paginatedRss = await resultMapper.PaginateList(request.PageNumber, request.PageSize);
                return paginatedRss;
            }
            catch (Exception ex)
            {
                return PaginationResponse<SubscribrdRssDto>.Fail("Failed to get data");
            }
        }

        public async Task<Response<RssDto>> Handle(GetRssByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var rss = await _rssService.GetRssById(request.RssId);
                if (rss == null)
                {
                    return NotFound<RssDto>("Failed to fetch rss");
                }
                var result = _mapper.Map<RssDto>(rss);
                return Success<RssDto>(result, "Rss Fetched Successfully");
            }
            catch(Exception ex)
            {
                return BadRequest<RssDto>("Failed to get data");
            }
        }

        public async Task<PaginationResponse<RssDto>> Handle(GetAllRssQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var rsses = await _rssService.GetAllRss(request.Search);
                if (rsses == null)
                {
                    return PaginationResponse<RssDto>.Fail("No rss data");
                }
                var result = _mapper.Map<List<RssDto>>(rsses);
                var RssPaginated = await result.PaginateList(request.PageNumber, request.PageSize);
                return RssPaginated;
            }
            catch (Exception ex)
            {
                return PaginationResponse<RssDto>.Fail("Failed to get data");
            }
        }
    }
}