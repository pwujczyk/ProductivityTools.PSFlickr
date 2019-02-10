using ProductivityTools.PSCmdlet;
using ProductivityTools.PSFlickr.Application.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.PSFlickr.PowershellModule.Commands
{
    class GetAlbums : PSCommandPT<PSFlickr>
    {
        protected override bool Condition => base.Cmdlet.done.IsPresent;

        public GetAlbums(PSFlickr flicr) : base(flicr) { }
        protected override void Invoke()
        {
            FlickrOperations autentication = new FlickrOperations();
            //autentication.BuildPhotoTree();

            //autentication.CreateAlbum();
        }
    }
}
