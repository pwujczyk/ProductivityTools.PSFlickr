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
    public class FlickrOperations
    {
        string Cover = "_Cover";
        Config config = new Config();
        FlickrManager manager = new FlickrManager();

        private Action<string> writeVerbose;
        Action<string> WriteVerbose
        {
            get
            {
                if (writeVerbose != null)
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

        private FlickrPhotoId coverPhotoId;
        private FlickrPhotoId CoverPhotoId
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

        public FlickrOperations()
        {

        }

        public FlickrOperations(Action<string> writeVerbose)
        {
            this.WriteVerbose = writeVerbose;
        }

        private FlickrPhotoId UploadCoverPhoto()
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
            return photoId.Id;
        }

        public string AddPhotoToAlbumName(string path, string albumName)
        {
            var albumId = GetAlbumByName(albumName);
            var photoid = AddPhotoToAlbumId(path, albumId);
            return photoid.Id;
        }

        private Album GetAlbumByName(string albumName)
        {
            var albums = this.manager.GetAlbums();
            var album = albums.SingleOrDefault(x => x.Name == albumName);
            if (album == null) return null;
            return album;
        }

        public Album GetAlbumById(string albumId)
        {
            var albums = this.manager.GetAlbums();
            var album = albums.SingleOrDefault(x => x.AlbumId.Id == albumId);
            if (album == null) return null;
            return album;
        }

        private FlickrPhotoId GetAlbumCoverId(Album albumId)
        {
            var album = GetAlbumById(albumId.AlbumId.Id);
            var coverPhoto = album.PrimaryPhotoId.Id;
            return new FlickrPhotoId(coverPhoto);
        }

        public FlickrPhotoId AddPhotoToAlbumId(string path, Album album)
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

        public string CreateAlbum(string name)
        {
            var x = CreateAlbumInternal(name);
            return x.AlbumId.Id;
        }

        private Album CreateAlbumInternal(string name)
        {
            WriteVerbose($"Creating album {name}");
            var x = manager.CreateAlbum(name, CoverPhotoId);
            return x;
        }

        private Album GetOrCreateAlbum(string name)
        {
            Album albumId = GetAlbumByName(name);
            if (albumId == null)
            {
                albumId = CreateAlbumInternal(name);
            }
            return albumId;
        }

        public void DeleteAlbumByName(string name, bool removeAlsoPhotos)
        {
            WriteVerbose($"Removing album {name}");
            Album album = GetAlbumByName(name);
            DeleteAlbum(album, removeAlsoPhotos);
        }

        public void DeleteAlbum(Album album, bool removeAlsoPhotos)
        {
            if (removeAlsoPhotos)
            {
                var photos = manager.GetPhotos(album);
                DeletePhotos(photos, album);
            }
            writeVerbose("Delete album");
            //not needed album without photos is deleted automatically
            //manager.DeleteAlbum(album);
        }

        public void ClearFlickr()
        {
            var temporaryAlbum = DateTime.Now.ToLongTimeString();
            CreateAlbum(temporaryAlbum);
            MoveSinglePhotosToAlbum(temporaryAlbum);

            var albums = this.manager.GetAlbums();
            foreach (var album in albums)
            {
                var checkAlbum=GetAlbumById(album.AlbumId.Id);
                if (checkAlbum != null)
                {//if we remove all pictures in previous iteration, album won't exist anymore
                    DeleteAlbum(album, true);
                }
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

        public Album GetMaintananceAlbum()
        {
            var maintananceAlbumName = config.MaintenanceAlbumName;
            var album = GetAlbumByName(maintananceAlbumName);
            if (album == null)
            {
                album = CreateMaintananceAlbum();
            }
            return album;
        }

        private Album CreateMaintananceAlbum()
        {
            var coverPhoto = UploadCoverPhoto();
            var album = manager.CreateAlbum(config.MaintenanceAlbumName, coverPhoto);
            return album;
        }

        public FlickrPhotoId GetCoverPhoto()
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

        public void MoveSinglePhotosToAlbum(string name)
        {
            WriteVerbose($"Moving single photos to {name}");
            List<FlickrPhoto> singlePhotos = manager.GetSinglePhotos();
            var albumId = GetAlbumByName(name);
            foreach (var photo in singlePhotos)
            {
                manager.AddPhotoToAlbum(albumId, photo.PhotoId);
            }
        }

        public void CreateAlbumsFromDirectories(string directoryPath)
        {
            var mainDirectory = System.IO.Directory.CreateDirectory(directoryPath);
            DirectoryInfo[] directories = mainDirectory.GetDirectories();
            foreach (var direcotry in directories)
            {
                CreateAlbumAndPushPhotos(direcotry.FullName);
            }
        }

        private FlickrPhoto PrimaryPhoto(List<FlickrPhoto> photos, FlickrPhotoId primaryPhotoId)
        {
            var primaryPhoto = photos.SingleOrDefault(x => x.PhotoId == primaryPhotoId);
            return primaryPhoto;
        }

        public void CreateAlbumAndPushPhotos(string absolutepath)
        {
            var directory = System.IO.Directory.CreateDirectory(absolutepath);
            string albumName = directory.Name;
            FileInfo[] files = directory.GetFiles();

            var album = GetOrCreateAlbum(albumName);
            var photos = manager.GetPhotos(album);
            var primaryPhoto = PrimaryPhoto(photos, album.PrimaryPhotoId);
            var allowedTypes = config.PhotoTypes.Split(' ', ',', ';').Select(x => x.ToLower());

            foreach (var file in files)
            {
                if (allowedTypes.Contains(file.Extension.Trim('.').ToLower()))
                {
                    string path = file.FullName;
                    var fileName = System.IO.Path.GetFileNameWithoutExtension(path);

                    var photoId = photos.SingleOrDefault(x => x.Title == fileName);
                    if (photoId != null)
                    {
                        WriteVerbose($"Photo {fileName} already exists in {albumName}");
                    }
                    else
                    {
                        WriteVerbose($"Pushing {file.FullName} to album {albumName}");
                        AddPhotoToAlbumId(file.FullName, album);
                    }

                    if (primaryPhoto != null)
                    {
                        SetPrimaryPhotoInDirectory(primaryPhoto, path);
                    }
                    else
                    {
                        SetCoverPhoto(album, photoId, path);
                    }
                }
                else
                {
                    writeVerbose($"Not supported type {file.Extension}");
                }

            }
        }


        private void SetCoverPhoto(Album album, FlickrPhoto photoId, string path)
        {
            var fileName = System.IO.Path.GetFileNameWithoutExtension(path);
            if (fileName.EndsWith(Cover))
            {
                this.manager.SetCoverPhoto(album, photoId.PhotoId);
            }
        }

        private void SetPrimaryPhotoInDirectory(FlickrPhoto primaryPhoto, string path)
        {

            var directory = Path.GetDirectoryName(path);
            var extension = Path.GetExtension(path);

            var fileName = System.IO.Path.GetFileNameWithoutExtension(path);
            if (fileName == primaryPhoto.Title + Cover)
            {
                if (fileName.StartsWith(primaryPhoto.Title))
                {
                    writeVerbose($"Directory has already cover photo setup");
                }
            }
            else
            {
                if (fileName.StartsWith(primaryPhoto.Title))
                {
                    writeVerbose($"Photo {fileName} will be cover photo setting it.");
                    var destName = Path.Combine(directory, fileName + Cover + extension);
                    System.IO.File.Move(path, destName);
                }

                if (fileName.EndsWith(Cover))
                {
                    writeVerbose($"Photo {fileName} was cover photo before, resetting it.");
                    var destName = Path.Combine(directory, fileName.Replace(Cover, string.Empty) + extension);
                    System.IO.File.Move(path, destName);
                }
            }
        }
    }
}
