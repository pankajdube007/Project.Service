using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofNearByOrgList
    {
        [Required]
        public int ExId { get; set; }

     

        [Required]
        public string Lat { get; set; }

        [Required]
        public string Lan { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class NearByOrgLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<NearByOrgList> data { get; set; }
    }

    public class NearByOrgList
    {
        public string orgname { get; set; }
        public string orgid { get; set; }
        public string catid { get; set; }
        public string catname { get; set; }
        public string Orglat { get; set; }
        public string Orglan { get; set; }

    }
}