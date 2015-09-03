
namespace NoughtsAndCrosses {

	public class Player {

		public string Name    { set; get; }
		public bool   IsHuman { set; get; }
		public bool   IsFirst { set; get; }

		public Player() : this("", false, false) {
			// Uses constructor initializer.
		}

		public Player(string name, bool isHuman, bool isFirst) {
			Name    = name;
			IsHuman = isHuman;
			IsFirst = isFirst;
		}

		public Player Copy() {
			Player copy  = new Player();

			copy.Name    = Name;
			copy.IsHuman = IsHuman;
			copy.IsFirst = IsFirst;

			return copy;
		}

	}
}