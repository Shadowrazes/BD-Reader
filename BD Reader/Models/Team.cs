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
        private uint pouints;
        private uint podiums;

        public Team(string _name = "", uint _years = 0, uint _championsips = 0, uint _pouints = 0, uint _podiums = 0)
        {
            name = _name;
            years = _years;
            championsips = _championsips;
            pouints = _pouints;
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

        public uint Pouints
        {
            get
            {
                return pouints;
            }
            set
            {
                pouints = value;
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
