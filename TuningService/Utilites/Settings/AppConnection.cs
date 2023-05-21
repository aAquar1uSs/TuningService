using System.Configuration;

namespace TuningService.Utilites.Settings;

public static class AppConnection
{
    public static string ConnectionString => ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
}