using System;
using System.Collections.Generic;

namespace BD_Reader.Models
{
    public partial class Car
    {
        private string? claass;
        public Car()
        {
            Drivers = new HashSet<Driver>();
        }

        public long Id { get; set; }
        public long? Number { get; set; }
        public string? Engine { get; set; }
        public string? Chassis { get; set; }
        public string? Class 
        { 
            get => claass;
            set
            {
                claass = value;
            }
        }

        public object? this[string property]
        {
            get
            {
                switch (property)
                {
                    case "Id": return Id;
                    case "Number": return Number;
                    case "Engine": return Engine;
                    case "Chassis": return Chassis;
                    case "Class": return Class;
                }
                return null;
            }
        }

        public virtual ICollection<Driver> Drivers { get; set; }
    }
}
