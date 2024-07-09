namespace Briefly.Core.Features.Rss.Commands.Model
{
    public class UpdateRssCommand:IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}
