public static class ConfigConnectionHelper
{
    private static readonly IConfigurationRoot Configuration;

    static ConfigConnectionHelper()
    {
        Configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
    }

    public static string GetConnectionString()
    {
        return Configuration.GetConnectionString("ShoppingAppCon");
    }
}