using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class salereturnrequestshowlist
    {
        [Required] public int CIN { get; set; }
        [Required] public string ClientSecret { get; set; }
        [Required] public int slno { get; set; }
    }

    public class salereturnrequestshows
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<salereturnrequestshow> data { get; set; }
    }

    public class salereturnrequestshow
    {
        public string rtype { get; set; }
        public int divid { get; set; }
        public int qty { get; set; }
        public int qtytype { get; set; }
        public int reason { get; set; }
        public string requestpickupfromdt { get; set; }
        public string requestpickuptodt { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string remark { get; set; }
        public string level { get; set; }
        public string requestno { get; set; }
        public string requestdt { get; set; }
        public string divisionnm { get; set; }
        public string reasonnm { get; set; }
        public string approve2dt { get; set; }
        public string name { get; set; }
        public string pickupdt { get; set; }
        public string pickuptime { get; set; }
        public string salesexnm { get; set; }
        public string contactno { get; set; }
        public string amount { get; set; }
        public string excutiveapprovedt { get; set; }
        public string itemapprlink { get; set; }
        public string billingappr { get; set; }
        public string billingapprdt { get; set; }
        public string invoicedebit { get; set; }
        public string lrno { get; set; }
        public string lrdt { get; set; }
        public string docno { get; set; }
        public string docdt { get; set; }
        public int doctype { get; set; }
        public string docamount { get; set; }
        public string transporternm { get; set; }
    }
}