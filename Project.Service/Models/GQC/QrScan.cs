using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Project.Service.Models
{
    public class ListofQrScan
    {
        [Required]
        public string QrCode { get; set; }
        [Required]
        public string ClientSecret { get; set; }
       
    }

    public class QrScans
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<data1> data { get; set; }

    }

    public class data1
    {
        public List<Item> Item { get; set; }
    }

    public class Item
    {
        public string itemName { get; set; }
        public string ItemId { get; set; }
        public string HeadID { get; set; }
        public string QRCODE { get; set; }
        public string QrSlno { get; set; }
        public List<Problem> problem { get; set; }
    
    }
    public class Problem
    {
        public string problemid { get; set; }
        public string problem { get; set; }

    }

}