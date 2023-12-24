using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KIT206_Assignment_01 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            ResearchController researchController = ResearchController.Instance;

            ListViewColumn.Width = new GridLength(1, GridUnitType.Star);
            DetailsViewColumn.Width = new GridLength(0); // Collapsed

            // Set the ItemsSource of the ListView
            ResearchersListView.ItemsSource = researchController.researchers;
        }

        private void ListViewItem_Selected(object sender, SelectionChangedEventArgs e) {
            if (ResearchersListView.SelectedItem is Researcher selectedResearcher) {

                // Adjust column widths
                ListViewColumn.Width = new GridLength(1, GridUnitType.Star);
                DetailsViewColumn.Width = new GridLength(2, GridUnitType.Star);
            }
        }

    }
}