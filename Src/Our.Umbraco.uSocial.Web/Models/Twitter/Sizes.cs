using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Our.Umbraco.uSocial.Models.Twitter
{
	[DataContract(Name = "Sizes")]
	public class Sizes
	{
		[DataMember]
		public Size Thumb { get; set; }

		[DataMember]
		public Size Small { get; set; }

		[DataMember]
		public Size Medium { get; set; }

		[DataMember]
		public Size Large { get; set; }
	}
}