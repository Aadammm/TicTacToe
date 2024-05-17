using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitTicTacToe
{
    public enum WhoWin { Nobody, Draw, WinX, WinO }
    internal static class Check
    {
        static int _winX;
        static int _winO;
        public static WhoWin Winner;
        public static void Checking()
        {
            Winner = WhoWin.Nobody;
            if (Winner == WhoWin.Nobody)
                RowsCheck();
            if (Winner == WhoWin.Nobody)
                ColumnsCheck();
            if (Winner == WhoWin.Nobody)
                DiagonalCheck();
            if (!Board.FreePlace() && Winner == WhoWin.Nobody)
                Winner = WhoWin.Draw;
        }
        public static void ChangeStatusToNobody()
        {
            Winner = WhoWin.Nobody;
        }
        static void RowsCheck()
        {
            for (int i = 0; i < 3 && Winner == WhoWin.Nobody; i++)
            {
                _winX = 0;
                _winO = 0;
                for (int j = 0; j < 3 && Winner == WhoWin.Nobody; j++)
                {
                    WinnerCheck(j,i); 
                }
            }

        }
        static void ColumnsCheck()
        {
            for (int i = 0; i < 3 && Winner == WhoWin.Nobody; i++)
            {
                _winX = 0;
                _winO = 0;
                for (int j = 0; j < 3 && Winner == WhoWin.Nobody; j++)
                {
                    WinnerCheck(i, j);
                }
            }
        }
        static void DiagonalCheck()
        {
            _winX = 0;
            _winO = 0;
            for (int i = 0; i < 3 && Winner == WhoWin.Nobody; i++)
            {
                WinnerCheck(i, i);
            }
            _winX = 0;
            _winO = 0;
            for (int i = 0; i < 3 && Winner == WhoWin.Nobody; i++)
            {
                WinnerCheck(2 - i, i);
            }
        }
        static void WinnerCheck(int i, int j)
        {
            _winX = Board.GameBoard[i, j] == 1 ? _winX + 1 : _winX;
            _winO = Board.GameBoard[i, j] == 2 ? _winO + 1 : _winO;
            if (_winX == 3)
            {
                Winner = WhoWin.WinX;
            }
            if (_winO == 3)
            {
                Winner = WhoWin.WinO;
            }

        }
    }
}
