using Connect4;
using System;
using System.Data.Common;

namespace ConnectFour
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            bool keepPlaying = true;

            while (keepPlaying)
            {

                GameState gameState = InitializeGameState();
                bool gameOver = false;

                gameState.PrintBoard();

                while (!gameOver)
                {
                    int column = GetColumn(gameState);
                    PlaceToken(gameState, column);
                    if (gameState.CheckWin())
                    {
                        gameState.PrintBoard();
                        Console.WriteLine($"Player {gameState.CurrentPlayer} wins!");
                        gameOver = true;
                    }
                    else if (gameState.IsBoardFull())
                    {
                        gameState.PrintBoard();
                        Console.WriteLine("It's a draw!");
                        gameOver = true;
                    }
                    else
                    {
                        gameState.PrintBoard();
                        gameState = SwitchPlayer(gameState);
                    }

                }

                while (true)
                {
                    Console.WriteLine("\n\nPlay Again?\nY or N\n");
                    var answer = Console.ReadLine();
                    if (answer != null)
                    {
                        if (answer.ToUpper() == "Y")
                        {
                            keepPlaying = true;
                            break;
                        }
                        else if (answer.ToUpper() == "N")
                        {
                            keepPlaying = false;
                            break;
                        }
                    }
                    Console.WriteLine("Invalid input, try again.");
                }

            }

            int GetColumn(GameState gameState)
            {
                int column;
                while (true)
                {
                    Console.Write($"Player {gameState.CurrentPlayer}, enter column number (0-6): ");
                    if (int.TryParse(Console.ReadLine(), out column) && column >= 0 && column < 7 && gameState.Board[0, column] == ' ')
                        break;
                    Console.WriteLine("Invalid input, try again.");
                }
                return column;
            }

            GameState PlaceToken(GameState gameState, int column)
            {
                for (int row = 5; row >= 0; row--)
                {
                    if (gameState.Board[row, column] == ' ')
                    {
                        gameState.Board[row, column] = gameState.CurrentPlayer;
                        break;
                    }
                }
                return gameState;
            }

            GameState SwitchPlayer(GameState gameState)
            {
                gameState.CurrentPlayer = gameState.CurrentPlayer == 'X' ? 'O' : 'X';
                return gameState;
            }
        }

        static GameState InitializeGameState()
        {
            char[,] board = new char[6, 7];
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    board[row, col] = ' ';
                }
            }
            return new GameState(board, 'X');
        }

    }
}