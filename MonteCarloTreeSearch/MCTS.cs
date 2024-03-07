using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonteCarloTreeSearch
{
    internal class MCTS
    {
        private int iterations;

        public MCTS(int iterations)
        {
            this.iterations = iterations;
        }

        public IGameState GetBestMove(IGameState initialState)
        {
            MCTSNode root = new MCTSNode(initialState);
            for (int i = 0; i < iterations; i++)
            {
                MCTSNode node = SelectNode(root);
                double score = Simulate(node.State);
                Backpropagate(node, score);
            }
            return GetMostVisitedChild(root).State;
        }

        private MCTSNode SelectNode(MCTSNode rootNode)
        {
            MCTSNode node = rootNode;
            while (node.Children.Any())
            {
                if (node.State.GetMoveList().Any())
                {
                    return Expand(node);
                }
                node = UCTSelect(node);
            }
            return node;
        }

        private MCTSNode Expand(MCTSNode node)
        {
            IGameState newState = node.State.GetMoveList().First(); // Change this line according to your GameState class
            MCTSNode newNode = new MCTSNode(newState, node);
            node.Children.Add(newNode);
            return newNode;
        }

        private MCTSNode UCTSelect(MCTSNode rootNode)
        {
            return rootNode.Children
                .OrderBy(child => UCTValue(child))
                .Last();
        }

        private double UCTValue(MCTSNode node)
        {
            double explorationFactor = Math.Sqrt(2);
            return (node.Score / node.Visits) + explorationFactor * Math.Sqrt(Math.Log(node.Parent.Visits) / node.Visits);
        }

        private double Simulate(IGameState state)
        {
            IGameState currentState = state; // Make a copy of the current state to simulate from

            while (!IsTerminalState(currentState))
            {
                List<IGameState> availableMoves = currentState.GetMoveList();
                if (availableMoves.Count == 0)
                {
                    // Game over, no more moves possible
                    break;
                }

                // Choose a random move
                Random random = new Random();
                int randomIndex = random.Next(0, availableMoves.Count);
                currentState = availableMoves[randomIndex];
            }

            // Evaluate the outcome
            return EvaluateOutcome(currentState);
        }

        private bool IsTerminalState(IGameState state)
        {
            return state.CheckWin() || state.CheckDraw();
        }

        private double EvaluateOutcome(IGameState state)
        {
            return state.GetResult();
        }

        private void Backpropagate(MCTSNode node, double score)
        {
            while (node != null)
            {
                node.UpdateScore(score);
                node = node.Parent;
            }
        }

        private MCTSNode GetMostVisitedChild(MCTSNode rootNode)
        {
            return rootNode.Children
                .OrderByDescending(child => child.Visits)
                .First();
        }
    }
}
