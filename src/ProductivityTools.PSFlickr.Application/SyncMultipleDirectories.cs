using ProductivityTools.PSFlickr.Application;
using ProductivityTools.PSFlickr.FlickrProxy;
using ProductivityTools.PSFlickr.FlickrProxy.FlickrSimpleObjects;
using ProductivityTools.PSFlickr.FlickrProxy.Ids;
using PSFlickr.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.PSFlickr.ApplicationClient
{
    public class SyncMultipleDirectories : BaseOperations
    {
      //  FlickrOperations FlickrOperations;
        protected CommonOperations commonOperations = new CommonOperations();
 

        public void CreateAlbumsFromDirectories(string directoryPath)
        {
            var mainDirectory = System.IO.Directory.CreateDirectory(directoryPath);
            DirectoryInfo[] directories = mainDirectory.GetDirectories();
            SyncOneDirectory syncOneDirectory = new SyncOneDirectory(this.WriteVerbose);
            foreach (var direcotry in directories)
            {
                syncOneDirectory.CreateAlbumAndPushPhotos(direcotry.FullName);
            }
        }

        public SyncMultipleDirectories(Action<string> writeVerbose)
        {
            this.WriteVerbose = writeVerbose;
          //  this.FlickrOperations = flickrOperations;
        }
        
      

    

      


       
    }
}
