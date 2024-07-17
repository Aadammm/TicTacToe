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
        Delegate Choose;
        readonly Board _board;
        readonly PlayerVs _playerVs;
        readonly string[] _optionsMenu = { "Player vs Player", "Easy", "Medium", "Hard" };
        int _enemy;

        public Game()
        {
            _board = new Board();
            _playerVs = new PlayerVs();
            _enemy = 0;
        }
        public void  Start()
        {
            DisplayMenu();
            ChooseAgainstWho();
            while (Check.Winner == WhoWin.Nobody)
            {
                Console.Clear();
                Console.WriteLine(_board);
                Choose();
                Check.Checking();
                Thread.Sleep(100);
                if (Check.Winner != WhoWin.Nobody)
                    EndTicTacToe();
            }

        }
        private void EndTicTacToe()
        {
            Console.Clear();
            Console.WriteLine(_board);
            switch (Check.Winner)
            {
                case WhoWin.WinX:
                    Console.WriteLine("Won X");
                    break;
                case WhoWin.WinO:
                    Console.WriteLine("Won O");
                    break;
                case WhoWin.Draw:
                    Console.WriteLine("DRAW");
                    break;
            }
            Console.WriteLine("Want to play again ? Press 'a'");
            char zadanyZnak = Console.ReadKey().KeyChar;
            if (zadanyZnak == 'a')
            {
                Check.ChangeStatusToNobody();
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        _board[j, i] = 0;
                    }
                }
                Start();
            }

        }
        private void DisplayMenu()
        {
            Console.CursorVisible = false;
            ConsoleKeyInfo key;
            do
            {
                Console.Clear();
                for (int i = 0; i < _optionsMenu.Length; i++)
                {
                    if (_enemy == i)
                        Console.WriteLine("<" + _optionsMenu[i] + ">");
                    else
                        Console.WriteLine(" " + _optionsMenu[i]);
                }
                key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        _enemy = (_enemy == 0) ? _optionsMenu.Length - 1 : _enemy - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        _enemy = (_enemy == _optionsMenu.Length - 1) ? 0 : _enemy + 1;
                        break;
                }
            }
            while (key.Key != ConsoleKey.Enter);
        }
        private void ChooseAgainstWho()
        {
            switch (_enemy)
            {
                case 0:
                    Choose = _playerVs.PlayerVsPlayer;
                    break;
                case 1:
                    Choose = _playerVs.Easy;
                    break;
                case 2:
                    Choose = _playerVs.Medium;
                    break;
                case 3:
                    Choose = _playerVs.Hard;
                    break;
            }
        }
    }
}
