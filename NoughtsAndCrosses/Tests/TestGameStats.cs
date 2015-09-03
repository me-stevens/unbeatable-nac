using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoughtsAndCrosses;

namespace Tests {

	[TestClass]
	public class TestGameStats {

		static int DIM      = 4;
		GameStats gameStats = new GameStats();
		Board board         = new Board(DIM);

		[TestMethod]
		public void TestUpdateWinnerInfo() {
			LineType expectedLine = LineType.DIA;
			int expectedIndex     = 2;
			int expectedWins      = gameStats.Wins + 1;

			gameStats.UpdateWinnerInfo(LineType.DIA, 2, true);
			
			Assert.AreEqual(expectedLine,  gameStats.WinnerLine,  " \n--- Expected: " + expectedLine  + ". Actual: gameStats.WinnerLine = "  + gameStats.WinnerLine);
			Assert.AreEqual(expectedIndex, gameStats.WinnerIndex, " \n--- Expected: " + expectedIndex + ". Actual: gameStats.WinnerIndex = " + gameStats.WinnerIndex);
			Assert.AreEqual(expectedWins,  gameStats.Wins,        " \n--- Expected: " + expectedWins  + ". Actual: gameStats.Wins = "        + gameStats.Wins);
		}

		[TestMethod]
		public void TestIsWinnerROW() {
			board.Reset();
			int row = 0;
			for (int j=0; j<DIM; j++)
				board.SetCell(row, j, true);
			bool expected = true;

			bool result = gameStats.IsWinner(board, true);

			Assert.AreEqual(expected, result, " \n--- Expected: " + expected + ". Actual: gameStats.IsWinner(board, true); = " + result);
		}

		[TestMethod]
		public void TestIsWinnerCOL() {
			board.Reset();
			int col = 0;
			for (int i=0; i<DIM; i++)
				board.SetCell(i, col, true);
			bool expected = true;

			bool result = gameStats.IsWinner(board, true);

			Assert.AreEqual(expected, result, " \n--- Expected: " + expected + ". Actual: gameStats.IsWinner(board, true); = " + result);
		}

		[TestMethod]
		public void TestIsWinnerDIA() {
			board.Reset();
			for (int i=0; i<DIM; i++)
				board.SetCell(i, i, true);
			bool expected = true;

			bool result = gameStats.IsWinner(board, true);

			Assert.AreEqual(expected, result, " \n--- Expected: " + expected + ". Actual: gameStats.IsWinner(board, true); = " + result);
		}

		[TestMethod]
		public void TestIsWinnerAntiDIA() {
			board.Reset();
			for (int i=0, j=DIM - 1; i<DIM && j>=0; i++, j--)
				board.SetCell(i, j, true);
			bool expected = true;

			bool result = gameStats.IsWinner(board, true);

			Assert.AreEqual(expected, result, " \n--- Expected: " + expected + ". Actual: gameStats.IsWinner(board, true); = " + result);
		}
	}
}
