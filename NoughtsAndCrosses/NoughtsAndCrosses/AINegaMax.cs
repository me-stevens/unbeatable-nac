using System.Collections.Generic;

namespace NoughtsAndCrosses {

	public class AINegaMax : AI {

		public int[] CalculateCell(Board board, bool player) {
			DIM = board.GetDIM();

			int[] result = Algorithm(board, player);
			return new[] {result[0], result[1]};
		}

		public int[] Algorithm(Board board, bool tempFirst) {
			int[] pos = new int[2] {-1, -1};
			int score = -100;

			List<int[]> empties = board.GetEmpties();

			for (int i=0; i<empties.Count; i++) {
				int[] tempPos = empties[i];
				bool winner   = PlaceMarkAndCheckWinner(board, tempPos, tempFirst);

				int tempScore = 0;
				if (!winner && tempBoard.GetEmpties().Count > 0) {
					int[] tempResult = Algorithm(tempBoard, !tempFirst);
					tempScore        = -tempResult[2];
				}

				else
					tempScore = Heuristics(winner);

				if (tempScore > score) {
					pos   = tempPos;
					score = tempScore;
				}
			}

			return new[] {pos[0], pos[1], score};
		}

	}
}
