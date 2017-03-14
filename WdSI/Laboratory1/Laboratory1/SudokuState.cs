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
            ///</summary>
            ///<param name="this.x"> przekazywana z buildchild definiowana w kontruktorze Statesdoku; domyślnie null. </param>
            ///<param name="this.y"> przekazywana z buildchild definiowana w kontruktorze Statesdoku; domyślnie null. </param>
            if (this.x == null || this.y == null) return 99999;

            #region wyznaczanie granic bloku w którym znajduje się komórka w tabeli
            int x_start = 0; int y_start = 0;
            int x_stop = 0; int y_stop = 0;

            if (x >= 0 && x <= 2)
            {
                x_start = 0;
                x_stop = 2;
            }
            else
            {
                if (x >= 3 && x <= 5)
                {
                    x_start = 3;
                    x_stop = 5;
                }
                else
                {
                    if (x >= 6 && x <= 8)
                    {
                        x_start = 6;
                        x_stop = 8;
                    }
                }
            }

            if (y >= 0 && y <= 2)
            {
                y_start = 0;
                y_stop = 2;
            }
            else
            {
                if (y >= 3 && y <= 5)
                {
                    y_start = 3;
                    y_stop = 5;
                }
                else
                {
                    if (y >= 6 && y <= 8)
                    {
                        y_start = 6;
                        y_stop = 8;
                    }
                }
            }
            #endregion //wyznaczanie granic small_grid w którym znajduje się komórka w tabeli

            #region repeat?

            List<int> repeat_list = new List<int>(); //lista liczb już umieszczoncyh

            #region przeszukiwanie powrórzeń w wierszu
            for (int i = 0; i < GRID_SIZE; i++)
            {
                if (this.Table[i, (int)this.y] != 0)
                {
                    foreach (int tmp_l in repeat_list)
                    {

                        if (this.Table[i, (int)this.y] == tmp_l)
                        {
                            return 99999;
                        }
                        else
                        {
                            repeat_list.Add(this.Table[i, (int)this.y]);
                        }

                    }
                }
            }
            repeat_list.Clear();
            #endregion // przeszukiwanie powrórzeń w wierszu

            #region przeszukiwanie powrórzeń w kolumnie
            for (int i = 0; i < GRID_SIZE; i++)
            {
                if (this.Table[i, (int)this.y] != 0)
                {
                    foreach (int tmp_l in repeat_list)
                    {

                        if (this.Table[(int)this.x, i] == tmp_l)
                        {
                            return 99999;
                        }
                        else
                        {
                            repeat_list.Add(this.Table[(int)this.x, i]);
                        }

                    }
                }
            }
            repeat_list.Clear();
            #endregion //przeszukiwanie powrórzeń w kolumnie

            #region przeszukiwanie bloku w poszukiwaniu powrórzeń
            for (int i = x_start; i <= x_stop; i++)
            {
                for (int j = y_start; j <= y_stop; j++)
               {
                    if (this.Table[i, j] != 0)
                    {
                        foreach (int tmp_l in repeat_list)
                        {

                            if (this.Table[i, j] == tmp_l)
                            {
                                return 99999;
                            }
                            else
                            {
                                repeat_list.Add(this.Table[i, j]);
                            }

                        }
                    }
                }
            }
            #endregion //przeszukiwanie bloku w poszukiwaniu powrórzeń

            #endregion //repeat

            #region Capabilities_value
            

            int[] Capabilities_list = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            
            #region wyszukiwanie wystapien w wierszu

            for (int i = 0; i < GRID_SIZE; i++)
            {
                if (this.Table[i, (int)(this.y)] != 0)
                { 
                    for(int j = 0; j < 9; j++)
                    {
                        if (Capabilities_list[j] == Table[i, (int)y])
                        {
                            Capabilities_list[j] = 0;
                        }
                    }
  
                }
            }
            #endregion // wyszukiwanie wystapien w wierszu

            #region wyszukiwanie wystapien w kolumnie

            for (int i = 0; i < GRID_SIZE; i++)
            {
                if (this.Table[(int)(this.x), i] != 0)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (Capabilities_list[j] == this.Table[(int)(this.x), i])
                        {
                            Capabilities_list[j] = 0;
                        }
                    }

                }
            }
            #endregion // wyszukiwanie wystapien w kolumnie

            #region Wyszukiewanie wystapien w blokach
            for (int i = x_start; i <= x_stop; i++)
            {
                for (int j = y_start; j <= y_stop; j++)
                {
                    if (this.Table[i, j] != 0)
                    {
                        for (int k = 0; k < 9; k++)
                        {
                            if (Capabilities_list[k] == this.Table[i, j])
                            {
                                Capabilities_list[k] = 0;
                            }
                        }

                    }
                }
            }
            #endregion //przeszukiwanie bloku w poszukiwaniu powrórzeń


            /* W tym miejscu teoretycznie mamy Capabilities_list ze wszystkimi
             * liczbami ktore wystepuja w wierszu kolumnie i kwadracie.
             */

            #endregion //Capabilities_value

            int counter = 0;

            for (int i = 0; i < 9; i++)
            {
                if (Capabilities_list[i] != 0) counter++;
            }

            return counter;
        }

        public void Print()
        {
            for (int i = 0; i < 9; i++)
            {
                Console.WriteLine("");
                for (int j = 0; j < 9; j++)
                {
                    Console.Write(table[i, j]);
                    if (j > 0 && j < 8 && ((j+1) % 3 == 0))
                    {
                        Console.Write(" ");
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write(" ");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write(" ");
                    }

                   
                }
                if (i > 0 && i < 8 && ((i + 1) % 3 == 0))
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.Write("\n               ");
                    Console.BackgroundColor = ConsoleColor.Black;
                }
            }
            Console.Write("\n\n####\n");
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
