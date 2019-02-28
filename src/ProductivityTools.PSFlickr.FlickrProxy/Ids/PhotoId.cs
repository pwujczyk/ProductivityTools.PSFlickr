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

            if (object.ReferenceEquals(a, null))
            {
                return object.ReferenceEquals(b, null);
            }

            if (object.ReferenceEquals(b, null))
            {
                return object.ReferenceEquals(a, null);
            }

            // return a.Equals(b);

            //if (a == null && b == null) return true;

            var r = a.Id == b.Id;
            return r;
        }

        public static bool operator !=(FlickrPhotoId a, FlickrPhotoId b)
        {

            if (object.ReferenceEquals(a, null))
            {
                return object.ReferenceEquals(b, null);
            }

            if (object.ReferenceEquals(b, null))
            {
                return object.ReferenceEquals(a, null);
            }


            var r = a.Id != b.Id;
            return r;
        }

    }
}
