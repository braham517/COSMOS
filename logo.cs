using Cosmos.HAL;
using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;


namespace minios
{
    class logo
    {
        internal static void Logo(string version)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(" ------------------------------------------------------------------------------ ");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("                    _        ");       
            Console.WriteLine("                   (_)       ");            
            Console.WriteLine("                             ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("                    _        ");
            Console.WriteLine("               *   | |   *   ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("              *    | |    *  ");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("              *    |_|    *  ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("               *         *   ");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("                  *   *      ");
            Console.WriteLine(" ------------------------------------------------------------------------------ ");
        }
    }
}
