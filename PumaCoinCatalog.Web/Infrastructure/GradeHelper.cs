namespace PumaCoinCatalog.Web.Infrastructure
{
    public static class GradeHelper
    {
        public static string GetGradeFromNumber(int gradeNumber)
        {
            switch(gradeNumber)
            {
                case 1:
                    return "Poor";
                case 2:
                    return "Fair";
                case 4:
                    return "Good";
                case 8:
                    return "Very Good";
                case 12:
                    return "Fine";
                case 20:
                    return "Very Fine";
                case 40:
                    return "Ex. Fine";
                case 50:
                    return "AU";
                case 58:
                    return "Uncirculated";
                case 60:
                    return "MS+";
                default:
                    return "";
            }
        }
    }
}