using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Chuck.Commands;
using Chuck.Models;

namespace Chuck.Contexts
{
    public class TestPlanDetailsContext : INotifyPropertyChanged
    {
        private ICommand
            _RunTestPlan,
            _SaveTestPlan;

        /// <summary>
        ///     Required to update interface from datacontext
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        public ICollection<TestDetailsModel> IncludedTests
        {
            get { return DetailsModel.IncludedTests; }
            set
            {
                if (!DetailsModel.IncludedTests.Equals(value))
                {
                    DetailsModel.IncludedTests = value;
                    OnPropertyChanged("IncludedTests");
                }
            }
        }

        public string TestPlanName
        {
            get { return DetailsModel.TestPlanName; }
            set
            {
                if (DetailsModel.TestPlanName != value)
                {
                    DetailsModel.TestPlanName = value;
                    OnPropertyChanged("TestPlanName");
                }
            }
        }

        public string TestPlanResults
        {
            get { return DetailsModel.TestPlanResults; }
            set
            {
                if (DetailsModel.TestPlanResults != value)
                {
                    DetailsModel.TestPlanResults = value;
                    OnPropertyChanged("TestPlanResults");
                }
            }
        }

        /// <summary>
        ///     Command that will call ExecuteTestPlan
        /// </summary>
        public ICommand RunTestPlan
        {
            get { return _RunTestPlan ?? (_RunTestPlan = new RelayedCommand(p => ExecuteTestPlan(), t => true)); }
        }

        /// <summary>
        ///     Command that will call ExecuteTestPlan
        /// </summary>
        public ICommand SaveTestPlan
        {
            get { return _SaveTestPlan ?? (_SaveTestPlan = new RelayedCommand(p => SaveTestPlanToFile(), t => true)); }
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

        /// <summary>
        ///     Occurs when a datacontext property changes.
        /// </summary>
        /// <param name="propertyName">The property to update</param>
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///     Save the testplan to file with JSON
        /// </summary>
        private void SaveTestPlanToFile()
        {

        }    
    }
}
