using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitTicTacToe
{
        public enum Winner { NoBody, Draw, WinX, WinO }
        internal static class Check
        {
            static int winX;
            static int winO;
            public static Winner Winner = Winner.NoBody;
            /// <summary>
            /// Kontrola hracej plochy
            /// </summary>
            public static void Checking()
            {
                if (Winner == Winner.NoBody)
                    Rows();
                if (Winner == Winner.NoBody)
                    Columns();
                if (Winner == Winner.NoBody)
                    Diagonal();
                if (!Board.FreePlace() && Winner == Winner.NoBody)
                    Winner = Winner.Draw;
            }
            /// <summary>
            /// skontroluje ci su 3 rovnake symboli vedla seba v riadku
            /// </summary>
            static void Rows()
            {
                for (int i = 0; i < 3 && Winner == Winner.NoBody; i++)
                {
                    winX = 0;
                    winO = 0;
                    for (int j = 0; j < 3 && Winner == Winner.NoBody; j++)
                    {
                        Win(j, i);
                    }
                }

            }
            /// <summary>
            /// skontroluje ci su 3 rovnake symboli vedla seba v stlpci
            /// </summary>
            static void Columns()
            {
                for (int i = 0; i < 3 && Winner == Winner.NoBody; i++)
                {
                    winX = 0;
                    winO = 0;
                    for (int j = 0; j < 3 && Winner == Winner.NoBody; j++)
                    {
                        Win(i, j);
                    }
                }
            }
            /// <summary>
            ///  skontroluje ci su 3 rovnake symboli diagonalne
            /// </summary>
            static void Diagonal()
            {
                winX = 0;
                winO = 0;
                for (int i = 0; i < 3 && Winner == Winner.NoBody; i++)
                {
                    Win(i, i);
                }
                winX = 0;
                winO = 0;
                for (int i = 0; i < 3 && Winner == Winner.NoBody; i++)
                {
                    Win(2 - i, i);
                }
            }

            /// <summary>
            /// Určuje výhercu
            /// </summary>
            /// <param name="i"></param>
            /// <param name="j"></param>
            static Winner Win(int i, int j)
            {
                winX = Board.GameBoard[i, j] == 1 ? winX + 1 : winX;
                winO = Board.GameBoard[i, j] == 2 ? winO + 1 : winO;
                if (winX == 3)
                {
                    return Winner = Winner.WinX;
                }
                if (winO == 3)
                {
                    return Winner = Winner.WinO;
                }
                return Winner;
            }
        }
    }
