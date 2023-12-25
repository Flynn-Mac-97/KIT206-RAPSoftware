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

        //Selected the list item in main view
        private void ListViewItem_Selected(object sender, SelectionChangedEventArgs e) {
            if (ResearchersListView.SelectedItem is Researcher selectedResearcher) {

                // Adjust column widths
                ListViewColumn.Width = new GridLength(1, GridUnitType.Star);
                DetailsViewColumn.Width = new GridLength(2, GridUnitType.Star);

                // Set the details view to the selected researcher
                SetDetailsViewToResearcher(selectedResearcher);
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (ResearchersListView.SelectedItem is Researcher selectedResearcher) {
                
            }
        }
        //Refresh ResearcherDetail
        private void SetDetailsViewToResearcher(Researcher selectedResearcher) {
            ResearcherImage.Source = new BitmapImage(new System.Uri(selectedResearcher.photo));
            //remove children on SelectedResearcherDetails
            SelectedResearcherDetails.Children.Clear();

            //Remove the staff or student specific text blocks.
            SelectedResearcherSpecificDetails.Children.Clear();

            YearlyPublications.Children.Clear();

            PublicationsList.Children.Clear();

            //Generic details
            AddTextBlockToStackPanel(SelectedResearcherDetails, selectedResearcher.givenName + " " + selectedResearcher.familyName, 14, FontWeights.Normal);
            AddTextBlockToStackPanel(SelectedResearcherDetails, selectedResearcher.title.ToString().ToLower(), 14, FontWeights.Normal);
            AddTextBlockToStackPanel(SelectedResearcherDetails, selectedResearcher.unit, 14, FontWeights.Normal);
            AddTextBlockToStackPanel(SelectedResearcherDetails, selectedResearcher.campus.ToString().ToLower(), 14, FontWeights.Normal);
            AddTextBlockToStackPanel(SelectedResearcherDetails, selectedResearcher.email, 14, FontWeights.Normal);
            AddTextBlockToStackPanel(SelectedResearcherDetails, selectedResearcher.level.ToString().ToLower(), 14, FontWeights.Normal);

            //Add title to Yearly Publications
            AddTextBlockToStackPanel(YearlyPublications, "Yearly Publications", 14, FontWeights.Bold);

            //Yearly count
            foreach (string s in selectedResearcher.PublicationsCountByYear()) {
                if(s.Contains(": 0") == false) {
                    AddTextBlockToStackPanel(YearlyPublications, s, 14, FontWeights.Normal);
                }
            }

            //Specific details
            AddTextBlockToStackPanel(SelectedResearcherSpecificDetails, "Commenced date:" + selectedResearcher.utasStart.Day, 14, FontWeights.Normal);
            AddTextBlockToStackPanel(SelectedResearcherSpecificDetails, "Position Commenced: " + selectedResearcher.currentStart.Day, 14, FontWeights.Normal);
            AddTextBlockToStackPanel(SelectedResearcherSpecificDetails, "Tenure: " + selectedResearcher.Tenure, 14, FontWeights.Normal);
            AddTextBlockToStackPanel(SelectedResearcherSpecificDetails, "Total Publications: " + selectedResearcher.PublicationsCount, 14, FontWeights.Normal);
            AddTextBlockToStackPanel(SelectedResearcherSpecificDetails, "Q1 Percentage: " + selectedResearcher.Q1percentage, 14, FontWeights.Normal);

            if (selectedResearcher is Student student) {
                AddTextBlockToStackPanel(SelectedResearcherSpecificDetails, "Degree: " + student.degree, 14, FontWeights.Normal);
                AddTextBlockToStackPanel(SelectedResearcherSpecificDetails, "Supervisor: " + student.supervisor.familyName +", "+ student.supervisor.givenName, 14, FontWeights.Normal);
            }
            else if(selectedResearcher is Staff staff) {
                //add blocks for staff
                AddTextBlockToStackPanel(SelectedResearcherSpecificDetails, "3 year avg: " + staff.ThreeYearAVG, 14, FontWeights.Normal);
                //funding
                AddTextBlockToStackPanel(SelectedResearcherSpecificDetails, "Funding Recieved: " + staff.FundingRecieved, 14, FontWeights.Normal);
                //publication performance
                AddTextBlockToStackPanel(SelectedResearcherSpecificDetails, "Publication Performance: " + staff.PublicationPerformance, 14, FontWeights.Normal);
                //funding performance
                AddTextBlockToStackPanel(SelectedResearcherSpecificDetails, "Funding Performance:" + staff.FundingPerformance, 14, FontWeights.Normal);
                //supervisions
                AddTextBlockToStackPanel(SelectedResearcherSpecificDetails, "Supervisions: " + staff.supervisions, 14, FontWeights.Normal);
            }

            //Publications list
            foreach(Publication p in selectedResearcher.publications) {
                ListViewItem item = new ListViewItem();
                item.HorizontalContentAlignment = HorizontalAlignment.Stretch;
                Button button = new Button {
                    Content = p.ToSummaryString(),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    FontSize = 14,
                    FontWeight = FontWeights.Normal,
                    Background = Brushes.Transparent,
                    BorderBrush = Brushes.Transparent,
                };
                item.Content = button;
                PublicationsList.Children.Add(item);

                button.Click += (sender, e) => {
                    PublicationPopup popup = new PublicationPopup(p);
                    popup.Show();
                };
            }
        }

        //Helper function to add a textblock to a stackpanel with given font size, text etc
        private void AddTextBlockToStackPanel(StackPanel sP, string text, int fontSize, FontWeight fontWeight) {
            TextBlock tb = new TextBlock();
            tb.Text = text;
            tb.FontSize = fontSize;
            tb.FontWeight = fontWeight;
            sP.Children.Add(tb);
        }

        private void Poor(object sender, RoutedEventArgs e)
        {
            test.Text = "Researchers with Poor Performace";

        }




        /*
        private void CopyEmail(object sender, RoutedEventArgs e)
        {
            //copy email addresses
        }
        */
    }
}