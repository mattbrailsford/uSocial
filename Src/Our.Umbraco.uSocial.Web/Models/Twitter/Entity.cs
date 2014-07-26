using System.Runtime.Serialization;

namespace Our.Umbraco.uSocial.Models.Twitter
{
    [DataContract(Name = "Entity")]
    public class Entity
    {
        [DataMember]
        public int[] Indices { get; internal set; }
    }
}