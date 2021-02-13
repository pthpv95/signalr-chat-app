using System;
namespace identityserver.Infrastructure.Settings
{
    public class ExternalAuthenticationSettings
    {
        public string GoogleClientId { get; set; }

        public string GoogleClientSecret { get; set; }

        public string FbClientId { get; set; }

        public string FbClientSecret { get; set; }
    }
}
