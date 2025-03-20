using System;
using InsERT.Moria.Sfera;

namespace SferaConsoleApp1.Connector
{
    public class NexoConnectionStatus : IPostepLadowaniaSfery
    {
        private const int MaxRange = 100;
        private string _description;
        private int _step = 0;

        public virtual void RaportujPostep(PostepLadowaniaSferyEventArgs @event)
        {
            if (@event.Opis != _description)
            {
                _description = @event.Opis;
                ++_step;
            }
            Console.Write($"\r{_step}. {@event.Opis}: {@event.BiezacyProcent} %");
            if (@event.BiezacyProcent == MaxRange)
            {
                Console.WriteLine();
            }
        }
    }
}