using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
  


    public class ListUserAprStatusCityWisedetails
    {

        public string CIN { get; set; }
        [Required]
        public string Cat { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string FromDate { get; set; }
        [Required]
        public string ToDate { get; set; }
        [Required]
        public int DistrictId { get; set; }
        [Required]
        public string CityName { get; set; }
        [Required]
        public string ApproveStatus { get; set; }
        [Required]
        public string ClientSecret { get; set; }
    }
    public class UserAprStatusCityWisedetailsLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<UserAprStatusCityWisedetailsList> data { get; set; }
    }
    public class UserAprStatusCityWisedetailsList
    {
        public string slno { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string Category { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public string ShopName { get; set; }
        public string JoinDate { get; set; }

    }
}