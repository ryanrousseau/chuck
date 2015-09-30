using System.Threading;
using System.Threading.Tasks;
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
        public async void ExecuteTest()
        {
            var testRunner = new TestRunner();

            TaskScheduler uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            DetailsModel.Status = "Running";
            DetailsModel.ScriptName = "Foo";
            
            await Task.Run(() =>
                {
                    var passed = testRunner.Run(new Test { Name = DetailsModel.TestName, Script = DetailsModel.ScriptText });
                });

            DetailsModel.Status = "";
        }
    }
}
