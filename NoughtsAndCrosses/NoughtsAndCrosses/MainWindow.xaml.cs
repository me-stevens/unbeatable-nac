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
		GameStats gameStats;

		private bool first;
		private bool winner;
		private bool full;

		private bool clicked;

		public MainWindow() {
			InitializeComponent();
		}
	
		private void MainWindow_Loaded(object sender, RoutedEventArgs e) {
			statusBar.Text = "Noughts & Crosses";

			// Hide footer until the game is started
			playAgain.Visibility    = Visibility.Hidden;
			footerlabel1.Visibility = Visibility.Hidden;
			footerlabel2.Visibility = Visibility.Hidden;
		}

		private void NewGame_Click(object sender, RoutedEventArgs e) {

			FormNew fn = new FormNew();
			fn.Owner   = this;

			if (fn.ShowDialog() == true) {
				// Convention for the whole application: 
				// player1 always stores the first player
				player1 = new Player(fn.Name1, fn.IsHuman1, true);
				player2 = new Player(fn.Name2, fn.IsHuman2, false);

				DIM = fn.DIM;
				InitialiseBoards();
				gameStats = new GameStats();

				// Update GUI
				player1name.Text = fn.Name1;
				player2name.Text = fn.Name2;

				player1stats.Text = "0 0 0";
				player2stats.Text = "0 0 0";

				footerlabel1.Visibility = Visibility.Visible;
				footerlabel2.Visibility = Visibility.Visible;

				Start();
			}
		}

		private void InitialiseBoards() {
			int windowSize = 700;
			int cellSize   = 140;

			this.Width  = windowSize + (DIM - 3) * cellSize;
			this.Height = windowSize + (DIM - 3) * cellSize;

			boardGrid.Children.Clear();

			boardGrid.Rows    = DIM;
			boardGrid.Columns = DIM;
			boardGrid.Height  = DIM*cellSize;
			boardGrid.Width   = DIM*cellSize;

			for (int i=0; i<DIM*DIM; i++) {
				TextBlock tb = new TextBlock();
				tb.Width     = cellSize;
				tb.Height    = cellSize;
				tb.Style     = (Style) Resources["Cells"] as Style;

				boardGrid.Children.Add(tb);
			}

			board = new Board(DIM);
		}

		private void Start() {
			// Reset everything
			board.Reset();

			foreach (UIElement child in boardGrid.Children) {
				((TextBlock) child).Text      = "";
				((TextBlock) child).IsEnabled = true;
				((TextBlock) child).Style     = (Style)Resources["Cells"] as Style;
			}

			first   = true;
			winner  = false;
			full    = false;
			
			// Disable clicks or listen for them
			clicked = true;

			statusBar.Text       = player1.Name;
			playAgain.Visibility = Visibility.Hidden;

			// If player is a robot, start its turn method
			if (!player1.IsHuman)
				RobotTurn();
 
			// If player is a human, wait for a click
			else
				clicked = false;
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

			GUISetCell(index);
			board.SetCell(pos, first);

			winner = gameStats.IsWinner(board, first);
			full   = (board.GetEmpties().Count == 0);

			if (winner || full) {
				clicked = true;
				GameOver();
			}

			else {
				// Check the humanity of the next player
				clicked = ((first && !player2.IsHuman) || (!first && !player1.IsHuman)) ? true : false;
				first   = !first;
				statusBar.Text = (first) ? player1.Name : player2.Name;

				if (clicked)
					RobotTurn();
			}
		}

		private void GameOver() {
			if (winner) {
				statusBar.Text = "Congratulations, " + ((first) ? player1.Name : player2.Name) + "!";
				PaintWinnerCells();

			} else if (full) {
				statusBar.Text = "It's a draw!";
				gameStats.Draws++;
			}

			player1stats.Text = gameStats.Wins   + " " + gameStats.Draws + " " + gameStats.Losses;
			player2stats.Text = gameStats.Losses + " " + gameStats.Draws + " " + gameStats.Wins;

			playAgain.Visibility = Visibility.Visible;
		}

		private void PlayAgain_Click(object sender, RoutedEventArgs e) {			
			Start();
		}

		private void GUISetCell(int index) {
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
				if (gameStats.WinnerLine == LineType.ROW) 
					for (int j=0; j<boardGrid.Columns; j++)
						((TextBlock) boardGrid.Children[gameStats.WinnerIndex * boardGrid.Columns + j]).Style = (Style) Resources["Winner"] as Style;
				
				else if (gameStats.WinnerLine == LineType.COL)
					for (int i=0; i<boardGrid.Rows; i++)
						((TextBlock) boardGrid.Children[i * boardGrid.Columns + gameStats.WinnerIndex]).Style = (Style) Resources["Winner"] as Style;
				
				else if (gameStats.WinnerLine == LineType.DIA && gameStats.WinnerIndex == 1)
					for (int i=0; i<boardGrid.Columns; i++)
						((TextBlock) boardGrid.Children[i * boardGrid.Columns + i]).Style = (Style) Resources["Winner"] as Style;

				else if (gameStats.WinnerLine == LineType.DIA && gameStats.WinnerIndex == 2)
					for (int i=0, j=boardGrid.Columns - 1; i<boardGrid.Rows && j>=0; i++, j--)
						((TextBlock) boardGrid.Children[i * boardGrid.Columns + j]).Style = (Style) Resources["Winner"] as Style;

			} catch (IndexOutOfRangeException e) {
				Debug.WriteLine("Index out of range \n" + e.Message);
			}
		}


		private void Instructions_Click(object sender, RoutedEventArgs e) {
			MenuPopUps mi = new MenuPopUps("instructions");
			mi.Owner      = this;
			mi.ShowDialog();
		}

		private void About_Click(object sender, RoutedEventArgs e) {
			MenuPopUps mi = new MenuPopUps("about");
			mi.Owner      = this;
			mi.ShowDialog();
		}

	}
}
