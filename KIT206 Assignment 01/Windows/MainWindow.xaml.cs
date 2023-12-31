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
using System.Collections.Generic;
using System;

namespace KIT206_Assignment_01 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

            ListViewColumn.Width = new GridLength(1, GridUnitType.Star);
            DetailsViewColumn.Width = new GridLength(0); // Collapsed

            // Set the ItemsSource of the ListView
            ResearcherListView.ItemsSource = ResearchController.Instance.ResearcherNames;
        }

        //Selected the list item in main view
        private void ResearcherListViewItem_Selected(object sender, SelectionChangedEventArgs e) {
            if (ResearcherListView.SelectedItem is ResearcherViewModel selectedResearcher) {

                // Adjust column widths
                ListViewColumn.Width = new GridLength(1, GridUnitType.Star);
                DetailsViewColumn.Width = new GridLength(2, GridUnitType.Star);

                // Set the details view to the selected researcher
                SetDetailsViewToResearcher(ResearchController.Instance.LoadResearcher(selectedResearcher.ID));
            }
        }

        private void ResearcherListFilter_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox != null) {
                ComboBoxItem selectedItem = comboBox.SelectedItem as ComboBoxItem;
                if (selectedItem != null) {
                    string selectedContent = selectedItem.Content.ToString();
                    // Set the ItemsSource of the ListView
                    if(selectedContent == "All") { 
                        ResearcherListView.ItemsSource = ResearchController.Instance.ResearcherNames;
                    }
                    else {
                        //ResearcherListView.ItemsSource = ResearchController.Instance.FilterResearcher("", "Student");
                    }
                }
            }
        }

        //Refresh ResearcherDetail
        private void SetDetailsViewToResearcher(Researcher selectedResearcher) {
            //clear out the selected researcher
            ResearchController.Instance.ClearSelectedResearcher();

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
            AddTextBlockToStackPanel(SelectedResearcherSpecificDetails, "Commenced date: " + selectedResearcher.utasStart.Day, 14, FontWeights.Normal);
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
                AddTextBlockToStackPanel(SelectedResearcherSpecificDetails, "Funding Recieved: " + GlobalXMLAdaptor.GetInstance(Globals.XmlFilePath).GetFundingForResearcher(staff.id), 14, FontWeights.Normal);
                //publication performance
                AddTextBlockToStackPanel(SelectedResearcherSpecificDetails, "Publication Performance: " + staff.PublicationPerformance, 14, FontWeights.Normal);
                //funding performance
                AddTextBlockToStackPanel(SelectedResearcherSpecificDetails, "Funding Performance:" + staff.performance, 14, FontWeights.Normal);
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


        private void ReportsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Dont run if we arent on the tab
            if(MainTabControl.SelectedIndex != 1) {
                return;
            }

            // Use SelectedItem instead of Text
            var selectedItem = PerformanceComboBox.SelectedItem;
            if (selectedItem == null) {
                return; // or handle the null case appropriately
            }

            string selectedPerformance = selectedItem.ToString();

            //Report report = new Report(); DONT NEED TO CREATE A NEW REPORT ;)
            List<Staff> filteredResearchers = ResearchController.Instance.FilterbyPerformance(MapStringToPerformance(selectedPerformance));

            // Display the filtered researchers
            ReportsListView.ItemsSource = filteredResearchers;
        }

        //helper function to map the string selection of report combo box to the enum
        private ResearcherPerformance MapStringToPerformance(string performance) {
            //Console.WriteLine(performance);
            ResearcherPerformance performanceEnum = ResearcherPerformance.POOR;
            /*< ComboBoxItem Content = "Poor" />
            < ComboBoxItem Content = "Below Expectation" />
            < ComboBoxItem Content = "Meeting Minimum" />
            < ComboBoxItem Content = "Star Performer" />*/
            if(performance.Contains("Poor")) {
                performanceEnum = ResearcherPerformance.POOR;
            }
            else if(performance.Contains("Below Expectation")) {
                performanceEnum = ResearcherPerformance.BELOW_EXPECTATIONS;
            }
            else if(performance.Contains("Meeting Minimum")) {
                performanceEnum = ResearcherPerformance.MEETING_MINIMUM;
            }
            else if(performance.Contains("Star Performer")) {
                performanceEnum = ResearcherPerformance.STAR_PERFORMER;
            }
            else {
                Console.WriteLine("Error: Could not map performance string to enum");
            }

            return performanceEnum;
        }

        /*// Click Copy Email button
        private void CopyEmail_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = PerformanceComboBox.SelectedItem;
            if (selectedItem == null)
            {
                return;
            }

            string selectedPerformance = selectedItem.ToString();

     
            List<Staff> filteredResearcher = ResearchController.Instance.FilterbyPerformance(MapStringToPerformance(selectedPerformance));

            // Gets email addresses for all staffs
            
            string[] emails = ResearchController.Instance.FetchResearcherEmails(filteredResearcher);
            

            if (emails.Length > 0)
            {
                string copy = string.Join(Environment.NewLine, emails);

                Clipboard.SetText(copy);
                _ = MessageBox.Show("Copied to clipboard");

            }
        }*/

        //Helper function to add a textblock to a stackpanel with given font size, text etc
        private void AddTextBlockToStackPanel(StackPanel sP, string text, int fontSize, FontWeight fontWeight) {
        TextBlock tb = new TextBlock();
        tb.Text = text;
        tb.FontSize = fontSize;
        tb.FontWeight = fontWeight;
        sP.Children.Add(tb);
        }

        //when search text is changed
        private void ResearcherListSearch_TextChanged(object sender, TextChangedEventArgs e) {

        }
    }
}