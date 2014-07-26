using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Our.Umbraco.uSocial.Models.Twitter
{
    public class Geo
    {
        public string Type { get; set; }
        public decimal[] Coodinates { get; set; } 
    }
}