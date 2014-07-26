using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Our.Umbraco.uSocial.Models.Facebook
{
	[DataContract(Name = "Post")]
	public class Profile
	{
		[DataMember]
		public string Id { get; internal set; }

		[DataMember]
		public string Name { get; internal set; }
	}
}