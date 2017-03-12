using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laboratory1
{
    public class SudokuState : State

    {
        public const int SMALL_GRID_SIZE = 3;

        public const int GRID_SIZE = SMALL_GRID_SIZE * SMALL_GRID_SIZE;

        private string id;

        private int? x;

        private int? y;

        private int[,] table;

        public int[,] Table {
            get { return this.table; }
            set { this.table = value; }
        }
        public override string ID //const. setowany w konstruktorze
        {
            get { return this.id; }
        }

        public override double ComputeHeuristicGrade() //wzór na obliczanie heurystyki
        {
            /// <summary>
            /// Heurystyka wg minimum pozostałych mozliwosci
            ///
            /// Utozsamijmy v z tablica sudoku. Niech Rv(i, j) oznacza pozostałe mozliwosci
            ///dla komórki(i, j) tablicy v — pozostałe poprzez wyeliminowanie liczb
            ///obecnych w wierszu i, kolumnie j i podkwadracie, do którego nalezy(i, j).
            ///Mówimy, ze stan jest tym blizszy rozwiazaniu, im ma mniej pozostałych
            ///mozliwosci dla pewnej komórki. Dzieci podpinamy w tej własnie komórce.
            /// </summary>

            if (this.x == null || this.y == null) return 99999;


            int Counter0 = 0;
            int Counter0x = 0;
            int Counter0y = 0;

            for (int i = 0; i < GRID_SIZE; i++)
            {
                if (this.Table[(int)this.x, i] == 0) Counter0x++;
                if (this.Table[i, (int)this.y] == 0) Counter0y++;
            }

            int x_start = 0; int y_start = 0;
            int x_stop = 0; int y_stop = 0;

            if (x >= 0 || x <= 2)
            {
                x_start = 0;
                x_stop = 2;
            }
            else
            {
                if (x >= 3 || x <= 5)
                {
                    x_start = 3;
                    x_stop = 5;
                }
                else
                {
                    if (x >= 6 || x <= 8)
                    {
                        x_start = 6;
                        x_stop = 8;
                    }
                }
            }

            if (y >= 0 || y <= 2)
            {
                y_start = 0;
                y_stop = 2;
            }
            else
            {
                if (y >= 3 || y <= 5)
                {
                    y_start = 3;
                    y_stop = 5;
                }
                else
                {
                    if (y >= 6 || y <= 8)
                    {
                        y_start = 6;
                        y_stop = 8;
                    }
                }
            }


            for (int i = x_start; i <= x_stop; i++)
            {
                for (int j = y_start; j <= y_stop; j++)
                {
                    if (this.Table[i, j] == 0) Counter0++;
                }
            }



            if (Counter0 < Counter0x || Counter0 < Counter0y)
            { return Counter0; }
            else
            {
               if(Counter0x < Counter0y)
                {
                    return Counter0x;
                }
               else
                {
                    return Counter0y;
                }
            }



        }

        public void Print()
        {
            for (int i = 0; i < 9; i++)
            {
                //Console.WriteLine("");
                for (int j = 0; j < 9; j++)
                {
                    Console.Write(table[i, j]);
                    if (table[i,j] == 0) 
                    {
                        //Console.Write(" ");
                    }
                    else{
                    
                    if (j == 2 || j == 5 || j == 8)
                    {
                        //Console.Write(" || ");
                    }
                    }
                }
                if (i == 2 || i == 5 || i == 8)
                {
                    //Console.WriteLine("----------------------------");
                }
            }
            Console.Write("\n####\n");
        }



        // Konstruktor SudokuState dziedziczący kostruktor po State + dodatkowe elementy
        public SudokuState(string sudokuPattern) : base()
        {
            if (sudokuPattern.Length != GRID_SIZE * GRID_SIZE)
            {
                throw new ArgumentException("SudokuString posiada niewlasciwa dlugosc.");
            } 

            this.id = sudokuPattern;
            this.table = new int[GRID_SIZE, GRID_SIZE];

            for (int i = 0; i < GRID_SIZE; ++i)
            {
                for (int j = 0; j < GRID_SIZE; ++j)
                {
                    this.table[i, j] = sudokuPattern[i * GRID_SIZE + j] - 48;
                }
            }
            this.h = ComputeHeuristicGrade();
        }
        public SudokuState(SudokuState parent, int newValue, int x , int y) : base(parent) {
            this.table = new int[GRID_SIZE, GRID_SIZE];
            Array.Copy(parent.table, this.table, this.table.Length);
            this.table[x, y] = newValue;
            StringBuilder builder = new StringBuilder(parent.id);
            builder[x * GRID_SIZE + y] = (char)(newValue + 48);
            this.id = builder.ToString();
            this.x = x;
            this.y = y;
            this.h = ComputeHeuristicGrade();



         }
    }
}
