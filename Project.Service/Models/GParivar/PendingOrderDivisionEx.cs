using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class PendingOrderDivisionExAction
    {
        [Required]
        public int ExId { get; set; }

        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int Hierarchy { get; set; }

        [Required]
        public int index { get; set; }

        [Required]
        public int count { get; set; }
    }

    public class PendingOrderDivisionExs
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<PendingOrderDivisionEx> data { get; set; }
    }

    public class PendingOrderDivisionEx
    {
        public List<PendingOrderDivisionExDetails> pendingdetails { get; set; }
        public List<PendingOrderDivisionExTotal> pendingtotal { get; set; }
        public bool ismore { get; set; }
    }

    public class PendingOrderDivisionExDetails
    {
        public string partynm { get; set; }
        public string exnm { get; set; }

        [JsonProperty(PropertyName = "wiring devices")]
        public string WIRINGDEVICES { get; set; }

        [JsonProperty(PropertyName = "lights")]
        public string LIGHTS { get; set; }

        [JsonProperty(PropertyName = "wire & cable")]
        public string WIRECABLE { get; set; }

        [JsonProperty(PropertyName = "pipes & fittings", NullValueHandling = NullValueHandling.Include)]
        public string PIPESFITTINGS { get; set; }

        [JsonProperty(PropertyName = "mcb & dbs")]
        public string MCBDBS { get; set; }

        [JsonProperty(PropertyName = "FAN")]
        public string FAN { get; set; }
        //   public string pending { get; set; }
    }

    public class PendingOrderDivisionExTotal
    {
        public string pendingtotal { get; set; }
    }
}