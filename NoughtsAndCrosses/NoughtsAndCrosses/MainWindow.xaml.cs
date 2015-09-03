using System;
using System.Linq;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;


namespace NoughtsAndCrosses {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>

	public partial class MainWindow : Window {

		private int DIM;

		Board  board;
		Player player1;
		Player player2;
		AI     ai;

		private bool first;
		private bool winner;
		private bool full;

		private bool clicked;

		public MainWindow() {
			DIM   = 3;

			board   = new Board(DIM);
			player1 = new Player();
			player2 = new Player();
			ai      = new AI();

			// Convention for the whole application: 
			// player1 always stores the first player
			player1.IsFirst = true;
			player2.IsFirst = false;

			first  = true;
			winner = false;
			full   = false;

			// Disable clicks or listen for them
			clicked = true;

			InitializeComponent();
		}
	
		private void MainWindow_Loaded(object sender, RoutedEventArgs e) {
			statusBar.Text = "Noughts & Crosses";

			boardGrid.Rows    = DIM;
			boardGrid.Columns = DIM;

			for (int i=0; i<boardGrid.Rows; i++) {
				for (int j=0; j<boardGrid.Columns; j++) {

					TextBlock tb = new TextBlock();
					tb.Width     = border.Width / 3;
					tb.Height    = border.Height / 3;
					tb.Style     = (Style)Resources["cells"] as Style;
					boardGrid.Children.Add(tb);
				}
			}

			// Hide footer until the game is started
			playAgain.Visibility    = Visibility.Hidden;
			footerlabel1.Visibility = Visibility.Hidden;
			footerlabel2.Visibility = Visibility.Hidden;
			PaintWinnerCells();
		}

		private void NewGame_Click(object sender, RoutedEventArgs e) {
			player1.Reset(true);
			player2.Reset(true);

			player1.IsFirst = true;

			FormNew fn = new FormNew();
			fn.Owner   = this;

			if (fn.ShowDialog() == true) {
				player1.Name    = fn.Name1;
				player2.Name    = fn.Name2;

				player1.IsHuman = fn.IsHuman1;
				player2.IsHuman = fn.IsHuman2;

				// Update GUI
				player1name.Text  = player1.Name;
				player2name.Text  = player2.Name;

				player1stats.Text = player1.Wins + " " + player1.Draws + " " + player1.Losses;
				player2stats.Text = player2.Wins + " " + player2.Draws + " " + player2.Losses;
				
				footerlabel1.Visibility = Visibility.Visible;
				footerlabel2.Visibility = Visibility.Visible;

				Start();
			}
		}

		private void Start() {
			statusBar.Text       = player1.Name;
			playAgain.Visibility = Visibility.Hidden;

			board.Reset();

			foreach (UIElement child in boardGrid.Children) {
				((TextBlock) child).Text      = "";
				((TextBlock) child).IsEnabled = true;
				((TextBlock) child).Style     = (Style)Resources["cells"] as Style;
			}

			first   = true;
			winner  = false;
			full    = false;
			
			// Disable clicks or listen for them
			clicked = true;

			// If player is a robot, start its turn method
			if (!player1.IsHuman) {
				RobotTurn();
			}
 
			// If player is a human, wait for a click
			else {
				clicked = false;
			}
		}

		private void RobotTurn() {

			int[] pos;
			int index;

			if (first)
				pos = ai.CalculateCell(board, player1, player2);
			else
				pos = ai.CalculateCell(board, player2, player1);

			// Convert from matrix to index notation
			index = pos[0]*DIM + pos[1];

			UpdateGameStatus(pos, index);
		}

		private void HumanTurn(object sender, MouseButtonEventArgs e) {
			try {
				if (clicked || !((TextBlock) e.Source).IsEnabled)
					e.Handled = true;

				else {
					int[] pos;
					int index;

					index = ((UniformGrid) sender).Children.IndexOf((UIElement) e.Source);

					// Convert from index to matrix notation
					int row = index / boardGrid.Columns;
					int col = index % boardGrid.Columns;

					pos = new int[] { row, col };

					UpdateGameStatus(pos, index);
				}
			} catch (InvalidCastException ex) {
				Debug.WriteLine("Reclicking an already disabled TextBlock" + ex.Message);
			}
		}
		
