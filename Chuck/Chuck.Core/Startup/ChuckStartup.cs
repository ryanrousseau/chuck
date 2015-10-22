using System.IO;

namespace Chuck.Core.Startup
{
    /// <summary>
    ///     Items that need to be run at Chuck startup
    /// </summary>
    public class ChuckStartup : IChuckStartup
    {
        /// <summary>
        ///     Create an instance of ChuckStartup
        /// </summary>
        public ChuckStartup()
        {
            CreateDirectories();
        }

        /// <summary>
        ///     Create the directories Chuck needs
        /// </summary>
        public void CreateDirectories()
        {
            if (!Directory.Exists(Directory.GetCurrentDirectory() + @"\Projects"))
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\Projects");
        }
    }
}
