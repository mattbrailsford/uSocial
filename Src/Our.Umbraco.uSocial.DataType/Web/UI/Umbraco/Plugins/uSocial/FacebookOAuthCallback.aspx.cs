using System;
using System.Linq;
using Facebook;
using Our.Umbraco.uSocial.DataType.Facebook;
using Our.Umbraco.uSocial.Helpers;
using Our.Umbraco.uSocial.Models; 

namespace Our.Umbraco.uSocial.Web.UI.Umbraco.Plugins.uSocial
{
    public partial class FacebookOAuthCallback : BaseCallbackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var clientId = "";
            var clientSecret = "";

            CallbackOptions options;
            if (Request.QueryString.AllKeys.Contains("o")
                && !string.IsNullOrWhiteSpace(Request.QueryString["o"])
                && CallbackOptions.TryParse(Request.QueryString["o"], out options)
                && options != null)
            {
                try
                {
                    var preValOptions = uSocialHelper.GetPreValueOptionsById<FacebookOAuthOptions, FacebookOAuthPreValueEditor>(options.DtdId);

                    if (!string.IsNullOrWhiteSpace(preValOptions.AppId)
                        || !string.IsNullOrWhiteSpace(preValOptions.AppSecret))
                    {
                        clientId = preValOptions.AppId;
                        clientSecret = preValOptions.AppSecret;
                    }
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Unable to retreive prevalue options for the data type with the id: " + options.DtdId, ex);
                }

	            var fb = new FacebookClient();
	            var redirectUrl = GetRedirectUrl("facebook", true);

				var code = Request["code"];

				if (string.IsNullOrEmpty(code))
				{
					var loginUrl = fb.GetLoginUrl(new
					{
						client_id = clientId,
						client_secret = clientSecret,
						redirect_uri = redirectUrl,
						response_type = "code",
						display = "popup",
					});

					Response.Redirect(loginUrl.AbsoluteUri, true);
				}
				else
				{
					// Get the access token
					dynamic result = fb.Post("oauth/access_token", new
					{
						client_id = clientId,
						client_secret = clientSecret,
						redirect_uri = redirectUrl,
						code = code
					});

					//throw new Exception(result.ToString());

					var shortLivedToken = result["access_token"];
					var extendedToken = GetExtendedAccessToken(clientId, clientSecret, shortLivedToken);

					dynamic userData = fb.Get("me", new
					{
						client_id = clientId,
						client_secret = clientSecret,
						access_token = extendedToken
					});

					Page.ClientScript.RegisterClientScriptBlock(typeof(InstagramOAuthCallback), "callback", @"<script>
                        self.opener.uSocial_SetValue('" + options.WrapperId + @"', '" + userData["id"] + @"', '" + extendedToken + @"', '');
                        window.close();
                        </script>");
				}
            }
        }

	    private string GetExtendedAccessToken(string clientId, string clientSecret, string shortLivedToken)
		{
			var client = new FacebookClient();
			var extendedToken = "";
			try
			{
				dynamic result = client.Get("/oauth/access_token", new
				{
					grant_type = "fb_exchange_token",
					client_id = clientId,
					client_secret = clientSecret,
					fb_exchange_token = shortLivedToken
				});
				extendedToken = result["access_token"];
			}
			catch
			{
				extendedToken = shortLivedToken;
			}
			return extendedToken;
		}
    }
}