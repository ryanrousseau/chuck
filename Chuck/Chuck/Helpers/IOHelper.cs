using System.IO;

namespace Chuck.Helpers
{
    /// <summary>
    ///     Generic helpers that are related to IO operations
    /// </summary>
    public class IOHelper
    {
        /// <summary>
        ///     Create a local directory (e.g., directory="foo")
        ///     would create Directory.CurrentDirectory\foo
        /// </summary>
        /// <param name="directory">the directory to create</param>
        public static void CreateLocalDirectoryIfNotExists(string directory)
        {
            var pwd = Directory.GetCurrentDirectory();

            if (!Directory.Exists(string.Format(@"{0}\{1}", pwd, directory)))
                Directory.CreateDirectory(string.Format(@"{0}\{1}", pwd, directory));
        }

        /// <summary>
        ///     Delete a local file (e.g., file="foo) deletes Directory.CurrentDirectory\foo
        /// </summary>
        /// <param name="localFilePath">The file to delete</param>
        public static void DeleteLocalFileIfExists(string localFilePath)
        {
            var pwd = Directory.GetCurrentDirectory();

            if(File.Exists(string.Format("{0}\\{1}", pwd, localFilePath)))
            {
                File.Delete(string.Format("{0}\\{1}", pwd, localFilePath));
            }
        }

        /// <summary>
        ///     Does a local directory exist?
        /// </summary>
        /// <param name="directory">the directory to check if exists</param>
        /// <returns>True if the directory exists</returns>
        public static bool LocalDirectoryExists(string directory)
        {
            return Directory.Exists(string.Format(@"{0}\{1}", Directory.GetCurrentDirectory(), directory));
        }
    }
}
