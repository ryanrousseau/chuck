using System.Windows;
using Chuck.Windows;
using LibGit2Sharp;

namespace Chuck.Helpers
{
    public class GitHelper
    {
        public static UsernamePasswordCredentials GetGitCredentials(bool previousBadCredentials = false)
        {
            var gc = new GitCredentials(previousBadCredentials);
            gc.ShowDialog();

            return gc.UserCredentials;
        }

        public static void NotifyBadCredentials()
        {
            MessageBox.Show(
                "During your latest push/pull we received a 401 error. This is generally due to an invalid password.",
                "Bad Credentials",
                MessageBoxButton.OK,
                MessageBoxImage.Error
                );
        }
    }
}
