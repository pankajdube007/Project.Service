using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofOrgAddDetailsEx
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public int OrgId { get; set; }

        [Required]
        public int CatId { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class OrgAddDetailsExs
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<OrgAddDetailsFinalEx> data { get; set; }
    }

    public class OrgAddDetailsFinalEx
    {
        public List<OrgAddDetailsEx> address { get; set; }
        public List<OrgContactpersonDetailsEx> contactperson { get; set; }
    }

    public class OrgAddDetailsEx
    {
        public int slno { get; set; }
        public string name { get; set; }
    }

    public class OrgContactpersonDetailsEx
    {
        public int slno { get; set; }
        public string contactname { get; set; }
    }
}