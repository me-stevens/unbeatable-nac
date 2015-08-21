using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NoughtsAndCrosses {

	/**********************************************
	 * ARTIFICIAL INTELLIGENCE CLASS
	 *********************************************/
	class AI {

		Board miniMaxBoard;
		Player player;
		Player opponent;

		bool first;

		/**********************************************
		 * CONSTRUCTOR  - INITIALIZE PROPERTIES
		 *********************************************/
		public AI() {
			player  = new Player();
			opponent = new Player();
			first   = false;
		}

		/**********************************************
		 * CALCULATE OPTIMAL POSITION
		 *********************************************/
		public int[] CalculateCell(Board board, Player player, Player opponent) {
			// Copy the values, not the reference
			this.player   = player.Copy();
			this.opponent = opponent.Copy();

			// Here, "first" is not the first player,
			// but the player for which MiniMax is optimising
			first   = player.IsFirst;
			int DIM = board.GetDIM();

			// If first move, return random corner
			if (first && player.GetPlaced() == 0) {
				Random r = new Random();
				int i    = (r.Next(2) == 0) ? 0 : DIM - 1;
				int j    = (r.Next(2) == 0) ? 0 : DIM - 1;
				return new[] { i, j };
			}

			// Else, call MiniMax
			int[] pos  = MiniMax(board, this.player, this.opponent);

			return pos;
		}

		/**********************************************
		 * MINIMAX IMPLEMENTATION
		 *********************************************/
		public int[] MiniMax(Board board, Player player, Player opponent) {

			// Output variables: pos and score
			int[] pos = new int[2] { -1, -1 };
			int score = 0;
			

			bool tempFirst = player.IsFirst;

			// List of empty cells
			List<int[]> empties = board.GetEmpties();

			// Let's get the party started
			for (int i=0; i<empties.Count; i++) {

				// Placeholders for output variables
				int[] miniMaxPos = empties[i];
				int tempScore    = 0;

				int DIM      = board.GetDIM();
				miniMaxBoard = new Board(DIM);
				miniMaxBoard.SetBoard(board.Copy());
				miniMaxBoard.SetCell(miniMaxPos, player);

				bool winner = miniMaxBoard.IsWinner(player);

				// If player didn't close a line, check the next branch
				if (!winner && miniMaxBoard.GetEmpties().Count > 0) {
					int[] tempResult = MiniMax(miniMaxBoard, opponent, player);
					tempScore        = tempResult[2];
				} 
				
				// Optimising for the player. Swap the scores to get an "Unbeatable Loser"
				else if (!winner)
					tempScore =   0;

				else if (tempFirst == first)
					tempScore =  10;

				else if (tempFirst != first)
					tempScore = -10;

				// Update output variables
				int[] none = new int[2] { -1, -1 };
				if (pos.SequenceEqual(none)                  ||
				   (tempFirst == first && tempScore > score) ||
				   (tempFirst != first && tempScore < score)) {
					pos   = miniMaxPos;
					score = tempScore;
				}
			}

			return new[] { pos[0], pos[1], score };
		}

	}
}
