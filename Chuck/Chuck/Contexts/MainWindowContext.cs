using Chuck.Models;
using System.Collections.Generic;
using System.ComponentModel;
namespace Chuck.Contexts
{
    /// <summary>
    ///     DataContext for MainWindow.xaml
    /// </summary>
    public class MainWindowContext : INotifyPropertyChanged
    {
        /// <summary>
        ///     Required to update interface from datacontext
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        public string Flair { get; set; }

        private bool _Enabled { get; set; }
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

        private IList<TestDetailsModel> _Tests;
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

        public MainWindowContext()
        {
            Flair = "Chuck Testa";
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
