using FlickrNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSFlickr.Application
{
    public class FlickrAutentication
    {
        public void a()
        {
            Flickr f = FlickrManager.GetInstance();
            OAuthRequestToken requestToken = f.OAuthGetRequestToken("oob");

            string url = f.OAuthCalculateAuthorizationUrl(requestToken.Token, AuthLevel.Delete);

            System.Diagnostics.Process.Start(url);
        }
    }
}
