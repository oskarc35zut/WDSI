using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laboratory1
{
    class PuzzleSearch : AStarSearch
    {
        public PuzzleSearch(PuzzleState state) : base(state, true, true) { }
        protected override void buildChildren(IState parent)
        {
            PuzzleState state = (PuzzleState)parent;

            PuzzleState child = new PuzzleState(state);
            parent.Children.Add(child);


            // poszukiwanie wolnego pola
            //for (int i = 0; i < SudokuState.GRID_SIZE; ++i)
            //{
            //     for (int j = 0; j < SudokuState.GRID_SIZE; ++j)
            //    {
            //         if (state.Table[i, j] == 0)
            //        {
            //             // wstawianie kolejnych potomkow w wolne pole
            //            for (int k = 1; k < SudokuState.GRID_SIZE + 1; ++k)
            //            {
            //                 SudokuState child = new SudokuState(state, k, i, j);
            //                 parent.Children.Add(child);
            //                 }
            //             break;
            //             }
            //         }
            //    }






        }


        protected override bool isSolution(IState state)
        {
            return state.H == 0.0;
        }
    }
}
