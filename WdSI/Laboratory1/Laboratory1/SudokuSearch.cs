using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laboratory1
{
    public class SudokuSearch : AStarSearch
    {
        public static int heurystyka(int[,] Table, int x, int y)
        {
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




        public SudokuSearch(SudokuState state) : base(state, true, true) { }// dziedziczy cały konstruktor po AStartSearch
        protected override void buildChildren(IState parent)
        {
            SudokuState state = (SudokuState)parent;

            int[,] Heuristis_array = new int[9,9];

            for (int i = 0; i < SudokuState.GRID_SIZE; ++i)
            {
                for (int j = 0; j < SudokuState.GRID_SIZE; ++j)
                {
                    Heuristis_array[i,j] = heurystyka(state.Table, i, j);
                }
            }

            for (int m = 0; m <= SudokuState.GRID_SIZE; m++)
            { 
                for (int i = 0; i < SudokuState.GRID_SIZE; ++i)
                {
                    for (int j = 0; j < SudokuState.GRID_SIZE; ++j)
                    { 
                        if (Heuristis_array[i, j] == m)
                        {
                            
                            for (int k = 1; k < SudokuState.GRID_SIZE + 1; ++k)
                            {
                                SudokuState child = new SudokuState(state, k, i, j);
                                parent.Children.Add(child);
                            }
                            break;
                        }
                    }
                }
            }

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
        }


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

        protected override bool isSolution(IState state)
        {
            return state.H == 0.0;
        }
    }
}
