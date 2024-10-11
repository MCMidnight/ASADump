using Rocket.API;

namespace PlutoScripts.Reincarnation
{
    public class ReincarnationConfiguration : IRocketPluginConfiguration
    {
        public float RespawnDelay { get; set; }
        public bool DebugMode { get; set; }
        public void LoadDefaults()
        {
            RespawnDelay = 5.0f;
            DebugMode = false;
        }
    }
}
