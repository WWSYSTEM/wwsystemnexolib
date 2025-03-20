using System;
using InsERT.Moria.Sfera;
using WWsystemLib;

namespace SferaConsoleApp1.Connector
{
    public class NexoUchwytFactory
    {
        public static Uchwyt CreateUchwyt(NexoSettings data)
        {
            return CreateUchwyt(data, new NexoConnectionStatus());
        }

        public static Uchwyt CreateUchwyt(NexoSettings data, IPostepLadowaniaSfery onLoadEvent)
        {
            var danePolaczenia = GetDanePolaczenia(data);
            var menedzerPolaczen = new MenedzerPolaczen();

            var sfera = menedzerPolaczen.Polacz(danePolaczenia, data.Produkt, onLoadEvent);
            var loginStatus = sfera.ZalogujOperatora(data.LoginNexo, data.HasloNexo);
            if (!loginStatus)
            {
                throw new Exception("Nie udało się zalogować do Nexo, błędny Login lub hasło");
            }


            return sfera;
        }


        public static DanePolaczenia GetDanePolaczenia(NexoSettings data)
        {
            return DanePolaczenia.Jawne(
                serwer: data.Server,
                baza: data.Database,
                uzytkownikSerwera: data.LoginSql,
                hasloUzytkownikaSerwera: data.HasloSql,
                autentykacjaWindowsDoSerwera: data.IsWindowsAuthroization);
        }
    }
}