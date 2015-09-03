
namespace NoughtsAndCrosses {

	public class Player {

		public string Name    { set; get; }
		public bool   IsHuman { set; get; }
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
		public bool   IsFirst { set; get; }

		public Player() : this("", false, false) {
			// Uses constructor initializer.
		}

		public Player(string name, bool isHuman, bool isFirst) {
			Name    = name;
			IsHuman = isHuman;
			placed   = 0;
			firstPos = new int[] { -1, -1 };
			lastPos  = new int[] { -1, -1 };
		}

		public int GetPlaced() {
			return placed;
		}


				Wins    = 0;
				Draws   = 0;
				Losses  = 0;
			IsFirst = isFirst;
		}

		public Player Copy() {
			Player copy  = new Player();

			copy.Name    = Name;
			copy.IsHuman = IsHuman;
			// Placed and FirstPos are trickier to copy:
			if (placed != 0) {
				copy.LastPos = FirstPos;
				for (int i=0; i<placed-1; i++)
					copy.LastPos = LastPos;
			}
			copy.IsFirst = IsFirst;

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
