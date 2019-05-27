using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEngine
{
    public enum EBuildingNames
    {
        NA,
        FD1,
        FD2,
        FD3,
        LTC,
        NAB,
        ANC,
        Looters,
        IPC
    }

    public class RoomLocation
    {
        public readonly EBuildingNames BuildingName;
        public readonly int RoomNo;
        public readonly char RoomSuffix;

        public RoomLocation()
        {
            BuildingName = EBuildingNames.NA;
            RoomNo = 0;
            RoomSuffix = '\0';
        }

        public bool Equals(RoomLocation loc) => BuildingName == loc.BuildingName && RoomNo == loc.RoomNo && RoomSuffix.ToString().ToLower() == loc.RoomSuffix.ToString().ToLower();

        public override string ToString() => 
            BuildingName.ToString() + "\t" + RoomNo + ((RoomSuffix == '\0') ? "" : ("-" + RoomSuffix));
    }
}
