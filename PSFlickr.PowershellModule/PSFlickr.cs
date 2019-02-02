using ProductivityTools.PSCmdlet;
using PSFlickr.PowershellModule.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace PSFlickr.PowershellModule
{
    public class PSFlickr : PSCmdletPT
    {
        public SwitchParameter Authenticate { get; set; }

        protected PSFlickr()
        {
            base.AddCommand(new Authenticate(this));
        }
    }
}
