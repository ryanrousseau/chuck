using System;
using System.Windows;
using Chuck.Core.Git;
using LibGit2Sharp;

namespace Chuck.Windows
{
    /// <summary>
    /// Interaction logic for GitCredentials.xaml
    /// </summary>
    public partial class GitCredentials
    {
        public RepositoryInfo RepoInfo { get; set; }

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

        public GitCredentials(bool previousLoginInfoIncorrect = false)
        {
            InitializeComponent();

            lblPreviousFail.Visibility = previousLoginInfoIncorrect
                ? Visibility.Visible
                : Visibility.Hidden;
        }

        private void BtnSubmit_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
