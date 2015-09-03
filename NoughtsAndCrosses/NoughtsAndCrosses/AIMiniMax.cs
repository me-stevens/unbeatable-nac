using System.Collections.Generic;
using System.Linq;

namespace NoughtsAndCrosses {

	public class AIMiniMax : AI {

		public int[] CalculateCell(Board board, bool player) {
			first = player;

			int[] result = Algorithm(board, first);
			return new[] { result[0], result[1] };
		}

		public int[] Algorithm(Board board, bool tempFirst) {
			int[] pos = new int[2] { -1, -1 };
			int score = (tempFirst == first) ? -100 : 100;

			List<int[]> empties = board.GetEmpties();

			for (int i=0; i<empties.Count; i++) {
				int[] tempPos = empties[i];
				bool winner   = PlaceMarkAndCheckWinner(board, tempPos, tempFirst);

				int tempScore = 0;
				if (!winner && tempBoard.GetEmpties().Count > 0) {
					int[] tempResult = Algorithm(tempBoard, !tempFirst);
					tempScore        = tempResult[2];
				}

				else
					tempScore = Heuristics(winner, tempFirst);

				if ((tempFirst == first && tempScore > score) ||
				    (tempFirst != first && tempScore < score)) {
					pos   = tempPos;
					score = tempScore;
				}
			}

			return new[] { pos[0], pos[1], score };
		}

	}
}
