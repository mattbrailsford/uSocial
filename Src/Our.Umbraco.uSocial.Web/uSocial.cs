using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.XPath;
using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;
using DevDefined.OAuth.Storage.Basic;
using InstaSharp;
using InstaSharp.Models;
using InstaSharp.Models.Responses;
using Facebook;
using Our.Umbraco.uSocial.Converters;
using Our.Umbraco.uSocial.DataType.Facebook;
using Our.Umbraco.uSocial.DataType.Instagram;
using Our.Umbraco.uSocial.DataType.Twitter;
using Our.Umbraco.uSocial.Extensions;
using Our.Umbraco.uSocial.Models;
using Our.Umbraco.uSocial.Models.Facebook;
using Our.Umbraco.uSocial.Models.Twitter;
using User = Our.Umbraco.uSocial.Models.Twitter.User;

namespace Our.Umbraco.uSocial
{
    public static class uSocial 
    {
        public static class Twitter
        {
            /// <summary>
            /// Deserializes a uSocial JSON value to an actual TwitterOAuthDataValue entity.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <returns></returns>
            public static TwitterOAuthDataValue DeserializeValue(string value)
            {
                return value.DeserializeJsonTo<TwitterOAuthDataValue>();
            }

			public static Status GetTweet(string oauthToken,
		        string oauthTokenSecret,
		        string consumerKey,
		        string consumerSecret,
		        string id)
			{
				return GetTweet(new TwitterOAuthDataValue
				{
					ConsumerKey = consumerKey,
					ConsumerSecret = consumerSecret,
					OAuthToken = oauthToken,
					OAuthTokenSecret = oauthTokenSecret
				}, id);
			}

	        public static Status GetTweet(TwitterOAuthDataValue config,
				string id)
			{
				var session = CreateOAuthSession(config.ConsumerKey,
					config.ConsumerSecret,
					config.OAuthToken,
					config.OAuthTokenSecret);

				var url = string.Format("https://api.twitter.com/1.1/statuses/show.json?id={0}",
					id);

				return session.Request()
					.Get()
					.ForUrl(url)
					.ToString()
					.DeserializeJsonTo<Status>(new[]
                {
                    new TwitterTypeConverter()
                });
			}

            /// <summary>
            /// Gets the latest tweets.
            /// </summary>
            /// <param name="config">The config.</param>
            /// <param name="count">The count.</param>
            /// <param name="includeReplies">if set to <c>true</c> include replies.</param>
            /// <param name="includeRetweets">if set to <c>true</c> include retweets.</param>
            /// <returns></returns>
            public static IEnumerable<Status> GetLatestTweets(TwitterOAuthDataValue config,
                int count = 10,
                bool includeReplies = true,
                bool includeRetweets = true)
            {
                return GetLatestTweets(config,
                    config.ScreenName,
                    count,
                    includeReplies,
                    includeRetweets);
            }

            /// <summary>
            /// Gets the latest tweets.
            /// </summary>
            /// <param name="config">The config.</param>
            /// <param name="screenName">Name of the screen.</param>
            /// <param name="count">The count.</param>
            /// <param name="includeReplies">if set to <c>true</c> [include replies].</param>
            /// <param name="includeRetweets">if set to <c>true</c> [include retweets].</param>
            /// <returns></returns>
            public static IEnumerable<Status> GetLatestTweets(TwitterOAuthDataValue config,
                string screenName,
                int count = 10,
                bool includeReplies = true,
                bool includeRetweets = true)
            {
                if (string.IsNullOrWhiteSpace(config.OAuthToken)
                    || string.IsNullOrWhiteSpace(config.OAuthTokenSecret)
                    || string.IsNullOrWhiteSpace(screenName))
                {
                    return Enumerable.Empty<Status>();
                }

                return GetLatestTweets(config.OAuthToken,
                    config.OAuthTokenSecret,
                    config.ConsumerKey,
                    config.ConsumerSecret,
                    screenName,
                    count,
                    includeReplies,
                    includeRetweets);
            }

