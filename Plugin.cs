using BepInEx;
using BepInEx.Logging;
using RevivalMod.Features;
using BepInEx.Bootstrap;
using RevivalMod.Patches;
using RevivalMod.Helpers;
using Fika.Core.Modding.Events;
using System.Runtime.CompilerServices;
using RevivalMod.Features.Packets;
using BepInEx.Configuration;

namespace RevivalMod
{
    // first string below is your plugin's GUID, it MUST be unique to any other mod. Read more about it in BepInEx docs. Be sure to update it if you copy this project.
    //[BepInDependency("com.fika.core", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInPlugin("com.kaikinoodles.revivalmod", "RevivalMod", "1.1.0")]
    public class Plugin : BaseUnityPlugin
    {
        /// <summary>
        /// Allows us to reuse the config later 
        /// </summary>
        /// <returns></returns>
        public ConfigFile GetConfigFile() => Config;
        public static ManualLogSource LogSource;


        // BaseUnityPlugin inherits MonoBehaviour, so you can use base unity functions like Awake() and Update()
        private void Awake()
        {
            // save the Logger to variable so we can use it elsewhere in the project
            LogSource = Logger;
            LogSource.LogInfo("Revival plugin loaded!");
            Settings.Init(Config);
            // Enable patches
            new DeathPatch().Enable();
            new RevivalFeatures().Enable();
            new GameStartedPatch().Enable();
            new MainMenuDebugPatch().Enable();

            LogSource.LogInfo("Revival plugin initialized! Press F5 to use your defibrillator when in critical state.");
        }



        

        private void OnFikaNetworkManagerCreated(FikaNetworkManagerCreatedEvent @event)
        {
            @event.Manager.RegisterPacket<SettingsPacket>(new System.Action<SettingsPacket>(Features.Handle.SettingsHandle.OnHandleSettings));


            ////PLAYER PACKETS - handle everything in one switch might not be effient but I already made the custom player packet
            //@event.Manager.RegisterPacket<CustomPlayerPacket>(new System.Action<CustomPlayerPacket>(Features.Handle.PlayerPackets.HandlePlayerPackets.OnHandlePlayerPackets));
        }
    }
}
