using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEngine
{
    public enum EBuildingNames
    {
        FD1,
        FD2,
        FD3,
        LTC,
        NAB,
        ANC,
        Looters,
        IPC
    }

    public struct RoomLocation
    {
        public EBuildingNames BuildingName { get; set; }
        public int RoomNo { get; set; }
        public char RoomSuffix { get; set; }

        public bool Equals(RoomLocation loc) => BuildingName == loc.BuildingName && RoomNo == loc.RoomNo && RoomSuffix.ToString().ToLower() == loc.RoomSuffix.ToString().ToLower();

        public override string ToString() => 
            BuildingName.ToString() + "\t" + RoomNo + ((RoomSuffix == '\0') ? "" : ("-" + RoomSuffix));
    }
}
