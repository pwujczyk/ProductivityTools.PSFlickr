using ProductivityTools.PSFlickr.Application.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.PSFlickr.PowershellModule.Cmdlets.AddPhoto.Commands
{
    class AddPhotoToAlbum :  PSCmdlet.PSCommandPT<AddFlickrPhoto>
    {
        protected override bool Condition => !string.IsNullOrEmpty(this.Cmdlet.Path) && !string.IsNullOrEmpty(this.Cmdlet.Album);

        public AddPhotoToAlbum(AddFlickrPhoto cmdlet):base(cmdlet)
        {

        }

        protected override void Invoke()
        {
            var absolutepath = this.Cmdlet.GetPath(this.Cmdlet.Path);
            FlickrOperations autentication = FlickrOperationsFactory.GetFlickrOperations();
            var photoId = autentication.AddPhotoToAlbumName(absolutepath,this.Cmdlet.Album);
            WriteOutput(photoId);
        }
    }
}
