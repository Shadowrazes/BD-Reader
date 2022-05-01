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

        public virtual ICollection<Driver> Drivers { get; set; }
    }
}
