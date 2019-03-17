using ProductivityTools.PSFlickr.Application;
using ProductivityTools.PSFlickr.ApplicationClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.PSFlickr.Cmdlet.Cmdlets.UpdateSet.Commands
{
    public class UpdateAlbumsAndPushPhotos : PSCmdlet.PSCommandPT<SyncFickrSet>
    {
        protected override bool Condition => true;

        public UpdateAlbumsAndPushPhotos(SyncFickrSet cmdlet) : base(cmdlet) { }

        protected override void Invoke()
        {
            FlickrOperations flickrOperations = FlickrOperationsFactory.GetFlickrOperations(WriteOutput);
            SyncMultipleDirectories flickrSync = new SyncMultipleDirectories(WriteOutput);
            var absolutepath = this.Cmdlet.GetPath(this.Cmdlet.Directory ?? string.Empty);
            flickrSync.CreateAlbumsFromDirectories(absolutepath);
        }
    }
}
