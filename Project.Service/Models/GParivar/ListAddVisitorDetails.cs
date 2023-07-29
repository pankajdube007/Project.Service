using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class ListAddVisitorDetails
    {

        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int tyepeofvisitor { get; set; }

      
        public string visitorimg { get; set; }

        [Required]
        public string fullaname { get; set; }

        [Required]
        public string Mobile { get; set; }
        
        public string emailid { get; set; }

       
        public string pincode { get; set; }


        [Required]
        public int cityid { get; set; }

        public string Address { get; set; }
        
        public string companyname { get; set; }
        public string concernperson { get; set; }
        public string designation { get; set; }
        public string contactno { get; set; }
        public string email { get; set; }
        [Required]
        public string pin { get; set; }
        
        public int city1 { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string visitingcardimg { get; set; }
        public int typeofvisitor1 { get; set; }
        public string FullNamevisitor1 { get; set; }
        public string mobilevisitor1 { get; set; }
        public int typeofvisitor2 { get; set; }
        public string FullNamevisitor2 { get; set; }
        public string mobilevisitor2 { get; set; }
        [Required]
        public int leadtype { get; set; }
        [Required]
        public DateTime followupdatetime { get; set; }
        public string followupremark { get; set; }
        
        public string itemid { get; set; }
        [Required]
        public int VisitorId { get; set; }

        [Required]
        public int Showroomid { get; set; }
    }

    public class AddVisitorDetails
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<AddVisitorDetail> data { get; set; }
    }

    public class AddVisitorDetail
    {
        public string output { get; set; }
    }
}