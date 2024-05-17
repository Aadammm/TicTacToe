using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GitTicTacToe
{
    internal class PlayerVs
    {
        readonly Board gameBoard;
        readonly string[] _Players = { "Nobody", "Player with X", "Player with O" };
        int _row = 0, _column = 0;
        int _actualPlayer = 1;
        int _freeCell = 0;
        Random _random;

        public PlayerVs()
        {
            gameBoard = new Board();
            _random = new Random();
        }

        private void Move()
        {
            Console.Write("X: ");
            while (!int.TryParse(Console.ReadLine(), out _row))
                Console.Write("Enter a whole number: ");
            Console.Write("Y: ");
            while (!int.TryParse(Console.ReadLine(), out _column))
                Console.Write("Enter a whole number: ");
            _row -= 1; _column -= 1;
            if (_row >= Board.GameBoard.GetLength(0) || _column >= Board.GameBoard.GetLength(1) || _row < 0 || _column < 0)
            {
                Console.WriteLine("position is invalid, please enter again\n");
                Move();
            }
            else if (Board.GameBoard[_row, _column] != _freeCell)
            {
                Console.WriteLine("This position is not a free, please again \n");
                Move();
            }

        }
        private void InsertSymbolIntoBoard()
        {
            gameBoard[_row, _column] = _actualPlayer;
        }
        public void PlayerVsPlayer()
        {
            _actualPlayer = (_actualPlayer == 1) ? 2 : 1;
            Console.WriteLine("{0} Turn:", _Players[_actualPlayer]);
            Move();
            InsertSymbolIntoBoard();

        }
        public void Easy()
        {
            if (_actualPlayer != 2)
            {
                Move();
                InsertSymbolIntoBoard();
                _actualPlayer = 2;
            }
            else
            {
                while (true)
                {
                    _row = _random.Next(0, 3);
                    _column = _random.Next(0, 3);
                    if (gameBoard[_row, _column] == _freeCell)
                    {
                        InsertSymbolIntoBoard();
                        _actualPlayer = 1;
                        break;
                    }
                }
            }

        }
        public void Medium()
        {
            bool symbolPut = false;
            if (_actualPlayer != 2)
            {
                Move();
                InsertSymbolIntoBoard();
                _actualPlayer = 2;
            }
            else
            {

                if (MediumDiagonalInsertSymbol() || MediumRowColumnInsertSymbol() || MediumSymbolNextSymbol())
                    symbolPut = true;

                //ak nebol doplneni znak doplni random
                while (!symbolPut)
                {
                    _row = _random.Next(0, 3);
                    _column = _random.Next(0, 3);

                    if (gameBoard[_row, _column] == _freeCell)
                    {
                        InsertSymbolIntoBoard();
                        symbolPut = true;
                    }
                }
                _actualPlayer = 1;
            }
        }
        public void Hard()
        {
            if (_actualPlayer != 1)
            {
                _actualPlayer = 1;
                Move();
                InsertSymbolIntoBoard();

            }
            else
            {
                _actualPlayer = 2;
                FindBestMove(Board.GameBoard);
                InsertSymbolIntoBoard();

            }

        }
        private bool MediumDiagonalInsertSymbol()
        {
            int diagonalSymbol = 0;
            if ((gameBoard[0, 0] == _actualPlayer && gameBoard[2, 2] == _actualPlayer && gameBoard[1, 1] == _freeCell)
                || (gameBoard[2, 0] == _actualPlayer && gameBoard[0, 2] == _actualPlayer && gameBoard[1, 1] == _freeCell))
            {
                gameBoard[1, 1] = _actualPlayer;
                return true;
            }
            for (int i = 0; i < 3; i++)
            {
                if (gameBoard[i, i] == _actualPlayer)
                    diagonalSymbol++;
            }
            if (diagonalSymbol == 2 && gameBoard[2, 2] == _freeCell)
            {
                gameBoard[2, 2] = _actualPlayer;
                return true;
            }
            if (diagonalSymbol == 2 && gameBoard[0, 0] == _freeCell)
            {
                gameBoard[0, 0] = _actualPlayer;
                return true;
            }
            diagonalSymbol = 0;
            for (int i = 0; i < 3; i++)
            {
                if (gameBoard[2 - i, i] == _actualPlayer)
                    diagonalSymbol++;
                if (diagonalSymbol == 2 && gameBoard[0, 2] == _freeCell)
                {
                    gameBoard[0, 2] = _actualPlayer;
                    return true;
                }
                if (diagonalSymbol == 2 && gameBoard[2, 0] == _freeCell)
                {
                    gameBoard[2, 0] = _actualPlayer;
                    return true;
                }
            }
            return false;

        }
        private bool MediumRowColumnInsertSymbol()
        {
            int symbolsInRow = 0;
            int symbolsInColumn = 0;
            for (int i = 0; i < 3; i++)
            {
                symbolsInRow = 0;
                symbolsInColumn = 0;
                if (gameBoard[0, i] == _actualPlayer && gameBoard[2, i] == _actualPlayer && gameBoard[1, i] == _freeCell)
                {
                    gameBoard[1, i] = _actualPlayer;
                    return true;
                }
                if (gameBoard[i, 0] == _actualPlayer && gameBoard[i, 2] == _actualPlayer && gameBoard[i, 1] == _freeCell)
                {
                    gameBoard[i, 1] = _actualPlayer;
                    return true;
                }
                for (int j = 0; j < 3; j++)
                {
                    if ((gameBoard[j, i]) == 2)
                        symbolsInRow++;

                    if (symbolsInRow == 2 && j != 2 && gameBoard[j + 1, i] == _freeCell)
                    {
                        gameBoard[j + 1, i] = _actualPlayer;
                        return true;
                    }
                    if (symbolsInRow == 2 && j == 2 && gameBoard[j - 2, i] == _freeCell)
                    {
                        gameBoard[j - 2, i] = _actualPlayer;
                        return true;
                    }
                }
                for (int j = 0; j < 3; j++)
                {
                    if ((gameBoard[i, j]) == 2)
                        symbolsInColumn++;
                    if (symbolsInColumn == 2 && j != 2 && gameBoard[i, j + 1] == _freeCell)
                    {
                        gameBoard[i, j + 1] = _actualPlayer;
                        return true;
                    }
                    if (symbolsInColumn == 2 && j == 2 && gameBoard[i, j - 2] == _freeCell)
                    {
                        gameBoard[i, j - 2] = _actualPlayer;
                        return true;
                    }
                }
            }
            return false;
        }
        private bool MediumSymbolNextSymbol()
        {
            int symbolsInRow = 0;
            int symbolsInColumn = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (gameBoard[j, i] == 2)
                    {
                        symbolsInRow = j;
                        symbolsInColumn = i;
                        if (symbolsInRow + 1 < 3 && gameBoard[symbolsInRow + 1, symbolsInColumn] == _freeCell)
                        {
                            gameBoard[symbolsInRow + 1, symbolsInColumn] = _actualPlayer;
                            return true;
                        }
                        if (symbolsInRow - 1 >= 0 && gameBoard[symbolsInRow - 1, symbolsInColumn] == _freeCell)
                        {
                            gameBoard[symbolsInRow - 1, symbolsInColumn] = _actualPlayer;
                            return true;
                        }
                        if (symbolsInColumn + 1 < 3 && gameBoard[symbolsInRow, symbolsInColumn + 1] == _freeCell)
                        {
                            gameBoard[symbolsInRow, symbolsInColumn + 1] = _actualPlayer;
                            return true;
                        }
                        if (symbolsInColumn - 1 >= 0 && gameBoard[symbolsInRow, symbolsInColumn - 1] == _freeCell)
                        {
                            gameBoard[symbolsInRow, symbolsInColumn - 1] = _actualPlayer;
                            return true;
                        }
                        if (symbolsInRow - 1 >= 0 && symbolsInColumn - 1 >= 0 && gameBoard[symbolsInRow - 1, symbolsInColumn - 1] == _freeCell)
                        {
                            gameBoard[symbolsInRow - 1, symbolsInColumn - 1] = _actualPlayer;
                            return true;
                        }
                        if (symbolsInRow - 1 >= 0 && symbolsInColumn + 1 <3 && gameBoard[symbolsInRow - 1, symbolsInColumn + 1] == _freeCell)
                        {
                            gameBoard[symbolsInRow - 1, symbolsInColumn + 1] = _actualPlayer;
                            return true;
                        }
                        if (symbolsInRow + 1 < 3 && symbolsInColumn + 1 < 3 && gameBoard[symbolsInRow + 1, symbolsInColumn + 1] == _freeCell)
                        {
                            gameBoard[symbolsInRow + 1, symbolsInColumn + 1] = _actualPlayer;
                            return true;
                        }
                        if (symbolsInRow + 1 < 3 && symbolsInColumn - 1 < 3 && gameBoard[symbolsInRow + 1, symbolsInColumn - 1] == _freeCell)
                        {
                            gameBoard[symbolsInRow + 1, symbolsInColumn - 1] = _actualPlayer;
                            return true;
                        }
                    }
                }
            }
            return false;

        }
        private int Minimax(int[,] board, bool isMax)
        {
            int score = CheckWinner();
            if (score == 10)
                return 10;
            if (score == -10)
                return -10;
            if (!Board.FreePlace())
            {
                return 0;
            }
            if (isMax)
            {
                int bestMove = -10;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (board[i, j] == _freeCell)
                        {
                            board[i, j] = _actualPlayer;
                            bestMove = Math.Max(bestMove, Minimax(board, false));
                            board[i, j] = _freeCell;
                        }
                    }
                }
                return bestMove;
            }
            else
            {
                int bestMove = 10;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (board[i, j] == _freeCell)
                        {
                            board[i, j] = 1;
                            bestMove = Math.Min(bestMove, Minimax(board, true));

                            board[i, j] = _freeCell;
                        }
                    }
                }
                return bestMove;
            }
        }
        private void FindBestMove(int[,] board)
        {

            int bestValue = -10;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (gameBoard[i, j] == _freeCell)
                    {
                        gameBoard[i, j] = _actualPlayer;
                        int move = Minimax(board, false);
                        gameBoard[i, j] = _freeCell;
                        if (move > bestValue)
                        {
                            _row = i;
                            _column = j;
                            bestValue = move;
                        }
                    }
                }
            }



        }
        private int CheckWinner()
        {
            Check.Checking();
            if (Check.Winner == WhoWin.WinX)
            {
                Check.ChangeStatusToNobody();
                return -10;
            }
            if (Check.Winner == WhoWin.WinO)
            {
                Check.ChangeStatusToNobody();
                return 10;
            }
            Check.ChangeStatusToNobody();
            return 0;

        }

    }
}

