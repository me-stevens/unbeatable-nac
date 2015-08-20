using System.Windows;

namespace NoughtsAndCrosses {
	/// <summary>
	/// Interaction logic for MenuPopUps.xaml
	/// </summary>

	public partial class MenuPopUps : Window {

		public MenuPopUps(string menuItem) {

			InitializeComponent();

			if (menuItem == "instructions")
				FillInstructions();
			else if (menuItem == "about")
				FillAbout();
		}

		private void FillInstructions() {

			title.Text   = "Instructions";
			content.Text = "Noughts and crosses (also known as Tic-tac-toe or Xs and Os) " + 
				"is a paper-and-pencil game for two players, X and O, who take turns marking the spaces in a 3×3 grid.\n\n" +
				"The player who succeeds in placing three of their marks in a horizontal, vertical, or diagonal row, wins the game.";
		}

		private void FillAbout() {

			title.Text   = "About";
			content.Text = "This app is open-sourced through GitHub.\n\n" + 
				"(c) 2015 MIT License 2015 - Octopus in vitro\n" +
				"octopusinvitro.tk";

			content.TextAlignment = TextAlignment.Center;
		}

	}
}
