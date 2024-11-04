namespace PeaceMaid.Domain.Entities
{
    public class Payment
    {
        public int PaymentId { get; set; }
        
        // User Foreign Key
        public int UserId { get; set; }
        public User? User { get; set; }

        // Booking Foreign Key
        public int BookingId { get; set; }
        public Booking? Booking { get; set; }

        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentStatus Status { get; set; }
        public PaymentMethod Method { get; set; }
    }

    public enum PaymentStatus
    {
        Pending, 
        Completed, 
        Failed
    }

    public enum PaymentMethod
    {
        Credit_Card,
        PayPal,
        EFT
    }
}
