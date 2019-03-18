using ProductivityTools.PSFlickr.FlickrProxy.FlickrSimpleObjects;
using ProductivityTools.PSFlickr.FlickrProxy.Ids;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.PSFlickr.ApplicationClient
{
    public class SyncOneDirectory: BaseOperations
    {
        protected CommonOperations commonOperations = new CommonOperations();
        string Cover = "_Cover";

        public SyncOneDirectory(Action<string> writeVerbose)
        {
            this.WriteVerbose = writeVerbose;
        }

        public void CreateAlbumAndPushPhotos(string absolutepath)
        {
            var directory = System.IO.Directory.CreateDirectory(absolutepath);
            string albumName = directory.Name;
            var onlineAlbum = GetOrCreateAlbum(albumName);

            ResettingMoreThanOneLocalPrimaryPhoto(directory);
            UploadPrimaryPhotoIfExists(directory, onlineAlbum);
            SetFirstFileAsCoverIfCoverDoesntExists(directory, onlineAlbum);
            SetCoverPhotoOnDiskAsInOnlineAlbum(directory, onlineAlbum);

            UploadFiles(directory, onlineAlbum);
        }

        private void SetCoverPhotoOnDiskAsInOnlineAlbum(DirectoryInfo directory, Album onlineAlbum)
        {
            var photosInOnlineAlbum = manager.GetPhotos(onlineAlbum);
            var onlinePrimaryPhoto = PrimaryPhoto(photosInOnlineAlbum, onlineAlbum.PrimaryPhotoId);
            FileInfo[] files = directory.GetFiles();
            if (onlineAlbum.PrimaryPhotoId.Id != null)
            {
                var onlinePrimaryPhotoWithoutCover = onlinePrimaryPhoto.Title.Replace(Cover, string.Empty);
                FileInfo diskPhotoToSetAsCover = files.FirstOrDefault(x => Path.GetFileNameWithoutExtension(x.FullName) == onlinePrimaryPhotoWithoutCover);
                if (diskPhotoToSetAsCover != null)
                {
                    ClearAllCoverPhotos(files);
                    RenameFileToBeCover(diskPhotoToSetAsCover);
                }
            }
        }

        private void SetFirstFileAsCoverIfCoverDoesntExists(DirectoryInfo directory, Album onlineAlbum)
        {
            var files = directory.GetFiles();
            var localPrimaryPhotos = files.Where(x => Path.GetFileNameWithoutExtension(x.FullName).EndsWith(Cover));
            var localPrimaryPhoto = localPrimaryPhotos.SingleOrDefault();
            if (onlineAlbum.PrimaryPhotoId.Id == null && localPrimaryPhoto == null)
            {
                var candidateForOnlinePrimaryPhoto = files.FirstOrDefault();
                if (candidateForOnlinePrimaryPhoto != null)
                {
                    RenameFileToBeCover(candidateForOnlinePrimaryPhoto);
                    files = directory.GetFiles();
                }
            }
        }

        private void UploadPrimaryPhotoIfExists(DirectoryInfo directory,Album onlineAlbum)
        {
            var files = directory.GetFiles();
            var localPrimaryPhotos = files.Where(x => Path.GetFileNameWithoutExtension(x.FullName).EndsWith(Cover));
            var localPrimaryPhoto = localPrimaryPhotos.SingleOrDefault();
            if (onlineAlbum.PrimaryPhotoId.Id == null && localPrimaryPhoto != null)
            {
                //updload local photo, cover will be set automatically as this is first photo
                FlickrPhotoId onlinePhotoId = commonOperations.AddPhotoToAlbumId(localPrimaryPhotos.Single().FullName, onlineAlbum);
            }
        }

        private void ResettingMoreThanOneLocalPrimaryPhoto(DirectoryInfo directory)
        {
            var files = directory.GetFiles();
            var localPrimaryPhotos = files.Where(x => Path.GetFileNameWithoutExtension(x.FullName).EndsWith(Cover));
            if (localPrimaryPhotos.Count() > 1)
            {
                ClearAllCoverPhotos(files);
                files = directory.GetFiles();
                localPrimaryPhotos = files.Where(x => Path.GetFileNameWithoutExtension(x.FullName).EndsWith(Cover));
            }
        }

        private void UploadFiles(DirectoryInfo directory, Album onlineAlbum)
        {
            var files = directory.GetFiles();
            var photosInOnlineAlbum = manager.GetPhotos(onlineAlbum);
            var allowedTypes = config.PhotoTypes.Split(' ', ',', ';').Select(x => x.ToLower());
            foreach (var file in files)
            {
                if (allowedTypes.Contains(file.Extension.Trim('.').ToLower()))
                {
                    string path = file.FullName;
                    var fileName = System.IO.Path.GetFileNameWithoutExtension(path);

                    var onlinePhoto = photosInOnlineAlbum.SingleOrDefault(x => x.Title == fileName);
                    if (onlinePhoto == null)
                    {
                        WriteVerbose($"Pushing {file.FullName} to album {onlineAlbum.Name}");
                        FlickrPhotoId onlinePhotoId = commonOperations.AddPhotoToAlbumId(file.FullName, onlineAlbum);

                    }
                    else
                    {
                        WriteVerbose($"Photo {fileName} already exists in {onlineAlbum.Name}");
                    }
                }
                else
                {
                    WriteVerbose($"Not supported type {file.Extension}");
                }
            }
        }

        private FlickrPhoto PrimaryPhoto(List<FlickrPhoto> photos, FlickrPhotoId primaryPhotoId)
        {
            var primaryPhoto = photos.SingleOrDefault(x => x.PhotoId == primaryPhotoId);
            return primaryPhoto;
        }

        private Album GetOrCreateAlbum(string name)
        {
            Album album = commonOperations.GetAlbumByName(name);
            if (album == null)
            {
                album = commonOperations.CreateAlbumInternal(name);
            }
            return album;
        }

        private void ClearAllCoverPhotos(FileInfo[] files)
        {
            var coverFiles = files.Where(x => Path.GetFileNameWithoutExtension(x.FullName).EndsWith(Cover));
            foreach (var coverFile in coverFiles)
            {
                var fileName = coverFile.FullName;
                WriteVerbose($"Photo {fileName} was cover photo before, resetting it.");
                var destName = fileName.Replace(Cover, string.Empty);
                System.IO.File.Move(fileName, destName);
            }
        }

        private void RenameFileToBeCover(FileInfo fileInfo)
        {
            var path = fileInfo.FullName;
            var directory = Path.GetDirectoryName(path);
            var extension = Path.GetExtension(path);
            var fileName = System.IO.Path.GetFileNameWithoutExtension(path);
            WriteVerbose($"Photo {fileName} will be cover photo setting it.");
            var destName = Path.Combine(directory, fileName + Cover + extension);
            File.Move(path, destName);
        }
    }
}
