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
            ResearcherListView.ItemsSource = ResearchController.Instance.FilteredResearcherNames;

            ResearchController.Instance.FilterbyPerformance(ResearcherPerformance.POOR);
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
                    ResearchController.Instance.FilterResearcherByLevel(selectedContent);
                }
            }
        }

        //when search text is changed
        private void ResearcherListSearch_TextChanged(object sender, TextChangedEventArgs e) {
            //get the text from the search box
            string searchText = ResearcherListSearch.Text;
            ResearchController.Instance.FilterResearcherByName(searchText);
        }

        //Refresh ResearcherDetail
        private void SetDetailsViewToResearcher(Researcher selectedResearcher) {
            //clear out the selected researcher
            ResearchController.Instance.ClearSelectedResearcher();
            ResearcherImage.Source = new BitmapImage(new System.Uri(selectedResearcher.photo));

            //remove children on SelectedResearcherDetails
            SelectedResearcherDetails.Children.Clear();
            SelectedResearcherSpecificDetails.Children.Clear();
            YearlyPublications.Children.Clear();

            PublicationsListView.ItemsSource = selectedResearcher.publications;

            //sort publications by year, same publications year sort alpabetically
            PublicationsListView.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("yearPublished", System.ComponentModel.ListSortDirection.Descending));

            var thisResearcherDetails = ResearchController.Instance.GenericResearcherDetails(selectedResearcher);
            var thisResearcherSpecificDetails = ResearchController.Instance.SpecificResearcherDetails(selectedResearcher);

            AddTextBlocksToStackPanel(SelectedResearcherDetails, ResearchController.Instance.GenericResearcherDetails(selectedResearcher), 14, FontWeights.Normal);
            AddTextBlocksToStackPanel(SelectedResearcherSpecificDetails, ResearchController.Instance.SpecificResearcherDetails(selectedResearcher), 14, FontWeights.Normal);
            AddTextBlocksToStackPanel(SelectedResearcherSpecificDetails, ResearchController.Instance.StaffOrStudentSpecificDetails(selectedResearcher), 14, FontWeights.Normal);

            AddTextBlockToStackPanel(YearlyPublications, "Yearly Publications", 14, FontWeights.Bold);

            //Yearly count
            foreach (string s in selectedResearcher.PublicationsCountByYear()) {
                if(s.Contains(": 0") == false) {
                    AddTextBlockToStackPanel(YearlyPublications, s, 14, FontWeights.Normal);
                }
            }
        }

        //select the list item in the publications list
        private void PublicationsListViewItem_Selected(object sender, SelectionChangedEventArgs e) {
            if (PublicationsListView.SelectedItem is Publication selectedPublication) {
                PublicationPopup popup = new PublicationPopup(selectedPublication);
                popup.Show();
            }
        }

        private void ReportsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Dont run if we arent on the tab
            if(MainTabControl.SelectedIndex != 1)return;

            // Use SelectedItem instead of Text
            var selectedItem = PerformanceComboBox.SelectedItem;
            if (selectedItem == null) return;

            ResearcherPerformance performance = MapStringToPerformance(selectedItem.ToString());

            //Filter the stafflist by performance
            ResearchController.Instance.FilterbyPerformance(performance);

            if(performance == ResearcherPerformance.POOR || performance == ResearcherPerformance.BELOW_EXPECTATIONS) {
                //sort descending
                ReportsListView.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("performance", System.ComponentModel.ListSortDirection.Descending));
            }
            else {
                //sort ascending
                ReportsListView.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("performance", System.ComponentModel.ListSortDirection.Ascending));
            }
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

        // Click Copy Email button
        private void CopyEmail_Click(object sender, RoutedEventArgs e) {
            var selectedItem = PerformanceComboBox.SelectedItem;
            if (selectedItem == null) {
                return;
            }

            string selectedPerformance = selectedItem.ToString();

            // Gets email addresses for all staffs

            string[] emails = ResearchController.Instance.FetchResearcherEmails();


            if (emails.Length > 0) {
                string copy = string.Join(Environment.NewLine, emails);

                Clipboard.SetText(copy);
                _ = MessageBox.Show("Copied to clipboard");
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

        //Helper function that adds text blocks to a stack panel given an array of string.
        private void AddTextBlocksToStackPanel(StackPanel sP, string[] text, int fontSize, FontWeight fontWeight) {
            foreach(string s in text) {
                AddTextBlockToStackPanel(sP, s, fontSize, fontWeight);
            }
        }

        private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            //if the index is tab 1
            if(MainTabControl.SelectedIndex == 1) {
                // Display the filtered researchers
                ReportsListView.ItemsSource = ResearchController.Instance.FilteredStaffPerformanceList;
            }
        }
    }
}