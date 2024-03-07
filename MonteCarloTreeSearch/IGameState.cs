namespace MonteCarloTreeSearch
{
    public interface IGameState
    {
        public List<IGameState> GetMoveList();
        public bool CheckDraw();
        public bool CheckWin();
        public double GetResult();
    }
}
