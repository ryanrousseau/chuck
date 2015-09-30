using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Chuck.Commands;
using Chuck.Core;
using Chuck.Models;
using ICSharpCode.AvalonEdit.Document;

namespace Chuck.Contexts
{
    /// <summary>
    ///     DataContext for TestDetails.xaml
    /// </summary>
    public class TestDetailsContext : INotifyPropertyChanged
    {
        private ICommand _RunTest;

        /// <summary>
        ///     Required to update interface from datacontext
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        public string ScriptName
        {
            get { return DetailsModel.ScriptName; }
            set
            {
                if (DetailsModel.ScriptName != value)
                {
                    DetailsModel.ScriptName = value;
                    OnPropertyChanged("ScriptName");
                }
            }
        }

        public TextDocument Script
        {
            get { return DetailsModel.Script; }
            set
            {
                if (DetailsModel.Script != value)
                {
                    DetailsModel.Script = value;
                    OnPropertyChanged("Script");
                }
            }
        }

        public ICollection<string> Tags
        {
            get { return DetailsModel.Tags; }
            set
            {
                if (!DetailsModel.Tags.Equals(value))
                {
                    DetailsModel.Tags = value;
                    OnPropertyChanged("Tags");
                }
            }
        }

        public string TestName
        {
            get { return DetailsModel.TestName; }
            set
            {
                if (DetailsModel.TestName != value)
                {
                    DetailsModel.TestName = value;
                    OnPropertyChanged("TestName");
                }
            }
        }

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
                    var passed = testRunner.Run(new Test { Name = DetailsModel.TestName, Script = DetailsModel.Script.Text });
                });

            DetailsModel.Status = "";
        }

        /// <summary>
        ///     Occurs when a datacontext property changes.
        /// </summary>
        /// <param name="propertyName">The property to update</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
