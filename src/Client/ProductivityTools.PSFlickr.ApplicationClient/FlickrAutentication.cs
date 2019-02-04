using FlickrNet;
using ProductivityTools.PSFlickr.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.PSFlickr.Application.Client
{
    public class FlickrAutentication
    {
        //pw: change it
        static OAuthRequestToken requestToken;
        static OAuthAccessToken accessToken;
        public void OpenAutorizeAddress()
        {
            Flickr f = FlickrManager.GetInstance();
            requestToken = f.OAuthGetRequestToken("oob");

            string url = f.OAuthCalculateAuthorizationUrl(requestToken.Token, AuthLevel.Delete);

            System.Diagnostics.Process.Start(url);
        }

        public void GetAccessToken(string verificationCode)
        {
            Flickr f = FlickrManager.GetInstance();
            accessToken = f.OAuthGetAccessToken(requestToken, verificationCode);
        }

        public void BuildPhotoTree()
        {
            //album phtoos
            Dictionary<Photoset, PhotosetPhotoCollection> albumPhotosList = new Dictionary<Photoset, PhotosetPhotoCollection>();

            Flickr f = FlickrManager.GetInstanceAutenticated(accessToken.Token);
            f.OAuthAccessToken = accessToken.Token;
            f.OAuthAccessTokenSecret = accessToken.TokenSecret;
            PhotosetCollection x=f.PhotosetsGetList();
            foreach (Photoset item in x)
            {
                var photoList = f.PhotosetsGetPhotos(item.PhotosetId);
                albumPhotosList.Add(item, photoList);
            }
        }

        public void AddPhoto()
        {
            Flickr f = FlickrManager.GetInstanceAutenticated(accessToken.Token);
            f.OAuthAccessToken = accessToken.Token;
            f.OAuthAccessTokenSecret = accessToken.TokenSecret;

            var photoId=f.UploadPicture(@"d:\Photographs\Processed\zdjeciaDone\2008.00.00 Wroclaw Zeromskiego\IMGP2130.JPG");

        }

        public void CreateAlbum()
        {
            AddPhoto();
            Flickr f = FlickrManager.GetInstanceAutenticated(accessToken.Token);
            f.OAuthAccessToken = accessToken.Token;
            f.OAuthAccessTokenSecret = accessToken.TokenSecret;
            f.PhotosetsCreate("pawel", "46934493902");
        }
    }
}
