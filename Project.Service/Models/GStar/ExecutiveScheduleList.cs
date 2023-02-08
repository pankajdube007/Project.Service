using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class ListsofExecutiveScheduleList
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        public int OrgId { get; set; }

        public int OrgCat { get; set; }
        public int EmpType { get; set; }
    }

    public class ExecutiveScheduleLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ExecutiveScheduleList> data { get; set; }
    }

    public class ExecutiveScheduleList
    {
        public int slno { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public string contactmodeid { get; set; }
        public string contactmodename { get; set; }
        public string partycatid { get; set; }
        public string partycatname { get; set; }
        public string organizationid { get; set; }
        public string organizationname { get; set; }
        public string productcatid { get; set; }
        public string productcatname { get; set; }
        public string addressid { get; set; }
        public string addressname { get; set; }
        public string contactpersonid { get; set; }
        public string contactperson { get; set; }
        public string purposeid { get; set; }
        public string purposename { get; set; }
        public string priority { get; set; }
        public string remark { get; set; }
        public string reason { get; set; }
        public string status { get; set; }
    }
}