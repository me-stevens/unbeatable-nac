﻿using System;
using System.Linq;
using System.Diagnostics;
using System.Windows;
using System.Collections.Generic;

namespace NoughtsAndCrosses {

	public enum LineType { ROW, COL, DIA, NONE }

	public class Board {

		public readonly int DIM;

		private string[,] board;

		public string   WinnerName  { set; get; }
		public LineType WinnerLine  { set; get; }
		public int      WinnerIndex { set; get; }


		public Board(int dim) {
			DIM   = dim;
			board = new string[DIM, DIM];

			for (int i=0; i<DIM; i++) 
				for (int j=0; j<DIM; j++) 
					board[i, j] = "";

			WinnerName  = "";
			WinnerLine  = LineType.NONE;
			WinnerIndex = -1;
		}

		public int GetDIM() {
			return DIM;
		}

		public void SetCell(int[] pos, Player player) {
			SetCell(pos[0], pos[1], player);
		}

		public void SetCell(int i, int j, Player player) {
			try {
				board[i, j]    = (player.IsFirst) ? "X" : "O";
				player.LastPos = new int[] { i, j };

			} catch (IndexOutOfRangeException e) {
				Debug.WriteLine("Index out of range \n\n" + e.Message);
			}
		}

		public string GetCell(int[] pos) {
			return GetCell(pos[0], pos[1]);
		}

		public string GetCell(int i, int j) {
			try {
				return board[i, j];
			} catch (IndexOutOfRangeException e) {
				Debug.WriteLine("Index out of range \n\n" + e.Message);
			}
			
			return "";
		}

		// Set board by reference
		public void SetBoard(string[,] newBoard) {
			board = newBoard;
		}

		public void Paste(string[,] newBoard) {
			if (newBoard.GetLength(0) == DIM && newBoard.Length == DIM*DIM) 
				for (int i=0; i<DIM; i++)
					for (int j=0; j<DIM; j++)
						board[i, j] = newBoard[i, j];
		}

		// Get board by reference
		public string[,] GetBoard() {
			return board;
		}

		// Get board by copying contents
		public string[,] Copy() {
			string[,] copy = new string[DIM, DIM];
			copy = (string[,]) board.Clone();
			return copy;
		}

		public string[] GetLine(LineType lt, int index) {

			string[] line = new[] {"", "", ""};

			switch (lt) {

				case LineType.ROW:
					for (int j=0; j<DIM; j++)
						line[j] = board[index, j];
					break;

				case LineType.COL:
					for (int i=0; i<DIM; i++)
						line[i] = board[i, index];
					break;

				case LineType.DIA:
					if (index == 1)
						for (int i=0, j=0; i<DIM && j<DIM; i++, j++)
							line[i] = board[i, j];

					else if (index == 2)
						for (int i=0, j=DIM - 1; i<DIM && j>=0; i++, j--)
							line[i] = board[i, j];
						break;
			}
			
			return line;
		}

		public void Reset() {
	
			for (int i=0; i<DIM; i++)
				for (int j=0; j<DIM; j++)
					board[i, j] = "";

			WinnerName  = "";
			WinnerLine  = LineType.NONE;
			WinnerIndex = -1;
		}
	
		public List<int[]> GetEmpties() {
			List<int[]> empties = new List<int[]>();

			for (int i=0; i<DIM; i++)
				for (int j=0; j<DIM; j++)
					if (GetCell(i, j) == "")
						empties.Add(new int[2] { i, j });

			return empties;
		}
	
		/**********************************************
		 * CHECK FOR A WINNER
		 *********************************************/
		public bool IsWinner(Player player) {

			string mark = (player.IsFirst)  ? "X" : "O";
			int count;

			// Cols 
			for (int j=0; j<DIM; j++) {

				count = 0;
				for (int i=0; i<DIM; i++)
					if (board[i, j] == mark)
						count++;

				if (count == DIM) {
					WinnerName  = player.Name;
					WinnerLine  = LineType.COL;
					WinnerIndex = j;
					return true;
				}
			}


			// Rows 
			for (int i=0; i<DIM; i++) {

				count = 0;
				for (int j=0; j<DIM; j++)
					if (board[i, j] == mark)
						count++;

				if (count == DIM) {
					WinnerName  = player.Name;
					WinnerLine  = LineType.ROW;
					WinnerIndex = i;
					return true;
				}
			}
	
			// Diag 
			count = 0;
			for (int i=0, j=0; i<DIM && j<DIM; i++, j++)
				if (board[i, j] == mark)
					count++;

			if (count == DIM) {
				WinnerName  = player.Name;
				WinnerLine  = LineType.DIA;
				WinnerIndex = 1;
				return true;
			}

			// AntiDiag 
			count = 0;
			for (int i=0, j=DIM - 1; i<DIM && j>=0; i++, j--)
				if (board[i, j] == mark)
					count++;

			if (count == DIM) {
				WinnerName  = player.Name;
				WinnerLine  = LineType.DIA;
				WinnerIndex = 2;
				return true;
			}

			return false;
		}

		/**********************************************
		 * BOARD PRINTER
		 *********************************************/
		public override string ToString(){
			string s = "";

			for (int i=0; i<DIM; i++) {
				for (int j=0; j<DIM; j++)
					s += board[i, j] + "\t";
				s += "\n";
			}
				
			return "WinnerName: "  + WinnerName  + ", " +
			       "WinnerLine: "  + WinnerLine  + ", " +
			       "WinnerIndex: " + WinnerIndex + ", " +
			       "Board:\n"      + s           + "\n" ;
		}
	
	}
}