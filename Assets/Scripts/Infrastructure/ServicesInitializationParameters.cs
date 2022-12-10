using Data;
using Level;
using WWW;
namespace Infrastructure
{
    public class InitializationParameters
    {
        public float ScreenHeight;
        public float ScreenWidth;
        public BackgroundDownloader BackgroundDownloader;
        public BackgroundContainer BackgroundContainer;
        public BackgroundManager BackgroundManager;
        public Spawner Spawner;
        public LevelsData LevelsData;
        public string BundleUrl;
    }
}
