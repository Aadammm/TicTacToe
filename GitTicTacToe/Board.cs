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
        string[] playerSign = { " ", "X", "O" };
        public int this[int row, int column]
        {
            get { return GameBoard[row, column]; }
            set { GameBoard[row, column] = value; }
        }

        /// <summary>
        /// vypis hracej plochy
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string text = "  1 2 3\n";
            for (int i = 1; i < GameBoard.GetLength(0) + 1; i++)
            {
                text += i + "|";
                for (int j = 0; j < GameBoard.GetLength(1); j++)
                {
                    text += playerSign[GameBoard[j, i - 1]] + "|";
                }
                text += "\n" + "";

            }
            return text;
        }
        /// <summary>
        /// skontroluje ci je volne policko
        /// </summary>
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
