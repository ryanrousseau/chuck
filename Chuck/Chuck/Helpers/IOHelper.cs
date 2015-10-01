using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chuck.Helpers
{
    public class IOHelper
    {
        public static void CreateLocalDirectoryIfNotExists(string directory)
        {
            var pwd = Directory.GetCurrentDirectory();

            if (!Directory.Exists(string.Format(@"{0}\{1}", pwd, directory)))
                Directory.CreateDirectory(string.Format(@"{0}\{1}", pwd, directory));
        }
    }
}
