using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingAndConfigurationTest.Utils
{
    class FileFolderUtils
    {
        public static string addAndCreateFolder(string originalFolder, string addedPath)
        {
            string newPath = originalFolder + addedPath;
            if (!Directory.Exists(newPath))
                Directory.CreateDirectory(newPath);
            return newPath;
        }
    }
}
