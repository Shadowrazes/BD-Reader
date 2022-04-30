using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD_Reader.Models
{
    public class Event
    {
        private string name;
        private string date;
        private string track;
        public Event(string _name = "", string _date = "", string _track = "")
        {
            name = _name;
            date = _date;
            track = _track;
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public string Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
            }
        }

        public string Track
        {
            get
            {
                return track;
            }
            set
            {
                track = value;
            }
        }
    }
}
