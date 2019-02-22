using ProductivityTools.PSFlickr.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.PSFlickr.Cmdlet.SingleCmdlets
{

    [Cmdlet(VerbsCommon.Get, "FlickrAlbums")]
    public class GetFlickrAlbums : System.Management.Automation.PSCmdlet
    {
        protected override void ProcessRecord()
        {
            FlickrOperations autentication = FlickrOperationsFactory.GetFlickrOperations();
            var albums = autentication.GetAlbumsName();
            WriteObject(albums);
        }
    }
}
