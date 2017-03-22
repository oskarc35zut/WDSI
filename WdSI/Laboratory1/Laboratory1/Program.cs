using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Laboratory1 {
    class Program {
        static void Main(string[] args)
        {
            Console.BufferHeight = 1000;

            string chose = "sudoku";
            chose = Console.ReadLine();
            Stopwatch stopWatch_search = new Stopwatch();
            Stopwatch stopWatch_print = new Stopwatch();
            switch (chose)
            {
                case "sudoku":
                    #region sudoku
                    string sudokuPattern = "083279465409583710270461893342058176597106284618720359954812037701645908826397540"; // sudoku w postaci stringa np .:" 010330218... "
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
                    SudokuState startState = new SudokuState(sudokuPattern);
                    SudokuSearch searcher = new SudokuSearch(startState);

                    
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

                    int[,] table_tmp1 = new int[9,9];
                    int[,] table_tmp2 = new int[9,9];

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
                    #endregion //sudoku
                    break;
                case "puzzle":
                    #region puzzle

                    int puzzleSize = 3;

                    PuzzleState puzzlestartState = new PuzzleState(puzzleSize);
                    PuzzleSearch puzzlesearcher = new PuzzleSearch(puzzlestartState);

                    
                    stopWatch_search.Start();

                    puzzlesearcher.DoSearch();
                    stopWatch_search.Stop();

                    //TimeSpan t_search = stopWatch_search.Elapsed;

                    IState puzzlestate = puzzlesearcher.Solutions[0];

                    List<PuzzleState> PuzzlesolutionPath = new List<PuzzleState>();

                    while (puzzlestate != null)
                    {
                        PuzzlesolutionPath.Add((PuzzleState)puzzlestate);
                        state = puzzlestate.Parent;
                    }
                    PuzzlesolutionPath.Reverse();

                    int[,] ptable_tmp1 = new int[9, 9];
                    int[,] ptable_tmp2 = new int[9, 9];

                    table_tmp1 = PuzzlesolutionPath[0].Table;


                    stopWatch_print.Start();

                    foreach (PuzzleState s in PuzzlesolutionPath)
                    {
                        ptable_tmp2 = ptable_tmp1;
                        ptable_tmp1 = s.Table;

                        s.Print(ptable_tmp2, ptable_tmp1);
                    }

                    stopWatch_print.Stop();
                    //TimeSpan t_print = stopWatch_print.Elapsed;

                    //string SearchTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    //t_search.Hours, t_search.Minutes, t_search.Seconds,
                    //t_search.Milliseconds / 10);
                    //Console.WriteLine("Czas przeszukiwania " + SearchTime);

                    //string PrintTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    //t_print.Hours, t_print.Minutes, t_print.Seconds,
                    //t_print.Milliseconds / 10);
                    //Console.WriteLine("Czas wyswietlania " + PrintTime);
                    #endregion //Puzzle


                    break;
            }

            Console.ReadLine();
        }
    }
    
}
