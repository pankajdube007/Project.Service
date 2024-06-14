using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
    public class GETQRCodeDetailsForStatus
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string QRCODE { get; set; } 

    }

    public class GETQRCodeDetailsForStatusLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GETQRCodeDetailsForStatusList> data { get; set; }
    }

    public class GETQRCodeDetailsForStatusList
    {
        public string QRType { get; set; }
        public string QRCode { get; set; }
        public string IBranch { get; set; }
        public string CBranch { get; set; }
        public string ProductName { get; set; }
        public string DCID { get; set; }
        public string POMappedDate { get; set; }
        public string DCNo { get; set; }
        public string DCDate { get; set; }
        public string DCPartyName { get; set; }
    }
}