using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interval_Timer
{
    [Serializable]
    public class Routine
    {
        private string name;
        private int warmup;
        private bool firstIntHigh;
        private int lowSet;
        private int highSet;
        private int sets;
        private int cooldown;
        private int repeat;
        private int rest;
        
        public Routine (string name, int warmup, int lowSet, int highSet, int sets, bool firstIntHigh = true, int cooldown = 0, int repeat = 0, int rest = 0)
        {
            this.name = name;
            this.warmup = warmup;
            this.lowSet = lowSet;
            this.highSet = highSet;
            this.sets = sets;
            this.firstIntHigh = firstIntHigh;
            this.cooldown = cooldown;
            this.repeat = repeat;
            this.rest = rest;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }
        public int Warmup
        {
            get
            {
                return this.warmup;
            }
            set
            {
                this.warmup = value;
            }
        }
        public int Sets
        {
            get
            {
                return this.sets;
            }
            set
            {
                this.sets = value;
            }
        }

        public int LowSet
        {
            get
            {
                return this.lowSet;
            }
            set
            {
                this.lowSet = value;
            }
        }

        public int HighSet
        {
            get
            {
                return this.highSet;
            }
            set
            {
                this.highSet = value;
            }
        }
        public int Cooldown
        {
            get
            {
                return this.cooldown;
            }
            set
            {
                this.cooldown = value;
            }
        }
        public bool FirstIntHigh
        {
            get
            {
                return this.firstIntHigh;
            }
            set
            {
                this.firstIntHigh = value;
            }
        }
        public int Repeat
        {
            get
            {
                return this.repeat;
            }
            set
            {
                this.repeat = value;
            }
        }
        public int Rest
        {
            get
            {
                return this.rest;
            }
            set
            {
                this.rest = value;
            }
        }
    }
}
