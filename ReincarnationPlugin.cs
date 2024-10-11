using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using Rocket.Core.Utils;
using SDG.Unturned;
using Steamworks;
using System;

namespace PlutoScripts.Reincarnation
{
    public class ReincarnationPlugin : RocketPlugin<ReincarnationConfiguration>
    {
        protected override void Load()
        {
            Logger.Log($"{Name} {Assembly.GetName().Version.ToString(3)} has been loaded!");
            Logger.Log("Made by Pluto Scripts.");
            PlayerLife.onPlayerDied += OnPlayerDied;

            if (Configuration.Instance.DebugMode)
            {
                Logger.Log("Debug Mode is enabled.");
                Logger.Log($"Respawn delay is set to {Configuration.Instance.RespawnDelay} seconds.");
            }
        }

        protected override void Unload()
        {
            Logger.Log($"{Name} {Assembly.GetName().Version.ToString(3)} has been unloaded!");
            Logger.Log("Made by Pluto Scripts.");
            PlayerLife.onPlayerDied -= OnPlayerDied;
        }

        private void OnPlayerDied(PlayerLife sender, EDeathCause cause, ELimb limb, CSteamID murderer)
        {
            var player = sender.player;
            TaskDispatcher.QueueOnMainThread(() =>
            {
                AutoRespawnPlayer(player);
            }, Configuration.Instance.RespawnDelay);
        }

        private void AutoRespawnPlayer(Player player)
        {
            if (player != null && player.life != null && player.life.isDead)
            {
                player.life.ServerRespawn(atHome: false);
            }
        }
    }
}
