using System.Collections.ObjectModel;
using System.Windows;
using Chuck.Contexts;
using Chuck.Core.Git;
using Chuck.Models;
using Chuck.Helpers;

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

        private readonly RepositoryInfo _Repository;
        private readonly ObservableCollection<TestDetailsContext> _TestDetails = new ObservableCollection<TestDetailsContext>();
        private readonly string _TestName;

        /// <summary>
        ///     When constructing a test details window, we need a test details model alongside project(repo) info
        ///     so we know where to save updates.
        /// </summary>
        /// <param name="detailsModel">The test details model.</param>
        /// <param name="repository">The project(Repo) info.</param>
        /// <param name="isReadOnly">Should this not be editable?</param>
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


        /// <summary>
        ///     When the Expander is nonexistant or unexpanded, we have extra space that needs to be hidden.
        ///     We also need to relocate the RunTest button to the new bottom of the window.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ExpScript_Collapsed(object sender, RoutedEventArgs e)
        {
            Height = 256;
            btnRunTest.Margin = _CollapsedRunTest;
        }

        /// <summary>
        ///     When the Expander is expanded, we need an extra 400 pixels of space to contain/show the Avalon TextDocument.
        ///     We also need to relocate the RunTest button to the bottom of the window.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ExpScript_Expanded(object sender, RoutedEventArgs e)
        {
            Height = 665;
            btnRunTest.Margin = _ExpandedRunTest;
        }

        /// <summary>
        ///     When the user clicks save, we save the test.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void rectSave_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //: TODO - potentially move this to the context via a command

            _TestDetails[0].DetailsModel.GetScriptTextFromAvalonDocument();
            IOHelper.CreateLocalDirectoryIfNotExists(string.Format("Projects\\{0}\\{1}", _Repository.Name, _TestName));
            IOHelper.DeleteLocalFileIfExists(string.Format("Projects\\{0}\\{1}\\{2}.Chuck", _Repository.Name, _TestName, txtScriptName.Text));
            JsonHelper<TestDetailsModel>.SaveToFile(_TestDetails[0].DetailsModel, string.Format("Projects\\{0}\\{1}\\{2}.Chuck", _Repository.Name, _TestName, txtScriptName.Text));
        }
    }
}
