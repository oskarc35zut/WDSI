using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laboratory2 {
    class Program {
        static void Main(string[] args) {
            Console.SetWindowPosition(0, 0);
            Console.CursorVisible = false;
            Console.Write(" Connect4");

            int width = 31;
            int heigth = 5;
            int deep = 5;

            Connect4State parrent = new Connect4State(width, heigth, deep);
            //Connect4State.GetChoise(parrent.Table);
            Console.WriteLine("\n\n\nWybrano" + Connect4State.GetChoise(parrent.Table));
            
            Console.Read();
        }
    }
}
