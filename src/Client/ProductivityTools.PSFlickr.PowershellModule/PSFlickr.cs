using ProductivityTools.PSCmdlet;
using ProductivityTools.PSFlickr.PowershellModule.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.PSFlickr.PowershellModule
{
    [Cmdlet(VerbsCommon.Set, "Flickr")]
    public class PSFlickr : PSCmdletPT
    {
        [Parameter]
        public SwitchParameter RegisterApplication { get; set; }

        [Parameter]
        public string Token { get; set; }

        [Parameter]
        public SwitchParameter done { get; set; }

        public PSFlickr()
        {
            base.AddCommand(new Authenticate(this));
            base.AddCommand(new SetFlickrToken(this));
            base.AddCommand(new GetAlbums(this));
        }

        protected override void BeginProcessing()
        {
            base.ProcessCommands();
            base.BeginProcessing();
        }
    }
}
