using ProductivityTools.PSFlickr.PowershellModule.Cmdlets.UpdateSet.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.PSFlickr.PowershellModule.Cmdlets.UpdateSet
{
    [Cmdlet("Update", "FlickrSet")]
    public class UpdateFlickrSet : FlickrCmdletsBase
    {
        [Parameter(Mandatory = false, Position = 0)]
        public string Directory { get; set; }

        public UpdateFlickrSet()
        {
            this.AddCommand(new UpdateAlbumsAndPushPhotos(this));
        }

        protected override void ProcessRecord()
        {
            base.ProcessCommands();
            base.ProcessRecord();
        }
    }
}
