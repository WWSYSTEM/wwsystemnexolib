using InsERT.Moria.Rozszerzanie;

namespace WWsystemLib.Interfaces
{
    public interface INexoAction
    {
        void onInit(IUchwyt uchwyt, NexoSettings settings);

        void onWork(IUchwyt uchwyt, NexoSettings settings);
        
        void onDisable(IUchwyt uchwyt, NexoSettings settings);
    }
}