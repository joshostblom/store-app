public static class ConfigHelper
{
    private static readonly IConfigurationRoot Configuration;

    static ConfigHelper()
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