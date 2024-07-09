using Briefly.Core.Pagination;
using System.Text.Json.Serialization;

namespace Briefly.Core.Features.Rss.Queires.Model
{
    public class SubscribedRssQuery:IRequest<PaginationResponse<SubscribrdRssDto>>
    {
        [JsonIgnore]
        public string? UserId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

    }
}
