using PumaCoinCatalog.Web.Models.CoinGrade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PumaCoinCatalog.Web.Controllers
{
    public class CoinGradeController : Controller
    {
        // GET: CoinGrade
        public ActionResult Index()
        {
            var model = GetAllCoinGradeModels();
            return View(model);
        }

        private IList<CoinGradeModel> GetAllCoinGradeModels()
        {
            var models = new List<CoinGradeModel>();

            models.Add(new CoinGradeModel
            {
                NumericGrade = "P-1",
                SheldonGrade = "Poor",
                GradeDescription = "Barely identifiable; must have date and mint mark, otherwise pretty thrashed."
            });
            
            models.Add(new CoinGradeModel
            {
                NumericGrade = "FR-2",
                SheldonGrade = "Fair",
                GradeDescription = "Worn almost smooth but lacking the damage Poor coins have."
            });
            
            models.Add(new CoinGradeModel
            {
                NumericGrade = "G-4",
                SheldonGrade = "Good",
                GradeDescription = "Heavily worn such that inscriptions merge into the rims in places; details are mostly gone."
            });
            
            models.Add(new CoinGradeModel
            {
                NumericGrade = "VG-8",
                SheldonGrade = "Very Good",
                GradeDescription = "Very worn, but all major design elements are clear, if faint. Little if any central detail."
            });
            
            models.Add(new CoinGradeModel
            {
                NumericGrade = "F-12",
                SheldonGrade = "Fine",
                GradeDescription = "Very worn, but wear is even and overall design elements stand out boldly. Almost fully-separated rims."
            });
            
            models.Add(new CoinGradeModel
            {
                NumericGrade = "VF-20",
                SheldonGrade = "Very Fine",
                GradeDescription = "Moderately worn, with some finer details remaining. All letters of LIBERTY, (if present,) should be readable. Full, clean rims."
            });
            
            models.Add(new CoinGradeModel
            {
                NumericGrade = "EF-40",
                SheldonGrade = "Extremely Fine",
                GradeDescription = "Lightly worn; all devices are clear, major devices bold."
            });
            
            models.Add(new CoinGradeModel
            {
                NumericGrade = "AU-50",
                SheldonGrade = "About Uncirculated",
                GradeDescription = "Slight traces of wear on high points; may have contact marks and little eye appeal."
            });
            
            models.Add(new CoinGradeModel
            {
                NumericGrade = "U-58",
                SheldonGrade = "Very Choice About Uncirculated",
                GradeDescription = "Slightest hints of wear marks, no major contact marks, almost full luster, and positive eye appeal."
            });
            
            models.Add(new CoinGradeModel
            {
                NumericGrade = "MS-60",
                SheldonGrade = "Mint State Basal",
                GradeDescription = "Strictly uncirculated but that's all; ugly coin with no luster, obvious contact marks, etc."
            });
            
            models.Add(new CoinGradeModel
            {
                NumericGrade = "MS-63",
                SheldonGrade = "Mint State Acceptable",
                GradeDescription = "Uncirculated, but with contact marks and nicks, slightly impaired luster, overall basically appealing appearance. Strike is average to weak."
            });
            
            models.Add(new CoinGradeModel
            {
                NumericGrade = "MS-65",
                SheldonGrade = "Mint State Choice",
                GradeDescription = "Uncirculated with strong luster, very few contact marks, excellent eye appeal. Strike is above average."
            });
            
            models.Add(new CoinGradeModel
            {
                NumericGrade = "MS-68",
                SheldonGrade = "Mint State Premium Quality",
                GradeDescription = "Uncirculated with perfect luster, no visible contact marks to the naked eye, exceptional eye appeal. Strike is sharp and attractive."
            });

            models.Add(new CoinGradeModel
            {
                NumericGrade = "MS-69",
                SheldonGrade = "Mint State Almost Perfect",
                GradeDescription = "Uncirculated with perfect luster, sharp, attractive strike, and very exceptional eye appeal. A perfect coin except for microscopic flaws (under 8x magnification) in planchet, strike, or contact marks."
            });

            models.Add(new CoinGradeModel
            {
                NumericGrade = "MS-70",
                SheldonGrade = "Mint State Perfect",
                GradeDescription = "The perfect coin. There are no microscopic flaws visible to 8x, the strike is sharp, perfectly-centered, and on a flawless planchet. Bright, full, original luster and outstanding eye appeal."
            });

            return models;
        }
    }
}