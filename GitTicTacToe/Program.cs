﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitTicTacToe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game ticTacToe = new Game();
            ticTacToe.Start();

            Console.ReadLine();
        }
    }
}
