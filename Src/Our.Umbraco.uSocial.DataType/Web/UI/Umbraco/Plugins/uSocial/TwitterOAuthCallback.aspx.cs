using System;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;
using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;
using Our.Umbraco.uSocial.DataType;
using Our.Umbraco.uSocial.DataType.Instagram;
using Our.Umbraco.uSocial.DataType.Twitter;
using Our.Umbraco.uSocial.Extensions;
using Our.Umbraco.uSocial.Helpers;
using Our.Umbraco.uSocial.Models;
using Our.Umbraco.uSocial.Models.Twitter;

namespace Our.Umbraco.uSocial.Web.UI.Umbraco.Plugins.uSocial
{
    public partial class TwitterOAuthCallback : BaseCallbackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var consumerKey = "";
            var consumerSecret = "";

            CallbackOptions options;
            if (Request.QueryString.AllKeys.Contains("o")
                && !string.IsNullOrWhiteSpace(Request.QueryString["o"])
                && CallbackOptions.TryParse(Request.QueryString["o"], out options)
                && options != null)
            {
                try
                {
                    var preValOptions = uSocialHelper.GetPreValueOptionsById<TwitterOAuthOptions, TwitterOAuthPreValueEditor>(options.DtdId);

                    if (!string.IsNullOrWhiteSpace(preValOptions.ConsumerKey)
                        || !string.IsNullOrWhiteSpace(preValOptions.ConsumerSecret))
                    {
                        consumerKey = preValOptions.ConsumerKey;
                        consumerSecret = preValOptions.ConsumerSecret;
                    }
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Unable to retreive prevalue options for the data type with the id: " + options.DtdId, ex);
                }

                var session = CreateOAuthSession(consumerKey, consumerSecret);

                var requestTokenString = Request[Parameters.OAuth_Token];
                var verifier = Request[Parameters.OAuth_Verifier];

                if(string.IsNullOrEmpty(verifier))
                {
                    var requestToken = session.GetRequestToken();

                    Session[requestToken.Token] = requestToken;

                    Response.Redirect(session.GetUserAuthorizationUrlForToken(requestToken), true);
                }
                else
                {
                    var requestToken = (IToken)Session[requestTokenString];

                    var accessToken = session.ExchangeRequestTokenForAccessToken(requestToken, verifier);

                    var accountSettings = session
                        .Request()
                        .Get()
                        .ForUrl(Constants.Twitter.VerifyCredentialsUrl)
                        .ToString()
                        .DeserializeJsonTo<User>();
                
                    Page.ClientScript.RegisterClientScriptBlock(typeof(TwitterOAuthCallback), "callback", @"<script>
                        self.opener.uSocial_SetValue('" + options.WrapperId + @"', '" + accountSettings.screen_name + @"', '" + accessToken.Token + @"', '" + accessToken.TokenSecret + @"');
                        window.close();
                        </script>");
                }
            }
        }

        private IOAuthSession CreateOAuthSession(string consumerKey,
            string consumerSecret)
        {
            var consumerContext = new OAuthConsumerContext
            {
                ConsumerKey = consumerKey,
                ConsumerSecret = consumerSecret,
                SignatureMethod = SignatureMethod.HmacSha1
            };

            return new OAuthSession(consumerContext,
                Constants.Twitter.RequestTokenUrl,
                Constants.Twitter.AuthorizeUrl,
                Constants.Twitter.AccessTokenUrl,
                Request.Url.ToString())
                    .RequiresCallbackConfirmation(); 
        }
    }
}