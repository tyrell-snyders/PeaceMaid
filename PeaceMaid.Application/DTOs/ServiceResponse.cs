namespace PeaceMaid.Application.DTOs
{
    public record ServiceResponse(bool Flag, string Message);
    public record PaymentResponse(bool Flag, string Message, Object? Data);
}
