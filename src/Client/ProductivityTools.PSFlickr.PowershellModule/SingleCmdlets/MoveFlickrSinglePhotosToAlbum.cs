using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.PSFlickr.PowershellModule.SingleCmdlets
{
    [Cmdlet(VerbsCommon.Move, "FlickrSinglePhotosToAlbum")]
    public class MoveFlickrSinglePhotosToAlbum : FlickrSingleCmdlet
    {
        [Parameter(Position = 0)]
        public string Name { get; set; }

        protected override void ProcessRecord()
        {
            base.FlickrOperation.MoveSinglePhotosToAlbum(this.Name);
            base.ProcessRecord();
        }
    }
}
