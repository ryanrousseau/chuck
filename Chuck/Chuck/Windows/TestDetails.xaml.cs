using System.Collections.ObjectModel;
using System.Windows;
using Chuck.Contexts;
using Chuck.Models;
using Chuck.Helpers;
using Chuck.Core.Git;

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

        private ObservableCollection<TestDetailsContext> _TestDetails = new ObservableCollection<TestDetailsContext>();
        private RepositoryInfo _Repository;
        private string _TestName;

        public TestDetails(TestDetailsModel detailsModel, RepositoryInfo repository, bool isReadOnly = false)
        {
            InitializeComponent();

            if (isReadOnly)
            {
                btnAddTag.IsEnabled = false;
                btnRunTest.IsEnabled = false;
                scriptEditor.IsReadOnly = true;
                txtScriptName.IsReadOnly = true;
            }

            _Repository = repository;
            _TestName = detailsModel.TestName;
            _TestDetails.Add(new TestDetailsContext(detailsModel));
            DataContext = _TestDetails;

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

        private void rectSave_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _TestDetails[0].DetailsModel.GetScriptTextFromAvalonDocument();
            IOHelper.CreateLocalDirectoryIfNotExists(string.Format("Projects\\{0}\\{1}", _Repository.Name, _TestName));
            IOHelper.DeleteLocalFileIfExists(string.Format("Projects\\{0}\\{1}\\{2}.Chuck", _Repository.Name, _TestName, txtScriptName.Text));
            JsonHelper<TestDetailsModel>.SaveToFile(_TestDetails[0].DetailsModel, string.Format("Projects\\{0}\\{1}\\{2}.Chuck", _Repository.Name, _TestName, txtScriptName.Text));
        }
    }
}
