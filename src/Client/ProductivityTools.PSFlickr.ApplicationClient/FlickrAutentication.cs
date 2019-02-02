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
            OAuthAccessToken accessToken = f.OAuthGetAccessToken(requestToken, verificationCode);
        }
    }
}
