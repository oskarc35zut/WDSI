using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Laboratory1
{
    public class SudokuState : State

    {
        #region zmienne
        public const int SMALL_GRID_SIZE = 3;

        public const int GRID_SIZE = SMALL_GRID_SIZE * SMALL_GRID_SIZE;

        private string id;

        public double infinity = Double.MaxValue; //"nieskończonosc"

        private int[,] table;

        public int[,] Table {
            get { return this.table; }
            set { this.table = value; }
        }

        private double[,] heuristic_array = new double[9,9];
        
        public double[,] Heuristic_array
        {
            get { return this.heuristic_array; }
        }

        public override string ID //const. setowany w konstruktorze
        {
            get { return this.id; }
        }
        #endregion //zmienne

        public override double ComputeHeuristicGrade()
        {

            return this.infinity;
        }

        public double ComputeHeuristicGrade(int x, int y, bool flaga) //wzór na obliczanie heurystyki
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


            if (flaga && (Table[x, y] != 0)) return this.infinity;

            #region Identyfikacja kratki oraz jej granic
            int x_start = 0; int y_start = 0;
            int x_stop = 0; int y_stop = 0;

            for (int i = 0; i < 8; i += 3)
            {
                if (x >= i && x <= i+2)
                    {
                    x_start = i;
                    x_stop = i + 2;
                        
                }
                if (y >= i && y <= i+2)
                    {
                    y_start = i;
                    y_stop = i + 2;
                }
            }
            #endregion //identyfikacja kratki oraz jej granic

            #region Powtorki - wersja zminimalizowana
            /* 
             * repeat_list wykresla liczby ktore już sie powtórzyły - w przypaku
             * gdy liczba powtarza funkcja zwraca heurystyke nieskonczona.
             * 
             * return infinity and break the method
             */

            for (int i = 0; i < GRID_SIZE; i++)
            {
                if ( (Table[i, y] != 0) && (i != x) && (Table[i, y] == Table[x, y]) ||
                   ( (Table[x, i] != 0) && (i != y) && (Table[x, i] == Table[x, y]) ))
                {
                    return infinity;
                }

                if ((i >= x_start) && (i <= x_stop))
                {
                    for (int j = y_start; j <= y_stop; j++)
                    {
                        if ((Table[i, j] != 0) && (Table[i, j] == Table[x, y]) && (i != x) && (j != y))
                        {
                            return infinity;
                        }
                    }
                }
            }
            #endregion //Powtorki - wersja zminimalizowana

            #region main
            /*
             * W tym miejscu powinniśmy już wiedzieć że wartosc badanej
             * komórki nie powtarza się wierszu & kolumnie & bloku.
             * 
             * Algorytm w wykreslaniu pomija wartosc badanej komórki!
             * Może to być istotne dla dzialania calej reszty programu.
             */

            int[] Capabilities_vector = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }; //wektor potencialnych możliwośći

            
            //bool flaga_1 = false;
            //if (!flaga)
            //{
            //    if (this.parent.H == 1) flaga_1 = !flaga;
            //}
            //"wykreslanie" z wektora potencialnych możliwości liczb które już występuja w wierszu kolumnie lub bloku
            for (int i = 0; i < GRID_SIZE; i++)
            {
                for (int j = 0; j < GRID_SIZE; j++)
                {
                    if ((Table[i, y] != 0) && (Table[i, y] == Capabilities_vector[j]) ||
                       ((Table[x, i] != 0) && (Table[x, i] == Capabilities_vector[j])))
                    {
                        Capabilities_vector[j] = 0; 
                    }
                
                    if ((i >= x_start) && (i <= x_stop))
                    {
                        for (int k = y_start; k <= y_stop; k++)
                        {
                            if ((Table[i, k] != 0) && (Table[i, k] == Capabilities_vector[j]))
                            {
                                Capabilities_vector[j] = 0;
                            }
                        }
                    }
                }
            }
            


            //zliczanie pozostałych możliwości
            int counterH = 0;
            int counter0 = 0;

            for (int i = 0; i < 9; i++)
            {
                if (Capabilities_vector[i] != 0) counter0++;
            }
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (Table[i,j] == 0) counter0++;
                }
            }


            return counterH+counter0;
            #endregion //main
        }

        public void Print(int[,] tab_before, int[,] tab_after)
        {
            for (int i = 0; i < 9; i++)
            {
                Console.WriteLine("");
                for (int j = 0; j < 9; j++)
                {
                    if (tab_before[i,j] != tab_after[i,j])
                    {
                        Console.BackgroundColor = ConsoleColor.DarkCyan;
                        Console.Write(tab_after[i, j]);
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        if(tab_after[i,j] == 0)
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
            
            //Tworzymy tablice heurystyk komórek węzła z Talicy
            for (int i = 0; i < GRID_SIZE; ++i)
            {
                for (int j = 0; j < GRID_SIZE; ++j)
                {
                    this.heuristic_array[i, j] = ComputeHeuristicGrade(i, j, true);
                }
            }
            this.h = infinity;
            //this.Print();
        }
        public SudokuState(SudokuState parent, int newValue, int x, int y) : base(parent) {
            this.table = new int[GRID_SIZE, GRID_SIZE];

            // Skopiowanie stanu sudoku do nowej tabeli
            Array.Copy(parent.table, this.table, this.table.Length);

            // Ustawienie nowej wartosci w wybranym polu sudoku
            this.table[x, y] = newValue;

            

            StringBuilder builder = new StringBuilder(parent.id);
            builder[x * GRID_SIZE + y] = (char)(newValue + 48);
            this.id = builder.ToString();

            this.h = ComputeHeuristicGrade(x, y, false);

            //Tworzymy tablice heurystyk komórek węzła z Talicy
            for (int i = 0; i < GRID_SIZE; ++i)
            {
                for (int j = 0; j < GRID_SIZE; ++j)
                {
                    this.heuristic_array[i, j] = ComputeHeuristicGrade(i, j, true);
                }
            }

            //Console.Clear();
            //this.Print();
            //Thread.Sleep(15);

        }
    }
}
