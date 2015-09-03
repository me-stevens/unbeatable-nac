using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoughtsAndCrosses;
using System.Diagnostics;
using System.Collections.Generic;

namespace Tests {

	[TestClass]
	public class TestBoard {

		static int DIM  = 4;
		Board  board    = new Board(DIM);
		Random random   = new Random();
		GameStats gameStats = new GameStats();

		[TestMethod]
		public void TestReset() {
			string expectedCell = "";
			
			board.Reset();

			foreach (string cell in board.GetBoard())
				Assert.AreEqual(expectedCell, cell, "\n--- Expected: empty. Actual: cell = " + cell);
		}
	
		[TestMethod]
		public void TestSetCellMark() {
			board.Reset();
			int i = random.Next(DIM);
			int j = random.Next(DIM);

			// Test X
			bool first = true;
			string expected = "X";

			board.SetCell(i, j, first);

			Assert.AreEqual(expected, board.GetCell(i, j), " \n--- Expected: " + expected + ". Actual: board.GetCell(i, j): " + board.GetCell(i, j));

			// Test O
			first = false;
			expected  = "O";

			board.SetCell(i, j, first);

			Assert.AreEqual(expected, board.GetCell(i, j), " \n--- Expected: " + expected + ". Actual: board.GetCell(i, j): " + board.GetCell(i, j));
		}


		[TestMethod]
		public void TestSetCellException() {
			int i = -1;
			int j = -1;
			bool first = true;

			board.SetCell(i, j, first);
			// Assert is handled by the expected Exception
		}

		[TestMethod]
		public void TestGetCellException() {
			int i = -1;
			int j = -1;

			string result = board.GetCell(i, j);
			// Assert is handled by the expected Exception
		}

		[TestMethod]
		public void TestSetBoard() {
			board.Reset();
			string[,] expected = RandomBoard();

			board.SetBoard(expected);

			for (int i=0; i<DIM; i++)
				for(int j=0; j<DIM; j++)
					Assert.AreEqual(expected[i, j], board.GetCell(i,j), "\n--- Expected:  expected[i, j] = " + expected[i, j] + ". Actual: board.GetCell(i, j) = " + board.GetCell(i, j));

			string mark    = (board.GetCell(1,1) == "X") ? "O" : "X";
			expected[1, 1] = mark;

			Assert.AreEqual(expected[1, 1], board.GetCell(1, 1), "\n--- Expected: expected[1, 1] = " + expected[1, 1] + ". Actual: board.GetCell(1, 1) = " + board.GetCell(1, 1));
		}

	
		[TestMethod]
		public void TestGetBoard() {
			board.Reset();

			string[,] expected = board.GetBoard();

			for (int i=0; i<DIM; i++)
				for(int j=0; j<DIM; j++)
					Assert.AreEqual(expected[i, j], board.GetCell(i,j), "\n--- Expected:  expected[i, j] = " + expected[i, j] + ". Actual: board.GetCell(i, j) = " + board.GetCell(i, j));
			
			bool first = (expected[1, 1] == "X") ? true : false;
			board.SetCell(1, 1, !first);

			Assert.AreEqual(expected[1, 1], board.GetCell(1, 1), "\n--- Expected: expected[1, 1] = " + expected[1, 1] + ". Actual: board.GetCell(1, 1) = " + board.GetCell(1, 1));
		}

		[TestMethod]
		public void TestCopy() {
			board.Reset();

			string[,] expected = board.Copy();

			for (int i=0; i<DIM; i++)
				for(int j=0; j<DIM; j++)
					Assert.AreEqual(expected[i, j], board.GetCell(i,j), "\n--- Expected:  expected[i, j] = " + expected[i, j] + ". Actual: board.GetCell(i, j) = " + board.GetCell(i, j));
			
			bool first = (expected[1, 1] == "X") ? true : false;
			board.SetCell(1, 1, !first);

			Assert.AreNotEqual(expected[1, 1], board.GetCell(1, 1), "\n--- Expected: expected[1, 1] = " + expected[1, 1] + ". Actual: board.GetCell(1, 1) = " + board.GetCell(1, 1));
		}


		[TestMethod]
		public void TestGetEmpties() {
			string[,] randomBoard = RandomBoard();
			randomBoard[1, 1]     = "";

			board.Reset();
			board.SetBoard(randomBoard);

			List<int[]> empties = new List<int[]>();
			empties = board.GetEmpties();

			Assert.AreEqual(1, empties.Count, " \n--- Expected: 1. Actual: empties.Count = " + empties.Count);
		}


		[TestMethod]
		public void TestCheckRow() {
			board.Reset();
			int row = 0;
			for (int j=0; j<DIM; j++)
				board.SetCell(row, j, true);
			bool expected = true;

			bool check = board.CheckRow("X", row);

			Assert.AreEqual(expected, check, " \n--- Expected: " + expected + ". Actual: check = " + check);
		}
	
		[TestMethod]
		public void TestCheckCol() {
			board.Reset();
			int col = 0;
			for (int i=0; i<DIM; i++)
				board.SetCell(i, col, true);
			bool expected = true;

			bool check = board.CheckCol("X", col);

			Assert.AreEqual(expected, check, " \n--- Expected: " + expected + ". Actual: check = " + check);
		}
		
		[TestMethod]
		public void TestCheckDia() {
			board.Reset();
			for (int i=0; i<DIM; i++)
				board.SetCell(i, i, true);
			bool expected = true;

			bool check = board.CheckDia("X");

			Assert.AreEqual(expected, check, " \n--- Expected: " + expected + ". Actual: check = " + check);
		}
		
		[TestMethod]
		public void TestCheckAntiDia() {
			board.Reset();
			for (int i=0, j=DIM - 1; i<DIM && j>=0; i++, j--)
				board.SetCell(i, j, true);
			bool expected = true;

			bool check = board.CheckAntiDia("X");

			Assert.AreEqual(expected, check, " \n--- Expected: " + expected + ". Actual: check = " + check);
		}
		
		
		
		private string[,] RandomBoard() {
			string[,] board = new string[DIM, DIM];
			for (int i=0; i<DIM; i++) {
				for (int j=0; j<DIM; j++) {					
					board[i, j] = (random.Next(2) == 0) ? "X" : "O";
				}
			}
			return board;
		}


	}
}
