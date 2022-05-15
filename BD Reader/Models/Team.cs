using System;
using System.Collections.Generic;

namespace BD_Reader.Models
{
    public partial class Team
    {
        public Team()
        {
            Drivers = new HashSet<Driver>();
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
                    case "Name": return Name;
                    case "Years": return Years;
                    case "Championships": return Championships;
                    case "Points": return Points;
                    case "Podiums": return Podiums;
                }
                return null;
            }
        }

        public virtual ICollection<Driver> Drivers { get; set; }
    }
}
