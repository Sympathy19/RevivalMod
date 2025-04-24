using BepInEx.Configuration;
using RevivalMod.Features.Packets;
using UnityEngine;
using static GClass1943;

namespace RevivalMod.Helpers
{

    /// <summary>
    /// Base Bepinex settings config
    /// </summary>
    internal class Settings
    {
        public static ConfigEntry<float> REVIVAL_DURATION;
        public static ConfigEntry<KeyCode> REVIVAL_KEY;
        public static ConfigEntry<float> REVIVAL_COOLDOWN;
        public static ConfigEntry<bool> RESTORE_DESTROYED_BODY_PARTS;

        public static ConfigEntry<bool> HARDCORE_MODE; 
        public static ConfigEntry<bool> HARDCORE_HEADSHOT_DEFAULT_DEAD;
        public static ConfigEntry<float> HARDCORE_CHANCE_OF_CRITICAL_STATE;

        public static ConfigEntry<bool> TESTING;

        /// <summary>
        /// setting to determine that if connected to a server, the client will do its best to use that server config if it can be downloaded
        /// </summary>
        public static bool UseServerConfig = true;


        public static void Init(ConfigFile config)
        {
            HARDCORE_MODE = config.Bind(
                "Hardcore Mode",
                "Enable Hardcore Mode",
                false,
               "Adapt the values below the change the hardcore settings"
            );
            HARDCORE_CHANCE_OF_CRITICAL_STATE = config.Bind(
                "Hardcore Mode",
                "Chance of critical mode",
                0.75f,
               "Adapt how big the odds are to enter critical state (be revivable) in hardcore mode. 0.75 is 75%"
            );
            HARDCORE_HEADSHOT_DEFAULT_DEAD = config.Bind(
                "Hardcore Mode",
                "Headshot is always dead",
                false,
               "Headshot kills always"
            );

            REVIVAL_DURATION = config.Bind(
                "General",
                "Revival Duration",
                4f,
               "Adapt the duration of the amount of time it takes to revive."
            );
            REVIVAL_KEY = config.Bind(
                "General",
                "Revival Key",
                KeyCode.F5
            );
            REVIVAL_COOLDOWN = config.Bind(
                "General",
                "Revival Cooldown",
                180f
              );
            RESTORE_DESTROYED_BODY_PARTS = config.Bind(
                "General",
                "Restore destroyed body parts after revive",
                false,
               "Does not work if Hardcore Mode is enabled"
            );

            TESTING = config.Bind(
                "Development",
                "Test Mode",
                false,
                new ConfigDescription("", null, new ConfigurationManagerAttributes { IsAdvanced = true })
            );
        }

        //NEEDS TO DOWNLOAD SERVER CONFIG SO THAT THEY CAN SYNC 
        public static void LoadServerConfig(ConfigFile config, SendableServerSettings ServerSettings)
        {
            

            HARDCORE_MODE = config.Bind(
                "Hardcore Mode",
                "Enable Hardcore Mode",
                ServerSettings.IsHardcore,
               "Adapt the values below the change the hardcore settings"
            );
            HARDCORE_CHANCE_OF_CRITICAL_STATE = config.Bind(
                "Hardcore Mode",
                "Chance of critical mode",
                ServerSettings.CriticalModeChance,
               "Adapt how big the odds are to enter critical state (be revivable) in hardcore mode. 0.75 is 75%"
            );
            HARDCORE_HEADSHOT_DEFAULT_DEAD = config.Bind(
                "Hardcore Mode",
                "Headshot is always dead",
                ServerSettings.HeadshotsAlwaysKill,
               "Headshot kills always"
            );

            REVIVAL_DURATION = config.Bind(
                "General",
                "Revival Duration",
                ServerSettings.ReviveDuration,
               "Adapt the duration of the amount of time it takes to revive."
            );
            REVIVAL_KEY = config.Bind(
                "General",
                "Revival Key",
                KeyCode.F5
            );
            REVIVAL_COOLDOWN = config.Bind(
                "General",
                "Revival Cooldown",
                ServerSettings.RevivalCooldown
              );
            RESTORE_DESTROYED_BODY_PARTS = config.Bind(
                "General",
                "Restore destroyed body parts after revive",
                ServerSettings.RestoreDamagedParts,
               "Does not work if Hardcore Mode is enabled"
            );

            TESTING = config.Bind(
                "Development",
                "Test Mode",
                ServerSettings.IsTestMode,
                new ConfigDescription("", null, new ConfigurationManagerAttributes { IsAdvanced = true })
            );
        }
    }



}
