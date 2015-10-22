using System.Collections.Generic;
using LibGit2Sharp;

namespace Chuck.Core.Git.Github
{
    /// <summary>
    ///     Github interface.
    /// </summary>
    public interface IGithub
    {
        /// <summary>
        ///     Add and stage specified file.
        /// </summary>
        /// <param name="fileName">Name of the file to be added to Lib2GitSharp, then staged ready for commit.</param>
        /// <param name="status">The git status of the file.</param>
        void Add(string fileName, FileStatus status);

        /// <summary>
        ///     Clone remote repo locally.
        /// </summary>
        void CloneRepository();

        /// <summary>
        ///     Commit changes to local repo. Use Github.Add first.
        /// </summary>
        void Commit();

        /// <summary>
        ///     "git status" in a nutshell.
        /// </summary>
        /// <returns>Dictionary with a KEY:VALUE structure of FileName:FileStatus</returns>
        IDictionary<string, FileStatus> Status();

        /// <summary>
        ///     Pull changes from remote to local, cross fingers no merge pls.
        /// </summary>
        /// <param name="credentials">User:Pass credentials</param>
        void Pull(UsernamePasswordCredentials credentials);

        /// <summary>
        ///     Push local changes to remote, no merge conflicts pls....
        /// </summary>
        /// <param name="credentials">The credentials of the user who is pushing this.</param>
        string Push(UsernamePasswordCredentials credentials);
    }
}
