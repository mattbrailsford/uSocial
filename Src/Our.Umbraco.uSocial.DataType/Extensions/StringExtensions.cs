using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Our.Umbraco.uSocial.Extensions
{
    internal static class StringExtensions
    {
        internal static string UrlTokenEncode(this string input)
        {
	        var bytes = new UTF8Encoding().GetBytes(input);
	        return HttpServerUtility.UrlTokenEncode(bytes);
        }

        internal static string UrlTokenDecode(this string input)
        {
	        var bytes = HttpServerUtility.UrlTokenDecode(input);
	        return new UTF8Encoding().GetString(bytes);
        }

		internal static string Stash(this string input)
	    {
			var id = Guid.NewGuid().ToString("N");
			HttpContext.Current.Session[id] = input;
			return id;
	    }

		internal static string Unstash(this string input)
	    {
			var obj = HttpContext.Current.Session[input];
			return obj == null ? "" : obj.ToString();
	    }
    }
}