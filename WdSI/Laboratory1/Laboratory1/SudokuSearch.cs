using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laboratory1
{
    public class SudokuSearch : AStarSearch
    {

        public SudokuSearch(SudokuState state) : base(state, true, true) { }// dziedziczy cały konstruktor po AStartSearch
        protected override void buildChildren(IState parent)
        {
            SudokuState state = (SudokuState)parent;

            

            for (int m = 1; m < (SudokuState.GRID_SIZE); m++)
            { 
                for (int i = 0; i < SudokuState.GRID_SIZE; ++i)
                {
                    for (int j = 0; j < SudokuState.GRID_SIZE; ++j)
                    { 
                        if ((state.Heuristic_array[i, j] == m) && (state.Table[i,j] == 0))
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
            
        }

        protected override bool isSolution(IState state)
        {
            return state.H == 0.0;
        }
    }
}
