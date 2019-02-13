using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.PSFlickr.PowershellModule.Cmdlets
{
    public abstract class FlickrCmdletsBase : PSCmdlet.PSCmdletPT
    {
        public string GetPath(string path)
        {
            string absolutePath;

            if (System.IO.Path.IsPathRooted(path))
            {
                absolutePath = path;
            }
            else
            {
                string currentDirectory = CurrentProviderLocation("FileSystem").ProviderPath;
                absolutePath = System.IO.Path.GetFullPath(System.IO.Path.Combine(currentDirectory, path));
            }
            return absolutePath;
        }
    }
}
