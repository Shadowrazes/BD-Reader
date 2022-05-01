using System;
using System.Collections.Generic;

namespace BD_Reader.Models
{
    public partial class Driver
    {
        public string FullName { get; set; } = null!;
        public long? CarId { get; set; }
        public string? TeamName { get; set; }
        public long? Age { get; set; }
        public long? Points { get; set; }
        public long? Starts { get; set; }
        public double? AvgFinish { get; set; }

        public virtual Car? Car { get; set; }
        public virtual Team? TeamNameNavigation { get; set; }
    }
}
