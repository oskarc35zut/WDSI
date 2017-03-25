﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Laboratory1
{
    class PuzzleState : State
    {
        private static double infinity = 9999;

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

            #region Mieszanie puzzli
            Random rnd = new Random();

            int mix_counter = 100;//rnd.Next(5, 10);

            Print(Table, Table);
            int x = 0, y = 0, gdzie = 0, tmp; // pozycja zera
            bool done;
            int[,] print_tmp = new int[puzzleSize, puzzleSize]; //tablica do podgladu kroków mieszania

            while ((mix_counter--) != 0)
            {
                print_tmp = (int[,])Table.Clone();
                done = false;
                while(!done)
                {

                   

                    gdzie = rnd.Next(1, 5);
                    //Console.WriteLine(gdzie);
                       switch (gdzie)
                    {
                        case 1:
                            if ((x-1) >= 0 && (x-1) < puzzleSize)
                            {
                                tmp = Table[x, y];
                                Table[x, y] = Table[x-1, y];
                                Table[x - 1, y] = tmp;
                                x--;
                                done = true;
                            }
                            break;
                        case 2:
                            if ((x + 1) >= 0 && (x + 1) < puzzleSize)
                            {
                                tmp = Table[x, y];
                                Table[x, y] = Table[x + 1, y];
                                Table[x + 1, y] = tmp;
                                x++;
                                done = true;
                            }
                            break;
                        case 3:
                            if ((y - 1) >= 0 && (y - 1) < puzzleSize)
                            {
                                tmp = Table[x, y];
                                Table[x, y] = Table[x , y - 1];
                                Table[x, y - 1] = tmp;
                                y--;
                                done = true;
                            }
                            break;
                        case 4:
                            if ((y + 1) >= 0 && (y + 1) < puzzleSize)
                            {
                                tmp = Table[x, y];
                                Table[x, y] = Table[x, y + 1];
                                Table[x, y + 1] = tmp;
                                y++;
                                done = true;
                            }
                            break;
                    }   

                }

                //Console.Write("\n");
                //Print(print_tmp, Table);
            }

            #endregion //Mieszanie puzzli

            //Generowanie id z aktualnego stanu tablicy
            for (int i = 0; i < puzzleSize; i++)
            {
                for (int j = 0; j < puzzleSize; j++)
                {
                    id += this.table[i, j];

                }
            }


            this.h = infinity;
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
            for (int i = 0; i < puzzleSize; i++)
            {


                for (int j=0; j < puzzleSize; j++)
                {
                    if (tab_before[i,j] != tab_after[i,j])
                    {
                        Console.BackgroundColor = ConsoleColor.DarkCyan;
                        Console.Write(tab_after[i, j]);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.Write(tab_after[i, j] + " ");
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
