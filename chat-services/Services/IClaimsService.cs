using realtime_app.Contracts;

namespace realtime_app.Services
{
    public interface IClaimsService
    {
        ClaimsContext GetUserClaims();
    }
}