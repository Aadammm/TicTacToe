using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GitTicTacToe
{
    delegate void Delegate();
    internal class Game
    {
        Board plocha = new Board();
        Delegate Choose;
        string[] optionsMenu = { "Player vs Player", "Easy", "Medium", "Hard" };
        int enemy = 0;
        /// <summary>
        /// začiatok hry
        /// </summary>
        public void Start()
        {
            //
            Menu();
            Against();
            while (Check.Winner == Winner.NoBody)
            {
                Console.Clear();
                Console.WriteLine(plocha);
                Choose();
                Check.Checking();
                Thread.Sleep(100);
                if (Check.Winner != Winner.NoBody)
                    TheEnd();
            }

        }
        /// <summary>
        /// vypíše víťaza a možnosť hrať znovu 
        /// </summary>
        private void TheEnd()
        {
            Console.Clear();
            Console.WriteLine(plocha);
            switch (Check.Winner)
            {
                case Winner.WinX:
                    Console.WriteLine("Won X");
                    break;
                case Winner.WinO:
                    Console.WriteLine("Won O");
                    break;
                case Winner.Draw:
                    Console.WriteLine("DRAW");
                    break;
            }
            Console.WriteLine("Want to play again ? Press 'a'");
            char zadanyZnak = Console.ReadKey().KeyChar;
            if (zadanyZnak == 'a')
            {
                Check.Winner = Winner.NoBody;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        plocha[j, i] = 0;
                    }
                }
                Start();
            }

        }
        /// <summary>
        /// Menu - určenie obtiažnosti
        /// </summary>
        private void Menu()
        {
            Console.CursorVisible = false;
            ConsoleKeyInfo key;
            do
            {
                Console.Clear();
                for (int i = 0; i < optionsMenu.Length; i++)
                {
                    if (enemy == i)
                        Console.WriteLine("<" + optionsMenu[i] + ">");
                    else
                        Console.WriteLine(" " + optionsMenu[i]);
                }
                key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        enemy = (enemy == 0) ? optionsMenu.Length - 1 : enemy - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        enemy = (enemy == optionsMenu.Length - 1) ? 0 : enemy + 1;
                        break;
                }
            }
            while (key.Key != ConsoleKey.Enter);
        }
        /// <summary>
        /// uloží do delegáta voľbu z Menu
        /// </summary>
        private void Against()
        {
            PlayerVs hracVs = new PlayerVs();
            switch (enemy)
            {
                case 0:
                    Choose = hracVs.PlayerVsPlayer;
                    break;
                case 1:
                    Choose = hracVs.Easy;
                    break;
                case 2:
                    Choose = hracVs.Medium;
                    break;
                case 3:
                    Choose = hracVs.Hard;
                    break;
            }

        }
    }
}
