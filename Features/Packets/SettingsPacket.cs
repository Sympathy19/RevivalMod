using LiteNetLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevivalMod.Features.Packets;

public enum SettingsPacketType
{
    ServerToClient, 
    ClientToServer, 
    ServerToAllClients
}

public struct SettingsPacket : INetSerializable
{
    /// <summary>
    /// ONLY NULL IF ALL PLAYERS
    /// </summary>
    public int PlayerId { get; set; } 
    public SettingsPacketType PacketType { get; set; }
    #nullable enable
    public string? ServerSettings { get; set; }
    #nullable disable


    public SettingsPacket CreateSettingsPacket(SettingsPacketType PacketType, string? ServerSettingsJSON, int TargetedPlayerId = -1) => new SettingsPacket() { PacketType = PacketType, ServerSettings = ServerSettingsJSON, PlayerId = TargetedPlayerId };


    public void Deserialize(NetDataReader reader)
    {
        this.PlayerId = reader.GetInt();
        this.PacketType = (SettingsPacketType)reader.GetInt();
        this.ServerSettings = reader.GetString();
    }

    public void Serialize(NetDataWriter writer)
    {
        writer.Put(PlayerId);
        writer.Put((int)PacketType); // Cast enum to int
        writer.Put(ServerSettings ?? ""); // Handle nullable string
    }
}





public class SendableServerSettings
{
    public SendableServerSettings() { }

    public SendableServerSettings(bool isTestMode, bool forceServerSettings, int reviveDuration, int revivalCooldown, bool restoreDamagedParts, bool headshotsAlwaysKill, bool isHardcore, float criticalModeChance)
    {
        IsTestMode = isTestMode;
        ForceServerSettings = forceServerSettings;
        ReviveDuration = reviveDuration;
        RevivalCooldown = revivalCooldown;
        RestoreDamagedParts = restoreDamagedParts;
        HeadshotsAlwaysKill = headshotsAlwaysKill;
        IsHardcore = isHardcore;
        CriticalModeChance = criticalModeChance;
    }



    public bool IsTestMode { get; set; }
    public bool ForceServerSettings { get; set; }
    /// <summary>
    /// Adapt the duration of the amount of time it takes to revive.
    /// </summary>
    public float ReviveDuration { get; set; }
    public float RevivalCooldown { get; set; }
    /// <summary>
    /// Restore destroyed body parts after revive = false
    /// oes not work if Hardcore Mode is enabled
    /// </summary>
    public bool RestoreDamagedParts { get; set; }
    public bool HeadshotsAlwaysKill { get; set; }

    /// <summary>
    /// Adapt the values below the change the hardcore settings
    /// </summary>
    public bool IsHardcore { get; set; }

    /// <summary>
    /// Adapt how big the odds are to enter critical state (be revivable) in hardcore mode. 0.75 is 75%
    /// </summary>
    public float CriticalModeChance { get; set; }

}


