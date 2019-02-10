using ProductivityTools.PSCmdlet;
using ProductivityTools.PSFlickr.Application.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.PSFlickr.PowershellModule
{
    class Authenticate : PSCommandPT<PSFlickr>
    {
        public Authenticate(PSFlickr cmdletType) : base(cmdletType)
        {

        }

        protected override bool Condition => this.Cmdlet.RegisterApplication.IsPresent;

        protected override void Invoke()
        {
            FlickrOperations autentication = new FlickrOperations();
            autentication.OpenAutorizeAddress();

        }
    }
}
