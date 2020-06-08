using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListofDCRExecutive
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public int slno { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string dcrdate { get; set; }

        [Required]
        public string dcrtime { get; set; }

        [Required]
        public string dcrduration { get; set; }

        [Required]
        public int modeid { get; set; }

        [Required]
        public int catid { get; set; }

        [Required]
        public int orgid { get; set; }

        [Required]
        public int addressid { get; set; }


        public string contactperson { get; set; }

        [Required]
        public string purposeid { get; set; }

        [Required]
        public string productid { get; set; }

        [Required]
        public string priority { get; set; }

        public string remark { get; set; }

        public int reschduled { get; set; }
        public string redcrdate { get; set; }
        public string redcrtime { get; set; }
        public int remodeid { get; set; }
        public int readdressid { get; set; }
        public string recontactperson { get; set; }
        public string repurposeid { get; set; }
        public string reproductid { get; set; }
        public string repriority { get; set; }
        public string reason { get; set; }
        public int transportid { get; set; }
        public string journeydistance { get; set; }
        public string systemdistance { get; set; }
    }

    //public class DCRExecutives
    //{
    //    public bool result { get; set; }
    //    public string message { get; set; }
    //    public string servertime { get; set; }
    //    public List<DCRExecutive> data { get; set; }
    //}
    //public class DCRExecutive
    //{
    //    public string Status { get; set; }
    //}
}