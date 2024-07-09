namespace Briefly.Data.Request
{
    public class UpdateRssRequest
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? CategoryId { get; set; }
    }
}