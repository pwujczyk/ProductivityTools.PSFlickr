using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.PSFlickr.FlickrProxy.Ids
{
    public class PhotoId
    {
        public string Id { get; set; }
        public PhotoId(string id)
        {
            this.Id = id;
        }
    }
}
