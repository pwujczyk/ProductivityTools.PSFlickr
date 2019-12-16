using ProductivityTools.PSFlickr.ApplicationClient;
using ProductivityTools.PSFlickr.FlickrProxy;
using ProductivityTools.PSFlickr.FlickrProxy.FlickrSimpleObjects;
using ProductivityTools.PSFlickr.FlickrProxy.Ids;
using PSFlickr.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.PSFlickr.Application
{
    public class FlickrOperations : BaseOperations
    {
        protected CommonOperations commonOperations;

        //public FlickrOperations()
        //{

        //}

        public FlickrOperations(Action<string> writeVerbose) : base(writeVerbose)
        {
            // this.WriteVerbose = writeVerbose;
            this.commonOperations = new CommonOperations(this.WriteVerbose);
        }

        public void OpenAutorizeAddress()
        {
            var url = manager.GetAuthorizationUrl();
            System.Diagnostics.Process.Start(url);
        }

        public void GetAndSaveAccessToken(string verificationCode)
        {
            manager.GetAndSaveAccessToken(verificationCode);
        }

        public string AddPhoto(string path)
        {
            var photoId = manager.AddPhoto(path);
            return photoId.Id;
        }

        public string AddPhotoToAlbumName(string path, string albumName)
        {
            var albumId = commonOperations.GetAlbumByName(albumName);
            var photoid = commonOperations.AddPhotoToAlbumId(path, albumId);
            return photoid.Id;
        }


        //public string AddPhoto(string path, string albumName)
        //{


        //    var fileName=System.IO.Path.GetFileNameWithoutExtension(path);
        //    var photoId = photosTitle.FirstOrDefault(x => x == fileName);
        //    if (photoId==null)
        //    {
        //        photoId = manager.AddPhoto(path);
        //        manager.AddPhotoToAlbum(albumId, photoId);

        //        var coverPhotoId = this.manager.GetAlbumCoverId(albumId);
        //        if (coverPhotoId == CoverPhotoId)
        //        {
        //            this.manager.SetCoverPhoto(albumId, photoId);
        //            this.manager.RemovePhotoFromAlbum(coverPhotoId, albumId);
        //        }
        //    }
        //    else
        //    {
        //        WriteVerbose($"Photo {fileName} already exists in {albumName}");
        //    }
        //    return photoId;
        //}

        public List<string> GetAlbumsName()
        {
            return this.manager.GetAlbums().Select(x => x.Name).ToList();
        }

        public void DeleteAlbumByName(string name, bool removeAlsoPhotos)
        {
            WriteVerbose($"Removing album {name}");
            Album album = commonOperations.GetAlbumByName(name);
            DeleteAlbum(album, removeAlsoPhotos);
        }

        public void DeleteAlbum(Album album, bool removeAlsoPhotos)
        {
            if (removeAlsoPhotos)
            {
                var photos = manager.GetPhotos(album);
                DeletePhotos(photos, album);
            }
            else
            {
                WriteVerbose("Delete album");
                //not needed album without photos is deleted automatically
                manager.DeleteAlbum(album);
            }
        }

        public void ClearFlickr()
        {
            var temporaryAlbum = DateTime.Now.ToLongTimeString();
            commonOperations.CreateAlbum(temporaryAlbum);
            MoveSinglePhotosToAlbum(temporaryAlbum);

            var albums = this.manager.GetAlbums();
            foreach (var album in albums)
            {
                var checkAlbum = commonOperations.GetAlbumById(album.AlbumId.Id);
                if (checkAlbum != null)
                {//if we remove all pictures in previous iteration, album won't exist anymore
                    DeleteAlbumExceptionSafe(album, true);
                }
            }
        }

        private void DeleteAlbumExceptionSafe(Album album, bool removeAlsoPhotos)
        {
            try
            {
                DeleteAlbum(album, removeAlsoPhotos);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void DeletePhotos(List<FlickrPhoto> photos, Album album)
        {
            foreach (var item in photos)
            {
                var message = $"Removing photo {item.Title}";
                if (album != null)
                {
                    message += $" from album: {album.Name}";
                }
                WriteVerbose(message);
                manager.RemovePhoto(item);
            }
        }


        public void MoveSinglePhotosToAlbum(string name)
        {
            WriteVerbose($"Moving single photos to {name}");
            List<FlickrPhoto> singlePhotos = manager.GetSinglePhotos();
            var albumId = commonOperations.GetAlbumByName(name);
            foreach (var photo in singlePhotos)
            {
                manager.AddPhotoToAlbum(albumId, photo.PhotoId);
            }
        }





        //private void SetCoverPhoto(Album album, FlickrPhoto photoId, string path)
        //{
        //    var fileName = System.IO.Path.GetFileNameWithoutExtension(path);
        //    if (fileName.EndsWith(Cover))
        //    {
        //        this.manager.SetCoverPhoto(album, photoId.PhotoId);
        //    }
        //}


    }
}
