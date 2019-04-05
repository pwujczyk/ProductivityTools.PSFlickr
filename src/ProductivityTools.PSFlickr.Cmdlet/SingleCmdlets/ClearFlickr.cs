using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.PSFlickr.SingleCmdlets
{
    [Cmdlet(VerbsCommon.Clear, "Flickr")]
    public class ClearFickr : FlickrSingleCmdletBase
    {
        protected override void ProcessRecord()
        {
            base.FlickrOperation.ClearFlickr();
            base.ProcessRecord();
        }
    }
}
