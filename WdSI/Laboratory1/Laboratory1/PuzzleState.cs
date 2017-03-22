using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laboratory1
{
    class PuzzleState : State
    {
        private static int puzzleSize;
        private string id;
        public override string ID
        {
            get { return this.id; }
        }

        private int[,] table;

        public int[,] Table
        {
            get { return this.table; }
            set { this.table = value; }
        }

        public override double ComputeHeuristicGrade()
        {

            return 0;
        }



        public PuzzleState(int PuzzleSize) : base()
        {
            puzzleSize = PuzzleSize;
            for (int i = 0; i < PuzzleSize; i++)
            {
                if(i != (PuzzleSize-1))
                {
                    this.id += i;
                }
            }
            this.table = new int[PuzzleSize, PuzzleSize];

            int licznik = 0;
            for (int i = 0; i <= PuzzleSize; i++)
            {
                for (int j = 0; j <= PuzzleSize; j++)
                {
                    this.table[i, j] = licznik;
                    licznik++;
                }
                licznik++;
            }
            Console.Read();


        }

        public PuzzleState(PuzzleState parent) : base(parent)
        {
            // ciało konstruktora

            this.h = 0;

            //W stanie w ktorym przybyliśmy droga jest o jeden większa niż w rodzicu

            this.g = parent.g + 1;
        }



        public void Print(int[,] tab_before, int[,] tab_after)
        {
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("");
                for (int j = 0; j < 3; j++)
                {
                    if (tab_before[i, j] != tab_after[i, j])
                    {
                        Console.BackgroundColor = ConsoleColor.DarkCyan;
                        Console.Write(tab_after[i, j]);
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        if (tab_after[i, j] == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write(tab_after[i, j]);
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    if (j > 0 && j < 3)
                    {
                        Console.Write(" ");
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write(" ");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write(" ");
                    }


                }
                if (i > 0 && i < 3)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.Write("\n               ");
                    Console.BackgroundColor = ConsoleColor.Black;
                }
            }
            Console.Write("\n\n####\n");
        }

    }
}
