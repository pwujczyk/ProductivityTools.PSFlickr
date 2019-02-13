using ProductivityTools.PSFlickr.FlickrProxy;
using PSFlickr.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.PSFlickr.Application.Client
{
    public class FlickrOperations
    {
        Config config = new Config();
        FlickrManager manager = new FlickrManager();

        private Action<string> writeVerbose;
        Action<string> WriteVerbose
        {
            get
            {
                if (writeVerbose!=null)
                {
                    return writeVerbose;
                }
                else
                {
                    return s => { };
                }
            }
            set
            {
                writeVerbose = value;
            }
        }

        private string coverPhotoId;
        private string CoverPhotoId
        {
            get
            {
                if (string.IsNullOrEmpty(coverPhotoId))
                {
                    coverPhotoId = GetCoverPhoto();
                    if (string.IsNullOrEmpty(coverPhotoId))
                    {
                        coverPhotoId = UploadCoverPhoto();
                    }
                }
                return coverPhotoId;
            }
        }

        public FlickrOperations()
        {

        }

        public FlickrOperations(Action<string> writeVerbose)
        {
            this.WriteVerbose = writeVerbose;
        }

        private string UploadCoverPhoto()
        {
            var assemblyLocation = System.Reflection.Assembly.GetCallingAssembly().Location;
            var assemblyLocationDirectory = System.IO.Path.GetDirectoryName(assemblyLocation);
            var path = System.IO.Path.Combine(assemblyLocationDirectory, "PSFlickrCover.jpg");
            var photoid = manager.AddPhoto(path);
            return photoid;
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
            return photoId;
        }

        public string AddPhoto(string path, string albumName)
        {
            var albumId = this.manager.GetAlbumId(albumName);
            var photoId = manager.AddPhoto(path);
            manager.AddPhotoToAlbum(albumId, photoId);

            var coverPhotoId = this.manager.GetAlbumCoverId(albumId);
            if (coverPhotoId == CoverPhotoId)
            {
                this.manager.SetCoverPhoto(albumId, photoId);
                this.manager.RemovePhotoFromAlbum(coverPhotoId, albumId);
            }
            return photoId;
        }

        public List<string> GetAlbums()
        {
            return this.manager.GetAlbums();
        }

        public string CreateAlbum(string name)
        {
            var x = manager.CreateAlbum(name, CoverPhotoId);
            return x;
        }


        public void DeleteAlbum(string name, bool removeAlsoPhotos)
        {
            var albumId = this.manager.GetAlbumId(name);
            manager.DeleteAlbum(albumId);
            if (removeAlsoPhotos)
            {
                var photosIds = manager.GetPhotosFromAlbum(albumId);
                DeletePhotos(photosIds);
            }
            //manager.DeleteAlbum(albumId);
        }

        public void DeletePhotos(List<string> photoIds)
        {
            foreach (var item in photoIds)
            {
                manager.RemovePhoto(item);
            }
        }

        public string GetCoverPhoto()
        {
            string title = "PSFlickrCover";
            var photoInAlbum=manager.AlbumPhotoByTitle (title);
            if (!string.IsNullOrEmpty(photoInAlbum))
            {
                return photoInAlbum;
            }
            var singlePhotos = manager.SinglePhotoByTitle(title);
            return singlePhotos;
        }

        public void MoveSinglePhotosToAlbum(string name)
        {
            var singlePhotos = manager.GetPhotosNotInAlbum();
            var albumId = manager.GetAlbumId(name);
            foreach (var photo in singlePhotos)
            {
                manager.AddPhotoToAlbum(albumId, photo);
            }
            manager.ReBuildPhotoTree();
        }
    }
}
