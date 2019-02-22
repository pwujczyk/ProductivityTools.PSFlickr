using ProductivityTools.PSFlickr.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.PSFlickr.Cmdlet
{
    public static class FlickrOperationsFactory
    {
        public static FlickrOperations GetFlickrOperations()
        {
            return new FlickrOperations();
        }

        public static FlickrOperations GetFlickrOperations(Action<string> writeVerbose)
        {
            return new FlickrOperations(writeVerbose);
        }
    }
}
