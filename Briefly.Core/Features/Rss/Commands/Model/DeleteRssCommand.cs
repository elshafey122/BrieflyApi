namespace Briefly.Core.Features.Rss.Commands.Model
{
    public class DeleteRssCommand:IRequest<Response<string>>
    {
        public int RssId { get; set; }
    }
}
