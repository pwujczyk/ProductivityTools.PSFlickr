using FlickrNet;
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
        Dictionary<Photoset, PhotosetPhotoCollection> photoTree;
        protected Dictionary<Photoset, PhotosetPhotoCollection> PhotoTree
        {
            get
            {
                if (photoTree == null)
                {
                    photoTree = new Dictionary<Photoset, PhotosetPhotoCollection>();
                    ReBuildPhotoTree();
                }
                return photoTree;
            }
        }

        private List<Photo> singlePhotos;
        protected List<Photo> SinglePhotos
        {
            get
            {
                if (singlePhotos==null)
                {
                    RebuildSinglePhotos();
                }
                
                return singlePhotos;
            }
        }

        private void RebuildSinglePhotos()
        {
            if (singlePhotos==null)
            {
                singlePhotos = new List<Photo>();
            }
            var photos = Flickr.PhotosGetNotInSet();
            foreach(var photo in photos)
            {
                this.singlePhotos.Add(photo);
            }
        }

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

        public void ReBuildPhotoTree()
        {
            PhotosetCollection x = Flickr.PhotosetsGetList();
            foreach (Photoset item in x)
            {
                var photoList = Flickr.PhotosetsGetPhotos(item.PhotosetId);
                photoTree.Add(item, photoList);
            }
        }

    }
}
