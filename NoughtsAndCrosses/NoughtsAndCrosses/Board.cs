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

		public Board(int dim) {
			DIM   = dim;
			board = new string[DIM, DIM];
			Reset();
		}

		public int GetDIM() {
			return DIM;
		}

		public void Reset() {	
			for (int i=0; i<DIM; i++)
				for (int j=0; j<DIM; j++)
					board[i, j] = "";
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

		public List<int[]> GetEmpties() {
			List<int[]> empties = new List<int[]>();

			for (int i=0; i<DIM; i++)
				for (int j=0; j<DIM; j++)
					if (GetCell(i, j) == "")
						empties.Add(new int[2] { i, j });

			return empties;
		}

		public bool CheckRow(string mark, int row) {
			for (int j=0; j<DIM; j++)
				if (board[row, j] != mark)
					return false;
			return true;
		}

		public bool CheckCol(string mark, int column) {
			for (int i=0; i<DIM; i++)
				if (board[i, column] != mark)
					return false;
			return true;
		}

		public bool CheckDia(string mark) {
			for (int i=0; i<DIM; i++)
				if (board[i, i] != mark)
					return false;
			return true;
		}

		public bool CheckAntiDia(string mark) {
			for (int i=0, j=DIM - 1; i<DIM && j>=0; i++, j--)
				if (board[i, j] != mark)
					return false;
			return true;
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