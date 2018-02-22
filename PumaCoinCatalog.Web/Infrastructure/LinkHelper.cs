namespace PumaCoinCatalog.Web.Infrastructure
{
    public static class LinkHelper
    {
        public static string NumisMediaLink(int id)
        {
            return $"http://www.numismedia.com/cgi-bin/coinprice.cgi?script=lrgcnt&searchtype=any&searchtext=fmv&search=any&guide=prices&guide2=pricesms&nmcode={id}";
        }

        public static string NgcLink(int id)
        {
            return $"http://www.ngccoin.com/NGCCoinExplorer/CoinDetail.aspx?CoinID={id}";
        }

        public static string PcgsLink(int id)
        {
            return $"http://www.pcgscoinfacts.com/Coin/Detail/{id}";
        }
    }
}