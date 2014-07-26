using System.Xml;
using System.Xml.Linq;
using Our.Umbraco.uSocial.Extensions;
using umbraco.cms.businesslogic.datatype;

namespace Our.Umbraco.uSocial.DataType.Facebook
{
    /// <summary>
    /// The Data for the Facebook OAuth authentication data type
    /// </summary>
    public class FacebookOAuthData : DefaultData
    {
        protected FacebookOAuthOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="FacebookOAuthData"/> class.
        /// </summary>
        /// <param name="DataType">Type of the data.</param>
        /// <param name="options">The options.</param>
        public FacebookOAuthData(BaseDataType DataType,
            FacebookOAuthOptions options) 
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
                var val = Value.ToString().DeserializeJsonTo<FacebookOAuthDataValue>();

                switch (_options.DataFormat)
                {
                    case DataFormat.Xml:

                        var xmlString = new XDocument(new XElement("uSocial",
							new XElement("UserId", val.UserId),
                            new XElement("OAuthToken", val.OAuthToken),
                            new XElement("AppId", _options.AppId),
							new XElement("AppSecret", _options.AppSecret),
							new XElement("ClientToken", _options.ClientToken)
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
