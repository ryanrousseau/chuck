using System.Windows;
using LibGit2Sharp;

namespace Chuck.Windows
{
    /// <summary>
    /// Interaction logic for GitCredentials.xaml
    /// </summary>
    public partial class GitCredentials
    {
        /// <summary>
        ///     The Credentials that the user just entered.
        /// </summary>
        public UsernamePasswordCredentials UserCredentials
        {
            get
            {
                return new UsernamePasswordCredentials
                {
                    Password = txtPass.Password,
                    Username = txtUser.Text
                };
            }
        }

        /// <summary>
        ///     Once this button is clicked, we have their credentials. Close window to allow caller to access credentials.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnSubmit_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        ///     Create a new GitCredentials object
        /// </summary>
        /// <param name="previousLoginInfoIncorrect">Was the previous info incorrect? If so we can display an error label.</param>
        public GitCredentials(bool previousLoginInfoIncorrect = false)
        {
            InitializeComponent();

            lblPreviousFail.Visibility = previousLoginInfoIncorrect
                ? Visibility.Visible
                : Visibility.Hidden;
        }
    }
}
