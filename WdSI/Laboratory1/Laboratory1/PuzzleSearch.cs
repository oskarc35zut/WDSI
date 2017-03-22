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

            




 

        }


        protected override bool isSolution(IState state)
        {
            return state.H == 0.0;
        }
    }
}
