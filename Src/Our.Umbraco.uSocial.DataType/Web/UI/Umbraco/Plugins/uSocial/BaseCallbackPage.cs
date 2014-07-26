using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Our.Umbraco.uSocial.Extensions;

namespace Our.Umbraco.uSocial.Web.UI.Umbraco.Plugins.uSocial
{
    public abstract class BaseCallbackPage : Page
    {
        protected string CreateOAuthProxyUrl(string url)
        {
            return string.Concat(Constants.OAuthProxyUrl,
                "?url=",
                url.UrlTokenEncode());
        }

	    protected string GetRedirectUrl(string key, bool proxy = false)
	    {
			var cookieKey = key + "_oauth_redirecturi_" + Request.QueryString["o"];
			var redirectUrl = "";

			if (Request.Cookies[cookieKey] == null)
			{
				redirectUrl = Request.Url.GetLeftPart(UriPartial.Path) + "?o=" + Request.QueryString["o"];
				
				if (proxy)
				{
					redirectUrl = CreateOAuthProxyUrl(redirectUrl);
				}

				Response.Cookies.Add(new HttpCookie(cookieKey, redirectUrl) { Expires = DateTime.Now.AddMinutes(5) });
			}
			else
			{
				redirectUrl = Request.Cookies[cookieKey].Value;
			}

		    return redirectUrl;
	    }
    }
}