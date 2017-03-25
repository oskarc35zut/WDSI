using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

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


        public static void start(int size)
        {
            Stopwatch stopWatch_search = new Stopwatch();
            Stopwatch stopWatch_print = new Stopwatch();

            puzzleSize = size;
            
            PuzzleState startState = new PuzzleState(puzzleSize);
            PuzzleSearch searcher = new PuzzleSearch(startState);


            stopWatch_search.Start();

            searcher.DoSearch();
            stopWatch_search.Stop();

            TimeSpan t_search = stopWatch_search.Elapsed;

            IState state = searcher.Solutions[0];

            List<SudokuState> solutionPath = new List<SudokuState>();

            while (state != null)
            {
                solutionPath.Add((SudokuState)state);
                state = state.Parent;
            }
            solutionPath.Reverse();

            int[,] table_tmp1 = new int[9, 9];
            int[,] table_tmp2 = new int[9, 9];

            table_tmp1 = solutionPath[0].Table;


            stopWatch_print.Start();

            foreach (SudokuState s in solutionPath)
            {
                table_tmp2 = table_tmp1;
                table_tmp1 = s.Table;

                s.Print(table_tmp2, table_tmp1);
            }
            stopWatch_print.Stop();
            TimeSpan t_print = stopWatch_print.Elapsed;

            string SearchTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            t_search.Hours, t_search.Minutes, t_search.Seconds,
            t_search.Milliseconds / 10);
            Console.WriteLine("Czas przeszukiwania " + SearchTime);

            string PrintTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            t_print.Hours, t_print.Minutes, t_print.Seconds,
            t_print.Milliseconds / 10);
            Console.WriteLine("Czas wyswietlania " + PrintTime);
        }

    }
}
