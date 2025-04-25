using EFT;
using EFT.UI;
using Fika.Core.Coop.Utils;
using RevivalMod.Helpers;
using SPT.Reflection.Patching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RevivalMod.Patches
{
    /// <summary>
    /// ONLY EXISTS SO THAT PATCHES CAN BE TESTED BEFORE 
    /// </summary>
    public class MainMenuDebugPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(MenuUI).GetMethod(nameof(MenuUI.Awake));
        }

        [PatchPostfix]
        static void PatchPostfix()
        {
            try
            {
                //CHECK THE BACKEND OF THE FIKA SERVER HERE 
                Plugin.LogSource.LogInfo($"MainMenuDebug::Fikabackend -> IsClient: {FikaBackendUtils.IsClient} IsServer: {FikaBackendUtils.IsServer} IsHeadless: {FikaBackendUtils.IsHeadless}");
                //Features.Handle.SettingsHandle.SyncRevivalSettings(playerClient);
            }
            catch (Exception Ex) { Plugin.LogSource.LogError(Ex.ToString()); }
        }
    }
}
