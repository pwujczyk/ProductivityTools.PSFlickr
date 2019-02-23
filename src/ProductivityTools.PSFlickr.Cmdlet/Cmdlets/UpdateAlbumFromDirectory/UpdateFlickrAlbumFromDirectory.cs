﻿using ProductivityTools.PSFlickr.Cmdlet.Cmdlets.NewAlbumFromDirectory.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.PSFlickr.Cmdlet.Cmdlets.NewAlbumFromDirectory
{
    [Cmdlet("Update", "FlickrAlbumFromDirectory")]
    public class NewFlickrAlbumFromDirectory : FlickrCmdletsBase
    {
        [Parameter(Mandatory = false, Position = 0)]
        public string Path { get; set; }

        public NewFlickrAlbumFromDirectory()
        {
            this.AddCommand(new UpdateAlbumAndPushPhotos(this));
        }

        protected override void ProcessRecord()
        {
            base.ProcessCommands();
            base.ProcessRecord();
        }
    }
}