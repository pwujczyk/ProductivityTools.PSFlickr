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
        public string AddPhoto(string path)
        {
            var photoId = Flickr.UploadPicture(path);
            return photoId;
        }

        public Album CreateAlbum(string albumName, string coverPhotoId)
        {
            var photoset = Flickr.PhotosetsCreate(albumName, coverPhotoId);
            Album result = Album.CreateAlbum(photoset);
            return result;
        }

        public List<Album> GetAlbums()
        {
            PhotosetCollection albums = Flickr.PhotosetsGetList();
            List<Album> albumlist = albums.Select(x => Album.CreateAlbum(x)).ToList();
            return albumlist;
        }

        //not used as removing all photos in ablum removes automatically album
        //public void DeleteAlbum(Album album)
        //{
        //    Flickr.PhotosetsDelete(album.AlbumId.Id);
        //}

        public void SetCoverPhoto(Album albumId, string photoId)
        {
            Flickr.PhotosetsSetPrimaryPhoto(albumId.AlbumId.Id, photoId);
        }

        public List<FlickrPhoto> GetSinglePhotos()
        {
            var photos = Flickr.PhotosGetNotInSet();
            var result = photos.Select(x => new FlickrPhoto(new FlickrPhotoId(x.PhotoId), x.Title)).ToList();
            return result;
        }

        public List<FlickrPhoto> GetPhotos(Album album)
        {
            var photos = Flickr.PhotosetsGetPhotos(album.AlbumId.Id);
            return photos.Select(x => new FlickrPhoto(new FlickrPhotoId(x.PhotoId), x.Title)).ToList();
        }

        public void AddPhotoToAlbum(Album album, string photoId)
        {
            Flickr.PhotosetsAddPhoto(album.AlbumId.Id, photoId);
        }

        public void RemovePhotoFromAlbum(string photoId, Album albumId)
        {
            Flickr.PhotosetsRemovePhoto(albumId.AlbumId.Id, photoId);
        }

        public void RemovePhoto(FlickrPhoto photo)
        {
            Flickr.PhotosDelete(photo.PhotoId.Id);
        }

        //public List<string> GetPhotosTitleFromAlbum(string albumId)
        //{
        //    var album = Flickr.PhotosetsGetPhotos(albumId);
        //    var photoIds = album.Select(x => x.Title).ToList<string>();
        //    return photoIds;
        //}
    }
}
