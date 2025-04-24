using Comfort.Common;
using EFT;
using Fika.Core.Networking;
using RevivalMod.Features.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevivalMod.Features.Handle;

public class SettingsHandle
{
    public static void OnHandleSettings(SettingsPacket packet)
    {
        switch (packet.PacketType)
        {
            case SettingsPacketType.ServerToAllClients:
                //SEND CONFIG TO ALL CLIENTS AT ONCE



                //Singleton<FikaServer>.Instance.SendDataToAll<FuelUpdatePacket>(ref fuelUpdatePacket2, DeliveryMethod.ReliableUnordered, null);
                break;
            case SettingsPacketType.ClientToServer:
                //ASK SERVER TO SEND CLIENT BACK THE CONFIG
                Plugin.LogSource.LogError("THIS IS NOT AN ERROR \n  VISUALLY CLIENT IS ASKING SERVER FOR SETTINGS");
                break;
            case SettingsPacketType.ServerToClient:
                //GET CONFIG FROM SERVER THEN SEND TO PLAYER
                Plugin.LogSource.LogError("THIS IS NOT AN ERROR \n  VISUALLY SERVER IS SENDING A CLIENT SETTINGS");
                break;
        }
    }

    

    /// <summary>
    /// PLAYER CALLS THIS FUNCTION ON GAME START AND ASKS THE SERVER FOR THE CONFIG OF THE SERVER 
    /// </summary>
    public static void SyncRevivalSettings(Player currentPlayer)
    {

    }
}
