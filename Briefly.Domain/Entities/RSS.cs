using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Briefly.Data.Entities
{
    public class RSS
    { 
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string Link { get; set; }
        public string? Image { get; set; }

        [BindNever]
        public ICollection<Article> Articles { get; set; } = new HashSet<Article>();
        [BindNever]
        public ICollection<RssSubscription> SubscribedBy { get; set; } = new HashSet<RssSubscription>();
       
    }
}
