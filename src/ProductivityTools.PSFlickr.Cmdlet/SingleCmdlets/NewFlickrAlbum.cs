using ProductivityTools.PSFlickr.Application;
using ProductivityTools.PSFlickr.ApplicationClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.PSFlickr.SingleCmdlets
{

    [Cmdlet(VerbsCommon.New, "FlickrAlbum")]
    public class NewFlickrAlbum : System.Management.Automation.PSCmdlet
    {
        [Parameter(Position = 0)]
        public string Name { get; set; }

        protected override void ProcessRecord()
        {
            FlickrOperations autentication = FlickrOperationsFactory.GetFlickrOperations(WriteVerbose);
            CommonOperations commonOperations = new CommonOperations(WriteVerbose);
            var albums = commonOperations.CreateAlbum(this.Name);
            WriteObject(albums);
        }
    }
}
