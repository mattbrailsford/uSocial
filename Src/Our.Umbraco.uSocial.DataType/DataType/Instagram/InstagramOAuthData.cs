using System.Xml;
using System.Xml.Linq;
using Our.Umbraco.uSocial.Extensions;
using umbraco.cms.businesslogic.datatype;

namespace Our.Umbraco.uSocial.DataType.Instagram
{
    /// <summary>
    /// The Data for the Instagram OAuth authentication data type
    /// </summary>
    public class InstagramOAuthData : DefaultData
    {
        protected InstagramOAuthOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="InstagramOAuthData"/> class.
        /// </summary>
        /// <param name="DataType">Type of the data.</param>
        /// <param name="options">The options.</param>
        public InstagramOAuthData(BaseDataType DataType,
            InstagramOAuthOptions options) 
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
                var val = Value.ToString().DeserializeJsonTo<InstagramOAuthDataValue>();

                switch (_options.DataFormat)
                {
                    case DataFormat.Xml:

                        var xmlString = new XDocument(new XElement("uSocial",
                            new XElement("ScreenName", val.ScreenName),
                            new XElement("OAuthToken", val.OAuthToken),
                            new XElement("ClientId", _options.ClientId),
                            new XElement("ClientSecret", _options.ClientSecret)
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
