using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Our.Umbraco.uSocial.Models.Twitter
{
    [DataContract(Name = "Entities")]
    public class Entities
    {
        [DataMember]
        public IEnumerable<UrlEntity> Urls { get; internal set; }

        [DataMember]
        public IEnumerable<UserMentionEntity> UserMentions { get; internal set; }

        [DataMember]
		public IEnumerable<HashtagEntity> Hashtags { get; internal set; }

		[DataMember]
		public IEnumerable<MediaEntity> Media { get; internal set; }
    }
}