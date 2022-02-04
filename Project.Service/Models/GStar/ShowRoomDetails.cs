using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class ShowRoomDetails
    {

        [Required]
        public int ExId { get; set; }

        [Required]
        public int Slno { get; set; }

        [Required]
        public string ClientSecret { get; set; }

    }

    public class ShowRoomAppDetails
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ShowRoomAppDetail> data { get; set; }
    }

    public class ShowRoomAppDetail
    {
        
        public string ShowRoomName { get; set; }
        public string Address { get; set; }
        public string Image { get; set; }
        public string AreaName { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public string ShowRoomType { get; set; }

    }
}