using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
    public class GetsalesHiearchyDataList
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }
    public class GetsalesHiearchyData
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetsalesHiearchyDatas> data { get; set; }
    }
    public class GetsalesHiearchyDatas
    {
        public string SlNo { get; set; }

        public string ExecutiveName { get; set; }
    }
}