using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoughtsAndCrosses {

	public class GameStats {

		public LineType WinnerLine  { set; get; }
		public int      WinnerIndex { set; get; }

		public int Wins   { set; get; }
		public int Draws  { set; get; }
		public int Losses { set; get; }

		public GameStats() {
			WinnerLine  = LineType.NONE;
			WinnerIndex = -1;

			Wins   = 0;
			Draws  = 0;
			Losses = 0;
		}

		public void UpdateWinnerInfo(LineType lineType, int index, bool first) {
			WinnerLine  = lineType;
			WinnerIndex = index;

			if (first)
				Wins++;
			else
				Losses++;
		}

		public bool IsWinner(Board board, bool first) {
			int DIM     = board.GetDIM();
			string mark = (first) ? "X" : "O";

			for (int i=0; i<DIM; i++) {
				if (board.CheckRow(mark, i)) {
					UpdateWinnerInfo(LineType.ROW, i, first);
					return true;
				}

				if (board.CheckCol(mark, i)) {
					UpdateWinnerInfo(LineType.COL, i, first);
					return true;
				}
			}

			if (board.CheckDia(mark)) {
				UpdateWinnerInfo(LineType.DIA, 1, first);
				return true;
			}

			if (board.CheckAntiDia(mark)) {
				UpdateWinnerInfo(LineType.DIA, 2, first);
				return true;
			}

			return false;
		}


	}
}
