using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laboratory1
{
    public class SudokuState : State

    {
        #region zmienne
        public const int SMALL_GRID_SIZE = 3;

        public const int GRID_SIZE = SMALL_GRID_SIZE * SMALL_GRID_SIZE;

        private string id;

        private double infinity = 99999; //"nieskończonosc"

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

        public double ComputeHeuristicGrade(int x, int y) //wzór na obliczanie heurystyki
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

            #region Powtorki
            /* 
             * repeat_list wykresla liczby ktore już sie powtórzyły, w przypaku
             * gdy liczba powtarza funkcja zwraca heurystyke nieskonczona.
             * 
             * return infinity and break the method
             */

            List<int> repeat_list = new List<int>();

            #region przeszukiwanie powrórzeń w wierszu i kolumnie
            for (int i = 0; i < GRID_SIZE; i++)
            {
                if ((Table[i, y] == Table[x, y])
                    &&
                    ((  (Table[i, y] != 0) && (i != x)) || 
                      ( (Table[x, i] != 0) && (i != y)) ))
                {
                    return infinity;
                }
            }

            #region przeszukiwanie bloku w poszukiwaniu powrórzeń
            for (int i = x_start; i <= x_stop; i++)
            {
                for (int j = y_start; j <= y_stop; j++)
                {
                    if (Table[i, j] != 0)
                    {
                        foreach (int tmp_l in repeat_list)
                        {

                            if (Table[i, j] == tmp_l)
                            {
                                return infinity;
                            }
                            else
                            {
                                repeat_list.Add(Table[i, j]);
                            }

                        }
                    }
                }
            }
            repeat_list.Clear();
            #endregion //przeszukiwanie bloku w poszukiwaniu powrórzeń

            #endregion //repeat

            return 8;
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
            
            //Tworzymy tablice heurystyk komórek węzła z Talicy
            for (int i = 0; i < GRID_SIZE; ++i)
            {
                for (int j = 0; j < GRID_SIZE; ++j)
                {
                    this.heuristic_array[i, j] = ComputeHeuristicGrade(i, j);
                }
            }

            this.h = infinity;
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

            //Tworzymy tablice heurystyk komórek węzła z Talicy
            for (int i = 0; i < GRID_SIZE; ++i)
            {
                for (int j = 0; j < GRID_SIZE; ++j)
                {
                    this.heuristic_array[i, j] = ComputeHeuristicGrade(i, j);
                }
            }

            //Wyciagamy heurystyke wezla dla 
            this.h = this.heuristic_array[x, y];

        }
    }
}
