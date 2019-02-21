using ProductivityTools.PSFlickr.Application.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.PSFlickr.PowershellModule.SingleCmdlets
{
    public abstract class FlickrSingleCmdletBase: System.Management.Automation.PSCmdlet
    {
        protected FlickrOperations FlickrOperation;

        public FlickrSingleCmdletBase()
        {
            FlickrOperation = FlickrOperationsFactory.GetFlickrOperations(WriteVerbose);
        }
    }
}
