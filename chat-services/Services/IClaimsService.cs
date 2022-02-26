using chat_service.Contracts;

namespace chat_service.Services
{
    public interface IClaimsService
    {
        ClaimsContext GetUserClaims();
    }
}