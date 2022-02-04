using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class FanCat
    {
        [Required]
        public string Cin { get; set; }

        [Required]
        public string ClientSecret { get; set; }

    }

    public class FanCatLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<FanCatListfinal> data { get; set; }

    }

    public class FanCatListfinal
    {
        public List<FanCatList> data { get; set; }
        public string startdate { get; set; }
        public string endsin { get; set; }
        public string ComboQtyBooked { get; set; }
        public string comboPrice { get; set; }
        public string comboQty { get; set; }
        public string maxcomboQty { get; set; }
        public string viewScheme { get; set; }
        public string isactive { get; set; }
        public string isbranchactive { get; set; }
        public string  isasiatourselect { get; set; }
        public string msg { get; set; }
    }


        public class FanCatList
    {
        public int CatID { get; set; }
        public string Cat { get; set; }
        public string Minqty { get; set; }
        public string OrderQty { get; set; }

    }

}