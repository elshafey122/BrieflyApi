using System.ComponentModel.DataAnnotations;

namespace Briefly.Data.Entities
{
    public class SubscriptionPlan
    {
        [Key]
        public int PlanID { get; set; }
        public string PlanName { get; set; }
        public double Price { get; set; }
        public string BillingCycle { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
