using ProductivityTools.PSFlickr.Application;
using ProductivityTools.PSFlickr.ApplicationClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.PSFlickr.Cmdlets.NewAlbumFromDirectory.Commands
{
    public class FlickrAlbumFromDirectoryCommand : PSCmdlet.PSCommandPT<SyncFlickrAlbumFromDirectory>
    {
        protected override bool Condition => true;

        public FlickrAlbumFromDirectoryCommand(SyncFlickrAlbumFromDirectory cmdlet) : base(cmdlet) { }

        protected override void Invoke()
        {
         //   FlickrOperations flickrOperations = FlickrOperationsFactory.GetFlickrOperations(WriteOutput);
            SyncOneDirectory flickrSync = new SyncOneDirectory(this.Cmdlet.WriteVerbose);
            var absolutepath = this.Cmdlet.GetPath(this.Cmdlet.Path ?? string.Empty);
            flickrSync.CreateAlbumAndPushPhotos(absolutepath);
        }
    }
}
