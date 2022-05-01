using System;
using System.Collections.Generic;

namespace BD_Reader.Models
{
    public partial class Event
    {
        public Event()
        {
            Results = new HashSet<Result>();
        }

        public string Name { get; set; } = null!;
        public string? Date { get; set; }
        public string? Track { get; set; }

        public virtual ICollection<Result> Results { get; set; }
    }
}
