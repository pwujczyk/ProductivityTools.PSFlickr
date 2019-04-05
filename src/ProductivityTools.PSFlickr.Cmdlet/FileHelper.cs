using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.PSFlickr
{
    public static class FileHelper
    {
        public static string GetPath(this System.Management.Automation.PSCmdlet cmdlet, string path)
        {
            string absolutePath;

            if (System.IO.Path.IsPathRooted(path))
            {
                absolutePath = path;
            }
            else
            {
                string currentDirectory = cmdlet.CurrentProviderLocation("FileSystem").ProviderPath;
                absolutePath = System.IO.Path.GetFullPath(System.IO.Path.Combine(currentDirectory, path));
            }
            return absolutePath;
        }
    }
}
