using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Laboratory1 {
    class Program {
        static void Main(string[] args)
        {
            Console.BufferHeight = 1000;

            DialogResult dialogResult_sudoku = MessageBox.Show("Chesz zobaczyć sudoku? ( ͡° ͜ʖ ͡°)", "", MessageBoxButtons.YesNo);
            if (dialogResult_sudoku == DialogResult.Yes)
            {
                string sudokuPattern = "083279465409583710270461893342058176597106284618720359954812037701645908826397540"; // sudoku w postaci stringa np .:" 010330218... "
                #region test
                //040000065469583712275461893342958176597136284618724359954812637731645928826397541
                //183279465469583712275461893342958176597136284618724359954812637731645928826397541
                /*"040 000 065
                    * 469 583 712
                    * 275 461 893
                    * 
                    * 342 958 176
                    * 597 136 284
                    * 618 724 359
                    * 
                    * 954 812 637
                    * 731 645 928
                    * 826 397 541"
                    */
                //000000465469583712275461893342958176597136284618724359954812637731645928826397541
                //000079065000003002005060093340050106000000000608020059950010600700600000820390000
                #endregion //test

                SudokuState.start(sudokuPattern);
                Console.ReadLine();
            }

            DialogResult dialogResult_puzzle = MessageBox.Show("A puzzle? ( ͡° ͜ʖ ͡°)", "", MessageBoxButtons.YesNo);
            if (dialogResult_puzzle == DialogResult.Yes)
            {
                int puzzlesize = 3;
                PuzzleState.PuzzleSize = puzzlesize;
                Laboratory1_m.PuzzleState.PuzzleSize = puzzlesize;
                gen();

                Stopwatch stopWatch_tails = new Stopwatch();
                stopWatch_tails.Start();
                PuzzleState.start(puzzlesize);
                stopWatch_tails.Stop();

                Stopwatch stopWatch_man = new Stopwatch();
                stopWatch_man.Start();
                Laboratory1_m.PuzzleState.start(puzzlesize);
                stopWatch_man.Stop();

                TimeSpan t_search = stopWatch_tails.Elapsed;
                string SearchTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    t_search.Hours, t_search.Minutes, t_search.Seconds,
                    t_search.Milliseconds / 10);
                Console.WriteLine("Calkowity czas tails: " + SearchTime);

                t_search = stopWatch_man.Elapsed;
                SearchTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                t_search.Hours, t_search.Minutes, t_search.Seconds,
                t_search.Milliseconds / 10);
                Console.WriteLine("Calkowity czas man: " + SearchTime);

                Console.ReadLine();

            }

           
        }
 
        public static bool isopen = false;
        public static bool iscloseed = false;

        #region smieci konieczne
        static public int[,] Table = new int[PuzzleState.PuzzleSize, PuzzleState.PuzzleSize];
        
        static private void gen()
        {
            #region gerowanie ułożonej tablcy 
            Table = new int[PuzzleState.PuzzleSize, PuzzleState.PuzzleSize];

            int licznik = 0;
            for (int i = 0; i < PuzzleState.PuzzleSize; i++)
            {
                for (int j = 0; j < PuzzleState.PuzzleSize; j++)
                {
                    Table[i, j] = licznik;
                    licznik++;
                }
            }


            #endregion //gerowanie ułożonej tablcy 

            int mix_counter = 1000;//rnd.Next(5, 10); //ilosc mieszan
            #region Mieszanie puzzli
            Random rnd = new Random();

            //int mix_counter = 10;//rnd.Next(5, 10);
            int x = 0, y = 0, gdzie = 0, tmp; // pozycja zera
            bool done;
            int[,] print_tmp = new int[PuzzleState.PuzzleSize, PuzzleState.PuzzleSize]; //tablica do podgladu kroków mieszania

            while ((mix_counter--) != 0)
            {
                print_tmp = (int[,])Table.Clone();
                done = false;
                while (!done)
                {



                    gdzie = rnd.Next(1, 5);
                    //Console.WriteLine(gdzie);
                    switch (gdzie)
                    {
                        case 1:
                            if ((x - 1) >= 0 && (x - 1) < PuzzleState.PuzzleSize)
                            {
                                tmp = Table[x, y];
                                Table[x, y] = Table[x - 1, y];
                                Table[x - 1, y] = tmp;
                                x--;
                                done = true;
                            }
                            break;
                        case 2:
                            if ((x + 1) >= 0 && (x + 1) < PuzzleState.PuzzleSize)
                            {
                                tmp = Table[x, y];
                                Table[x, y] = Table[x + 1, y];
                                Table[x + 1, y] = tmp;
                                x++;
                                done = true;
                            }
                            break;
                        case 3:
                            if ((y - 1) >= 0 && (y - 1) < PuzzleState.PuzzleSize)
                            {
                                tmp = Table[x, y];
                                Table[x, y] = Table[x, y - 1];
                                Table[x, y - 1] = tmp;
                                y--;
                                done = true;
                            }
                            break;
                        case 4:
                            if ((y + 1) >= 0 && (y + 1) < PuzzleState.PuzzleSize)
                            {
                                tmp = Table[x, y];
                                Table[x, y] = Table[x, y + 1];
                                Table[x, y + 1] = tmp;
                                y++;
                                done = true;
                            }
                            break;

                    }
                    int[] tab_str = new int[] { 1, 2, 5, 3, 4, 8, 6, 0, 7 };
                    licznik = 0;
                    for (int i = 0; i < PuzzleState.PuzzleSize; i++)
                    {
                        for (int j = 0; j < PuzzleState.PuzzleSize; j++)
                        {
                            Table[i, j] = (int)tab_str[licznik];
                            licznik++;
                        }
                    }


                 

                    #endregion //Mieszanie puzzli

                }
                
            }
        }
        #endregion //smieci konieczne
    }

}