            /// <summary>
            /// Gets the latest tweets.
            /// </summary>
            /// <param name="screenName">Name of the screen.</param>
            /// <param name="oauthToken">The oauth token.</param>
            /// <param name="oauthTokenSecret">The oauth token secret.</param>
            /// <param name="consumerKey">The consumer key.</param>
            /// <param name="consumerSecret">The consumer secret.</param>
            /// <param name="count">The count.</param>
            /// <param name="includeReplies">if set to <c>true</c> include replies.</param>
            /// <param name="includeRetweets">if set to <c>true</c> include retweets.</param>
            /// <returns></returns>
            public static IEnumerable<Status> GetLatestTweets(string oauthToken,
                string oauthTokenSecret,
                string consumerKey,
                string consumerSecret,
                string screenName,
                int count = 10,
                bool includeReplies = true,
                bool includeRetweets = true)
            {
                var session = CreateOAuthSession(consumerKey,
                    consumerSecret,
                    oauthToken,
                    oauthTokenSecret);

                var url = string.Format("https://api.twitter.com/1.1/statuses/user_timeline.json?screen_name={0}&count={1}&exclude_replies={2}&include_rts={3}",
                    screenName,
                    count,
                    (!includeReplies).ToString().ToLower(),
                    includeRetweets.ToString().ToLower());

                var json = session.Request()
                    .Get()
                    .ForUrl(url)
                    .ToString();

				var results = json.DeserializeJsonTo<List<Status>>(new[]
                {
                    new TwitterTypeConverter()
                });

	            return results;
            }

            /// <summary>
            /// Searches the tweets.
            /// </summary>
            /// <param name="config">The config.</param>
            /// <param name="query">The query.</param>
            /// <param name="count">The count.</param>
            /// <returns></returns>
            public static IEnumerable<Status> SearchTweets(TwitterOAuthDataValue config,
                string query,
                int count = 10)
            {
                return SearchTweets(config.OAuthToken,
                    config.OAuthTokenSecret,
                    config.ConsumerKey,
                    config.ConsumerSecret,
                    query,
                    count);
            }

            /// <summary>
            /// Searches the tweets.
            /// </summary>
            /// <param name="oauthToken">The oauth token.</param>
            /// <param name="oauthTokenSecret">The oauth token secret.</param>
            /// <param name="consumerKey">The consumer key.</param>
            /// <param name="consumerSecret">The consumer secret.</param>
            /// <param name="query">The query.</param>
            /// <param name="count">The count.</param>
            /// <returns></returns>
            public static IEnumerable<Status> SearchTweets(string oauthToken,
                string oauthTokenSecret,
                string consumerKey,
                string consumerSecret,
                string query,
                int count = 10)
            {
                var session = CreateOAuthSession(consumerKey,
                    consumerSecret,
                    oauthToken,
                    oauthTokenSecret);

                var url = string.Format("https://api.twitter.com/1.1/search/tweets.json?q={0}&count={1}",
                    HttpUtility.UrlEncode(query),
                    count);

                var results = session.Request()
                    .Get()
                    .ForUrl(url)
                    .ToString()
                    .DeserializeJsonTo<SearchResults>(new[]
                {
                    new TwitterTypeConverter()
                });

                return results != null ? results.Statuses : Enumerable.Empty<Status>();
            }

            /// <summary>
            /// Gets the user.
            /// </summary>
            /// <param name="config">The config.</param>
            /// <returns></returns>
            public static User GetUser(TwitterOAuthDataValue config)
            {
                return GetUser(config, config.ScreenName);
            }

            /// <summary>
            /// Gets the user.
            /// </summary>
            /// <param name="config">The config.</param>
            /// <param name="screenName">Name of the screen.</param>
            /// <returns></returns>
            public static User GetUser(TwitterOAuthDataValue config,
                string screenName)
            {
                return GetUser(config.OAuthToken,
                    config.OAuthTokenSecret,
                    config.ConsumerKey,
                    config.ConsumerSecret,
                    screenName);
            }

            /// <summary>
            /// Gets the user.
            /// </summary>
            /// <param name="oauthToken">The oauth token.</param>
            /// <param name="oauthTokenSecret">The oauth token secret.</param>
            /// <param name="consumerKey">The consumer key.</param>
            /// <param name="consumerSecret">The consumer secret.</param>
            /// <param name="screenName">Name of the screen.</param>
            /// <returns></returns>
            public static User GetUser(string oauthToken,
                string oauthTokenSecret,
                string consumerKey,
                string consumerSecret,
                string screenName)
            {
                return GetUsers(oauthToken,
                    oauthTokenSecret,
                    consumerKey,
                    consumerSecret,
                    new[] { screenName }).SingleOrDefault();
            }

