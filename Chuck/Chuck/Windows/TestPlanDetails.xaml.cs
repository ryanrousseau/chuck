using System.Windows;
using Chuck.Models;

namespace Chuck.Windows
{
    /// <summary>
    ///     Interaction logic for TestPlanDetails.xaml
    /// </summary>
    public partial class TestPlanDetails
    {
        public TestPlanDetails()
        {
            InitializeComponent();

            ExpResults.Collapsed += ExpResults_Collapsed;
            ExpResults.Expanded += ExpResults_Expanded;
        }

        public TestPlanDetails(TestPlanDetailsModel detailsModel)
        {
            InitializeComponent();
            DataContext = detailsModel;

            foreach (var test in detailsModel.IncludedTests)
            {
                //: Add tests to lbTests
            }
        }

        /// <summary>
        ///     Handles the Collapsed event of the ExpResults control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ExpResults_Collapsed(object sender, RoutedEventArgs e)
        {
            Height = 466;
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
    }
}
