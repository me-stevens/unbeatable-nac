using System.Collections.Generic;
using System.Linq;

namespace NoughtsAndCrosses {

	public class AINegaMaxAlphaBeta : AI {

		public int[] CalculateCell(Board board, bool player) {
			DIM   = board.GetDIM();

			int alpha = -10;
			int beta  =  10;
			int depth =   8;

			int[] result = Algorithm(board, player, alpha, beta, depth);
			return new[] {result[0], result[1]};
		}

		public int[] Algorithm(Board board, bool tempFirst, int alpha, int beta, int depth) {
			int[] pos = new int[2] {-1, -1};
			int score = -100;

			List<int[]> empties = board.GetEmpties();
			bool exit = false;

			for (int i=0; i<empties.Count && !exit; i++) {
				int[] tempPos = empties[i];
				bool winner   = PlaceMarkAndCheckWinner(board, tempPos, tempFirst);

				int tempScore = 0;
				if (!winner && tempBoard.GetEmpties().Count > 0 && depth > 0) {
					int[] tempResult = Algorithm(tempBoard, !tempFirst, -beta, -alpha, depth - 1);
					tempScore        = -tempResult[2];
				}

				else
					tempScore = Heuristics(winner);

				if (tempScore > score) {
					pos   = tempPos;
					score = tempScore;
				}

				if (tempScore > alpha)
					alpha = tempScore;
				
				if (alpha >= beta)
					exit = true;
			}

			return new[] {pos[0], pos[1], score};
		}


	}
}