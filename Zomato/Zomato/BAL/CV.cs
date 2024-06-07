namespace Zomato.BAL
{
    public class CV
    {
        private static IHttpContextAccessor _HttpContextAccessor;
        static CV()
        {
            _HttpContextAccessor = new HttpContextAccessor();
        }

        public static int? UserID()
        {
            return Convert.ToInt32(_HttpContextAccessor.HttpContext.Session.GetString("UserID"));
        }

        public static string UserName()
        {
            return _HttpContextAccessor.HttpContext.Session.GetString("UserName");
        }

        public static string FirstName()
        {
            return _HttpContextAccessor.HttpContext.Session.GetString("FirstName");
        }

        public static string RestaurantName()
        {
            return _HttpContextAccessor.HttpContext.Session.GetString("RestaurantName");
        }

        public static int? RestaurantID()
        {
            return Convert.ToInt32(_HttpContextAccessor.HttpContext.Session.GetString("RestaurantID"));
        }

        public static int? CategoryID()
        {
            return Convert.ToInt32(_HttpContextAccessor.HttpContext.Session.GetString("CategoryID"));
        }

        public static int? CityID()
        {
            return Convert.ToInt32(_HttpContextAccessor.HttpContext.Session.GetString("CityID"));
        }

        public static string CityName()
        {
            return _HttpContextAccessor.HttpContext.Session.GetString("CityName");
        }

        public static int? CartCount()
        {
            return Convert.ToInt32(_HttpContextAccessor.HttpContext.Session.GetString("CartCount"));
        }

        public static bool IsAdmin()
        {
            return Convert.ToBoolean(_HttpContextAccessor.HttpContext.Session.GetString("IsAdmin"));
        }

        public static string UserOrder()
        {
            return _HttpContextAccessor.HttpContext.Session.GetString("UserOrder");
        }

        public static string Address()
        {
            return _HttpContextAccessor.HttpContext.Session.GetString("Address");
        }
    }
}
