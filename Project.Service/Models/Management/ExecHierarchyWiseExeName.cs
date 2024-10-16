using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.Management
{
    public class ExecHierarchyWiseExeName
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string ExecId { get; set; }

    }
    public class ExecHierarchyWiseExeNameLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<ExecHierarchyWiseExeNameList> data { get; set; }
    }
    public class ExecHierarchyWiseExeNameList
    {
        public string ExecId { get; set; }
        public string Name { get; set; }
    }
}