using ProductivityTools.PSFlickr.FlickrProxy.Ids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.PSFlickr.FlickrProxy.FlickrSimpleObjects
{
    public class PSPhoto
    {
        public PhotoId PhotoId { get; set; }

        public string Title { get; set; }

        public PSPhoto(PhotoId photoId, string title)
        {
            this.PhotoId = photoId;
            this.Title = title;
        }
    }
}
