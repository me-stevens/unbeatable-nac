
using System;
namespace NoughtsAndCrosses {

	public class Player {

		public string Name    { set; get; }
		public bool   IsFirst { set; get; }
		public bool   IsHuman { set; get; }

		public int Wins   { set; get; }
		public int Draws  { set; get; }
		public int Losses { set; get; }

		private int placed;

		private int[] firstPos;
		public  int[] FirstPos { get { return firstPos; } }

		private int[] lastPos;
		public  int[] LastPos  {
			set {
				placed++;
				lastPos = value;

				if (placed == 1)
					firstPos = value;

				if (placed > Int32.MaxValue)
					placed = 0;
			}
			get { return lastPos; }
		}

		public Player() : this("", false, false) {
			// Uses constructor initializer.
		}

		public Player(string name, bool isFirst, bool isHuman) {
			Name    = name;
			IsFirst = isFirst;
			IsHuman = isHuman;

			Wins   = 0;
			Draws  = 0;
			Losses = 0;

			placed   = 0;
			firstPos = new int[] { -1, -1 };
			lastPos  = new int[] { -1, -1 };
		}

		public int GetPlaced() {
			return placed;
		}

		public void Reset(bool newGame) {

			if (newGame) {
				Name    = "";
				IsFirst = false;
				IsHuman = false;

				Wins    = 0;
				Draws   = 0;
				Losses  = 0;
			}

			placed   = 0;
			firstPos = new int[] { -1, -1 };
			lastPos  = new int[] { -1, -1 };
		}

		public Player Copy() {
			Player copy  = new Player();

			copy.Name    = Name;
			copy.IsFirst = IsFirst;
			copy.IsHuman = IsHuman;

			copy.Wins   = Wins;
			copy.Draws  = Draws;
			copy.Losses = Losses;

			// Placed and FirstPos are trickier to copy:
			if (placed != 0) {
				copy.LastPos = FirstPos;
				for (int i=0; i<placed-1; i++)
					copy.LastPos = LastPos;
			}

			return copy;
		}

		public override string ToString(){
			return "Name: "      + Name     + ", " +
			       "Is First: "  + IsFirst  + ", " +
			       "Is Human: "  + IsHuman  + ", " +

			       "Wins: "      + Wins     + ", " +
			       "Draws: "     + Draws    + ", " +
			       "Losses: "    + Losses   + ", " +

				   "Placed: "     + placed      + ", " +
			       "First Pos: (" + FirstPos[0] + ", " + FirstPos[1] + "), " +
			       "Last Pos: ("  + LastPos[0]  + ", " + LastPos[1]  + ")."  ;
		}

	}
}
