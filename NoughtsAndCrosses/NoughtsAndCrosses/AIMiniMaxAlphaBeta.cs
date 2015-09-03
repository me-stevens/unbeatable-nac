using System.Collections.Generic;
using System.Linq;

namespace NoughtsAndCrosses {

	public class AIMiniMaxAlphaBeta : AI {

		public int[] CalculateCell(Board board, bool player) {
			first = player;

			int alpha = -10;
			int beta  =  10;
			int depth =   8;

			int[] result = Algorithm(board, first, alpha, beta, depth);
			return new[] { result[0], result[1] };
		}


		public int[] Algorithm(Board board, bool tempFirst, int alpha, int beta, int depth) {
			int[] pos = new int[2] { -1, -1 };
			int score = (tempFirst == first) ? -100 : 100;

			List<int[]> empties = board.GetEmpties();
			bool exit = false;

			for (int i=0; i<empties.Count && !exit; i++) {
				int[] tempPos = empties[i];
				bool winner   = PlaceMarkAndCheckWinner(board, tempPos, tempFirst);

				int tempScore = 0;
				if (!winner && tempBoard.GetEmpties().Count > 0 && depth > 0) {
					int[] tempResult = Algorithm(tempBoard, !tempFirst, alpha, beta, depth - 1);
					tempScore        = tempResult[2];
				} 
				
				else
					tempScore = Heuristics(winner, tempFirst);

				if ((tempFirst == first) ) {
					if(tempScore > score) {
						pos   = tempPos;
						score = tempScore;
					}

					if (score > alpha)
						alpha = score;
				} 
				
				else {
					if (tempScore < score) {
						pos   = tempPos;
						score = tempScore;
					}

					if (score < beta)
						beta  = score;
				} 

				if (alpha >= beta)
					exit = true;
			}

			return new[] { pos[0], pos[1], score };
		}

	}
}