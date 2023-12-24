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
                DetailsPanel.Children.Clear(); // Clear existing content

                // Create and add new content based on selectedResearcher
                DetailsPanel.Children.Add(new TextBlock { Text = $"Name: {selectedResearcher.givenName} {selectedResearcher.familyName}", Margin = new Thickness(5) });
                DetailsPanel.Children.Add(new TextBlock { Text = $"Title: {selectedResearcher.title}", Margin = new Thickness(5) });
                // ... Add more details as needed ...

                // Adjust column widths
                ListViewColumn.Width = new GridLength(1, GridUnitType.Star);
                DetailsViewColumn.Width = new GridLength(2, GridUnitType.Star);
            }
        }

    }
}