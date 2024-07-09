using System.ComponentModel.DataAnnotations;

namespace Briefly.Data.Entities
{
    public class SubscriptionUser
    {
        [Key]
        public int Id { get; set; }
        public int PlanID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public SubscriptionPlan SubscriptionPlan { get; set; }
    }
}