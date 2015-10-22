using System;
using System.Collections.Generic;
using System.Linq;
using LibGit2Sharp;

namespace Chuck.Core.Git.Github
{
    /// <summary>
    ///     Github interface for Lib2GitSharp
    /// </summary>
    public class Github : IGithub
    {
        private readonly string _LocalRepo;
        private readonly RepositoryInfo _RepoInfo;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Github"/> class.
        /// </summary>
        /// <param name="repositoryInfo">The repository information.</param>
        public Github(RepositoryInfo repositoryInfo)
        {
            _RepoInfo = repositoryInfo;
            _LocalRepo = string.Format("Projects\\{0}", _RepoInfo.Name);
        }

        /// <summary>
        ///     Add and stage specified file.
        /// </summary>
        /// <param name="fileName">Name of the file to be added to Lib2GitSharp, then staged ready for commit.</param>
        public void Add(string fileName, FileStatus status)
        {
            using (var repo = new Repository(_LocalRepo))
            {
                if (status != FileStatus.Removed && status != FileStatus.Missing)
                {
                    repo.Index.Add(fileName);
                }
                repo.Stage(fileName);
            }
        }

        /// <summary>
        ///     Clone remote repo locally.
        /// </summary>
        public void CloneRepository()
        {
            Repository.Clone(_RepoInfo.HttpsLink.OriginalString, _LocalRepo);
        }

        /// <summary>
        ///     Commit changes to local repo. Use Github.Add first.
        ///     TODO: Possibly add ability for users to specify msg?
        /// </summary>
        public void Commit()
        {
            using (var repo = new Repository(_LocalRepo))
            {
                repo.Commit("Update via Chuck interface.");
            }
        }

        /// <summary>
        ///     "git status" in a nutshell.
        /// </summary>
        /// <returns>Dictionary with a KEY:VALUE structure of FileName:FileStatus</returns>
        public IDictionary<string, FileStatus> Status()
        {
            var files = new Dictionary<string, FileStatus>();

            using (var repo = new Repository(_LocalRepo))
            {
                foreach (var file in repo.RetrieveStatus().Where(file => file.State != FileStatus.Ignored))
                {
                    files.Add(file.FilePath, file.State);
                }
            }

            return files;
        }

        /// <summary>
        ///     Pull changes from remote to local, cross fingers no merge pls.
        /// </summary>
        /// <param name="credentials">User:Pass credentials</param>
        public void Pull(UsernamePasswordCredentials credentials)
        {
            using (var repo = new Repository(_LocalRepo))
            {
                var pullOptions = new PullOptions
                {
                    FetchOptions = new FetchOptions
                    {
                        CredentialsProvider = (url, username, types) => credentials
                    }
                };

                repo.Network.Pull(new Signature("Chuck Interface","--", new DateTimeOffset(DateTime.Now)), pullOptions);
            }
        }

        /// <summary>
        ///     Push local changes to remote, no merge conflicts pls....
        /// </summary>
        /// <param name="credentials">The credentials of the user who is pushing this.</param>
        public string Push(UsernamePasswordCredentials credentials)
        {
            using (var repo = new Repository(_LocalRepo))
            {
                var pushOptions = new PushOptions
                {
                    CredentialsProvider = (url, username, types) => credentials   
                };

                //: TODO - Add support for choosing which branch
                try
                {
                    repo.Network.Push(repo.Branches["master"], pushOptions);
                }
                catch (LibGit2SharpException e)
                {
                    return e.Message;
                }
            }

            //: No errors!
            return string.Empty;
        }
    }
}
