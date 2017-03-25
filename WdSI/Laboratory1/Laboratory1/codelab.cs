using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laboratory1
{
    class codelab
    {
        void counter0()
        {
            //#region Counter0
            //int Counter0 = 0;
            //int Counter0x = 0;
            //int Counter0y = 0;
            //return 0;
            //for (int i = 0; i < GRID_SIZE; i++)
            //{
            //    if (this.Table[(int)this.x, i] == 0) Counter0x++;
            //    if (this.Table[i, (int)this.y] == 0) Counter0y++;
            //}

            //for (int i = x_start; i <= x_stop; i++)
            //{
            //    for (int j = y_start; j <= y_stop; j++)
            //    {
            //        if (this.Table[i, j] == 0) Counter0++;
            //    }
            //}


            ////if (Counter0 < Counter0x || Counter0 < Counter0y)
            ////{ return Counter0; }
            ////else
            ////{
            ////   if(Counter0x < Counter0y)
            ////    {
            ////        return Counter0x;
            ////    }
            ////   else
            ////    {
            ////        return Counter0y;
            ////    }
            ////}
            //#endregion //Counter0
        }

        public static int heurystyka(int[,] Table, int x, int y)
        {
            if (Table[x, y] != 0) return 99999;

            int GRID_SIZE = 9;

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
                if (Table[i, y] != 0)
                {
                    foreach (int tmp_l in repeat_list)
                    {

                        if (Table[i, y] == tmp_l)
                        {
                            return 99999;
                        }
                        else
                        {
                            repeat_list.Add(Table[i, y]);
                        }

                    }
                }
            }
            repeat_list.Clear();
            #endregion // przeszukiwanie powrórzeń w wierszu

            #region przeszukiwanie powrórzeń w kolumnie
            for (int i = 0; i < GRID_SIZE; i++)
            {
                if (Table[i, y] != 0)
                {
                    foreach (int tmp_l in repeat_list)
                    {

                        if (Table[x, i] == tmp_l)
                        {
                            return 99999;
                        }
                        else
                        {
                            repeat_list.Add(Table[x, i]);
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
                    if (Table[i, j] != 0)
                    {
                        foreach (int tmp_l in repeat_list)
                        {

                            if (Table[i, j] == tmp_l)
                            {
                                return 99999;
                            }
                            else
                            {
                                repeat_list.Add(Table[i, j]);
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
                if (Table[i, y] != 0)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (Capabilities_list[j] == Table[i, y])
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
                if (Table[x, i] != 0)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (Capabilities_list[j] == Table[x, i])
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
                    if (Table[i, j] != 0)
                    {
                        for (int k = 0; k < 9; k++)
                        {
                            if (Capabilities_list[k] == Table[i, j])
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

            for (i=0; i <= 8; i+=2)
            {
                if (x == i)
                {
                    x_start = i;
                    x_stop = i + 2;
                }
                if (y == i)
                {
                    y_start = i;
                    y_stop = i + 2;
                }

            }


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
                    for (int j = 0; j < 9; j++)
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
    }
}


//for (int i = 0; i < SudokuState.GRID_SIZE; ++i)
//{
//    for (int j = 0; j < SudokuState.GRID_SIZE; ++j)
//    {
//        if (Heuristic_array[i, j] == 99999)
//        {

//            for (int k = 1; k < SudokuState.GRID_SIZE + 1; ++k)
//            {
//                SudokuState child = new SudokuState(state, k, i, j);
//                parent.Children.Add(child);
//            }
//            break;
//        }
//    }
//}
//Console.Read();
//// poszukiwanie wolnego pola
//for (int i = 0; i < SudokuState.GRID_SIZE; ++i)
//{
//    for (int j = 0; j < SudokuState.GRID_SIZE; ++j)
//    {
//        if (state.Table[i, j] == 0)
//        {
//            //wstawianie kolejnych potomkow w wolne pole
//            for (int k = 1; k < SudokuState.GRID_SIZE + 1; ++k)
//            {
//                SudokuState child = new SudokuState(state, k, i, j);
//                parent.Children.Add(child);
//            }
//            break;
//        }
//    }
//}



//protected override void buildChildren(IState parent)
//{
//    SudokuState state = (SudokuState)parent;

//    // poszukiwanie wolnego pola
//    for (int i = 0; i < SudokuState.GRID_SIZE; ++i)
//    {
//        for (int j = 0; j < SudokuState.GRID_SIZE; ++j)
//        {
//            if (state.Table[i, j] == 0)
//            {
//                // wstawianie kolejnych potomkow w wolne pole
//                for (int k = 1; k < SudokuState.GRID_SIZE + 1; ++k)
//                {
//                    SudokuState child = new SudokuState(state, k, i, j);
//                    parent.Children.Add(child);
//                }
//                break;
//            }
//        }
//    }
//}

string chose = "sudoku";
            switch (chose)
            {
                
                case "sudoku":
                    #region sudoku

                    //SudokuState.start();

                    #endregion //sudoku
                    break;
                case "puzzle":
                    #region puzzle

                    //int puzzleSize = 3;
                    //Stopwatch stopWatch_search = new Stopwatch();
                    //Stopwatch stopWatch_print = new Stopwatch();
                    //PuzzleState puzzlestartState = new PuzzleState(puzzleSize);
                    //PuzzleSearch puzzlesearcher = new PuzzleSearch(puzzlestartState);

                    
                    //stopWatch_search.Start();

                    //puzzlesearcher.DoSearch();
                    //stopWatch_search.Stop();

                    ////TimeSpan t_search = stopWatch_search.Elapsed;

                    //IState puzzlestate = puzzlesearcher.Solutions[0];

                    //List<PuzzleState> PuzzlesolutionPath = new List<PuzzleState>();

                    //while (puzzlestate != null)
                    //{
                    //    PuzzlesolutionPath.Add((PuzzleState)puzzlestate);
                    //    state = puzzlestate.Parent;
                    //}
                    //PuzzlesolutionPath.Reverse();

                    //int[,] ptable_tmp1 = new int[9, 9];
                    //int[,] ptable_tmp2 = new int[9, 9];

                    //table_tmp1 = PuzzlesolutionPath[0].Table;


                    //stopWatch_print.Start();

                    //foreach (PuzzleState s in PuzzlesolutionPath)
                    //{
                    //    ptable_tmp2 = ptable_tmp1;
                    //    ptable_tmp1 = s.Table;

                    //    s.Print(ptable_tmp2, ptable_tmp1);
                    //}

                    //stopWatch_print.Stop();
                    ////TimeSpan t_print = stopWatch_print.Elapsed;

                    ////string SearchTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ////t_search.Hours, t_search.Minutes, t_search.Seconds,
                    ////t_search.Milliseconds / 10);
                    ////Console.WriteLine("Czas przeszukiwania " + SearchTime);

                    ////string PrintTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ////t_print.Hours, t_print.Minutes, t_print.Seconds,
                    ////t_print.Milliseconds / 10);
                    ////Console.WriteLine("Czas wyswietlania " + PrintTime);
                    #endregion //Puzzle


                    break;
            }