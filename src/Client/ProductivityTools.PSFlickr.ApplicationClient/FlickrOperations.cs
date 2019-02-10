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
            if(coverPhotoId==CoverPhotoId)
            {
                this.manager.SetCoverPhoto(albumId, photoId);
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
    }
}
