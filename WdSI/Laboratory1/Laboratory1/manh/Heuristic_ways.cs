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

        public int x;
        public int y;

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

        private string id;
        public string ID
        {
            get { return this.id;  }
        }
        public void id_generate()
        {
            id = null;

            for (int i = 0; i < PuzzleState.PuzzleSize; i++)
            {
                for (int j = 0; j < PuzzleState.PuzzleSize; j++)
                {
                    id += this.table[i, j];

                }
            }
        }

        

       
        public Heuristic_ways(int w)
        {
            this.way = w;
        }
    }
}
