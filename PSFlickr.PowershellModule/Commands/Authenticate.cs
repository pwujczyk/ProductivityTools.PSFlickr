using ProductivityTools.PSCmdlet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSFlickr.PowershellModule.Commands
{
    class Authenticate : PSBaseCommandPT<PSFlickr>
    {
        public Authenticate(PSFlickr cmdletType) : base(cmdletType)
        {

        }

        protected override bool Condition => this.Cmdlet.Authenticate.IsPresent;

        protected override void Invoke()
        {
            throw new NotImplementedException();
        }
    }
}
