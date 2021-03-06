﻿using System.Collections.Generic;
namespace LiveSplit.SteamWorldDig {
    public delegate bool Access(List<string> have);
    public class Location
    {
        public enum RandomizeType
        {
            None,
            Upgrade,
            Area
        }

        public RandomizeType Type { get; set; }
        public string Name { get; set; }
        public Access CanAccess { get; set; }
        public string Grant { get; set; }
        public Access CanEscape { get; set; }

        public Location()
        {
            Type = RandomizeType.None;
            CanAccess = have => true;
            CanEscape = have => true;
        }
    }
}
 