using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InstaSharp;
using Our.Umbraco.uSocial.DataType.Instagram;
using Our.Umbraco.uSocial.Helpers;
using Our.Umbraco.uSocial.Models;

namespace Our.Umbraco.uSocial.Web.UI.Umbraco.Plugins.uSocial
{
    public partial class InstagramOAuthCallback : BaseCallbackPage
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
                    var preValOptions = uSocialHelper.GetPreValueOptionsById<InstagramOAuthOptions, InstagramOAuthPreValueEditor>(options.DtdId);

                    if (!string.IsNullOrWhiteSpace(preValOptions.ClientId)
                        || !string.IsNullOrWhiteSpace(preValOptions.ClientSecret))
                    {
                        clientId = preValOptions.ClientId;
                        clientSecret = preValOptions.ClientSecret;
                    }
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Unable to retreive prevalue options for the data type with the id: " + options.DtdId, ex);
                }

	            var redirectUrl = GetRedirectUrl("instagram");

	            var config = new InstagramConfig(Constants.Instagram.ApiUrl, Constants.Instagram.OauthUrl)
	            {
		            ClientId = clientId,
		            ClientSecret = clientSecret,
					RedirectURI = redirectUrl
	            };

                var code = Request["code"];

                if(string.IsNullOrEmpty(code))
                {
					string authLink = OAuth.AuthLink(config.OAuthURI + "/authorize/",
                        config.ClientId,
                        config.RedirectURI,
						new List<OAuth.Scope> { OAuth.Scope.Basic },
						OAuth.ResponseType.Code);

                    Response.Redirect(authLink, true);
                } 
                else
                {
					var auth = new OAuth(config);
                    var authInfo = auth.RequestToken(code);

                    Page.ClientScript.RegisterClientScriptBlock(typeof(InstagramOAuthCallback), "callback", @"<script>
                        self.opener.uSocial_SetValue('" + options.WrapperId + @"', '" + authInfo.Data.User.Username + @"', '" + authInfo.Data.Access_Token + @"', '');
                        window.close();
                        </script>");
                }
            }
        }
    }
}