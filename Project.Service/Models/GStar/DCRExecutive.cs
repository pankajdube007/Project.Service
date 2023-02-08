using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListofDCRExecutive
    {
        [Required(ErrorMessage = "Please enter ExId.")]
        public int ExId { get; set; }

        [Required]
        public int slno { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required(ErrorMessage = "Please enter dcrdate.")]
        public string dcrdate { get; set; }

        [Required(ErrorMessage = "Please enter dcrtime.")]
        public string dcrtime { get; set; }

        [Required(ErrorMessage = "Please enter dcrduration.")]
        public string dcrduration { get; set; }

        [Required(ErrorMessage = "Please enter modeid.")]
        public int modeid { get; set; }

        [Required(ErrorMessage = "Please enter catid.")]
        public int catid { get; set; }

        [Required(ErrorMessage = "Please enter orgid.")]
        public int orgid { get; set; }

        [Required(ErrorMessage = "Please enter addressid.")]
        public int addressid { get; set; }


        public string contactperson { get; set; }

        [Required(ErrorMessage = "Please enter purposeid.")]
        public string purposeid { get; set; }

        
        public string productid { get; set; }

        [Required(ErrorMessage = "Please enter priority.")]
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
        public int EmpType { get; set; }
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