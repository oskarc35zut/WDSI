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
            DialogResult dialogResult_sudoku = MessageBox.Show("Chesz zobaczyć sudoku? ( ͡° ͜ʖ ͡°)", " ", MessageBoxButtons.YesNo);
            if (dialogResult_sudoku == DialogResult.Yes)
            {
                SudokuState.start();
            }

            DialogResult dialogResult_puzzle = MessageBox.Show("A puzzle? ( ͡° ͜ʖ ͡°)", " ", MessageBoxButtons.YesNo);
            if (dialogResult_puzzle == DialogResult.Yes)
            {
                //PuzzleState.start();
            }

            Console.ReadLine();
        }
    }
    
}
