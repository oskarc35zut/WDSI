using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laboratory2
{
    class Connect4State : State
    {
        private static int size;

        public static int Size
        {
            get { return Connect4State.size; }
        }

        private static int howdeep;

        public static int Howdeep
        {
            get { return Connect4State.howdeep; }
        }

        int[,] table;

        public int[,] Table
        {
            get { return this.table;  }
            set { this.table = value; }
        }

        private string id;

        public override string ID
        {
            get { return this.id; }
        }

        public override double ComputeHeuristicGrade()
        {
            throw new NotImplementedException();
        }

        public Connect4State(int size, int deep) : base() //konstruktor inicjujący
        {
            Connect4State.size = size;
            Connect4State.howdeep = deep;

            table = new int[Size, Size];

            //table 0 builder
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Table[i,j] = 0;
                }
            }

            //id builder
            id = "";
            for (int i = 0; i < Size*Size; i++)
            {
                this.id += 0;
            }


        }

        public Connect4State(Connect4State parent, int[,] tab) : base(parent)
        {
            // reszta implementacji

            // ustawienie stringa identyfikujacego stan.
            //id builder
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    id += Table[i, j];
                }
            }
            // ustawienie na ktorym poziomie w drzwie znajduje sie stan .
            this.depth = parent.depth + 0.5;

            // Bardzo wazne nie ustawiany na czubek drzewa z ktorego budujemy stany.Tylko na pierwsze pokolenie stanow potomnych
            if (parent.rootMove == null)
            {
                this.rootMove = this.id;
            }
            else {
                this.rootMove = parent.rootMove;
            }
        }
    }
}
