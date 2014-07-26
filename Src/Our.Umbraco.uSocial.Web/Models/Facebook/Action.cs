using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Our.Umbraco.uSocial.Models.Facebook
{
	[DataContract(Name = "Action")]
	public class Action
	{
		[DataMember]
		public string Name { get; internal set; }

		[DataMember]
		public string Link { get; internal set; }
	}
}