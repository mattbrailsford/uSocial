using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Our.Umbraco.uSocial.Models.Facebook
{
	[DataContract(Name = "Post")]
	public class Post
	{
		[DataMember]
		public string Id { get; internal set; }

		[DataMember]
		public string Picture { get; internal set; }

		[DataMember]
		public string Message { get; internal set; }

		[DataMember]
		public string Link { get; internal set; }

		[DataMember]
		public DateTime CreatedTime { get; internal set; }

		[DataMember]
		public Profile From { get; internal set; }

		[DataMember]
		public Action[] Actions { get; internal set; }
	}
}