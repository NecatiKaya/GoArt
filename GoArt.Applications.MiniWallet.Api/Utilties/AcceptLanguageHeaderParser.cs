using System.Net.Http.Headers;

namespace GoArt.Applications.MiniWallet.Api.Utilties
{
    public static class AcceptLanguageHeaderParser
	{
		public static string Parse(string? headerValue, string defaultLang = "en")
		{
            //string header = "en-ca,en;q=0.8,en-us;q=0.6,de-de;q=0.4,de;q=0.2";
            var languages = headerValue?.Split(',')
                .Select(StringWithQualityHeaderValue.Parse)
                .OrderByDescending(s => s.Quality.GetValueOrDefault(1));

            StringWithQualityHeaderValue? firstLangHeader = languages?.FirstOrDefault();
            if (firstLangHeader is not null)
            {
                return firstLangHeader.Value;
            }

            return defaultLang;
        }
	}
}

