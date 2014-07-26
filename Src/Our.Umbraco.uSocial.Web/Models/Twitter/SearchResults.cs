using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Our.Umbraco.uSocial.Models.Twitter
{
    [DataContract(Name = "SearchResults")]
    public class SearchResults
    {
        [DataMember]
        public IList<Status> Statuses { get; set; }
    }
}