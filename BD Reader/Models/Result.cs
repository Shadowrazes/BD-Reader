using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD_Reader.Models
{
    public class Result
    {
        private string driverFullName;
        private string stageName;
        private string eventName;
        private uint position;
        private string time;

        public Result(string _driverFullName = "", string _stageName = "", string _eventName = "", uint _position = 0, string _time = "")
        {
            driverFullName = _driverFullName;
            stageName = _stageName;
            eventName = _eventName;
            position = _position;
            time = _time;
        }

        public string DriverFullName
        {
            get 
            { 
                return driverFullName; 
            }
            set
            {
                driverFullName = value;
            }
        }

        public string StageName
        {
            get
            {
                return stageName;
            }
            set
            {
                stageName = value;
            }
        }

        public string EventName
        {
            get
            {
                return eventName;
            }
            set
            {
                eventName = value;
            }
        }

        public uint Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }

        public string Time
        {
            get
            {
                return time;
            }
            set
            {
                time = value;
            }
        }
    }
}
