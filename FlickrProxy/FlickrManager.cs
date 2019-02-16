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
            ReBuildPhotoTree();
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

        public List<string> GetPhotosNotInAlbum()
        {
            var result = SinglePhotos.Select(x => x.PhotoId).ToList() ;
            return result;
        }

        public Photoset GetAlbumById(string albumId)
        {
            PhotosetCollection photocollection = Flickr.PhotosetsGetList();
            Photoset album = photocollection.Single(x => x.PhotosetId==albumId);
            return album;
        }

        public string GetAlbumId(string albumName)
        {
            var album=this.PhotoTree.SingleOrDefault(x => x.Key.Title == albumName);
            return album.Key?.PhotosetId;
        }

        public string GetAlbumCoverId(string albumId)
        {
            var album = GetAlbumById(albumId);
            var coverPhoto = album.PrimaryPhoto.PhotoId;
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

        public void RemovePhotoFromAlbum(string photoId, string albumId)
        {
            Flickr.PhotosetsRemovePhoto(albumId, photoId);
        }

        public void RemovePhoto(string photoId)
        {
            Flickr.PhotosDelete(photoId);
        }

        public List<string> GetPhotosIdFromAlbum(string albumId)
        {
            var album=this.PhotoTree.Single(x => x.Key.PhotosetId == albumId);
            var photoIds=album.Value.Select(x => x.PhotoId).ToList<string>();
            return photoIds;
        }

        public List<string> GetPhotosTitleFromAlbum(string albumId)
        {
            var album = Flickr.PhotosetsGetPhotos(albumId);
            var photoIds = album.Select(x => x.Title).ToList<string>();
            return photoIds;
        }

        public string AlbumPhotoByTitle(string title)
        {
            var coverPhoto= PhotoTree.SelectMany(x => x.Value).FirstOrDefault(photo => photo.Title == title);
            return coverPhoto?.PhotoId;
        }

        public string SinglePhotoByTitle(string title)
        {
            var singlePhoto = SinglePhotos.FirstOrDefault(x => x.Title == title);
            return singlePhoto?.PhotoId;
        }
    }
}
