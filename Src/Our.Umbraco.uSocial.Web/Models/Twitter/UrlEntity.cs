using System.Runtime.Serialization;

namespace Our.Umbraco.uSocial.Models.Twitter
{
    [DataContract(Name = "Url")]
    public class UrlEntity : Entity
    {
        [DataMember]
        public string Url { get; internal set; }

        [DataMember]
        public string DisplayUrl { get; internal set; }

        [DataMember]
        public string ExpandedUrl { get; internal set; }
    }
}