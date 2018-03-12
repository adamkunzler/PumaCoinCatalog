using CsQuery;
using System.Net;

namespace PumaCoinCatalog.Console
{
    public abstract class BaseDataScraper
    {
        protected CQ GetHtml(string uri)
        {
            CQ dom;

            using (var client = new WebClient())
            {
                dom = client.DownloadString(uri);
            }

            return dom;
        }
    }
}
