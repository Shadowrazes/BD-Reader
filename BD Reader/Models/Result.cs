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

        public virtual Event? EventNameNavigation { get; set; }
    }
}
