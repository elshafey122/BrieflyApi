
namespace Briefly.Data.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public double Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string TransactionID { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentDate { get; set; }
        public User user { get; set; }
    }
}
