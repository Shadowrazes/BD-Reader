using System;
using System.Collections.Generic;

namespace BD_Reader.Models
{
    public partial class Event
    {
        public Event()
        {
            Results = new HashSet<Result>();
            Name = "None";
            Date = "0000-00-00 00:00:00";
            Track = "None";
        }

        public string Name { get; set; } = null!;
        public string? Date { get; set; }
        public string? Track { get; set; }

        public object? this[string property]
        {
            get
            {
                switch (property)
                {
                    case "EventName": return Name;
                    case "Date": return Date;
                    case "Track": return Track;
                }
                return null;
            }
        }

        public string Key()
        {
            return "EventName";
        }

        public virtual ICollection<Result> Results { get; set; }
    }
}
