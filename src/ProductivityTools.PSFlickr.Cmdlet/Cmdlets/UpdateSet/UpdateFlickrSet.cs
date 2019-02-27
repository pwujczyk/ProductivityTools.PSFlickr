﻿using ProductivityTools.PSFlickr.Cmdlet.Cmdlets.UpdateSet.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.PSFlickr.Cmdlet.Cmdlets.UpdateSet
{
    [Cmdlet("Sync", "FlickrSet")]
    public class SyncFickrSet : FlickrCmdletsBase
    {
        [Parameter(Mandatory = false, Position = 0)]
        public string Directory { get; set; }

        public SyncFickrSet()
        {
            this.AddCommand(new UpdateAlbumsAndPushPhotos(this));
        }

        protected override void ProcessRecord()
        {
            base.ProcessCommands();
            base.ProcessRecord();
        }
    }
}
