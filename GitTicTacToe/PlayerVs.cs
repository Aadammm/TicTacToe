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
            Board gameBoard = new Board();
            int row = 0, column = 0;
            string[] Players = { "Nobody", "Player with X", "Player with O" };
            int actualPlayer = 1;
            int freeCell = 0;
            Random random = new Random();
            /// <summary> 
            /// Zadanie súradníc pre hráčov
            /// </summary>
            private void Move()
            {
                Console.Write("X: ");
                while (!int.TryParse(Console.ReadLine(), out row))
                    Console.Write("Enter a whole number: ");
                Console.Write("Y: ");
                while (!int.TryParse(Console.ReadLine(), out column))
                    Console.Write("Enter a whole number: ");
                row -= 1; column -= 1;
                if (row >= Board.GameBoard.GetLength(0) || column >= Board.GameBoard.GetLength(1) || row < 0 || column < 0)
                {
                    Console.WriteLine("Position is outside the playing field, please enter again\n");
                    Move();
                }
                else if (Board.GameBoard[row, column] != freeCell)
                {
                    Console.WriteLine("This position is not a free, please again \n");
                    Move();
                }

            }
            /// <summary>
            /// vloží symbol na pozíciu v hracej ploche
            /// </summary>
            private void InsertSymbol()
            {
                gameBoard[row, column] = actualPlayer;
            }
            /// <summary>
            /// Hráč vs Hráč
            /// </summary>
            public void PlayerVsPlayer()
            {
                actualPlayer = (actualPlayer == 1) ? 2 : 1;
                Console.WriteLine("{0} Turn:", Players[actualPlayer]);
                Move();
                InsertSymbol();

            }
            /// <summary>
            /// Hráč vs Pc easy obtiažnosť
            /// </summary>
            public void Easy()
            {
                if (actualPlayer != 2)
                {
                    Move();
                    InsertSymbol();
                    actualPlayer = 2;
                }
                else
                {
                    while (true)
                    {
                        row = random.Next(0, 3);
                        column = random.Next(0, 3);
                        if (gameBoard[row, column] == freeCell)
                        {
                            InsertSymbol();
                            actualPlayer = 1;
                            break;
                        }
                    }
                }

            }
            /// <summary>
            /// hráč vs pc medium obtiažnosť
            /// </summary>
            public void Medium()
            {
                bool symbolPut = false;
                if (actualPlayer != 2)
                {
                    Move();
                    InsertSymbol();
                    actualPlayer = 2;
                }
                else
                {

                    if (MediumDiagonalInsertSymbol() || MediumRowColumnInsertSymbol() || MediumSymbolNextSymbol())
                        symbolPut = true;

                    //ak nebol doplneni znak doplni random
                    while (!symbolPut)
                    {
                        row = random.Next(0, 3);
                        column = random.Next(0, 3);

                        if (gameBoard[row, column] == freeCell)
                        {
                            InsertSymbol();
                            symbolPut = true;
                        }
                    }
                    actualPlayer = 1;
                }
            }
            /// <summary>
            /// Hráč vs pc hard obtiažnosť
            /// </summary>
            public void Hard()
            {
                if (actualPlayer != 1)
                {
                    actualPlayer = 1;
                    Move();
                    InsertSymbol();

                }
                else
                {
                    actualPlayer = 2;
                    FindBestMove(Board.GameBoard);
                    InsertSymbol();

                }

            }
            /// <summary>
            /// hľadá 2 rovnake znaky pre Pc a vráti true ak nájde
            /// </summary>
            /// <returns></returns>
            private bool MediumDiagonalInsertSymbol()
            {
                int diagonalSymbol = 0;
                //diagonalne vlozenie znaku do stredu
                if ((gameBoard[0, 0] == actualPlayer && gameBoard[2, 2] == actualPlayer && gameBoard[1, 1] == freeCell)
                    || (gameBoard[2, 0] == actualPlayer && gameBoard[0, 2] == actualPlayer && gameBoard[1, 1] == freeCell))
                {
                    gameBoard[1, 1] = actualPlayer;
                    return true;
                }
                //z lava do prava diagonalne dva znaky prida 3
                for (int i = 0; i < 3; i++)
                {
                    if (gameBoard[i, i] == actualPlayer)
                        diagonalSymbol++;
                }
                if (diagonalSymbol == 2 && gameBoard[2, 2] == freeCell)
                {
                    gameBoard[2, 2] = actualPlayer;
                    return true;
                }
                if (diagonalSymbol == 2 && gameBoard[0, 0] == freeCell)
                {
                    gameBoard[0, 0] = actualPlayer;
                    return true;
                }
                //z prava do lava diagonalne dva znaky prida 3
                diagonalSymbol = 0;
                for (int i = 0; i < 3; i++)
                {
                    if (gameBoard[2 - i, i] == actualPlayer)
                        diagonalSymbol++;
                    if (diagonalSymbol == 2 && gameBoard[0, 2] == freeCell)
                    {
                        gameBoard[0, 2] = actualPlayer;
                        return true;
                    }
                    if (diagonalSymbol == 2 && gameBoard[2, 0] == freeCell)
                    {
                        gameBoard[2, 0] = actualPlayer;
                        return true;
                    }
                }
                return false;

            }
            /// <summary>
            ///  hľadá 2 rovnake znaky pre Pc a vráti true ak nájde
            /// </summary>
            /// <returns></returns>
            private bool MediumRowColumnInsertSymbol()
            {
                int symbolsInRow = 0;
                int symbolsInColumn = 0;
                for (int i = 0; i < 3; i++)
                {
                    symbolsInRow = 0;
                    symbolsInColumn = 0;
                    //vlozenie znaku do stredu na riadkoch
                    if (gameBoard[0, i] == actualPlayer && gameBoard[2, i] == actualPlayer && gameBoard[1, i] == freeCell)
                    {
                        gameBoard[1, i] = actualPlayer;
                        return true;
                    }
                    //vlozenie znaku do stredu v stlpcoch
                    if (gameBoard[i, 0] == actualPlayer && gameBoard[i, 2] == actualPlayer && gameBoard[i, 1] == freeCell)
                    {
                        gameBoard[i, 1] = actualPlayer;//alalla
                        return true;
                    }
                    //hlada 2 znaky vedla seba a priradi treti v riadku
                    for (int j = 0; j < 3; j++)
                    {
                        if ((gameBoard[j, i]) == 2)
                            symbolsInRow++;

                        //ak su dva znaky za sebou v riadku prida treti
                        if (symbolsInRow == 2 && j != 2 && gameBoard[j + 1, i] == freeCell)
                        {
                            gameBoard[j + 1, i] = actualPlayer;
                            return true;
                        }
                        //ak su dva znaky zo zadu za sebou prida treti na zaciatok riadku
                        if (symbolsInRow == 2 && j == 2 && gameBoard[j - 2, i] == freeCell)
                        {
                            gameBoard[j - 2, i] = actualPlayer;
                            return true;
                        }
                    }
                    //hlada dva znaky vedla seba a priradi treti v stlpci
                    for (int j = 0; j < 3; j++)
                    {
                        if ((gameBoard[i, j]) == 2)
                            symbolsInColumn++;
                        //ak su dva znaky za sebou v stlpci prida treti 
                        if (symbolsInColumn == 2 && j != 2 && gameBoard[i, j + 1] == freeCell)
                        {
                            gameBoard[i, j + 1] = actualPlayer;
                            return true;
                        }

                        //ak su dva znaky zo spodu za sebou v stlpci prida treti na zaciatok
                        if (symbolsInColumn == 2 && j == 2 && gameBoard[i, j - 2] == freeCell)
                        {
                            gameBoard[i, j - 2] = actualPlayer;
                            return true;
                        }
                    }
                }
                return false;
            }
            /// <summary>
            /// hľadá znak a vedľa neho vloží aby nebol random
            /// </summary>
            /// <returns></returns>
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
                            //položí znak v pravo
                            if (symbolsInRow + 1 < 3 && gameBoard[symbolsInRow + 1, symbolsInColumn] == freeCell)
                            {
                                gameBoard[symbolsInRow + 1, symbolsInColumn] = actualPlayer;
                                return true;
                            }
                            //položí znak v ľavo
                            if (symbolsInRow - 1 >= 0 && gameBoard[symbolsInRow - 1, symbolsInColumn] == freeCell)
                            {
                                gameBoard[symbolsInRow - 1, symbolsInColumn] = actualPlayer;
                                return true;
                            }
                            //položí znak hore 
                            if (symbolsInColumn + 1 < 3 && gameBoard[symbolsInRow, symbolsInColumn + 1] == freeCell)
                            {
                                gameBoard[symbolsInRow, symbolsInColumn + 1] = actualPlayer;
                                return true;
                            }
                            //položí znak dole
                            if (symbolsInColumn - 1 >= 0 && gameBoard[symbolsInRow, symbolsInColumn - 1] == freeCell)
                            {
                                gameBoard[symbolsInRow, symbolsInColumn - 1] = actualPlayer;
                                return true;
                            }
                            //položí znak diagonalne v ľavo hore
                            if (symbolsInRow - 1 >= 0 && symbolsInColumn - 1 >= 0 && gameBoard[symbolsInRow - 1, symbolsInColumn - 1] == freeCell)
                            {
                                gameBoard[symbolsInRow - 1, symbolsInColumn - 1] = actualPlayer;
                                return true;
                            }
                            //položí znak diagonalne v ľavo dole
                            if (symbolsInRow - 1 >= 0 && symbolsInColumn + 1 >= 0 && gameBoard[symbolsInRow - 1, symbolsInColumn + 1] == freeCell)
                            {
                                gameBoard[symbolsInRow - 1, symbolsInColumn + 1] = actualPlayer;
                                return true;
                            }
                            //položí znak diagononalne v pravo dolu
                            if (symbolsInRow + 1 < 3 && symbolsInColumn + 1 < 3 && gameBoard[symbolsInRow + 1, symbolsInColumn + 1] == freeCell)
                            {
                                gameBoard[symbolsInRow + 1, symbolsInColumn + 1] = actualPlayer;
                                return true;
                            }
                            //položí znak diagononalne v pravo hore
                            if (symbolsInRow + 1 < 3 && symbolsInColumn - 1 < 3 && gameBoard[symbolsInRow + 1, symbolsInColumn - 1] == freeCell)
                            {
                                gameBoard[symbolsInRow + 1, symbolsInColumn - 1] = actualPlayer;
                                return true;
                            }
                        }
                    }
                }
                return false;

            }
            /// <summary>
            /// algoritmus minimax-hľadá najlepšiu alternatívu pre výhru
            /// </summary>
            /// <param name="board"></param>
            /// <param name="isMax"></param>
            /// <returns></returns>
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
                            if (board[i, j] == freeCell)
                            {
                                board[i, j] = actualPlayer;
                                bestMove = Math.Max(bestMove, Minimax(board, false));
                                board[i, j] = freeCell;
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
                            if (board[i, j] == freeCell)
                            {
                                board[i, j] = 1;
                                bestMove = Math.Min(bestMove, Minimax(board, true));

                                board[i, j] = freeCell;
                            }
                        }
                    }
                    return bestMove;
                }
            }
            /// <summary>
            /// našiel najlepší pozíciu pre vloženie znaku
            /// </summary>
            /// <param name="board"></param>
            private void FindBestMove(int[,] board)
            {

                int bestValue = -10;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (gameBoard[i, j] == freeCell)
                        {
                            gameBoard[i, j] = actualPlayer;
                            int move = Minimax(board, false);
                            gameBoard[i, j] = freeCell;
                            if (move > bestValue)
                            {
                                row = i;
                                column = j;
                                bestValue = move;
                            }
                        }
                    }
                }



            }
            /// <summary>
            /// kontrola hracej plochy počas fungovania metody Minimax
            /// </summary>
            /// <returns></returns>
            private int CheckWinner()
            {
                Check.Checking();
                if (Check.Winner == Winner.WinX)
                {
                    Check.Winner = Winner.NoBody;
                    return -10;
                }
                if (Check.Winner == Winner.WinO)
                {
                    Check.Winner = Winner.NoBody;
                    return 10;
                }
                Check.Winner = Winner.NoBody;
                return 0;

            }

        }
    }

