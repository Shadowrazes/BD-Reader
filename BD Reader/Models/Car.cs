using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD_Reader.Models
{
    public class Car
    {
        private uint id;
        private uint number;
        private string engine;
        private string chassis;
        private string claass;

        public Car(uint _id = 0, uint _number = 0, string _engine = "", string _chassis = "", string _claass = "")
        {
            id = _id;
            number = _number;
            engine = _engine;
            chassis = _chassis;
            claass = _claass;
        }

        public uint Id
        {
            get 
            { 
                return id; 
            }
            set
            {
                id = value;
            }
        }

        public uint Number
        {
            get
            {
                return number;
            }
            set
            {
                number = value;
            }
        }

        public string Engine
        {
            get
            {
                return engine;
            }
            set
            {
                engine = value;
            }
        }

        public string Chassis
        {
            get
            {
                return chassis;
            }
            set
            {
                chassis = value;
            }
        }

        public string Class
        {
            get
            {
                return claass;
            }
            set
            {
                claass = value;
            }
        }
    }
}
