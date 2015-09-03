using System.Windows;

namespace NoughtsAndCrosses {
	/// <summary>
	/// Interaction logic for FormNew.xaml
	/// </summary>
	/// 

	public partial class FormNew : Window {

		// Parent variables
		public bool IsHuman1 { set; get; }
		public bool IsHuman2 { set; get; }
		public string Name1  { set; get; }
		public string Name2  { set; get; }

		// GUI placeholders
		private string you;
		private string friend;
		private string robot1;
		private string robot2;


		public FormNew() {
			InitializeComponent();
		}


		private void FormNew_Loaded(object sender, RoutedEventArgs e) {
			you    = "You";
			friend = "Friend";
			robot1 = "Bender";
			robot2 = "Flexo";

			players1.IsChecked = true;
			first1.IsChecked   = true;

			first1.Content = you;
			first2.Content = robot1;

			name1title.Visibility = Visibility.Visible;
			name1.Visibility      = Visibility.Visible;

			name2title.Visibility = Visibility.Hidden;
			name2.Visibility      = Visibility.Hidden;
		}


		private void players1_Checked(object sender, RoutedEventArgs e) {
			first1.Content = you;
			first2.Content = robot1;

			name1title.Visibility = Visibility.Visible;
			name1.Visibility      = Visibility.Visible;

			name2title.Visibility = Visibility.Hidden;
			name2.Visibility      = Visibility.Hidden;
		}

		private void players2_Checked(object sender, RoutedEventArgs e) {
			first1.Content = you;
			first2.Content = friend;

			name1title.Visibility = Visibility.Visible;
			name1.Visibility      = Visibility.Visible;

			name2title.Visibility = Visibility.Visible;
			name2.Visibility      = Visibility.Visible;
		}

		private void players3_Checked(object sender, RoutedEventArgs e) {
			first1.Content = robot1;
			first2.Content = robot2;

			name1title.Visibility = Visibility.Hidden;
			name1box.Visibility   = Visibility.Hidden;

			name2title.Visibility = Visibility.Hidden;
			name2box.Visibility   = Visibility.Hidden;
		}


		private void ok_Click(object sender, RoutedEventArgs e) {
			CheckFormValues();
			DialogResult = true;
		}

		private void cancel_Click(object sender, RoutedEventArgs e) {
			DialogResult = false;
		}


		private void CheckFormValues() {

			if ((bool) players1.IsChecked) {
				IsHuman1 = true;
				IsHuman2 = false;

				Name1 = (name1.Text == "") ? you : name1.Text;
				Name2 = robot1;
			} 
			
			else if ((bool) players2.IsChecked) {
				IsHuman1 = true;
				IsHuman2 = true;

				Name1 = (name1.Text == "") ? you    : name1.Text;
				Name2 = (name2.Text == "") ? friend : name2.Text;
			} 
			
			else if ((bool) players3.IsChecked) {
				IsHuman1 = false;
				IsHuman2 = false;

				Name1 = robot1;
				Name2 = robot2;

			}
			
			// If the second is first, swap the relevant variables
			// The convention is that 1 is always the first player
			if ((bool) first2.IsChecked) {

				if ((bool) players1.IsChecked) {
					bool aux = IsHuman1;
					IsHuman1 = IsHuman2;
					IsHuman2 = aux;
				}

				string auxn = Name1;
				Name1 = Name2;
				Name2 = auxn;
			}
		}

	
	}
}
