using System.Windows;

namespace Chuck.Windows
{
    /// <summary>
    /// Interaction logic for GetInput.xaml
    /// </summary>
    public partial class GetInput
    {
        /// <summary>
        ///     The input obtained from the user
        /// </summary>
        public string Input { get; set; }

        /// <summary>
        ///     Create a new instance of GetInput
        /// </summary>
        /// <param name="inputName">What should the label state this input is named?</param>
        public GetInput(string inputName)
        {
            InitializeComponent();
            lblName.Content = inputName;
        }

        /// <summary>
        ///     When they click submit, input was obtained and we're ready to allow the caller to access it.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Input = txtInput.Text;
            Close();
        }
    }
}
