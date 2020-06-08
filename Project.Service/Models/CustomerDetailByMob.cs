using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Project.Service.Models
{


    public class ListCustomerDetailByMob
    {
        [Required]
        public string Cat { get; set; }
        [Required]
        public string CIN { get; set; }
        [Required]
        public string Mobile { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }
    public class CustomerDetailByMobLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<CustomerDetailByMobList> data { get; set; }
    }
    public class CustomerDetailByMobList
    {
        public string SlNo { get; set; }
        public string UserCat { get; set; }
        public string categorynm { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string MobileNo { get; set; }
        public string DateOfBirth { get; set; }
        public string Sex { get; set; }
        public string RefCode { get; set; }
        public string Email { get; set; }
        public string Hmaddress { get; set; }
        public string Hmaddress1 { get; set; }
        public string Hmstate { get; set; }
        public string statenm { get; set; }
        public string Hmdistrict { get; set; }
        public string Distrctnm { get; set; }
        public string Hmcity { get; set; }
        public string citynm { get; set; }
        public string Hmpincode { get; set; }
        public string CIN { get; set; }
        public string AddressTypeId { get; set; }
        public string ShopName { get; set; }
        public string GstNo { get; set; }
        public string Deluid { get; set; }
        public string Gstscan { get; set; }
        public string ShopPhoto { get; set; }
        public string ShopEstCerti { get; set; }
        public string Profilephoto { get; set; }
        public string Wrkaddress { get; set; }
        public string Wrkaddress1 { get; set; }
        public string Wrkstate { get; set; }
        public string Wrkdistrict { get; set; }
        public string Wrkcity { get; set; }
        public string Wrkpincode { get; set; }
        public string workAddressTypeId { get; set; }
        public string wrkstatenm { get; set; }
        public string wrkdistrictnm { get; set; }
        public string wrkcitynm { get; set; }
        public string KycdocumentNo1 { get; set; }
        public string documentimglink1 { get; set; }
        public string KycDocMasterId1{ get; set; }
        public string KycdocumentNo2 { get; set; }
        public string documentimglink2 { get; set; }
        public string KycDocMasterId2 { get; set; }
        public string ApprovalStatus { get; set; }
        



    }
}