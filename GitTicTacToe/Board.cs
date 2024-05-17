using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitTicTacToe
{
    internal class Board
    {
        public static readonly int[,] GameBoard = new int[3, 3];
        readonly string[] playerSign = { " ", "X", "O" };
        public int this[int row, int column]
        {
            get { return GameBoard[row, column]; }
            set { GameBoard[row, column] = value; }
        }

        public override string ToString()
        {
            StringBuilder text = new StringBuilder("  1 2 3\n");
            for (int i = 1; i < GameBoard.GetLength(0) + 1; i++)
            {
                text.Append(i + "|");
                for (int j = 0; j < GameBoard.GetLength(1); j++)
                {
                    text.Append(playerSign[GameBoard[j, i - 1]] + "|");
                }
                text.Append("\n" + "");

            }
            return text.ToString() ;
        }
        public static bool FreePlace()
        {
            foreach (int number in GameBoard)
            {
                if (number == 0)
                    return true;
            }
            return false;


        }
    }
}
