using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoughtsAndCrosses;

namespace Tests {

	[TestClass]
	public class TestAI {

		static int DIM = 3;
		Board board    = new Board(DIM);
		Random random  = new Random();
		AI ai          = new AI();
	
		[TestMethod]
		public void TestPlaceMarkAndCheckWinner() {
			int[] tempPos  = Expected();
			bool tempFirst = true;
			bool expected  = true;

			bool winner = ai.PlaceMarkAndCheckWinner(board, tempPos, tempFirst);

			Assert.AreEqual(expected, winner, " \n--- Expected: " + expected + ". Actual: winner = " + winner);

		}

		[TestMethod]
		public void TestHeuristicsMM() {
			bool winner    = true;
			bool tempFirst = true;
			int expected   = 10;

			ai.SetFirst(true);
			int tempScore  = ai.Heuristics(winner, tempFirst);

			Assert.AreEqual(expected, tempScore, " \n--- Expected: " + expected + ". Actual: tempScore = " + tempScore);
		}

		[TestMethod]
		public void TestHeuristicsNM() {
			bool winner   = false;
			int expected  = 0;

			int tempScore = ai.Heuristics(winner);

			Assert.AreEqual(expected, tempScore, " \n--- Expected: " + expected + ". Actual: tempScore = " + tempScore);
		}

		private int[] Expected() {
			string[,] s = new string[DIM, DIM];
			if (DIM == 3) {
				s = new string[,] { {"O", "X", "O" },
				                    { "", "X", "X" },
				                    { "", "O",  "" } };
			} 
			
			else if (DIM == 4) {
				s = new string[,] { {"O", "X", "O", "X" }, 
				                    { "", "X", "X", "X" }, 
				                    {"O", "O",  "", "O" },
				                    {"O", "X", "O", "X" } };
			}

			board = new Board(DIM);
			board.SetBoard(s);
			int[] expected = new[] { 1, 0 };

			return expected;
		}

	}
}
