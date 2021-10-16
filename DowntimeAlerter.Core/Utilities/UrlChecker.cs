using System;

namespace DowntimeAlerter.Core.Utilities
{
    public static class UrlChecker
    {
        public static bool CheckUrl(string uriName)
        {
            Uri uriResult;
            return Uri.TryCreate(uriName, UriKind.Absolute, out uriResult)
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}