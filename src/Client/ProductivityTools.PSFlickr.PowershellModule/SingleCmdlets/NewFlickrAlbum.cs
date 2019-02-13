using ProductivityTools.PSFlickr.Application.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.PSFlickr.PowershellModule.SingleCmdlets
{

    [Cmdlet(VerbsCommon.New, "FlickrAlbum")]
    public class NewFlickrAlbum : System.Management.Automation.PSCmdlet
    {
        [Parameter(Position = 0)]
        public string Name { get; set; }

        protected override void ProcessRecord()
        {
            FlickrOperations autentication = FlickrOperationsFactory.GetFlickrOperations();
            var albums = autentication.CreateAlbum(this.Name);
            WriteObject(albums);
        }
    }
}
