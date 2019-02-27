using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.PSFlickr.FlickrProxy.Ids
{
    public class FlickrPhotoId
    {
        public string Id { get; set; }
        public FlickrPhotoId(string id)
        {
            this.Id = id;
        }

        public static bool operator ==(FlickrPhotoId a, FlickrPhotoId b)
        {
            var r = a.Id == b.Id;
            return r;
        }

        public static bool operator !=(FlickrPhotoId a, FlickrPhotoId b)
        {
            var r = a.Id != b.Id;
            return r;
        }

    }
}
