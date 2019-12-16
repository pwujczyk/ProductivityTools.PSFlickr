using ProductivityTools.PSFlickr.FlickrProxy;
using PSFlickr.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.PSFlickr.ApplicationClient
{
    public abstract class BaseOperations
    {
        protected Config config = new Config();
        protected FlickrManager manager = new FlickrManager();


        private Action<string> writeVerbose;
        protected Action<string> WriteVerbose
        {
            get
            {
                if (writeVerbose != null)
                {
                    return writeVerbose;
                }
                else
                {
                    return s => { };
                }
            }
            set
            {
                writeVerbose = value;
            }
        }

        public BaseOperations(Action<string> writeVerbose)
        {
            this.WriteVerbose = writeVerbose;
        }


    }
}
