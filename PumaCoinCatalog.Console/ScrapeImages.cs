using PumaCoinCatalog.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PumaCoinCatalog.Console
{
    public enum EntityType
    {
        Variety,
        Type
    }

    public class EntityImage
    {
        public int EntityId { get; set; }
        public EntityType EntityType { get; set; }
        public string EntityTitle { get; set; }
        public string ObverseUrl { get; set; }
        public string ReverseUrl { get; set; }
    }

    public class ScrapeImages
    {
        private readonly DataContext _context;

        public ScrapeImages()
        {
            _context = new DataContext();
        }

        public void ScrapeVarietyImages()
        {
            var varietyImages = _context.CbVarieties.Select(x => new EntityImage
            {
                EntityId = x.Id,
                EntityType = EntityType.Variety,
                EntityTitle = "Variety_" + x.Title,
                ObverseUrl = x.ObverseImageUri,
                ReverseUrl = x.ReverseImageUri
            }).ToList();

            System.Console.WriteLine($"\n\nBegin processing {varietyImages.Count} variety images...");
            ProcessImages(varietyImages);
        }

        public void ScrapeTypeImages()
        {
            var typeImages = _context.CbTypes.Select(x => new EntityImage
            {
                EntityId = x.Id,
                EntityType = EntityType.Type,
                EntityTitle = "Type_" + x.Title,
                ObverseUrl = x.ObverseImageUri,
                ReverseUrl = x.ReverseImageUri
            }).ToList();

            System.Console.WriteLine($"\n\nBegin processing {typeImages.Count} type images...");
            ProcessImages(typeImages);
        }

        private void ProcessImages(List<EntityImage> images)
        {
            var typePath = "C:\\_repos\\PumaCoinCatalog\\PumaCoinCatalog.Web\\Images\\TypeImages\\";
            var varietyPath = "C:\\_repos\\PumaCoinCatalog\\PumaCoinCatalog.Web\\Images\\VarietyImages\\";
            var i = 0;

            using (var client = new WebClient())
            {
                foreach (var img in images)
                {
                    i++;
                    var path = img.EntityType == EntityType.Type ? typePath : varietyPath;

                    // obverse image
                    var oUrl = img.ObverseUrl;
                    var oExt = oUrl.Substring(oUrl.LastIndexOf("."));
                    var oFilename = $"{img.EntityTitle}_Obverse{oExt}";

                    // reverse image
                    var rUrl = img.ReverseUrl;
                    var rExt = oUrl.Substring(oUrl.LastIndexOf("."));
                    var rFilename = $"{img.EntityTitle}_Reverse{rExt}";


                    // download images
                    client.DownloadFile(new Uri(oUrl), $"{path}{oFilename}");
                    while (client.IsBusy) { }
                    client.DownloadFile(new Uri(rUrl), $"{path}{rFilename}");
                    while (client.IsBusy) { }

                    System.Console.Write($" {i}");
                }
            }
        }
    }
}
