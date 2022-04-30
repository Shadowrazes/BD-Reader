using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD_Reader.Models
{
    public class Driver
    {
        private string fullName;
        private uint carID;
        private string teamName;
        private uint age;
        private uint points;
        private uint starts;
        private double avgFinish;
        public Driver(string _fullName = "", uint _carID = 0, string _teamName = "",
            uint _age = 0, uint _points = 0, uint _starts = 0, double _avgFinish = 0)
        {
            fullName = _fullName;
            carID = _carID;
            teamName = _teamName;
            age = _age;
            points = _points;
            starts = _starts;
            avgFinish = _avgFinish;
        }

        public string FullName
        {
            get 
            { 
                return fullName; 
            }
            set
            {
                fullName = value;
            }
        }

        public uint CarID
        {
            get
            {
                return carID;
            }
            set
            {
                carID = value;
            }
        }

        public string TeamName
        {
            get
            {
                return teamName;
            }
            set
            {
                teamName = value;
            }
        }

        public uint Age
        {
            get
            {
                return age;
            }
            set
            {
                age = value;
            }
        }

        public uint Points
        {
            get
            {
                return points;
            }
            set
            {
                points = value;
            }
        }

        public uint Starts
        {
            get
            {
                return starts;
            }
            set
            {
                starts = value;
            }
        }

        public double AvgFinish
        {
            get
            {
                return avgFinish;
            }
            set
            {
                avgFinish = value;
            }
        }
    }
}
