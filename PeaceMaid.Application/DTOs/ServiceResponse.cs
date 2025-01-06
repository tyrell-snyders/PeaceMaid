namespace PeaceMaid.Application.DTOs
{
    public record ServiceResponse(bool Flag, string Message);
    public record DataResponse(bool Flag, string Message, Object? Data);
    public record LoginResponse(bool Flag, string Message, string Token);
}
