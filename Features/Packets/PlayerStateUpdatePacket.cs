using LiteNetLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevivalMod.Features.Packets
{
    //public enum EBodyPart
    //{
    //    // Token: 0x04002F77 RID: 12151
    //    Head,
    //    // Token: 0x04002F78 RID: 12152
    //    Chest,
    //    // Token: 0x04002F79 RID: 12153
    //    Stomach,
    //    // Token: 0x04002F7A RID: 12154
    //    LeftArm,
    //    // Token: 0x04002F7B RID: 12155
    //    RightArm,
    //    // Token: 0x04002F7C RID: 12156
    //    LeftLeg,
    //    // Token: 0x04002F7D RID: 12157
    //    RightLeg,
    //    // Token: 0x04002F7E RID: 12158
    //    Common
    //}



    //EFT.HealthSystem

    //SEND ALL OF THE CLIENTS (WHOLE BODY IN "EBodyPart")

    public enum EBodyPartStatus
    {

    }



    public class PlayerPartState
    {
        public PlayerPartState() { }

        public EBodyPart Part { get; set; }
        public EBodyPartStatus Status { get; set; }

        public PlayerPartState(EBodyPart part, EBodyPartStatus status)
        {
            Part = part;
            Status = status;
        }
    }

    //SEND STATUS BACK TO SERVER WEATHER OR NOT THEY ARE INJURED AND WHAT THE INJURIES ARE
    public class PlayerStateUpdatePacket : INetSerializable
    {
       



        public void Deserialize(NetDataReader reader)
        {
            throw new NotImplementedException();
        }

        public void Serialize(NetDataWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
