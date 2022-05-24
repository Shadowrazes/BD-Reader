using System;
using System.Collections.Generic;

namespace BD_Reader.Models
{
    public partial class Result
    {
        public string DriverFullName { get; set; } = null!;
        public string StageName { get; set; } = null!;
        public string? EventName { get; set; }
        public long? Position { get; set; }
        public string? Time { get; set; }

        public object? this[string property]
        {
            get
            {
                switch (property)
                {
                    case "DriverFullName": return DriverFullName;
                    case "StageName": return StageName;
                    case "EventName": return EventName;
                    case "Position": return Position;
                    case "Time": return Time;
                }
                return null;
            }
        }

        public string Key()
        {
            return "DriverFullName";
        }

        public virtual Driver DriverFullNameNavigation { get; set; } = null!;
        public virtual Event? EventNameNavigation { get; set; }
    }
}
