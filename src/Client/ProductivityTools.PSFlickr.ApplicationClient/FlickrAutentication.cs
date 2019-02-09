using FlickrNet;
using ProductivityTools.PSFlickr.Application.Common;
using PSFlickr.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.PSFlickr.Application.Client
{
    public class FlickrAutentication
    {
        //pw: change it
        static OAuthRequestToken requestToken;
        static OAuthAccessToken oauthAccessToken;
        //string oauthAccessTokenToken;
        //string oauthAccessTokenTokenSecret;

        Config config = new Config();

        Flickr flickr;
        Flickr Flickr
        {
            get
            {
                if (flickr == null)
                {
                    //flickr = FlickrManager.GetInstanceAutenticated(config.OauthAccessTokenToken);
                    flickr = FlickrManager.GetInstanceAutenticated(config.OauthAccessTokenToken);
                    flickr.OAuthAccessToken = config.OauthAccessTokenToken;
                    flickr.OAuthAccessTokenSecret = config.OauthAccessTokenTokenSecret;
                }
                return flickr;
            }
        }

        private string CoverPhotoId
        {
            get
            {
                if (string.IsNullOrEmpty(config.CoverPhotoId))
                {
                    config.CoverPhotoId = UploadCoverPhoto();
                }
                return config.CoverPhotoId;

            }
        }

        //string OauthAccessTokenToken
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(oauthAccessTokenToken))
        //        {
        //            oauthAccessTokenTokenSecret = new Config().OauthAccessTokenToken;
        //        }
        //        return oauthAccessTokenToken;
        //    }
        //    set
        //    {
        //        oauthAccessTokenToken = value;
        //    }
        //}
        //string OauthAccessTokenTokenSecret
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(oauthAccessTokenTokenSecret))
        //        {
        //            oauthAccessTokenTokenSecret = new Config().OauthAccessTokenTokenSecret;
        //        }
        //        return oauthAccessTokenTokenSecret;
        //    }
        //    set
        //    {
        //        oauthAccessTokenTokenSecret = value;
        //    }
        //}

        Dictionary<Photoset, PhotosetPhotoCollection> photoTree;
        private Dictionary<Photoset, PhotosetPhotoCollection> PhotoTree
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

        private string UploadCoverPhoto()
        {
            var assemblyLocation = System.Reflection.Assembly.GetCallingAssembly().Location;
            var assemblyLocationDirectory = System.IO.Path.GetDirectoryName(assemblyLocation);
            var path = System.IO.Path.Combine(assemblyLocationDirectory, "PSFlickrCover.jpg");
            var photoid=AddPhoto(path);
            return photoid;
        }














        public List<string> GetAlbums()
        {
            List<string> albumlist = this.PhotoTree.Select(x => x.Key.Title).ToList();
            return albumlist;
        }

        public string CreateAlbum(string albumName)
        {
            var album = Flickr.PhotosetsCreate(albumName, CoverPhotoId);
            return album.PhotosetId;
        }

        public void OpenAutorizeAddress()
        {
            Flickr f = FlickrManager.GetInstance();
            requestToken = f.OAuthGetRequestToken("oob");

            string url = f.OAuthCalculateAuthorizationUrl(requestToken.Token, AuthLevel.Delete);

            System.Diagnostics.Process.Start(url);
        }

        public void GetAndSaveAccessToken(string verificationCode)
        {
            Flickr f = FlickrManager.GetInstance();
            oauthAccessToken = f.OAuthGetAccessToken(requestToken, verificationCode);
            Config c = new Config();
            c.OauthAccessTokenToken = oauthAccessToken.Token;
            c.OauthAccessTokenTokenSecret = oauthAccessToken.TokenSecret;
        }

        public void ReBuildPhotoTree()
        {
            //Flickr f = FlickrManager.GetInstanceAutenticated(config.OauthAccessTokenToken);
            //f.OAuthAccessToken = config.OauthAccessTokenToken;
            //f.OAuthAccessTokenSecret = config.OauthAccessTokenTokenSecret;
            PhotosetCollection x = Flickr.PhotosetsGetList();
            foreach (Photoset item in x)
            {
                var photoList = Flickr.PhotosetsGetPhotos(item.PhotosetId);
                PhotoTree.Add(item, photoList);
            }
        }

        public string AddPhoto(string path,string albumName)
        {
            var photoId = Flickr.UploadPicture(path);
            return photoId;
        }

        public string AddPhoto(string path)
        {
            var photoId = Flickr.UploadPicture(path);
            return photoId;
        }

        //public string CreateAlbum(string name)
        //{
        //    AddPhoto();
        //    Flickr f = FlickrManager.GetInstanceAutenticated(oauthAccessToken.Token);
        //    f.OAuthAccessToken = oauthAccessToken.Token;
        //    f.OAuthAccessTokenSecret = oauthAccessToken.TokenSecret;
        //    f.PhotosetsCreate("pawel", "46934493902");
        //}
    }
}