            /// <summary>
            /// Gets the users.
            /// </summary>
            /// <param name="config">The config.</param>
            /// <param name="screenNames">The screen names.</param>
            /// <returns></returns>
            public static IEnumerable<User> GetUsers(TwitterOAuthDataValue config,
                IEnumerable<string> screenNames)
            {
                return GetUsers(config.OAuthToken,
                    config.OAuthTokenSecret,
                    config.ConsumerKey,
                    config.ConsumerSecret,
                    screenNames);
            }

            /// <summary>
            /// Gets the user.
            /// </summary>
            /// <param name="oauthToken">The oauth token.</param>
            /// <param name="oauthTokenSecret">The oauth token secret.</param>
            /// <param name="consumerKey">The consumer key.</param>
            /// <param name="consumerSecret">The consumer secret.</param>
            /// <param name="screenNames">The screen names.</param>
            /// <returns></returns>
            public static IEnumerable<User> GetUsers(string oauthToken,
                string oauthTokenSecret,
                string consumerKey,
                string consumerSecret,
                IEnumerable<string> screenNames)
            {
                var session = CreateOAuthSession(consumerKey,
                    consumerSecret,
                    oauthToken,
                    oauthTokenSecret);

                var url = string.Format("https://api.twitter.com/1.1/users/lookup.json?screen_name={0}",
                    string.Join(",", screenNames));

                return session.Request()
                    .Get()
                    .ForUrl(url)
                    .ToString()
                    .DeserializeJsonTo<List<User>>(new[]
                {
                    new TwitterTypeConverter()
                });
            }

            /// <summary>
            /// Formats the date in the official Twitter format.
            /// </summary>
            /// <param name="date">The date.</param>
            /// <returns></returns>
            public static string FormatDate(DateTime date)
            {
                var timeSpan = DateTime.Now - date;

                if (timeSpan <= TimeSpan.FromSeconds(60))
                {
                    return timeSpan.Seconds + "s";
                }

                if (timeSpan <= TimeSpan.FromMinutes(60))
                {
                    return timeSpan.Minutes + "m";
                }

                if (timeSpan <= TimeSpan.FromHours(24))
                {
                    return timeSpan.Hours + "h";
                }

                return date.ToString("d MMM");
            }

