using ProductivityTools.PSCmdlet;
using ProductivityTools.PSFlickr.Application.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.PSFlickr.PowershellModule.Commands
{
    internal class SetFlickrToken : PSCommandPT<PSFlickr>
    {
        protected override bool Condition => !string.IsNullOrEmpty(this.Cmdlet.Token);

        public SetFlickrToken(PSFlickr pSFlickr) : base(pSFlickr) { }

        protected override void Invoke()
        {
            string s = this.Cmdlet.Token;
            FlickrAutentication autentication = new FlickrAutentication();
            autentication.GetAccessToken(s);
        }
    }
}
