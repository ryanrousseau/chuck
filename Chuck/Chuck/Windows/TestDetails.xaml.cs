using System.Windows;
using Chuck.Contexts;
using Chuck.Models;

namespace Chuck.Windows
{
    /// <summary>
    ///     Interaction logic for TestDetails.xaml
    /// </summary>
    public partial class TestDetails
    {
        private readonly Thickness
            _CollapsedRunTest = new Thickness(40, 180, 0, 0),
            _ExpandedRunTest = new Thickness(40, 585, 0, 0);

        public TestDetails(TestDetailsModel detailsModel, bool isReadOnly = false)
        {
            InitializeComponent();

            if (isReadOnly)
            {
                btnAddTag.IsEnabled = false;
                btnRunTest.IsEnabled = false;
                scriptEditor.IsReadOnly = true;
                txtTestName.IsReadOnly = true;
            }
            
            DataContext = new TestDetailsContext(detailsModel);
            Title = string.Format(Title, detailsModel.TestName);

            foreach (var tag in detailsModel.Tags)
            {
                //: Add to interface, but how to display?
            }

            ExpScript.Collapsed += ExpScript_Collapsed;
            ExpScript.Expanded += ExpScript_Expanded;
        }

        private void ExpScript_Collapsed(object sender, RoutedEventArgs e)
        {
            Height = 256;
            btnRunTest.Margin = _CollapsedRunTest;
        }
        
        private void ExpScript_Expanded(object sender, RoutedEventArgs e)
        {
            Height = 665;
            btnRunTest.Margin = _ExpandedRunTest;
        }
    }
}
