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
    public class FlickrManager : FlickrManagerCore
    {

        static OAuthRequestToken requestToken;
        static OAuthAccessToken oauthAccessToken;

        public string AddPhoto(string path)
        {
            var photoId = Flickr.UploadPicture(path);
            return photoId;
        }

        public Album CreateAlbum(string albumName, string coverPhotoId)
        {
            var album = Flickr.PhotosetsCreate(albumName, coverPhotoId);
            return new Album(new AlbumId(album.PhotosetId), album.Title);
        }

        public string GetAuthorizationUrl()
        {
            var requestToken = Flickr.OAuthGetRequestToken("oob");
            string url = Flickr.OAuthCalculateAuthorizationUrl(requestToken.Token, AuthLevel.Delete);
            return url;
        }

        public List<string> GetCachedAlbums()
        {
            List<string> albumlist = this.PhotoTree.Select(x => x.Key.Title).ToList();
            return albumlist;
        }

        public List<Album> GetAlbums()
        {
            PhotosetCollection albums = Flickr.PhotosetsGetList();
            List<Album> albumlist = albums.Select(x => new Album(new AlbumId(x.PhotosetId), x.Title)).ToList();
            return albumlist;
        }

        public List<string> GetPhotosNotInAlbum()
        {
            var result = SinglePhotos.Select(x => x.PhotoId).ToList();
            return result;
        }

        public Photoset GetAlbumById(string albumId)
        {
            PhotosetCollection photocollection = Flickr.PhotosetsGetList();
            Photoset album = photocollection.Single(x => x.PhotosetId == albumId);
            return album;
        }

        //public Album GetAlbumId(string albumName)
        //{
        //    var album = this.PhotoTree.SingleOrDefault(x => x.Key.Title == albumName);
        //    return album.Key?.PhotosetId;
        //}

        public Album GetAlbumByName(string albumName)
        {
            PhotosetCollection photocollection = Flickr.PhotosetsGetList();
            Photoset album = photocollection.SingleOrDefault(x => x.Title == albumName);
            if (album == null) return null;
            return new Album(new AlbumId(album.PhotosetId), album.Title);
        }

        public string GetAlbumCoverId(Album albumId)
        {
            var album = GetAlbumById(albumId.AlbumId.Id);
            var coverPhoto = album.PrimaryPhoto.PhotoId;
            return coverPhoto;
        }

        public void SetCoverPhoto(Album albumId, string photoId)
        {
            Flickr.PhotosetsSetPrimaryPhoto(albumId.AlbumId.Id, photoId);
        }

        public void GetAndSaveAccessToken(string verificationCode)
        {
            oauthAccessToken = Flickr.OAuthGetAccessToken(requestToken, verificationCode);
            config.OauthAccessTokenToken = oauthAccessToken.Token;
            config.OauthAccessTokenTokenSecret = oauthAccessToken.TokenSecret;
        }

        public void AddPhotoToAlbum(Album album, string photoId)
        {
            Flickr.PhotosetsAddPhoto(album.AlbumId.Id, photoId);
        }

        public void RemovePhotoFromAlbum(string photoId, Album albumId)
        {
            Flickr.PhotosetsRemovePhoto(albumId.AlbumId.Id, photoId);
        }

        public void RemovePhoto(PSPhoto photo)
        {
            Flickr.PhotosDelete(photo.PhotoId.Id);
        }

        public List<string> GetPhotosIdFromAlbum(string albumId)
        {
            var album = this.PhotoTree.Single(x => x.Key.PhotosetId == albumId);
            var photoIds = album.Value.Select(x => x.PhotoId).ToList<string>();
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
            var coverPhoto = PhotoTree.SelectMany(x => x.Value).FirstOrDefault(photo => photo.Title == title);
            return coverPhoto?.PhotoId;
        }

        public string SinglePhotoByTitle(string title)
        {
            var singlePhoto = SinglePhotos.FirstOrDefault(x => x.Title == title);
            return singlePhoto?.PhotoId;
        }
    }
}
