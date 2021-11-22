using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofCategoryWiseFilter
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public int Hierarchy { get; set; }
    }

    public class CategoryWiseFilters
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<CategoryWiseFilterFinal> data { get; set; }
    }

    public class CategoryWiseFilterFinal
    {
        public List<CategoryWiseFilterdistrict> districts { get; set; }
        public List<CategoryWiseFilterarea> areas { get; set; }
        public List<CategoryWiseFilterbranch> branches { get; set; }
        public List<CategoryWiseFilterexcutive> executives { get; set; }
    }

    public class CategoryWiseFilterdistrict
    {
        public int districtid { get; set; }
        public string district { get; set; }
    }

    public class CategoryWiseFilterbranch
    {
        public int branchid { get; set; }
        public string branch { get; set; }
    }

    public class CategoryWiseFilterarea
    {
        public int areaid { get; set; }
        public string area { get; set; }
    }

    public class CategoryWiseFilterexcutive
    {
        public int exid { get; set; }
        public string executive { get; set; }
    }
}