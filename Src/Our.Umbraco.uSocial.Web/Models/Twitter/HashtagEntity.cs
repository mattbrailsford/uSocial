using System.Runtime.Serialization;

namespace Our.Umbraco.uSocial.Models.Twitter
{
    [DataContract(Name = "Hashtag")]
    public class HashtagEntity : Entity
    {
        [DataMember]
        public string Text { get; internal set; }
    }
}