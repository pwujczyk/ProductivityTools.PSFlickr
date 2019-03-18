using ProductivityTools.PSFlickr.Application;
using ProductivityTools.PSFlickr.ApplicationClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.PSFlickr.Cmdlet.Cmdlets.UpdateSet.Commands
{
    public class FlickrSetCommand : PSCmdlet.PSCommandPT<SyncFickrSet>
    {
        protected override bool Condition => true;

        public FlickrSetCommand(SyncFickrSet cmdlet) : base(cmdlet) { }

        protected override void Invoke()
        {
            //FlickrOperations flickrOperations = FlickrOperationsFactory.GetFlickrOperations(this.Cmdlet.WriteVerbose);
            SyncMultipleDirectories flickrSync = new SyncMultipleDirectories(this.Cmdlet.WriteVerbose);
            var absolutepath = this.Cmdlet.GetPath(this.Cmdlet.Directory ?? string.Empty);
            flickrSync.CreateAlbumsFromDirectories(absolutepath);
        }
    }
}
