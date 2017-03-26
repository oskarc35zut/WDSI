using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
            }

            DialogResult dialogResult_puzzle = MessageBox.Show("A puzzle? ( ͡° ͜ʖ ͡°)", "", MessageBoxButtons.YesNo);
            if (dialogResult_puzzle == DialogResult.Yes)
            {
                int puzzlesize = 3;

                PuzzleState.start(puzzlesize);
            }

            Console.ReadLine();
        }
    }
    
}