            /// <summary>
            /// Creates an OAuth session.
            /// </summary>
            /// <param name="consumerKey">The consumer key.</param>
            /// <param name="consumerSecret">The consumer secret.</param>
            /// <param name="oauthAccessToken">The oauth access token.</param>
            /// <param name="oauthAccessTokenSecret">The oauth access token secret.</param>
            /// <returns></returns>
            internal static IOAuthSession CreateOAuthSession(string consumerKey,
                string consumerSecret,
                string oauthAccessToken,
                string oauthAccessTokenSecret)
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
                    Constants.Twitter.AccessTokenUrl)
                {
                    AccessToken = new AccessToken
                    {
                        ConsumerKey = consumerKey,
                        Token = oauthAccessToken,
                        TokenSecret = oauthAccessTokenSecret
                    }
                };
            }
        }

        public static class Instagram
        {
            /// <summary>
            /// Deserializes a uSocial JSON value to an actual InstagramOAuthDataValue entity.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <returns></returns>
            public static InstagramOAuthDataValue DeserializeValue(string value)
            {
                return value.DeserializeJsonTo<InstagramOAuthDataValue>();
            }

			public static InstaSharp.Models.Media GetMedia(InstagramOAuthDataValue config,
				string id)
			{
				return GetMedia(config.OAuthToken,
					config.ClientId,
					config.ClientSecret,
					id);
			}

			public static InstaSharp.Models.Media GetMedia(string accessToken,
				string clientId,
				string clientSecret,
				string id)
			{
				var config = new InstagramConfig(
					clientId,
					clientSecret,
					Constants.OAuthProxyUrl,
					null);

				var authInfo = new OAuthResponse { Access_Token = accessToken };

				var endpoint = new InstaSharp.Endpoints.Media(config, authInfo);
				var data = endpoint.Get(id).Data;
				return data != null ? data.Data : null;
			}

            public static IEnumerable<InstaSharp.Models.Media> SearchMedia(InstagramOAuthDataValue config,
                double latitude,
                double longitude,
                int distance = 1000)
            {
                return SearchMedia(config.OAuthToken,
                    config.ClientId,
                    config.ClientSecret,
                    latitude,
                    longitude,
                    distance);
            }

            public static IEnumerable<InstaSharp.Models.Media> SearchMedia(string accessToken,
                string clientId,
                string clientSecret,
                double latitude,
                double longitude,
                int distance = 1000)
            {
				var config = new InstagramConfig(
					clientId,
					clientSecret,
					Constants.OAuthProxyUrl,
					null);

				var authInfo = new OAuthResponse { Access_Token = accessToken };

                var endpoint = new InstaSharp.Endpoints.Media(config, authInfo);
                var data = endpoint.Search(latitude, longitude, null, null, distance).Data;
	            return data != null ? data.Data : null;
            }

            public static IEnumerable<InstaSharp.Models.Media> TaggedRecently(InstagramOAuthDataValue config,
                string tagName,
                string minId = "",
                string maxId = "")
            {
                return TaggedRecently(config.OAuthToken,
                    config.ClientId,
                    config.ClientSecret,
                    tagName,
                    minId,
                    maxId);
            }

            public static IEnumerable<InstaSharp.Models.Media> TaggedRecently(string accessToken,
                string clientId,
                string clientSecret,
                string tagName,
                string minId = "",
                string maxId = "")
            {
				var config = new InstagramConfig(
					clientId,
					clientSecret,
					Constants.OAuthProxyUrl,
					null);

                var endpoint = new InstaSharp.Endpoints.Tags(config);
				var data = endpoint.Recent(tagName, minId, maxId).Data;
				return data != null ? data.Data : null;
            }

			public static IEnumerable<InstaSharp.Models.Media> RecentMedia(InstagramOAuthDataValue config,
				string userId,
				int count = 10,
				string minId = "",
				string maxId = "")
			{
				return RecentMedia(config.OAuthToken,
					config.ClientId,
					config.ClientSecret,
					userId,
					count,
					minId,
					maxId);
			}

			public static IEnumerable<InstaSharp.Models.Media> RecentMedia(string accessToken,
				string clientId,
				string clientSecret,
				string userId,
				int count = 10,
				string minId = "",
				string maxId = "")
			{
				var config = new InstagramConfig(
					clientId,
					clientSecret,
					Constants.OAuthProxyUrl,
					null);

				var authInfo = new OAuthResponse { Access_Token = accessToken };
				var endpoint = new InstaSharp.Endpoints.Users(config, authInfo);
				var resp = endpoint.Recent(userId, maxId, minId, count);
				var data = resp.Data;
				return data != null ? data.Data : null;
			}
        }

		public static class Facebook
		{
			/// <summary>
			/// Deserializes a uSocial JSON value to an actual FacebookOAuthDataValue entity.
			/// </summary>
			/// <param name="value">The value.</param>
			/// <returns></returns>
			public static FacebookOAuthDataValue DeserializeValue(string value)
			{
				return value.DeserializeJsonTo<FacebookOAuthDataValue>();
			}

			public static Post GetPost(string accessToken,
				string clientId,
				string clientSecret,
				string clientToken,
				string id)
			{
				return GetPost(new FacebookOAuthDataValue
				{
					AppId = clientId,
					AppSecret = clientSecret,
					OAuthToken = accessToken,
					ClientToken = clientToken
				}, id);
			}

			public static Post GetPost(FacebookOAuthDataValue config,
				string id)
			{
				var fb = new FacebookClient();

				return fb.Get(id, new
				{
					client_id = config.AppId,
					client_secret = config.AppSecret,
					access_token = config.OAuthToken
				})
				.ToString()
				.DeserializeJsonTo<Post>(new[]
                {
                    new FacebookTypeConverter()
                });
			}

			public static IEnumerable<Post> GetPosts(string accessToken,
				string clientId,
				string clientSecret,
				string clientToken,
				string id)
			{
				return GetPosts(new FacebookOAuthDataValue
				{
					AppId = clientId,
					AppSecret = clientSecret,
					OAuthToken = accessToken,
					ClientToken = clientToken
				}, id);
			}

			public static IEnumerable<Post> GetPosts(FacebookOAuthDataValue config,
				string id)
			{
				var fb = new FacebookClient();

				var json = fb.Get(id + "/posts", new
				{
					client_id = config.AppId,
					client_secret = config.AppSecret,
					access_token = config.OAuthToken
				})
				.ToString();

				return json.DeserializeJsonTo<DataResponse<List<Post>>>(new[]
                {
                    new FacebookTypeConverter()
                })
				.Data;
			}
		}
    }
}