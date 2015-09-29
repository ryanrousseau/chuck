using System.Windows;
using System.Windows.Input;
using Chuck.Commands;
using Chuck.Models;

namespace Chuck.Contexts
{
    public class TestPlanDetailsContext
    {
        private ICommand _RunTestPlan;

        /// <summary>
        ///     Command that will call ExecuteTestPlan
        /// </summary>
        public ICommand RunTestPlan
        {
            get { return _RunTestPlan ?? (_RunTestPlan = new RelayedCommand(p => ExecuteTestPlan(), t => true)); }
        }

        /// <summary>
        ///     The TestPlanDetailsModel associated with this context
        /// </summary>
        public TestPlanDetailsModel DetailsModel { get; set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TestPlanDetailsContext"/> class.
        /// </summary>
        /// <param name="detailsModel">The TestPlanDetails model.</param>
        public TestPlanDetailsContext(TestPlanDetailsModel detailsModel)
        {
            DetailsModel = detailsModel;
        }

        /// <summary>
        ///     Gets called when the user clicks run from the TestPlanDetails window
        /// </summary>
        private void ExecuteTestPlan()
        {
            MessageBox.Show("hi");
        }
    }
}
