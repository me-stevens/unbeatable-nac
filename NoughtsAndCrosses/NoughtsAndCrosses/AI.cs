
namespace NoughtsAndCrosses {

	public class AI {

		protected Board tempBoard;
		protected bool first;

		public bool PlaceMarkAndCheckWinner(Board board, int[] tempPos, bool tempFirst) {
			int DIM = board.GetDIM();
			tempBoard = new Board(DIM);
			tempBoard.SetBoard(board.Copy());
			tempBoard.SetCell(tempPos, tempFirst);

			GameStats gameStats = new GameStats();
			return gameStats.IsWinner(tempBoard, tempFirst);
		}

		// MiniMax classes
		public int Heuristics(bool winner, bool tempFirst) {
			if (winner && tempFirst == first)
				return 10;

			if (winner && tempFirst != first)
				return -10;

			return 0;
		}

		// NegaMax classes
		public int Heuristics(bool winner) {
			if (winner)
				return 10;
			return 0;
		}

		public void SetFirst(bool value) {
			first = value;
		}

	}
}
