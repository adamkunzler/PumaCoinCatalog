namespace PumaCoinCatalog.Models.UsaCoinBook
{
    public class CbCoin
    {

        public int Id { get; set; }
        public int Year { get; set; }
        public string MintMark { get; set; }
        public string Details { get; set; }
        public long Mintage { get; set; }
        
        public virtual CbType Type { get; set; }        
    }
}
