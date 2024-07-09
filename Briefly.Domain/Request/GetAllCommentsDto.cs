namespace Briefly.Data.Request
{
    public class GetAllCommentsDto 
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime PostedDate { get; set; } = DateTime.Now;
        public int? Likes { get; set; }
        public string UserName { get; set; }
        public List<GetAllCommentsDto>? Replies { get; set; }
    }
}
