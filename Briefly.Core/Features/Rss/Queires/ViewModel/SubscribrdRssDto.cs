namespace Briefly.Core.Features.Rss.Queires.ViewModel
{
    public class SubscribrdRssDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string Link { get; set; }
        public string? Image { get; set; }
        public int? CategoryId { get; set; }
    }
}
