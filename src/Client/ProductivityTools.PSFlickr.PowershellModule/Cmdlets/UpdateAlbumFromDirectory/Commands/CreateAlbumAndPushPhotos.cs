using ProductivityTools.PSFlickr.Application.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.PSFlickr.PowershellModule.Cmdlets.NewAlbumFromDirectory.Commands
{
    public class UpdateAlbumAndPushPhotos : PSCmdlet.PSCommandPT<NewFlickrAlbumFromDirectory>
    {
        protected override bool Condition => true;

        public UpdateAlbumAndPushPhotos(NewFlickrAlbumFromDirectory cmdlet) : base(cmdlet) { }

        protected override void Invoke()
        {
            FlickrOperations flickrOperations = FlickrOperationsFactory.GetFlickrOperations(WriteOutput);
            var absolutepath = this.Cmdlet.GetPath(this.Cmdlet.Path ?? string.Empty);
            flickrOperations.CreateAlbumAndPushPhotos(absolutepath);
        }
    }
}
