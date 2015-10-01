namespace Chuck.Core.Startup
{
    /// <summary>
    ///     Items that need to be run at ChuckStartup
    /// </summary>
    public interface IChuckStartup
    {
        /// <summary>
        ///     Create the directories that Chuck needs.
        /// </summary>
        void CreateDirectories();
    }
}
