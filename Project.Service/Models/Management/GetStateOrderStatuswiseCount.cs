using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.Management
{
    public class ListGetStateOrderStatuswiseCount
    {
        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public int StatusId { get; set; }

        public int StateId { get; set; }

        [Required]
        public string Category { get; set; }

    }
    public class GetStateOrderStatuswiseCounts
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetStateOrderStatuswiseCount> data { get; set; }
    }

    public class GetStateOrderStatuswiseCount
    {
        public int UserCategoryId { get; set; }
        public string CategoryName { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int OrderStatusId { get; set; }
        public string SevenToFifteenOrderCount { get; set; }
        public string SixteenToThirtyOrderCount { get; set; }
        public string ThirtyOneToFortyFiveOrderCount { get; set; }
        public string FortySixToSixtyOrderCount { get; set; }
        public string SixtyOneTo120DaysOrderCount { get; set; }
        public string OneTwentyOneTo365DaysOrderCount { get; set; }
        public string MoreThan365DaysOrderCount { get; set; }
    }

}