		private void UpdateGameStatus(int[] pos, int index) {

			SetCellGUI(index);

			if (first)
				board.SetCell(pos, player1);
			else 
				board.SetCell(pos, player2);				

			winner = (first) ? board.IsWinner(player1) : board.IsWinner(player2);
			full   = (player1.GetPlaced() + player2.GetPlaced() == DIM*DIM);

			if (winner || full) {
				clicked = true;
				GameOver();
			}

			else if ( ( first && !player2.IsHuman) 
			       || (!first && !player1.IsHuman) ) {
				clicked = true;
				first   = !first;
				statusBar.Text = (first) ? player1.Name : player2.Name;

				RobotTurn();
			} 
			
			else {
				clicked = false;
				first   = !first;
				statusBar.Text = (first) ? player1.Name : player2.Name;
			}
		}

		private void GameOver() {
			if (winner) {
				statusBar.Text = "Congratulations, " + board.WinnerName + "!";
				PaintWinnerCells();

				if (first) {
					player1.Wins++;
					player2.Losses++;
				} else {
					player2.Wins++;
					player1.Losses++;
				}
			} else if (full) {
				statusBar.Text = "It's a draw!";

				player1.Draws++;
				player2.Draws++;
			}

			player1stats.Text = player1.Wins + " " + player1.Draws + " " + player1.Losses;
			player2stats.Text = player2.Wins + " " + player2.Draws + " " + player2.Losses;

			playAgain.Visibility = Visibility.Visible;
		}

		private void PlayAgain_Click(object sender, RoutedEventArgs e) {			
			// If playing again, do a soft reset
			player1.Reset(false);
			player2.Reset(false);
			
			Start();
		}

		private void SetCellGUI(int index) {
			try {
				((TextBlock) boardGrid.Children[index]).Text = (first) ? "X" : "O";
				((TextBlock) boardGrid.Children[index]).IsEnabled = false;

			} catch (IndexOutOfRangeException e) {
				Debug.WriteLine("Index out of range \n" + e.Message);
			}
		}

		private void PaintWinnerCells() {
			try {
				// Remember that index = i * cols + j
				if (board.WinnerLine == LineType.ROW) 
					for (int j=0; j<boardGrid.Columns; j++)
						((TextBlock) boardGrid.Children[board.WinnerIndex * boardGrid.Columns + j]).Style = (Style) Resources["winner"] as Style;
				
				else if (board.WinnerLine == LineType.COL)
					for (int i=0; i<boardGrid.Rows; i++)
						((TextBlock) boardGrid.Children[i * boardGrid.Columns + board.WinnerIndex]).Style = (Style) Resources["winner"] as Style;
				
				else if (board.WinnerLine == LineType.DIA && board.WinnerIndex == 1)
					for (int i=0; i<boardGrid.Columns; i++)
						((TextBlock) boardGrid.Children[i * boardGrid.Columns + i]).Style = (Style) Resources["winner"] as Style;

				else if (board.WinnerLine == LineType.DIA && board.WinnerIndex == 2)
					for (int i=0, j=boardGrid.Columns - 1; i<boardGrid.Rows && j>=0; i++, j--)
						((TextBlock) boardGrid.Children[i * boardGrid.Columns + j]).Style = (Style) Resources["winner"] as Style;

			} catch (IndexOutOfRangeException e) {
				Debug.WriteLine("Index out of range \n" + e.Message);
			}
		}


		private void Instructions_Click(object sender, RoutedEventArgs e) {
			MenuPopUps mi = new MenuPopUps("instructions");
			mi.Owner      = this;
			mi.Show();
		}

		private void About_Click(object sender, RoutedEventArgs e) {
			MenuPopUps mi = new MenuPopUps("about");
			mi.Owner      = this;
			mi.Show();
		}
	}
}
