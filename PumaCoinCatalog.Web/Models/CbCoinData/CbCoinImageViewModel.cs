namespace PumaCoinCatalog.Web.Models.CbCoinData
{
    public class CbCoinImageViewModel
    {
        public string Title { get; set; }

        public string ObverseImageUri { get; set; }
        public bool HasObverseImage => !string.IsNullOrEmpty(ObverseImageUri);
        
        public string ReverseImageUri { get; set; }
        public bool HasReverseImage => !string.IsNullOrEmpty(ReverseImageUri);

        public bool HasAnyImage => HasObverseImage || HasReverseImage;
    }
}