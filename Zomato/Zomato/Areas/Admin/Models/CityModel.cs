namespace Zomato.Areas.Admin.Models
{
    public class CityModel
    {
        public int CityID { get; set; }

        public string CityName { get; set; }


    }

    public class StateModel
    {
        public int StateID { get; set;}

        public string StateName { get; set; } 
    }

    public class CountryModel
    {
        public int CountryID { get; set; }  
        public string CountryName { get; set; } 
    }
}
