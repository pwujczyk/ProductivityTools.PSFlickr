using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.PSFlickr.PowershellModule.SingleCmdlets
{
    [Cmdlet(VerbsCommon.Remove, "FlickrAlbum")]
    public class DeleteFlickrAlbum : FlickrSingleCmdlet
    {
        [Parameter(Position = 0)]
        public string Name { get; set; }

        [Parameter(Position = 1)]
        public SwitchParameter RemoveAlsoPhotosInside;

        protected override void ProcessRecord()
        {
            base.FlickrOperation.DeleteAlbum(this.Name, this.RemoveAlsoPhotosInside.IsPresent);
            base.ProcessRecord();
        }
    }
}
