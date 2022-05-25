using System;
using System.Collections.Generic;

namespace BD_Reader.Models
{
    public partial class Team
    {
        public Team()
        {
            Drivers = new HashSet<Driver>();
            Name = "None";
            Years = 0;
            Championships = 0;
            Points = 0;
            Podiums = 0;
        }

        public string Name { get; set; } = null!;
        public long? Years { get; set; }
        public long? Championships { get; set; }
        public long? Points { get; set; }
        public long? Podiums { get; set; }

        public object? this[string property]
        {
            get
            {
                switch (property)
                {
                    case "TeamName": return Name;
                    case "Years": return Years;
                    case "Championships": return Championships;
                    case "Points": return Points;
                    case "Podiums": return Podiums;
                }
                return null;
            }
        }

        public string Key()
        {
            return "TeamName";
        }

        public virtual ICollection<Driver> Drivers { get; set; }
    }
}
