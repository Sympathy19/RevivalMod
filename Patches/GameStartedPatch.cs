using EFT;
using EFT.Communications;
using Comfort.Common;
using RevivalMod;
using RevivalMod.Features;
using SPT.Reflection.Patching;
using System;
using System.Reflection;
using UnityEngine;
using EFT.InventoryLogic;
using System.Linq;
using RevivalMod.Helpers;
using SPT.Common.Http;
using Fika.Core.Networking;
using Fika.Core.Coop.Utils;
using System.Runtime.CompilerServices;
using BepInEx.Configuration;
using Newtonsoft.Json;
using RevivalMod.Features.Packets;

namespace RevivalMod.Patches
{
    internal class GameStartedPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(GameWorld).GetMethod(nameof(GameWorld.OnGameStarted));
        }



        [PatchPostfix]
        static void PatchPostfix()
        {
            try
            {
                Plugin.LogSource.LogInfo("Game started, checking revival item");

                // Make sure GameWorld is instantiated
                if (!Singleton<GameWorld>.Instantiated)
                {
                    Plugin.LogSource.LogError("GameWorld not instantiated yet");
                    return;
                }

                // Initialize player client directly
                Player playerClient = Singleton<GameWorld>.Instance.MainPlayer;
                if (playerClient == null)
                {
                    Plugin.LogSource.LogError("MainPlayer is null");
                    return;
                }

                Plugin.LogSource.LogInfo($"Fikabackend -> IsClient: {FikaBackendUtils.IsClient} IsServer: {FikaBackendUtils.IsServer} IsHeadless: {FikaBackendUtils.IsHeadless}");

                //REQUEST SERVER CONFIG IF CLIENT
                if (!FikaBackendUtils.IsSinglePlayer && FikaBackendUtils.IsClient) Features.Handle.SettingsHandle.SyncRevivalSettings(playerClient);
                else
                {
                    if(FikaBackendUtils.IsSinglePlayer) Plugin.LogSource.LogInfo("CLIENT IS SINGLE PLAYER!!");
                    else
                    {
                        try
                        {
                            SendableServerSettings ServerConfig = Settings.GetServerConfig();
                            string JSON = JsonConvert.SerializeObject(ServerConfig);
                            Plugin.LogSource.LogInfo($"Server config -> {JSON}");
                        }
                        catch (Exception Ex) { Plugin.LogSource.LogError($"Exception Debugging Settings JSON\n\n {Ex.ToString()}"); }
                    }





                    //foreach(var key in Plugin.ClientConfig.Keys)
                    //{
                    //    //Plugin.LogSource.LogInfo($"     {key.Section} - {key.Key}");
                    //    if (Plugin.ClientConfig.TryGetEntry<object>(key, out ConfigEntry<object> Entry))
                    //    {


                    //        Plugin.LogSource.LogInfo($"     {key.Section} - {Entry.GetType()} - {key.Key}");
                    //    }
                    //    else Plugin.LogSource.LogError("Failed to show config entry...");

                    //    //config.TryGetEntry
                    //    //
                    //}

                    //Features.Handle.SettingsHandle.SyncRevivalSettings(playerClient);
                }

                //Singleton<FikaServer>.Instance.SERVERE








                // Check if player has revival item
                string playerId = playerClient.ProfileId;
                var inRaidItems = playerClient.Inventory.GetPlayerItems(EPlayerItems.Equipment);
                bool hasItem = false;

                try
                {
                    hasItem = inRaidItems.Any(item => item.TemplateId == Constants.Constants.ITEM_ID);
                }
                catch (Exception ex)
                {
                    Plugin.LogSource.LogError($"Error checking player items: {ex.Message}");
                }

                Plugin.LogSource.LogInfo($"Player {playerId} has revival item: {hasItem}");

                // Send packet if Fika is installed


                // Display notification about revival item status
                if (Settings.TESTING.Value)
                {
                    NotificationManagerClass.DisplayMessageNotification(
                    $"Revival System: {(hasItem ? "Revival item found" : "No revival item found")}",
                    ENotificationDurationType.Default,
                    ENotificationIconType.Default,
                    hasItem ? Color.green : Color.yellow);
                }
            }
            catch (Exception ex)
            {
                Plugin.LogSource.LogError($"Error in GameStartedPatch: {ex.Message}");
            }
        }
    }
}