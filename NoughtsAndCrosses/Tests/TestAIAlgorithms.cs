using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoughtsAndCrosses;

namespace Tests {

	[TestClass]
	public class TestAIAlgorithms {

		static int DIM = 4;
		Board  board   = new Board(DIM);
		Random random  = new Random();

		[TestMethod]
		public void TestAIMiniMax() {
			int[] expected = Expected();

			AIMiniMax ai = new AIMiniMax();
			int[] pos = ai.CalculateCell(board, false);

			CollectionAssert.AreEqual(expected, pos);
		}

		[TestMethod]
		public void TestAINegaMax() {
			int[] expected = Expected();

			AINegaMax ai = new AINegaMax();
			int[] pos = ai.CalculateCell(board, false);

			CollectionAssert.AreEqual(expected, pos);
		}

		[TestMethod]
		public void TestAIMiniMaxAlphaBeta() {
			int[] expected = Expected();

			AIMiniMaxAlphaBeta ai = new AIMiniMaxAlphaBeta();
			int[] pos = ai.CalculateCell(board, false);

			CollectionAssert.AreEqual(expected, pos);
		}

		[TestMethod]
		public void TestAINegaMaxAlphaBeta() {
			int[] expected = Expected();

			AINegaMaxAlphaBeta ai = new AINegaMaxAlphaBeta();
			int[] pos = ai.CalculateCell(board, false);

			CollectionAssert.AreEqual(expected, pos);
		}


		private int[] Expected() {
			string[,] s = new string[DIM, DIM];
			if (DIM == 3) {
				s = new string[,] { {"O", "X", "" },
				                    { "", "X", "X"},
				                    { "", "O", "" } };
			} 
			
			else if (DIM == 4) {
				s = new string[,] { {"O", "X", "O", "X" }, 
				                    { "", "X", "X", "X" }, 
				                    { "", "O",  "", "O" },
				                    {"O", "X", "O", "X" } };
			}

			board = new Board(DIM);
			board.SetBoard(s);
			int[] expected = new[] { 1, 0 };

			return expected;
		}
	}
}

