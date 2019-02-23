using FlickrNet;
using ProductivityTools.PSFlickr.FlickrProxy.Ids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.PSFlickr.FlickrProxy.FlickrSimpleObjects
{
    public class Album
    {
        public AlbumId AlbumId { get; set; }
        public string Name { get; set; }

        public PSPhoto PrimaryPhoto { get; set; }

        public Album(AlbumId albumId, string name, PSPhoto primaryPhoto)
        {
            this.AlbumId = albumId;
            this.Name = name;
            this.PrimaryPhoto = primaryPhoto;
        }

        public static Album CreateAlbum(Photoset photoset)
        {
            var result = new Album(
            new AlbumId(photoset.PhotosetId),
            photoset.Title,
            new PSPhoto(new PhotoId(photoset.PrimaryPhotoId), photoset.PrimaryPhoto.Title));
            return result;
        }
    }
}
