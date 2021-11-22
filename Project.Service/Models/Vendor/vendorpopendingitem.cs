using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
  
    public class Listofvendorpopendingitem
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string distdlid { get; set; }

        [Required]
        public string asondate { get; set; }

        [Required]
        public string divisionid1 { get; set; }

        [Required]
        public string pocat { get; set; }

        [Required]
        public string PartyID { get; set; }

        [Required]
        public string TypeCat { get; set; }

        [Required]
        public string branchIDs { get; set; }

        [Required]
        public string Cat { get; set; }

    }

    public class vendorpopendingitems
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<vendorpopendingitem> data { get; set; }
    }

    public class vendorpopendingitem
    {
        public string materialissuefrom { get; set; }
        public string increaselimit { get; set; }
        public string tolarencelimit1 { get; set; }
        public string Urgent { get; set; }
        public string ApproveQty { get; set; }
        public string DispatchQty { get; set; }
        public string addline1 { get; set; }
        public string pending { get; set; }
        public string SchemeQty { get; set; }
        public string BaseCode { get; set; }
        public string itemnm { get; set; }
        public string colornm { get; set; }
        public string rangenm { get; set; }
        public string categorynm { get; set; }
        public string divisionnm { get; set; }
        public string unitnm { get; set; }
        public string slno { get; set; }
        public string PCategory { get; set; }
        public string PartyName { get; set; }
        public string MaterialIssueBranch { get; set; }
        public string areanm { get; set; }
        public string city { get; set; }
        public string statenm { get; set; }
        public string salesexname { get; set; }
        public string PartyType { get; set; }
        public string HomeBranch { get; set; }
        public string mobile { get; set; }
        public string SchemePer { get; set; }
        public string cartoonbox { get; set; }
        public string boxqty { get; set; }
        public string lose { get; set; }
        public string Stockqty { get; set; }

    }
}