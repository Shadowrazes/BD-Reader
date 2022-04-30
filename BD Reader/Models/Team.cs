using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD_Reader.Models
{
    public class Team
    {
        private string name;
        private uint years;
        private uint championsips;
        private uint points;
        private uint podiums;

        public Team(string _name = "", uint _years = 0, uint _championsips = 0, uint _points = 0, uint _podiums = 0)
        {
            name = _name;
            years = _years;
            championsips = _championsips;
            points = _points;
            podiums = _podiums;
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

        public uint Years
        {
            get
            {
                return years;
            }
            set
            {
                years = value;
            }
        }

        public uint Championsips
        {
            get
            {
                return championsips;
            }
            set
            {
                championsips = value;
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

        public uint Podiums
        {
            get
            {
                return podiums;
            }
            set
            {
                podiums = value;
            }
        }
    }
}
