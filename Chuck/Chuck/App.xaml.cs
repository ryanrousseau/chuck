using System.IO;
using Chuck.Core.Startup;

namespace Chuck
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public App()
        {
            var cs = new ChuckStartup();
            cs.CreateDirectories();
        }
    }
}
