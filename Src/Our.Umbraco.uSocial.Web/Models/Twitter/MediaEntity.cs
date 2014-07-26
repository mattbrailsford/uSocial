using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Our.Umbraco.uSocial.Models.Twitter
{
	[DataContract(Name = "Media")]
	public class MediaEntity
	{
		[DataMember]
		public long Id { get; set; }

		[DataMember]
		public string MediaUrl { get; set; }

		[DataMember]
		public string MediaUrlHttps { get; set; }

		[DataMember]
		public string Url { get; set; }

		[DataMember]
		public string DisplayUrl { get; set; }

		[DataMember]
		public string ExpandedUrl { get; set; }

		[DataMember]
		public Sizes Sizes { get; set; }

		[DataMember]
		public string Type { get; set; }

		[DataMember]
		public int[] Indices { get; set; }
	}
}