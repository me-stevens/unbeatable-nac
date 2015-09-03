using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoughtsAndCrosses;
using System.Diagnostics;
using System.Collections.Generic;

namespace Tests {

	[TestClass]
	public class TestBoard {

		static int DIM  = 3;
		Board  b = new Board(DIM);
		Player p = new Player();
		Random r = new Random();

		/*****************************************************
		 * SETCELL - The correct mark is stored?
		 *****************************************************/ 
		[TestMethod]
		public void TestSetCellMark() {
			int i = r.Next(DIM);
			int j = r.Next(DIM);

			// Test X - Arrange --------------------------
			p.IsFirst = true;
			string expected = "X";

			// Act
			b.SetCell(i, j, p);

			// Assert
			Assert.AreEqual(expected, b.GetCell(i, j), " \n--- Expected: " + expected + ". Actual b.GetCell(i, j): " + b.GetCell(i, j));
			Debug.WriteLine(b.ToString());

			// Test O - Arrange --------------------------
			p.IsFirst = false;
			expected  = "O";

			// Act
			b.SetCell(i, j, p);

			// Assert
			Assert.AreEqual(expected, b.GetCell(i, j), " \n--- Expected: " + expected + ". Actual b.GetCell(i, j): " + b.GetCell(i, j));
			Debug.WriteLine(b.ToString());
		}

		/*****************************************************
		 * SETCELL - LastPos is set with the cell?
		 *****************************************************/ 
		[TestMethod]
		public void TestSetCellLastPos() {
			// Arrange
			int i = r.Next(DIM);
			int j = r.Next(DIM);
			int[] pexpected = new int[] { i, j }; 

			// Act
			b.SetCell(pexpected, p);

			// Assert
			CollectionAssert.AreEqual(pexpected, p.LastPos);
		}

		/**********************************************
		 * SETCELL - Exception is thrown and catched?
		 *********************************************/ 
		[TestMethod]
		public void TestSetCellLastException() {
			// Arrange
			int i = -1;
			int j = -1;

			// Act
			b.SetCell(i, j, p);
			// Assert is handled by the expected Exception
		}

		/**********************************************
		 * GETCELL - Exception is thrown and catched?
		 *********************************************/ 
		[TestMethod]
		public void TestGetCellException() {
			string result;

			// Arrange
			int i = -1;
			int j = -1;

			// Act
			result = b.GetCell(i, j);
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

			p.Reset(true);
			b.Reset();

			for (int i=0; i<DIM; i++)
				for(int j=0; j<DIM; j++)
					Assert.AreEqual(expected[i, j], board.GetCell(i,j), "\n--- Expected:  expected[i, j] = " + expected[i, j] + ". Actual: board.GetCell(i, j) = " + board.GetCell(i, j));
			
			bool first = (expected[1, 1] == "X") ? true : false;
			board.SetCell(1, 1, !first);

			Assert.AreNotEqual(expected[1, 1], board.GetCell(1, 1), "\n--- Expected: expected[1, 1] = " + expected[1, 1] + ". Actual: board.GetCell(1, 1) = " + board.GetCell(1, 1));
		}

		/**********************************************
		 * GETEMPTIES - Returns empty cells?
		 *********************************************/ 
		[TestMethod]
		public void TestGetEmpties() {
			string[,] copy;
			// Arrange
			Random r = new Random();
			p.Reset(true);
			b.Reset();
			for (int i=0; i<DIM; i++) {
				for (int j=0; j<DIM; j++) {
					p.IsFirst = (r.Next(2) == 0) ? true : false;
					b.SetCell(i, j, p);
				}
			}

			copy = b.Copy();
			copy[2, 2] = "";
			int[] expected = new int[] { 2, 2 };
			Board resultBoard = new Board(DIM);
			resultBoard.SetBoard(copy);


			// Act
			List<int[]> empties = new List<int[]>();
			empties = resultBoard.GetEmpties();

			// Assert 
			Assert.AreEqual(1, empties.Count, " \n--- Expected: 1. Actual empties.Count = " + empties.Count);
			CollectionAssert.AreEqual(expected, empties[0]);
		}

