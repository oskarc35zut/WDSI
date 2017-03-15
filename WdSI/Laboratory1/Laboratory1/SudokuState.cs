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

        public const double infinity = 99999; //"nieskończonosc"

        private int[,] table;

        public int[,] Table {
            get { return this.table; }
            set { this.table = value; }
        }

        private double[,] heuristic_array;
        
        public double[,] Heuristic_array
        {
            get { return this.heuristic_array; }
        }

        public override string ID //const. setowany w konstruktorze
        {
            get { return this.id; }
        }

        public override double ComputeHeuristicGrade()
        {

            return infinity;
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
            
            #region identyfikacja kratki oraz jej granic
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

            return 0;
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
                    heuristic_array[i, j] = ComputeHeuristicGrade(i, j);//Tworzymy tablice heurystyk komórek węzła
                }
            }
            this.h = ComputeHeuristicGrade();
        }
        public SudokuState(SudokuState parent, int newValue, int x, int y) : base(parent) {
            this.table = new int[GRID_SIZE, GRID_SIZE];

            // Skopiowanie stanu sudoku do nowej tabeli
            Array.Copy(parent.table, this.table, this.table.Length);

            //tworzymy heurysyke węzła; chyba przed zamianą wartości z tego względu ze to heurystyka dla parrenta albo kij wie
            this.h = ComputeHeuristicGrade(x, y);

            // Ustawienie nowej wartosci w wybranym polu sudoku
            this.table[x, y] = newValue;

            StringBuilder builder = new StringBuilder(parent.id);
            builder[x * GRID_SIZE + y] = (char)(newValue + 48);
            this.id = builder.ToString();

            //Tworzymy tablice heurystyk komórek węzła
            for (int i = 0; i < SudokuState.GRID_SIZE; ++i)
            {
                for (int j = 0; j < SudokuState.GRID_SIZE; ++j)
                {
                    heuristic_array[i, j] = ComputeHeuristicGrade(i, j);
                }
            }

        }
    }
}
