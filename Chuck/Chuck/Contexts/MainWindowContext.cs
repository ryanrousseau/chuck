using System.Collections.Generic;
using System.ComponentModel;
using Chuck.Models;

namespace Chuck.Contexts
{
    /// <summary>
    ///     DataContext for MainWindow.xaml
    /// </summary>
    public class MainWindowContext : INotifyPropertyChanged
    {
        private bool _Enabled;
        private IList<TestDetailsModel> _Tests;

        /// <summary>
        ///     Required to update interface from datacontext
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Control whether or not certain items on the form are enabled.
        /// </summary>
        public bool Enabled
        {
            get { return _Enabled; }
            set
            {
                if (_Enabled != value)
                {
                    _Enabled = value;
                    OnPropertyChanged("Enabled");
                }
            }
        }

        /// <summary>
        ///     A list of tests.
        /// </summary>
        public IList<TestDetailsModel> Tests
        {
            get { return _Tests; }
            set
            {
                if (_Tests != value)
                {
                    _Tests = value;
                    OnPropertyChanged("Tests");
                }
            }
        }

        /// <summary>
        ///     Create a new instance of MainWindowContext
        /// </summary>
        public MainWindowContext()
        {
            _Tests = new List<TestDetailsModel>();
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
