using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.PSFlickr.FlickrProxy.Ids
{
    public class AlbumId
    {
        public string Id { get; set; }

        public AlbumId(string id)
        {
            this.Id = id;
        }
    }
}
