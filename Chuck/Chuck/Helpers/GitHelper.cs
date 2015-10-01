using System.Windows;
using Chuck.Core.Git.Github;
using Chuck.Windows;
using LibGit2Sharp;

namespace Chuck.Helpers
{
    /// <summary>
    ///     A helper class for items related to git
    /// </summary>
    public class GitHelper
    {
        /// <summary>
        ///     Get github credentials from the user for use with pull/push.
        /// </summary>
        /// <param name="previousBadCredentials"></param>
        /// <returns></returns>
        public static UsernamePasswordCredentials GetGitCredentials(bool previousBadCredentials = false)
        {
            var gc = new GitCredentials(previousBadCredentials);
            gc.ShowDialog();

            return gc.UserCredentials;
        }

        /// <summary>
        ///     Notify the user that their github credentials were bad.
        /// </summary>
        public static void NotifyBadCredentials()
        {
            MessageBox.Show(
                "During your latest push/pull we received a 401 error. This is generally due to an invalid password.",
                "Bad Credentials",
                MessageBoxButton.OK,
                MessageBoxImage.Error
                );
        }

        /// <summary>
        ///     git add *
        /// </summary>
        /// <param name="hubimusMaximus">the github repo we're working with</param>
        public static void StageAll(Github hubimusMaximus)
        {
            foreach (var file in hubimusMaximus.Status())
            {
                hubimusMaximus.Add(file.Key, file.Value);
            }
        }
    }
}
