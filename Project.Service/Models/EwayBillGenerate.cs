using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    public class ListsofEwayBillGenerate
    {
        [Required] public int userid { get; set; }
        [Required] public string ClientSecret { get; set; }
        [Required] public int slno { get; set; }
    }


    public class ewaybills
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ewaybill> data { get; set; }
    }

    public class ewaybill
    {
        public string ewaybillno { get; set; }
    }

    public class EwayBillGenerateItemList
    {
        public string productName { get; set; }
        public string productDesc { get; set; }
        public string hsnCode { get; set; }
        public string quantity { get; set; }
        public string qtyUnit { get; set; }
        public string cgstRate { get; set; }
        public string sgstRate { get; set; }
        public string igstRate { get; set; }
        public string cessRate { get; set; }
        public string cessAdvol { get; set; }
        public string taxableAmount { get; set; }
    }

    public class EwayBillGenerates
    {
        public string supplyType { get; set; }
        public string subSupplyType { get; set; }
        public string docType { get; set; }
        public string subSupplyDesc { get; set; }
        public string docNo { get; set; }
        public string docDate { get; set; }
        public string fromGstin { get; set; }
        public string fromTrdName { get; set; }
        public string fromAddr1 { get; set; }
        public string fromAddr2 { get; set; }
        public string fromPlace { get; set; }
        public string fromPincode { get; set; }
        public string actFromStateCode { get; set; }
        public string fromStateCode { get; set; }
        public string toGstin { get; set; }
        public string toTrdName { get; set; }
        public string toAddr1 { get; set; }
        public string toAddr2 { get; set; }
        public string toPlace { get; set; }
        public string toPincode { get; set; }
        public string actToStateCode { get; set; }
        public string toStateCode { get; set; }
        public string totalValue { get; set; }
        public string cgstValue { get; set; }
        public string sgstValue { get; set; }
        public string igstValue { get; set; }
        public string cessValue { get; set; }
        public string totInvValue { get; set; }
        public string transporterId { get; set; }
        public string transporterName { get; set; }
        public string transDocNo { get; set; }
        public string transMode { get; set; }
        public string transDistance { get; set; }
        public string transDocDate { get; set; }
        public string vehicleNo { get; set; }
        public string vehicleType { get; set; }
        public string TransactionType { get; set; }
        public List<EwayBillGenerateItemList> itemList { get; set; }
    }



    // for both part

    public class ListsofEwayBillGenerateBoth
    {
        [Required] public int userid { get; set; }
        [Required] public string ClientSecret { get; set; }
        [Required] public int slno { get; set; }
        [Required] public string vehicleNo { get; set; }
        [Required] public string vehicleType { get; set; }
        public string drivername { get; set; }
        public string driverlicence { get; set; }
        public string logno { get; set; }
    }
}