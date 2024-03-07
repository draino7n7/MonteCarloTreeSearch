using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonteCarloTreeSearch
{
    internal class MCTSNode
    {
        public IGameState State { get; }
        public MCTSNode Parent { get; }
        public List<MCTSNode> Children { get; }
        public int Visits { get; private set; }
        public double Score { get; private set; }

        public MCTSNode(IGameState state, MCTSNode parent = null)
        {
            State = state;
            Parent = parent;
            Children = new List<MCTSNode>();
            Visits = 0;
            Score = 0;
        }

        public void UpdateScore(double value)
        {
            Score += value;
            Visits++;
        }
    }
}
