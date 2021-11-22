using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class StockBalanceAction
    {
        [Required]
        public string uniquekey { get; set; }

        [Range(1, 1000)]
        public int BranchId { get; set; }

        [Range(0, 1000)]
        public int WareHouseId { get; set; }

        [Range(0, int.MaxValue)]
        public int ItemId { get; set; }

        public int CatId { get; set; }
        public string Searchtext { get; set; }
        public int index { get; set; }
        public int Count { get; set; }
    }

    public class StockBalances
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public bool Ismore { get; set; }
        public List<StockBalance> data { get; set; }
    }

    public class StockBalance
    {
        public int slno { get; set; }
        public string itemcode { get; set; }
        public string ItemName { get; set; }
        public string colornm { get; set; }
        public string balanceqty { get; set; }
    }
}