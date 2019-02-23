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
            var result = new Album(
               new AlbumId(album.PhotosetId),
               album.Title,
               new PSPhoto(new PhotoId(album.PrimaryPhotoId), album.PrimaryPhoto.Title));
            return result;
        }

        public string GetAuthorizationUrl()
        {
            var requestToken = Flickr.OAuthGetRequestToken("oob");
            string url = Flickr.OAuthCalculateAuthorizationUrl(requestToken.Token, AuthLevel.Delete);
            return url;
        }

        public List<Album> GetAlbums()
        {
            PhotosetCollection albums = Flickr.PhotosetsGetList();
            List<Album> albumlist = albums.Select(x => 
            new Album(
                new AlbumId(x.PhotosetId), 
                x.Title, 
                new PSPhoto(new PhotoId(x.PrimaryPhotoId), x.PrimaryPhoto.Title))).ToList();
            return albumlist;
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

        public List<string> GetPhotosTitleFromAlbum(string albumId)
        {
            var album = Flickr.PhotosetsGetPhotos(albumId);
            var photoIds = album.Select(x => x.Title).ToList<string>();
            return photoIds;
        }
    }
}
