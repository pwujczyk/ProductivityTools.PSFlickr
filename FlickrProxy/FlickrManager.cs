using FlickrNet;
using PSFlickr.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.PSFlickr.FlickrProxy
{
    public class FlickrManager: FlickrManagerCore
    {
        
        static OAuthRequestToken requestToken;
        static OAuthAccessToken oauthAccessToken;

        public string AddPhoto(string path)
        {
            var photoId = Flickr.UploadPicture(path);
            return photoId;
        }

        public string CreateAlbum(string albumName, string coverPhotoId)
        {
            var album = Flickr.PhotosetsCreate(albumName, coverPhotoId);
            return album.PhotosetId;
        }

        public string GetAuthorizationUrl()
        {
            var requestToken = Flickr.OAuthGetRequestToken("oob");
            string url = Flickr.OAuthCalculateAuthorizationUrl(requestToken.Token, AuthLevel.Delete);
            return url;
        }

        public List<string> GetAlbums()
        {
            List<string> albumlist = this.PhotoTree.Select(x => x.Key.Title).ToList();
            return albumlist;
        }

        public string GetAlbumId(string albumName)
        {
            var album=this.PhotoTree.SingleOrDefault(x => x.Key.Title == albumName);
            return album.Key.PhotosetId;
        }

        public string GetAlbumCoverId(string albumId)
        {
            var album= this.PhotoTree.Single(x => x.Key.PhotosetId == albumId);
            var coverPhoto = album.Key.PrimaryPhoto.PhotoId;
            return coverPhoto;
        }

        public void SetCoverPhoto(string albumId, string photoId)
        {
            Flickr.PhotosetsSetPrimaryPhoto(albumId, photoId);
        }

        public void GetAndSaveAccessToken(string verificationCode)
        {
            oauthAccessToken = Flickr.OAuthGetAccessToken(requestToken, verificationCode);
            config.OauthAccessTokenToken = oauthAccessToken.Token;
            config.OauthAccessTokenTokenSecret = oauthAccessToken.TokenSecret;
        }

        public void AddPhotoToAlbum(string albumId, string photoId)
        {
            Flickr.PhotosetsAddPhoto(albumId, photoId);
        }

        public void DeleteAlbum(string albumId)
        {
            Flickr.PhotosetsDelete(albumId);
        }
    }
}
