using System.Runtime.Serialization;

namespace Our.Umbraco.uSocial.Models.Twitter
{
    [DataContract(Name = "UserMention")]
    public class UserMentionEntity : Entity
    {
        [DataMember]
        public long Id { get; internal set; }

        [DataMember]
        public string IdStr { get; internal set; }

        [DataMember]
        public string ScreenName { get; internal set; }

        [DataMember]
        public string Name { get; internal set; }
    }
}