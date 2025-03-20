using System.Data;
using InsERT.Moria.ModelDanych;
using InsERT.Moria.PolaWlasne2;
using InsERT.Moria.Rozszerzanie;
using InsERT.Mox.DataAccess.EntityFramework;

namespace WWsystemLib
{
    public static class UchwytExtensions
    {
        public static bool CzyPoleWlasneIstnieje<T>(this IUchwyt uchwyt, string nazwaPola)  where T : EntityDataObjectBase
        {
            var zaawansowanePolaWlasne = uchwyt.PodajObiektTypu<IZaawansowanePolaWlasne>();
            var result = zaawansowanePolaWlasne.PosiadaZaawansowanePoleWlasne<T>(nazwaPola);
            return result;
        }
        
        public static DataTable ExecuteQuery(this IUchwyt uchwyt, string query)
        {
            using (var connection = uchwyt.PodajPolaczenie())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    using (var reader = command.ExecuteReader())
                    {
                        var table = new DataTable();
                        table.Load(reader);
                        return table;
                    }
                }
            }
        }
    }
}