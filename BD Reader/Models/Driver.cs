using System;
using System.Collections.Generic;

namespace BD_Reader.Models
{
    public partial class Driver
    {
        public Driver()
        {
            Results = new HashSet<Result>();
            FullName = "None";
            CarId = 0;
            TeamName = "None";
            Age = 0;
            Points = 0;
            Starts = 0;
            AvgFinish = 0;
        }

        public string FullName { get; set; } = null!;
        public long? CarId { get; set; }
        public string? TeamName { get; set; }
        public long? Age { get; set; }
        public long? Points { get; set; }
        public long? Starts { get; set; }
        public double? AvgFinish { get; set; }

        public object? this[string property]
        {
            get
            {
                switch (property)
                {
                    case "DriverFullName": return FullName;
                    case "CarId": return CarId;
                    case "TeamName": return TeamName;
                    case "Age": return Age;
                    case "Points": return Points;
                    case "Starts": return Starts;
                    case "AvgFinish": return AvgFinish;
                }
                return null;
            }
        }

        public string Key()
        {
            return "DriverFullName";
        }

        public virtual Car? Car { get; set; }
        public virtual Team? TeamNameNavigation { get; set; }
        public virtual ICollection<Result> Results { get; set; }
    }
}
