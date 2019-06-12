using System;
using System.Runtime.Serialization;

namespace spacex.api.Models
{
    /// <summary>
    /// Launchpad gets the data from the spacex api
    /// </summary>
    [Serializable]
    [DataContract]
    public class Launchpad
    {

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string SiteID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Status { get; set; }
    }
}