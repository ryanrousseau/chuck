using System.IO;

namespace Chuck.Core.Startup
{
    public class ChuckStartup : IChuckStartup
    {
        public ChuckStartup()
        {
            CreateDirectories();
        }

        public void CreateDirectories()
        {
            if (!Directory.Exists(Directory.GetCurrentDirectory() + @"\Projects"))
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\Projects");
        }
    }
}
