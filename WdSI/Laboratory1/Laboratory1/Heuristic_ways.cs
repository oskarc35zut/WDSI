using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laboratory1
{
    class Heuristic_ways
    {
        private int way;
        public int Way
        {
            get { return this.way; }
        }

        private int[,] table;

        public int[,] Table
        {
            get { return this.table; }
            set { this.table = value; }
        }

        public double h;
        private double g = 0;
        public double F
        {
            get { return h + g; }
        }

        public Heuristic_ways(int w)
        {
            this.way = w;
        }
    }
}
