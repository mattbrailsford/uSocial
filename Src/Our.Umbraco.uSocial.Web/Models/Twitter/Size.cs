using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Our.Umbraco.uSocial.Models.Twitter
{
	[DataContract(Name = "Size")]
	public class Size
	{
		[DataMember]
		public int W { get; set; }

		[DataMember]
		public int H { get; set; }

		[DataMember]
		public string Resize { get; set; }
	}
}