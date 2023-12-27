using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KIT206_Assignment_01 {
    /// <summary>
    /// Interaction logic for PublicationPopup.xaml
    /// </summary>
    public partial class PublicationPopup : Window {
        public PublicationPopup(Publication p) {
            InitializeComponent();

            SetDetailsViewToPublication(p);
        }

        private void SetDetailsViewToPublication(Publication selectedPublication) {
            PublicationDetails.Children.Clear();

            //Generic details
            AddTextBlockToStackPanel(PublicationDetails, selectedPublication.DOI, 14, FontWeights.Normal);
            AddTextBlockToStackPanel(PublicationDetails, selectedPublication.title.ToString().ToLower(), 14, FontWeights.Normal);
            AddTextBlockToStackPanel(PublicationDetails, selectedPublication.ranking.ToString(), 14, FontWeights.Normal);
            AddTextBlockToStackPanel(PublicationDetails, selectedPublication.type.ToString(), 14, FontWeights.Normal);
            AddTextBlockToStackPanel(PublicationDetails, selectedPublication.author, 14, FontWeights.Normal);
            AddTextBlockToStackPanel(PublicationDetails, selectedPublication.yearPublished.ToString(), 14, FontWeights.Normal);
            AddTextBlockToStackPanel(PublicationDetails, selectedPublication.citeLink, 14, FontWeights.Normal);
            AddTextBlockToStackPanel(PublicationDetails, selectedPublication.availability.ToString(), 14, FontWeights.Normal);
            AddTextBlockToStackPanel(PublicationDetails, selectedPublication.Age.ToString() + " years", 14, FontWeights.Normal);
        }



        //Helper function to add a textblock to a stackpanel with given font size, text etc
        private void AddTextBlockToStackPanel(StackPanel sP, string text, int fontSize, FontWeight fontWeight) {
            TextBlock tb = new TextBlock();
            tb.Text = text;
            tb.FontSize = fontSize;
            tb.FontWeight = fontWeight;
            sP.Children.Add(tb);
        }
    }
}
