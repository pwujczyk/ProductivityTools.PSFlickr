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
        public FlickrPhotoId AddPhoto(string path)
        {
            var photoId = Flickr.UploadPicture(path);
            return new FlickrPhotoId(photoId);
        }

        public Album CreateAlbum(string albumName, FlickrPhotoId coverPhotoId)
        {
            var photoset = Flickr.PhotosetsCreate(albumName, coverPhotoId.Id);
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

        public void SetCoverPhoto(Album albumId, FlickrPhotoId photoId)
        {
            Flickr.PhotosetsSetPrimaryPhoto(albumId.AlbumId.Id, photoId.Id);
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

        public void AddPhotoToAlbum(Album album, FlickrPhotoId photoId)
        {
            Flickr.PhotosetsAddPhoto(album.AlbumId.Id, photoId.Id);
        }

        public void RemovePhotoFromAlbum(FlickrPhotoId photoId, Album albumId)
        {
            Flickr.PhotosetsRemovePhoto(albumId.AlbumId.Id, photoId.Id);
        }

        public void RemovePhoto(FlickrPhoto photo)
        {
            Flickr.PhotosDelete(photo.PhotoId.Id);
        }

        public void SetPermissions(FlickrPhoto photo,bool @public, bool friends, bool family)
        {
            Flickr.PhotosSetPerms(photo.PhotoId.Id, @public, friends, family, PermissionComment.Nobody, PermissionAddMeta.Owner);
        }

        //public List<string> GetPhotosTitleFromAlbum(string albumId)
        //{
        //    var album = Flickr.PhotosetsGetPhotos(albumId);
        //    var photoIds = album.Select(x => x.Title).ToList<string>();
        //    return photoIds;
        //}
    }
}
