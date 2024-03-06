using System;
using System.Data.Common;

internal class Program
{
    private static void Main(string[] args)
    {
        char[,] board = new char[6, 7];
        char currentPlayer = 'X';

        bool keepPlaying = true;

        while (keepPlaying)
        {

            InitializeBoard();
            bool gameOver = false;

            PrintBoard();

            while (!gameOver)
            {
                int column = GetColumn();
                PlaceToken(column);
                if (CheckWin())
                {
                    PrintBoard();
                    Console.WriteLine($"Player {currentPlayer} wins!");
                    gameOver = true;
                }
                else if (IsBoardFull())
                {
                    PrintBoard();
                    Console.WriteLine("It's a draw!");
                    gameOver = true;
                }
                else
                {
                    PrintBoard();
                    SwitchPlayer();
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

        void InitializeBoard()
        {
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    board[row, col] = ' ';
                }
            }
        }

        void PrintBoard()
        {
            Console.Clear();
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    Console.Write(board[row, col] + "|");
                }
                Console.WriteLine();
            }
            Console.WriteLine("---------------");
            Console.WriteLine("0 1 2 3 4 5 6");
        }

        int GetColumn()
        {
            int column;
            while (true)
            {
                Console.Write($"Player {currentPlayer}, enter column number (0-6): ");
                if (int.TryParse(Console.ReadLine(), out column) && column >= 0 && column < 7 && board[0, column] == ' ')
                    break;
                Console.WriteLine("Invalid input, try again.");
            }
            return column;
        }

        void PlaceToken(int column)
        {
            for (int row = 5; row >= 0; row--)
            {
                if (board[row, column] == ' ')
                {
                    board[row, column] = currentPlayer;
                    break;
                }
            }
        }

        bool CheckWin()
        {
            // Check horizontal
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (board[row, col] == currentPlayer && board[row, col + 1] == currentPlayer &&
                        board[row, col + 2] == currentPlayer && board[row, col + 3] == currentPlayer)
                        return true;
                }
            }

            // Check vertical
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    if (board[row, col] == currentPlayer && board[row + 1, col] == currentPlayer &&
                        board[row + 2, col] == currentPlayer && board[row + 3, col] == currentPlayer)
                        return true;
                }
            }

            // Check diagonal (positive slope)
            for (int row = 3; row < 6; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (board[row, col] == currentPlayer && board[row - 1, col + 1] == currentPlayer &&
                        board[row - 2, col + 2] == currentPlayer && board[row - 3, col + 3] == currentPlayer)
                        return true;
                }
            }

            // Check diagonal (negative slope)
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (board[row, col] == currentPlayer && board[row + 1, col + 1] == currentPlayer &&
                        board[row + 2, col + 2] == currentPlayer && board[row + 3, col + 3] == currentPlayer)
                        return true;
                }
            }

            return false;
        }

        bool IsBoardFull()
        {
            for (int col = 0; col < 7; col++)
            {
                if (board[0, col] == ' ')
                    return false;
            }
            return true;
        }

        void SwitchPlayer()
        {
            currentPlayer = currentPlayer == 'X' ? 'O' : 'X';
        }
    }
}