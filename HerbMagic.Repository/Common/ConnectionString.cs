using System.Configuration;

namespace HerbMagic.Repository.Common
{
    public static class ConnectionString
    {
        public static string NorthwindConnectionString => ConfigurationManager.ConnectionStrings["Northwind"].ConnectionString;

        public  static string BookStoreConnectionString => ConfigurationManager.ConnectionStrings["BookConnection"].ConnectionString;
    }
}
