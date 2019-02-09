using ProductivityTools.PSFlickr.Application.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.PSFlickr.PowershellModule.SingleCmdlets
{
    [Cmdlet(VerbsCommon.Add, "FlickrPhoto")]
    public class AddFlickrPhoto : System.Management.Automation.PSCmdlet
    {
        [Parameter(Position = 0)]
        public string Path { get; set; }

        [Parameter(Position = 1)]
        public string Album { get; set; }

        protected override void ProcessRecord()
        {
            FlickrAutentication autentication = new FlickrAutentication();
            string absolutePath;

            if (System.IO.Path.IsPathRooted(Path))
            {
                absolutePath = Path;
            }
            else
            {
                string currentDirectory = CurrentProviderLocation("FileSystem").ProviderPath;
                absolutePath = System.IO.Path.GetFullPath(System.IO.Path.Combine(currentDirectory, Path));
            }

            var photoId = autentication.AddPhoto(absolutePath);
            WriteObject(photoId);
        }
    }
}
