namespace Briefly.Core.Features.Rss.Queires.Model
{
    public class GetRssByIdQuery : IRequest<Response<RssDto>>
    {
        public string UserId { get; set; }
        public int RssId { get; set; }
    }
}
