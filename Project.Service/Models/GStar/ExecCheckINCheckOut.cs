using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofExecCheckINCheckOut
    {
        [Required]
        public int ExId { get; set; }
        public int EmpType { get; set; }
        

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int OrgId { get; set; }

        [Required]
        public int OrgCat { get; set; }

        [Required]
        public string DeviceId { get; set; }

        [Required]
        public string Lat { get; set; }

        [Required]
        public string Long { get; set; }

        public string address { get; set; }

        [Required]
        public decimal Distance { get; set; }

        
        public decimal MDistance { get; set; }

        [Required]
        public int Type { get; set; }

        [Required]
        public int InOuttype { get; set; }

        [Required]
        public bool IsForceFully { get; set; }

        public string InOuttime { get; set; }

        public string Image { get; set; }
    }

    public class ExecCheckINCheckOuts
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public object data { get; set; }
    }

    public class ExecCheckINCheckOut
    {
        public string CheckInLat { get; set; }
        public string CheckInLong { get; set; }
        public string CheckOutLat { get; set; }
        public string CheckOutLong { get; set; }
        public string orgid { get; set; }
        public string orgcat { get; set; }

        public string orgCate { get; set; }
        public string orgName { get; set; } 
        public string orgId { get; set; } 
        public string orgCatId { get; set; }
        public string checkInTime { get; set; } 
        public string checkOutTime { get; set; } 
        public string slno { get; set; }
        public string distnce { get; set; }

    }

    //public class ExecCheckINCheckOut
    //{
    //    public string output { get; set; }
    //}
}