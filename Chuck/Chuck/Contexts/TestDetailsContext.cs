using System.Windows;
using System.Windows.Input;
using Chuck.Commands;
using Chuck.Core;
using Chuck.Models;

namespace Chuck.Contexts
{
    /// <summary>
    ///     DataContext for TestDetails.xaml
    /// </summary>
    public class TestDetailsContext
    {
        private ICommand _RunTest;

        /// <summary>
        ///     Command that will call ExecuteTest
        /// </summary>
        public ICommand RunTest
        {
            get { return _RunTest ?? (_RunTest = new RelayedCommand(p => ExecuteTest(), t => true)); }
        }

        /// <summary>
        ///     The TestDetailsModel associated with this context
        /// </summary>
        public TestDetailsModel DetailsModel { get; set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TestDetailsContext"/> class.
        /// </summary>
        /// <param name="detailsModel">The TestDetails model.</param>
        public TestDetailsContext(TestDetailsModel detailsModel)
        {
            DetailsModel = detailsModel;
        }

        /// <summary>
        ///     Gets called when the user clicks run from TestDetails window
        /// </summary>
        public void ExecuteTest()
        {
            MessageBox.Show("hi");
            var scriptHost = new ScriptHost();
            scriptHost.Execute(DetailsModel.ScriptText);
        }
    }
}
