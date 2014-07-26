using System.Xml;
using System.Xml.Linq;
using Our.Umbraco.uSocial.Extensions;
using umbraco.cms.businesslogic.datatype;

namespace Our.Umbraco.uSocial.DataType.Twitter
{
    /// <summary>
    /// The Data for the Twitter OAuth authentication data type
    /// </summary>
    public class TwitterOAuthData : DefaultData
    {
        protected TwitterOAuthOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterOAuthData"/> class.
        /// </summary>
        /// <param name="DataType">Type of the data.</param>
        /// <param name="options">The options.</param>
        public TwitterOAuthData(BaseDataType DataType,
            TwitterOAuthOptions options) 
            : base(DataType)
        {
            _options = options;
        }

        /// <summary>
        /// Converts the data to XML.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>
        /// The data as XML.
        /// </returns>
        public override XmlNode ToXMl(XmlDocument data)
        {
            if (Value != null && !string.IsNullOrEmpty(Value.ToString()))
            {
                var val = Value.ToString().DeserializeJsonTo<TwitterOAuthDataValue>();

                switch (_options.DataFormat)
                {
                    case DataFormat.Xml:

                        var xmlString = new XDocument(new XElement("uSocial",
                            new XElement("ScreenName", val.ScreenName),
                            new XElement("OAuthToken", val.OAuthToken),
                            new XElement("OAuthTokenSecret", val.OAuthTokenSecret),
                            new XElement("ConsumerKey", _options.ConsumerKey),
                            new XElement("ConsumerSecret", _options.ConsumerSecret)
                        )).ToString();

                        var xd = new XmlDocument();
                        xd.LoadXml(xmlString);

                        return data.ImportNode(xd.DocumentElement, true);
                }

            }

            return base.ToXMl(data);
        }
    }
}
