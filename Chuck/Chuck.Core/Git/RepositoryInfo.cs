using System;

namespace Chuck.Core.Git
{
    /// <summary>
    ///     Model for repository info
    /// </summary>
    public class RepositoryInfo
    {
        /// <summary>
        ///     The HTTPS link for this repository.
        /// </summary>
        public Uri HttpsLink { get; set; }

        /// <summary>
        ///     The local name given to this repository.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Human friendly version of repository info.
        /// </summary>
        /// <returns>Human friendly version of repository info.</returns>
        public override string ToString()
        {
            return string.Format("{0} - {1}", Name, HttpsLink.OriginalString);
        }
    }
}
