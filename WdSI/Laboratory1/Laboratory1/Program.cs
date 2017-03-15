using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laboratory1 {
    class Program {
        static void Main(string[] args)
        {
            
            Console.BufferHeight = 1000;
            string sudokuPattern = "000000465469583712275461893342958176597136284618724359954812637731645928826397541"; // sudoku w postaci stringa np .:" 010330218... "
                                  //183279465469583712275461893342958176597136284618724359954812637731645928826397541
            //000079065000003002005060093340050106000000000608020059950010600700600000820390000
            SudokuState startState = new SudokuState(sudokuPattern);
            SudokuSearch searcher = new SudokuSearch(startState);
            searcher.DoSearch();

            IState state = searcher.Solutions[0];

            List<SudokuState> solutionPath = new List<SudokuState>();

            while (state != null)
            {
                solutionPath.Add((SudokuState)state);
                state = state.Parent;
            }
            solutionPath.Reverse();

            foreach (SudokuState s in solutionPath)
            {
                s.Print();
            }
            Console.ReadLine();
        }
    }
    
}
