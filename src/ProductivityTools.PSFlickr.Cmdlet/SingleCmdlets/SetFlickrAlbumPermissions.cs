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
    [Cmdlet(VerbsCommon.Set, "FlickrAlbumPermissions")]
    public class SetFlickrAlbumPermissions :  System.Management.Automation.PSCmdlet
    {
        [Parameter(Position = 0)]
        public string Name { get; set; }

        [Parameter(Position = 1)]
        public bool Public { get; set; }

        [Parameter(Position = 2)]
        public bool Friends { get; set; }

        [Parameter(Position = 3)]
        public bool Family { get; set; }

        protected override void ProcessRecord()
        {
            FlickrOperations autentication = FlickrOperationsFactory.GetFlickrOperations();
            CommonOperations commonOperations = new CommonOperations();
            commonOperations.SetAlbumPermissions(this.Name,Public, Family,Friends);
        }
    }
}
