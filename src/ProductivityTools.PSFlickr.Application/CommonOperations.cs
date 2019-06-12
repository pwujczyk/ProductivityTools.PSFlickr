using ProductivityTools.PSFlickr.FlickrProxy.FlickrSimpleObjects;
using ProductivityTools.PSFlickr.FlickrProxy.Ids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.PSFlickr.ApplicationClient
{
    public class CommonOperations : BaseOperations
    {
        internal FlickrPhotoId AddPhotoToAlbumId(string path, Album album)
        {
            var photoId = manager.AddPhoto(path);
            manager.AddPhotoToAlbum(album, photoId);

            var coverPhotoId = GetAlbumCoverId(album);
            if (coverPhotoId == CoverPhotoId)
            {
                this.manager.SetCoverPhoto(album, photoId);
                this.manager.RemovePhotoFromAlbum(coverPhotoId, album);
            }
            return photoId;
        }

        private FlickrPhotoId GetAlbumCoverId(Album albumId)
        {
            var album = GetAlbumById(albumId.AlbumId.Id);
            var coverPhoto = album.PrimaryPhotoId.Id;
            return new FlickrPhotoId(coverPhoto);
        }

        internal Album GetAlbumById(string albumId)
        {
            var albums = this.manager.GetAlbums();
            var album = albums.SingleOrDefault(x => x.AlbumId.Id == albumId);
            if (album == null) return null;
            return album;
        }

        private FlickrPhotoId coverPhotoId;
        internal FlickrPhotoId CoverPhotoId
        {
            get
            {

                if (coverPhotoId != null)
                {
                    coverPhotoId = GetCoverPhoto();
                    if (coverPhotoId != null)
                    {
                        coverPhotoId = UploadCoverPhoto();
                    }
                }
                return coverPhotoId;
            }
        }

        private FlickrPhotoId UploadCoverPhoto()
        {
            var assemblyLocation = System.Reflection.Assembly.GetCallingAssembly().Location;
            var assemblyLocationDirectory = System.IO.Path.GetDirectoryName(assemblyLocation);
            var path = System.IO.Path.Combine(assemblyLocationDirectory, "PSFlickrCover.jpg");
            var photoid = manager.AddPhoto(path);
            return photoid;
        }

        internal FlickrPhotoId GetCoverPhoto()
        {
            string title = "PSFlickrCover";
            var maintanceAlbum = GetMaintananceAlbum();
            var photosTitle = manager.GetPhotos(maintanceAlbum);
            var flickCover = photosTitle.FirstOrDefault(x => x.Title == title);
            var flickCoverId = flickCover.PhotoId;

            // var photoInAlbum = manager.AlbumPhotoByTitle(title);
            if (flickCoverId != null)
            {
                flickCoverId = UploadCoverPhoto();
                manager.AddPhotoToAlbum(maintanceAlbum, flickCoverId);
            }

            return flickCoverId;

            //var singlePhotos = manager.SinglePhotoByTitle(title);
            //return singlePhotos;
        }

        internal Album GetMaintananceAlbum()
        {
            var maintananceAlbumName = config.MaintenanceAlbumName;
            var album = GetAlbumByName(maintananceAlbumName);
            if (album == null)
            {
                album = CreateMaintananceAlbum();
            }
            return album;
        }

        internal Album GetAlbumByName(string albumName)
        {
            var albums = this.manager.GetAlbums();
            WriteVerbose($"Album {albumName} found");
            var album = albums.SingleOrDefault(x => x.Name == albumName);
            if (album == null) return null;
            return album;
        }

        private Album CreateMaintananceAlbum()
        {
            var coverPhoto = UploadCoverPhoto();
            var album = manager.CreateAlbum(config.MaintenanceAlbumName, coverPhoto);
            return album;
        }

        public string CreateAlbum(string name)
        {
            var x = CreateAlbumInternal(name);
            return x.AlbumId.Id;
        }

        public Album CreateAlbumInternal(string name)
        {
            WriteVerbose($"Creating album {name}");
            var x = manager.CreateAlbum(name, CoverPhotoId);
            return x;
        }

        public void SetAlbumPermissions(string albumName, bool @public, bool family, bool friends)
        {
            var album = GetAlbumByName(albumName);
            var photos = manager.GetPhotos(album);
            foreach (var photo in photos)
            {
                WriteVerbose($"Set permission for {photo.PhotoId}");
                manager.SetPermissions(photo, @public, friends, family);
            }
        }

        public void SetPhotosPermissions(bool @public, bool family, bool friends)
        {
            var albums = this.manager.GetAlbums();
            foreach (var album in albums)
            {
                WriteVerbose($"Set permission for {album.Name}");
                SetAlbumPermissions(album.Name, @public, family, friends);
            }
        }
    }
}
