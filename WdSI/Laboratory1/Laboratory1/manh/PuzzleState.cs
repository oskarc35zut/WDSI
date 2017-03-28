using System;
using System.Collections.Generic;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Laboratory1
{
    class PuzzleState : State
    {
#region zmienne
        public List<Heuristic_ways> Heuristic_vetor = new List<Heuristic_ways>();
        public List<string> id_list = new List<string>();

        public static double infinity = 9999;
        private int x;
        private int y;

        private static int puzzleSize;
        public static int PuzzleSize
        {
            get { return puzzleSize; }
        }
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
#endregion //zmienne

        public override double ComputeHeuristicGrade()
        {
            return 0;
        }

        public double ComputeHeuristicGrade_Manhattan(int[,] tab)
        {
            /// <summary>
            /// Heurystyka "Misplaced tiles"
            /// Zlicza liczbe pól nie na zwoim miejscu przy pominięciu zera.
            ///</summary>

            int counter = 0;
            int value = 0;
            for (int i = 0; i < puzzleSize; i++)
            {
                for (int j = 0; j < puzzleSize; j++)
                {
                    if (tab[i, j] != value && tab[i, j] != 0) counter++;
                    value++;
                }
            }

            return counter;
        }

        public virtual double ComputeHeuristicGrade(int[,] tab)
        {
            /// <summary>
            /// Heurystyka "Misplaced tiles"
            /// Zlicza liczbe pól nie na zwoim miejscu przy pominięciu zera.
            ///</summary>

            int counter = 0;
            int value = 0;
            for (int i = 0; i < puzzleSize; i++)
            {
                for (int j = 0; j < puzzleSize; j++)
                {
                    if (tab[i, j] != value && tab[i, j] != 0) counter++;
                    value++;
                }
            }

            return counter;
        }


        public PuzzleState() : base()
        {
            #region gerowanie ułożonej tablcy 
            this.table = new int[puzzleSize, puzzleSize];

            int licznik = 0;
            for (int i = 0; i < puzzleSize; i++)
            {
                for (int j = 0; j < puzzleSize; j++)
                {
                    this.table[i, j] = licznik;
                    licznik++;
                }
            }


            #endregion //gerowanie ułożonej tablcy 

            int mix_counter = 1000;//rnd.Next(5, 10); //ilosc mieszan
            #region Mieszanie puzzli
            Random rnd = new Random();

            //int mix_counter = 10;//rnd.Next(5, 10);

            Print(Table, Table);
            int x = 0, y = 0, gdzie = 0, tmp; // pozycja zera
            bool done;
            int[,] print_tmp = new int[puzzleSize, puzzleSize]; //tablica do podgladu kroków mieszania

            //while ((mix_counter--) != 0)
            //{
            //    print_tmp = (int[,])Table.Clone();
            //    done = false;
            //    while (!done)
            //    {



            //        gdzie = rnd.Next(1, 5);
            //        //Console.WriteLine(gdzie);
            //        switch (gdzie)
            //        {
            //            case 1:
            //                if ((x - 1) >= 0 && (x - 1) < puzzleSize)
            //                {
            //                    tmp = Table[x, y];
            //                    Table[x, y] = Table[x - 1, y];
            //                    Table[x - 1, y] = tmp;
            //                    x--;
            //                    done = true;
            //                }
            //                break;
            //            case 2:
            //                if ((x + 1) >= 0 && (x + 1) < puzzleSize)
            //                {
            //                    tmp = Table[x, y];
            //                    Table[x, y] = Table[x + 1, y];
            //                    Table[x + 1, y] = tmp;
            //                    x++;
            //                    done = true;
            //                }
            //                break;
            //            case 3:
            //                if ((y - 1) >= 0 && (y - 1) < puzzleSize)
            //                {
            //                    tmp = Table[x, y];
            //                    Table[x, y] = Table[x, y - 1];
            //                    Table[x, y - 1] = tmp;
            //                    y--;
            //                    done = true;
            //                }
            //                break;
            //            case 4:
            //                if ((y + 1) >= 0 && (y + 1) < puzzleSize)
            //                {
            //                    tmp = Table[x, y];
            //                    Table[x, y] = Table[x, y + 1];
            //                    Table[x, y + 1] = tmp;
            //                    y++;
            //                    done = true;
            //                }
            //                break;
            //        }

            //    }

            //    //Console.Write("\n");
            //    //Print(print_tmp, Table);
            //}

            int[] tab_str = new int[] { 1, 2, 5, 3, 4, 8, 6, 0, 7 };
            licznik = 0;
            for (int i = 0; i < puzzleSize; i++)
            {
                for (int j = 0; j < puzzleSize; j++)
                {
                    this.table[i, j] = (int)tab_str[licznik];
                    licznik++;
                }
            }


            Console.WriteLine("Pomieszane puzle ##############");
            Print(Table, Table);
            Console.WriteLine("///////////////##############");

            #endregion //Mieszanie puzzli

            //Generowanie id z aktualnego stanu tablicy
            for (int i = 0; i < puzzleSize; i++)
            {
                for (int j = 0; j < puzzleSize; j++)
                {
                    id += this.table[i, j];

                }
            }

            #region Szukanie X Y

            for (int i = 0; i < puzzleSize; i++)
            {
                for (int j = 0; j < puzzleSize; j++)
                {
                    if(Table[i,j] == 0)
                    {
                        x = i;
                        y = j;
                    }

                }
            }
            #endregion //Szukanie X Y

            Heuristic_vector(x, y);

            this.h = infinity;
        }

        public void Heuristic_vector(int x, int y)
        {
            #region Generowanie vektora heurystyk

            Heuristic_ways htmp;

            int x_tmp = x;
            int y_tmp = y;
            int tmp;

            #region gora
            htmp = new Heuristic_ways(1);
            htmp.Table = (int[,])Table.Clone();

            if ((x_tmp - 1) >= 0 && (x_tmp - 1) < puzzleSize)
            {
                tmp = htmp.Table[x_tmp, y];
                htmp.Table[x_tmp, y] = htmp.Table[x_tmp - 1, y];
                htmp.Table[x_tmp - 1, y] = tmp;
                x_tmp--;
                
                htmp.h = ComputeHeuristicGrade(htmp.Table);
            }
            else
            {
                htmp.h = infinity;
            }

            htmp.x = x_tmp;
            htmp.y = y_tmp;
            htmp.id_generate();
            Heuristic_vetor.Add(htmp);
            #endregion //gora

            #region dol
            x_tmp = x;
            y_tmp = y;
            htmp = new Heuristic_ways(2);
            htmp.Table = (int[,])Table.Clone();

            if ((x_tmp + 1) >= 0 && (x_tmp + 1) < puzzleSize)
            {
                tmp = htmp.Table[x_tmp, y_tmp];
                htmp.Table[x_tmp, y_tmp] = htmp.Table[x_tmp + 1, y_tmp];
                htmp.Table[x_tmp + 1, y_tmp] = tmp;
                x_tmp++;

                htmp.h = ComputeHeuristicGrade(htmp.Table);
            }
            else
            {
                htmp.h = infinity;
            }

            htmp.x = x_tmp;
            htmp.y = y_tmp;
            htmp.id_generate();
            Heuristic_vetor.Add(htmp);
            #endregion //dol

            #region lewo
            x_tmp = x;
            y_tmp = y;
            htmp = new Heuristic_ways(3);
            htmp.Table = (int[,])Table.Clone();

            if ((y - 1) >= 0 && (y - 1) < puzzleSize)
            {
                tmp = htmp.Table[x_tmp, y_tmp];
                htmp.Table[x_tmp, y_tmp] = htmp.Table[x_tmp, y_tmp - 1];
                htmp.Table[x_tmp, y_tmp - 1] = tmp;
                y_tmp--;

                htmp.h = ComputeHeuristicGrade(htmp.Table);
            }
            else
            {
                htmp.h = infinity;
            }

            htmp.x = x_tmp;
            htmp.y = y_tmp;
            htmp.id_generate();
            Heuristic_vetor.Add(htmp);
            #endregion //lewo

            #region prawo
            x_tmp = x;
            y_tmp = y;
            htmp = new Heuristic_ways(4);
            htmp.Table = (int[,])Table.Clone();

            if ((y_tmp + 1) >= 0 && (y_tmp + 1) < puzzleSize)
            {
                tmp = htmp.Table[x_tmp, y_tmp];
                htmp.Table[x_tmp, y_tmp] = htmp.Table[x_tmp, y_tmp + 1];
                htmp.Table[x_tmp, y_tmp + 1] = tmp;
                y_tmp++;

                htmp.h = ComputeHeuristicGrade(htmp.Table);
            }
            else
            {
                htmp.h = infinity;
            }

            htmp.x = x_tmp;
            htmp.y = y_tmp;
            htmp.id_generate();
            Heuristic_vetor.Add(htmp);
            #endregion //prawo

            #endregion //Generowanie vektora heurystyk
        }

        public PuzzleState(PuzzleState parent, Heuristic_ways tmp) : base(parent)
        {
            this.table = new int[PuzzleSize, PuzzleSize];

            Array.Copy(tmp.Table, this.table, this.table.Length);
            // ciało konstruktora

            this.id = tmp.ID;
            id_list.Add(this.id);

            this.x = tmp.x;
            this.y = tmp.y;

            if (tmp.h != 0) Heuristic_vector(x, y);
            this.h = tmp.h;

            //W stanie w ktorym jestesmy droga jest o jeden większa niż w rodzicu
            this.g = parent.g + 1;
        }


        public void Print(int[,] tab_before, int[,] tab_after)
        {
            for (int i = 0; i < puzzleSize; i++)
            {


                for (int j=0; j < puzzleSize; j++)
                {
                    if (tab_before[i,j] != tab_after[i,j])
                    {
                        Console.BackgroundColor = ConsoleColor.DarkCyan;
                        Console.Write("   {0}",tab_after[i, j]);
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.Write("   {0}", tab_after[i, j]);
                    }
                    
                    if ((j + 1) % puzzleSize == 0) Console.Write("\n");
                    
                }
            }
            
            Console.Write("\n\n####\n");
        }


        public static void start(int size)
        {
            Stopwatch stopWatch_search = new Stopwatch();
            Stopwatch stopWatch_print = new Stopwatch();

                puzzleSize = size;
            
            PuzzleState startState = new PuzzleState();
            PuzzleSearch searcher = new PuzzleSearch(startState);


                stopWatch_search.Start();

            searcher.DoSearch();
            stopWatch_search.Stop();

                TimeSpan t_search = stopWatch_search.Elapsed;

            IState state = searcher.Solutions[0];

            List<PuzzleState> solutionPath = new List<PuzzleState>();

            while (state != null)
            {
                solutionPath.Add((PuzzleState)state);
                state = state.Parent;
            }
            solutionPath.Reverse();

            int[,] table_tmp1 = new int[puzzleSize, puzzleSize];
            int[,] table_tmp2 = new int[puzzleSize, puzzleSize];

            table_tmp1 = solutionPath[0].Table;


            stopWatch_print.Start();

            foreach (PuzzleState s in solutionPath)
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
