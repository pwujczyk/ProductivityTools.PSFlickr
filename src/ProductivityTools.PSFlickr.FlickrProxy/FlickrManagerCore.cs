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
    public class FlickrManagerCore
    {
        protected Config config = new Config();
        static OAuthRequestToken requestToken;
        static OAuthAccessToken oauthAccessToken;

        Flickr flickr;
        protected Flickr Flickr
        {
            get
            {
                if (flickr == null)
                {
                    flickr = GetInstanceAutenticated(config.OauthAccessTokenToken);
                    flickr.OAuthAccessToken = config.OauthAccessTokenToken;
                    flickr.OAuthAccessTokenSecret = config.OauthAccessTokenTokenSecret;
                }
                return flickr;
            }
        }

        public Flickr GetInstanceAutenticated(string token)
        {
            return new Flickr(config.ApiKey, config.SharedSecret, token);
        }

        public void GetAndSaveAccessToken(string verificationCode)
        {
            oauthAccessToken = Flickr.OAuthGetAccessToken(requestToken, verificationCode);
            config.OauthAccessTokenToken = oauthAccessToken.Token;
            config.OauthAccessTokenTokenSecret = oauthAccessToken.TokenSecret;
        }

        public string GetAuthorizationUrl()
        {
            requestToken = Flickr.OAuthGetRequestToken("oob");
            string url = Flickr.OAuthCalculateAuthorizationUrl(requestToken.Token, AuthLevel.Delete);
            return url;
        }



    }
}
