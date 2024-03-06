using System;

namespace Connect4
{
    public class GameState
    {
        public char[,] Board { get; set; }
        public char CurrentPlayer { get; set; }

        public GameState(char[,] board, char currentPlayer)
        {
            Board = board;
            CurrentPlayer = currentPlayer;
        }

        public List<GameState> GetMoveList()
        {
            List<GameState> moveList = new List<GameState>();

            foreach (int col in GetPossibleMoves())
            {
                char[,] newBoard = (char[,])Board.Clone();
                PlaceToken(newBoard, col);
                char nextPlayer = (CurrentPlayer == 'X') ? 'O' : 'X';
                moveList.Add(new GameState(newBoard, nextPlayer));
            }

            return moveList;
        }

        public bool CheckDraw()
        {
            for (int col = 0; col < 7; col++)
            {
                if (Board[0, col] == ' ')
                    return false;
            }
            return true;
        }

        public bool CheckWin()
        {
            // Check horizontal
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (Board[row, col] == CurrentPlayer && Board[row, col + 1] == CurrentPlayer &&
                        Board[row, col + 2] == CurrentPlayer && Board[row, col + 3] == CurrentPlayer)
                        return true;
                }
            }

            // Check vertical
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    if (Board[row, col] == CurrentPlayer && Board[row + 1, col] == CurrentPlayer &&
                        Board[row + 2, col] == CurrentPlayer && Board[row + 3, col] == CurrentPlayer)
                        return true;
                }
            }

            // Check diagonal (positive slope)
            for (int row = 3; row < 6; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (Board[row, col] == CurrentPlayer && Board[row - 1, col + 1] == CurrentPlayer &&
                        Board[row - 2, col + 2] == CurrentPlayer && Board[row - 3, col + 3] == CurrentPlayer)
                        return true;
                }
            }

            // Check diagonal (negative slope)
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (Board[row, col] == CurrentPlayer && Board[row + 1, col + 1] == CurrentPlayer &&
                        Board[row + 2, col + 2] == CurrentPlayer && Board[row + 3, col + 3] == CurrentPlayer)
                        return true;
                }
            }

            return false;
        }

        public void PrintBoard()
        {

            Console.Clear();
            Console.WriteLine("|0|1|2|3|4|5|6|");
            Console.WriteLine("_______________");
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    Console.Write("|" + Board[row, col]);
                }
                Console.WriteLine("|");
            }
            Console.WriteLine("===============");
        }

        private void PlaceToken(char[,] board, int column)
        {
            for (int row = 5; row >= 0; row--)
            {
                if (board[row, column] == ' ')
                {
                    board[row, column] = CurrentPlayer;
                    break;
                }
            }
        }

        private List<int> GetPossibleMoves()
        {
            List<int> possibleMoves = new List<int>();

            for (int col = 0; col < 7; col++)
            {
                if (Board[0, col] == ' ') // Check if column is not full
                {
                    possibleMoves.Add(col);
                }
            }

            return possibleMoves;
        }
    }
}