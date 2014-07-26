using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Our.Umbraco.uSocial.Extensions;

namespace Our.Umbraco.uSocial
{
    internal class Constants
    {
        internal const string OAuthProxyUrl = "https://oauthproxy.apphb.com/redirect.aspx";

        internal class Twitter
        {
            internal const string RequestTokenUrl = "https://api.twitter.com/oauth/request_token";
            internal const string AuthorizeUrl = "https://api.twitter.com/oauth/authorize";
            internal const string AccessTokenUrl = "https://api.twitter.com/oauth/access_token";

            internal const string VerifyCredentialsUrl = "https://api.twitter.com/1.1/account/verify_credentials.json";
        }

        internal class Instagram
        {
            internal const string ApiUrl = "https://api.instagram.com/v1";
			internal const string OauthUrl = "https://api.instagram.com/oauth";
        }

		internal class Facebook
		{

		}
    }
}
