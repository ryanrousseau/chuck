using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Chuck.Contexts;
using Chuck.Models;

namespace Chuck.Windows
{
    /// <summary>
    ///     Interaction logic for TestPlanDetails.xaml
    /// </summary>
    public partial class TestPlanDetails
    {
        private TestPlanDetailsModel _DetailsModel;
        private ObservableCollection<TestPlanDetailsContext> _TestPlanDetails = new ObservableCollection<TestPlanDetailsContext>(); 

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="detailsModel">The model we will use to make the datacontext.</param>
        public TestPlanDetails(TestPlanDetailsModel detailsModel)
        {
            InitializeComponent();
            ExpResults.Collapsed += ExpResults_Collapsed;
            ExpResults.Expanded += ExpResults_Expanded;
            lbIncludedTests.MouseDoubleClick += IncludedTests_MouseDoubleClick;
            Title = string.Format(Title, detailsModel.TestPlanName);

            _DetailsModel = detailsModel;
            _TestPlanDetails.Add(new TestPlanDetailsContext(_DetailsModel));
            DataContext = _TestPlanDetails;

            foreach (var test in _DetailsModel.IncludedTests)
            {
                lbIncludedTests.Items.Add(test.TestName);
            }
        }

        /// <summary>
        ///     Handles the Collapsed event of the ExpResults control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ExpResults_Collapsed(object sender, RoutedEventArgs e)
        {
            Height = 472;
        }

        /// <summary>
        ///     Handles the Expanded event of the ExpResults control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ExpResults_Expanded(object sender, RoutedEventArgs e)
        {
            Height = 726;
        }

        /// <summary>
        ///     Handles the MouseDoubleClick event of the IncludedTests control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void IncludedTests_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            (new TestDetails(
                (_DetailsModel.IncludedTests.First(t => t.TestName == lbIncludedTests.SelectedItem.ToString())),true) //: Readonly?
                ).ShowDialog();
        }

        /// <summary>
        ///     Handles the OnClick event of the BtnRunTest control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void BtnRunTest_OnClick(object sender, RoutedEventArgs e)
        {
            Height = 726;
        }
    }
}
