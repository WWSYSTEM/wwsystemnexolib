using System.Data.SqlClient;
using InsERT.Mox.Product;
using Newtonsoft.Json;
using WWsystemLib.WWsystemLib;

namespace WWsystemLib
{
    public class NexoSettings
    {
        [JsonProperty("log")] public string Logs { get; set; }

        [JsonProperty("Cron")] public string Cron { get; set; }
        [JsonProperty("serwer")] public string Server { get; set; }

        [JsonProperty("baza")] public string Database { get; set; }

        [JsonProperty("autoryzacja_windows")] public bool IsWindowsAuthroization { get; set; }

        [JsonProperty("login_sql")] public string LoginSql { get; set; }

        [JsonProperty("haslo_sql")] public string HasloSql { get; set; }

        [JsonProperty("login_nexo")] public string LoginNexo { get; set; }

        [JsonProperty("haslo_nexo")] public string HasloNexo { get; set; }
        public ProductId Produkt { get; set; } = ProductId.Subiekt;


        public SqlConnectionStringBuilder GetConnectionString()
        {
            var builder = new SqlConnectionStringBuilder();
            builder.Password = HasloSql;
            builder.UserID = LoginSql;
            builder.DataSource = Server;
            builder.InitialCatalog = Database;
            builder.TrustServerCertificate = true;
            return builder;
        }

        public static NexoSettings Load(string path = "./Resources", string profile = "")
        {
            var finalPath = $"{path}\\settings.{profile}.json";
            if (profile.IsEmpty())
            {
                finalPath = $"{path}\\settings.json";
            }

            var content = FileUtils.Load(finalPath);
            var nexoSettings = JsonConvert.DeserializeObject<NexoSettings>(content);
            if (nexoSettings.Cron.IsEmpty())
            {
                nexoSettings.Cron = "* * * * *";
            }

            return nexoSettings;
        }
    }
}