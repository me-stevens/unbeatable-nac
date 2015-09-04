
namespace NoughtsAndCrosses {

	class AIMTDF : AI {
	
		public int[] CalculateCell(Board board, bool player) {
			first = player;

			// First guess should come from the move ordering
			// WORK IN PROGRESS
			int firstGuess = 0; 
			int alpha = -10;
			int beta  =  10;
			int depth =   8;

			int[] result = Algorithm(board, first, firstGuess, alpha, beta, depth);
			return new[] { result[0], result[1] };
		}

		public int[] Algorithm(Board board, bool first, int firstGuess, int alpha, int beta, int depth) {
			int[] pos      = new int[] { -1, -1 };
			int tempGuess  = firstGuess;
			int upperBound =  100;
			int lowerBound = -100;

			while (lowerBound < upperBound) {
				beta = tempGuess;

				if (tempGuess == lowerBound)
					beta++;

				AIMiniMaxAlphaBeta ai = new AIMiniMaxAlphaBeta();
				ai.SetFirst(first);
				int[] result = ai.Algorithm(board, first, beta - 1, beta, depth);
				pos[0]    = result[0];
				pos[1]    = result[1];
				tempGuess = result[2];

				if (tempGuess < beta)
					upperBound = tempGuess;
				else
					lowerBound = tempGuess;
			}

			return new int[] {pos[0], pos[1], tempGuess};
		}

	}
}
