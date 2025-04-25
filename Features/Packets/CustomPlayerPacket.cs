using Fika.Core.Networking;
using LiteNetLib.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevivalMod.Features.Packets;

/// <summary>
/// probably isnt gonna be used as its too generic 
/// </summary>
public struct CustomPlayerPacket : INetSerializable
{
    public static JsonSerializerSettings settings = new JsonSerializerSettings
    {                    TypeNameHandling = TypeNameHandling.All             };

    public int PlayerId { get; set; }
    public string PacketType { get; set; }
    public byte[] Data { get; set; }


    public static CustomPlayerPacket Pack<T>(string PacketType, int PlayerId, T DataPackage)
    {
        string json = JsonConvert.SerializeObject(DataPackage, settings);
        byte[] dataBytes = Encoding.UTF8.GetBytes(json);

        return new CustomPlayerPacket
        {
            PacketType = PacketType,
            PlayerId = PlayerId,
            Data = dataBytes
        };
    }

    public static object Unpack(CustomPlayerPacket packet)
    {
        string json = Encoding.UTF8.GetString(packet.Data);
        return JsonConvert.DeserializeObject(json, settings);
    }

    public object Unpack()
    {
        string json = Encoding.UTF8.GetString(this.Data);
        return JsonConvert.DeserializeObject(json, settings);
    }

    public void Deserialize(NetDataReader reader)
    {
        this.PlayerId = reader.GetInt();
        this.PacketType = reader.GetString();
        this.Data = reader.GetBytesWithLength();
    }

    public void Serialize(NetDataWriter writer)
    {
        writer.Put(this.PlayerId);
        writer.Put(this.PacketType);
        writer.PutBytesWithLength(this.Data);
    }
}