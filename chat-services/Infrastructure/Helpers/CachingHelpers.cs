using System;

namespace chat_services.Infrastructure.Helpers
{
    public static class CachingHelpers
    {
        public static string BuildKey(string prefix, Guid key) => $"{prefix}_{key}";
    }
}