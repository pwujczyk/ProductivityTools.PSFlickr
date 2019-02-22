using ProductivityTools.PSFlickr.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.PSFlickr.Cmdlet.Cmdlets.UpdateSet.Commands
{
    public class UpdateAlbumsAndPushPhotos : PSCmdlet.PSCommandPT<UpdateFlickrSet>
    {
        protected override bool Condition => true;

        public UpdateAlbumsAndPushPhotos(UpdateFlickrSet cmdlet) : base(cmdlet) { }

        protected override void Invoke()
        {
            FlickrOperations flickrOperations = FlickrOperationsFactory.GetFlickrOperations(WriteOutput);
            var absolutepath = this.Cmdlet.GetPath(this.Cmdlet.Directory ?? string.Empty);
            flickrOperations.CreateAlbumsFromDirectories(absolutepath);
        }
    }
}
