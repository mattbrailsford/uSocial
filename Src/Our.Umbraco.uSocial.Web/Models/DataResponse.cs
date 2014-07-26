using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Our.Umbraco.uSocial.Models
{
	internal class DataResponse<TDataType>
	{
		public TDataType Data { get; set; }
	}
}