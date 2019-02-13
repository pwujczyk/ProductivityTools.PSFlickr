using ProductivityTools.PSFlickr.Application.Client;
using ProductivityTools.PSFlickr.PowershellModule.Cmdlets.AddPhoto.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.PSFlickr.PowershellModule.Cmdlets.AddPhoto
{
    [Cmdlet(VerbsCommon.Add, "FlickrPhoto")]
    public class AddFlickrPhoto : FlickrCmdletsBase
    {
        [Parameter(Position = 0)]
        public string Path { get; set; }

        [Parameter(Position = 1)]
        public string Album { get; set; }

        public AddFlickrPhoto()
        {
            this.AddCommand(new AddSinglePhoto(this));
            this.AddCommand(new AddPhotoToAlbum(this));
        }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
        }

        protected override void ProcessRecord()
        {
            this.ProcessCommands();
            
        }


    }
}
