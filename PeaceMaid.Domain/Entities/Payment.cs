namespace PeaceMaid.Domain.Entities
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int UserId { get; set; }
        public int BookingId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public enum PaymentStatus { Pending, Completed, Failed }
        public enum PaymentMethod { Credit_Card, PayPal, EFT }
    }
}
