using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models
{
    
    public class ListofGetItemDetailsByCategoryAndNameorCode
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public int CategoryID { get; set; }

        
        public string ItemSearchText { get; set; }
    }

    public class GetItemDetailsByCategoryAndNameorCodeLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetItemDetailsByCategoryAndNameorCodeList> data { get; set; }
    }

    public class GetItemDetailsByCategoryAndNameorCodeList
    {
      
        public string itemid { get; set; }
        public string CategoryId { get; set; }
        public string categorynm { get; set; }
        public string SubCategoryId { get; set; }
        public string Subcategorynm { get; set; }
        public string DivisionId { get; set; }
        public string divisionnm { get; set; }
        public string itemcode { get; set; }
        public string ERPItemCode { get; set; }
        public string mrp { get; set; }
        public string dlp { get; set; }
        public string discount { get; set; }
        public string taxtype { get; set; }
        public string taxpercent { get; set; }
        public string pramotionaldiscount { get; set; }
        public string ApproveQty { get; set; }
        public string UnapproveQty { get; set; }
        public string CartoonQty { get; set; }
        public string BoxQty { get; set; }
        public string Unitnm { get; set; }
        public string ColorHexValue { get; set; }
        public string ItemImages { get; set; }
        public string ImageBaseURL { get; set; }
        public string ColorName { get; set; }

    }

    //public class OrderItemDetailsVM
    //{
    //    public string ERPItemCode { get; set; }
    //    public string slno { get; set; }
    //    public string ProductCode { get; set; }
    //    public string categoryid { get; set; }
    //    public string divisionid { get; set; }
    //    public string categorynm { get; set; }
    //    public string divisionnm { get; set; }
    //    public string mrp { get; set; }
    //    public string dlp { get; set; }
    //    public string discount { get; set; }

    //    public string tax { get; set; }
    //    public string taxtype { get; set; }
    //    public string taxpercent { get; set; }

    //    public string promotional { get; set; }
    //    public string approvedqty { get; set; }
    //    public string unapprovedqty { get; set; }
    //    public string cartonqty { get; set; }
    //    public string boxqty { get; set; }
    //    public string unitnm { get; set; }
    //    public string rangeid { get; set; }
    //    public string rangenm { get; set; }
    //    public string ItemImages { get; set; }
    //    public string ImageBaseURL { get; set; }
    //    public string ColorHexValue { get; set; }
    //    public string ColorName { get; set; }

    //}
}