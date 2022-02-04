using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class EwayBillGeneratePart2
    {
        [Required] public string ClientSecret { get; set; }
        [Required] public string ewaybillno { get; set; }
        [Required] public string vehicleno { get; set; }
        [Required] public string fromplace { get; set; }
        [Required] public int fromstate { get; set; }
        [Required] public string resoncode { get; set; }
        [Required] public string reasonrem { get; set; }
        [Required] public string transdocno { get; set; }
        [Required] public string transdocdate { get; set; }
        [Required] public string transmode { get; set; }
        [Required] public int type { get; set; }
    }

    public class EwayBillGeneratePart2body
    {
        [Required] public string EwbNo { get; set; }
        [Required] public string VehicleNo { get; set; }
        [Required] public string FromPlace { get; set; }
        [Required] public int FromState { get; set; }
        [Required] public string ReasonCode { get; set; }
        [Required] public string ReasonRem { get; set; }
        [Required] public string TransDocNo { get; set; }
        [Required] public string TransDocDate { get; set; }
        [Required] public string TransMode { get; set; }
    }

}