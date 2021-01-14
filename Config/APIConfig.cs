
namespace SpecFlowCurrencyAPI.Config
{
    class APIConfig
    {
        //for Java APIs Testing
        public static string BaseUrl = "http://localhost:8080";
        public static string relativePath_GET = "/fixer/convert";
        public static string relativePath_POST = "/fixer/convert/currency";

        // for dot net core API Testing
//        public static string BaseUrl = "https://localhost:44336";
  //      public static string relativePath_GET = "/currency";
    //    public static string relativePath_POST = "/currency";


        public static string ApiKey = "dUYejUupPl39gip5f1wzTjdsLHHOGoOV";
    }
}
