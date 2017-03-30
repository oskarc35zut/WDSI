using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laboratory1_m
{
    class PuzzleSearch : AStarSearch
    {
        

        public PuzzleSearch(PuzzleState state) : base(state, true, true) { }
        protected override void buildChildren(IState parent)
        {
            PuzzleState state = (PuzzleState)parent;

            foreach (string id in state.id_list)
            { 
                foreach (Heuristic_ways obj in state.Heuristic_vetor)
                {
                    if (obj.ID == id)
                    {
                        obj.h = PuzzleState.infinity;
                    }
                }
            }


            bool isZero = false;		

            for (int i = 0; i <= (PuzzleState.PuzzleSize* PuzzleState.PuzzleSize* PuzzleState.PuzzleSize + state.G); i++)
            {
                foreach (Heuristic_ways obj in state.Heuristic_vetor)
                {

                    if (obj.F == i)
                    {
                        PuzzleState child = new PuzzleState(state, obj);
                        if (child.H == 0)
                        {
                            //isZero = true;
                        }
                        parent.Children.Add(child);
                        //state.Print(state.Table, child.Table);
                        
                    }
                    if (isZero) break;
                }
                if (isZero) break;
            }

        }


        protected override bool isSolution(IState state)
        {
            return state.H == 0.0;
        }
    }
}
