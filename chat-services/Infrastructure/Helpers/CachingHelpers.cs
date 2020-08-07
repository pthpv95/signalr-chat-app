using System;

namespace chat_services.Infrastructure.Helpers
{
    public static class CachingHelpers
    {
        public static string BuildKey(Guid key) => $"CS_{key}";
    }
}