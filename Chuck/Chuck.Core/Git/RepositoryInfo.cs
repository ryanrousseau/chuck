using System;

namespace Chuck.Core.Git
{
    public class RepositoryInfo
    {
        public Uri HttpsLink { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - {1}", Name, HttpsLink.OriginalString);
        }
    }
}