		/*****************************************************
		 * SETBOARD - Saves a reference to another board?
		 *****************************************************/ 
		[TestMethod]
		public void TestSetBoard() {

			// Arrange
			Random r       = new Random();
			Board expected = new Board(DIM);
			p.Reset(true);
			b.Reset();
			for (int i=0; i<DIM; i++) {
				for (int j=0; j<DIM; j++) {
					p.IsFirst = (r.Next(2) == 0) ? true : false;
					expected.SetCell(i, j, p);
				}
			}

			// Act
			b.SetBoard(expected.GetBoard());

			// Assert
			for (int i=0; i<DIM; i++)
				CollectionAssert.AreEqual(expected.GetLine(LineType.ROW, i), b.GetLine(LineType.ROW, i));

			// Act
			p.IsFirst = (b.GetCell(1,1) == "X") ? false : true;
			expected.SetCell(1, 1, p);

			// Assert
			Assert.AreEqual(expected.GetCell(1,1), b.GetCell(1,1), " \n--- Expected: not Equal. Actual expected.GetCell(1,1) = " + expected.GetCell(1,1) + ", b.GetCell(1,1) = " + b.GetCell(1,1));
		}
		/*****************************************************
		 * PASTE - Copies the contents of a board?
		 *****************************************************/ 
		[TestMethod]
		public void TestPaste() {

			// Arrange
			Random r       = new Random();
			Board expected = new Board(DIM);
			p.Reset(true);
			b.Reset();
			for (int i=0; i<DIM; i++) {
				for (int j=0; j<DIM; j++) {
					p.IsFirst = (r.Next(2) == 0) ? true : false;
					expected.SetCell(i, j, p);
				}
			}

			// Act
			b.Paste(expected.GetBoard());

			// Assert
			for (int i=0; i<DIM; i++)
				CollectionAssert.AreEqual(expected.GetLine(LineType.ROW, i), b.GetLine(LineType.ROW, i));

			// Act
			p.IsFirst = (b.GetCell(1,1) == "X") ? false : true;
			expected.SetCell(1, 1, p);

			// Assert
			Assert.AreNotEqual(expected.GetCell(1,1), b.GetCell(1,1), " \n--- Expected: not Equal. Actual expected.GetCell(1,1) = " + expected.GetCell(1,1) + ", b.GetCell(1,1) = " + b.GetCell(1,1));
		}
	
		/**********************************************
		 * GETBOARD - Gets a reference to the board?
		 *********************************************/ 
		[TestMethod]
		public void TestGetBoard() {
			string[,] result;

			// Arrange
			Random r = new Random();
			Board resultBoard = new Board(DIM);
			p.Reset(true);
			b.Reset();
			for (int i=0; i<DIM; i++) {
				for (int j=0; j<DIM; j++) {
					p.IsFirst = (r.Next(2) == 0) ? true : false;
					b.SetCell(i, j, p);
				}
			}

			// Act
			result = b.GetBoard();
			resultBoard.SetBoard(result);

			// Assert
			for (int i=0; i<DIM; i++)
				CollectionAssert.AreEqual(b.GetLine(LineType.ROW, i), resultBoard.GetLine(LineType.ROW, i));
			
			// Act
			p.IsFirst = (b.GetCell(1,1) == "X") ? false : true;
			resultBoard.SetCell(1, 1, p);

			// Assert
			Assert.AreEqual(b.GetCell(1,1), resultBoard.GetCell(1,1), " \n--- Expected: b.GetCell(1,1) = " + b.GetCell(1,1) + ". Actual resultBoard.GetCell(1,1) = " + resultBoard.GetCell(1,1));
		}

		/**********************************************
		 * COPY - Copies only the board contents?
		 *********************************************/ 
		[TestMethod]
		public void TestCopy() {
			string[,] result;

			// Arrange
			Random r = new Random();
			Board resultBoard = new Board(DIM);
			p.Reset(true);
			b.Reset();
			for (int i=0; i<DIM; i++) {
				for (int j=0; j<DIM; j++) {
					p.IsFirst = (r.Next(2) == 0) ? true : false;
					b.SetCell(i, j, p);
				}
			}

			// Act
			result = b.Copy();
			resultBoard.SetBoard(result);

			// Assert
			for (int i=0; i<DIM; i++)
				CollectionAssert.AreEqual(b.GetLine(LineType.ROW, i), resultBoard.GetLine(LineType.ROW, i));
			
			// Act
			result[1,1] = (b.GetCell(1,1) == "X") ? "O" : "X";

			// Assert
			Assert.AreNotEqual(b.GetCell(1,1), result[1,1], " \n--- Expected: not equal. Actual b.GetCell(1,1) = " + b.GetCell(1,1) + ", result[1,1] = " + result[1,1]);
		}


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
