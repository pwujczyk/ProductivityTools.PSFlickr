using FlickrNet;
using ProductivityTools.PSFlickr.FlickrProxy.FlickrSimpleObjects;
using ProductivityTools.PSFlickr.FlickrProxy.Ids;
using PSFlickr.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.PSFlickr.FlickrProxy
{
    public class FlickrManagerCore
    {
        protected Config config = new Config();

        Flickr flickr;
        protected Flickr Flickr
        {
            get
            {
                if (flickr == null)
                {
                    flickr = GetInstanceAutenticated(config.OauthAccessTokenToken);
                    flickr.OAuthAccessToken = config.OauthAccessTokenToken;
                    flickr.OAuthAccessTokenSecret = config.OauthAccessTokenTokenSecret;
                }
                return flickr;
            }
        }

        public Flickr GetInstanceAutenticated(string token)
        {
            return new Flickr(config.ApiKey, config.SharedSecret, token);
        }

        public List<PSPhoto> GetSinglePhotos()
        {
            var photos = Flickr.PhotosGetNotInSet();
            var result = photos.Select(x => new PSPhoto(new PhotoId(x.PhotoId), x.Title)).ToList();
            return result;
        }

        public List<PSPhoto> GetPhotos(Album album)
        {
            var photos=Flickr.PhotosetsGetPhotos(album.AlbumId.Id);
            return photos.Select(x => new PSPhoto(new PhotoId(x.PhotoId), x.Title)).ToList();
        }

        //not used as removing all photos in ablum removes automatically album
        //public void DeleteAlbum(Album album)
        //{
        //    Flickr.PhotosetsDelete(album.AlbumId.Id);
        //}
    }
}
