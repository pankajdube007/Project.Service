using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class TotalPaymentDivisionWiseManagementAction
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public int BranchId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }

        [Required]
        public int index { get; set; }

        [Required]
        public int count { get; set; }
    }

    public class TotalPaymentDivisionWiseManagements
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<TotalPaymentDivisionWiseManagementFinal> data { get; set; }
    }

    public class TotalPaymentDivisionWiseManagementFinal
    {
        public List<TotalPaymentDivisionWiseManagement> paymentdetails { get; set; }
        public TotalPaymentDivisionWiseManagementTotal PaymentdetailsTotal { get; set; }
        public bool ismore { get; set; }
    }

    public class TotalPaymentDivisionWiseManagement
    {
        public string partynm { get; set; }
        public string cin { get; set; }
        public string partystatus { get; set; }
        public string exnm { get; set; }

        [JsonProperty(PropertyName = "wiringdevices")]
        public string WIRINGDEVICES { get; set; }

        [JsonProperty(PropertyName = "lights")]
        public string LIGHTS { get; set; }

        [JsonProperty(PropertyName = "wireandcable")]
        public string WIRECABLE { get; set; }

        [JsonProperty(PropertyName = "pipesandfittings", NullValueHandling = NullValueHandling.Include)]
        public string PIPESFITTINGS { get; set; }

        [JsonProperty(PropertyName = "mcbanddbs")]
        public string MCBDBS { get; set; }

        [JsonProperty(PropertyName = "fans")]
        public string FAN { get; set; }
        [JsonProperty(PropertyName = "HEALTHCARE")]
        public string HEALTHCARE { get; set; }
    }


    public class TotalPaymentDivisionWiseManagementTotal
    {
        public string wiringdevicetotal { get; set; }
        public string lightetotal { get; set; }
        public string wireandcabletotal { get; set; }
        public string pipesandfittingtotal { get; set; }
        public string mcbanddbtotal { get; set; }
        public string fantotal { get; set; }
        public string HEALTHCAREtotal { get; set; }
    }
}