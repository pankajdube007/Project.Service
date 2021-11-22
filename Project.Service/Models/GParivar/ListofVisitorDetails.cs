using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class ListofVisitorDetails
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

       
        public string SearchBy { get; set; }
    }

    public class GetListofVisitorDetails
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetListofVisitorDetail> data { get; set; }
    }

    public class GetListofVisitorDetail
    {
        public int  visitorid { get; set; }
        public int ExId { get; set; }
        public string salesexnm { get; set; }
        public string visitorcode { get; set; }
        public int tyepeofvisitor { get; set; }
        public string visitortype { get; set; }
        public string visitorimg { get; set; }
        public string fullaname { get; set; }
        public string Mobile { get; set; }
        public string emailid { get; set; }
        public string pincode { get; set; }
        public int cityid { get; set; }
        public string citynm { get; set; }
        public string Address { get; set; }
        public string companyname { get; set; }
        public string concernperson { get; set; }
        public string designation { get; set; }
        public string contactno { get; set; }
        public string Companyemail { get; set; }
        public string Companypin { get; set; }
        public int CompanyCityId { get; set; }
        public string CompanyCityName { get; set; }
        public string CompanyAddress1 { get; set; }
        public string CompanyAddress2 { get; set; }
        public string visitingcardimg { get; set; }
        public string typeofvisitor1 { get; set; }
        public string FullNamevisitor1 { get; set; }
        public string mobilevisitor1 { get; set; }
        public string typeofvisitor2 { get; set; }
        public string FullNamevisitor2 { get; set; }
        public string mobilevisitor2 { get; set; }
        public int leadtypeID { get; set; }
        public string leadtype { get; set; }
        public string followupdatetime { get; set; }
        public string followupremark { get; set; }
        public string itemid { get; set; }
        public string itemids { get; set; }
        public int stateid { get; set; }
        public string statename { get; set; }
        public int companystateid { get; set; }
        public string companystatename { get; set; }
    }
}