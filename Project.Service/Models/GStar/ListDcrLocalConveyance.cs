using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GStar
{
    
    public class ListofDcrLocalConveyance
    {
        [Required]
        public int ExId { get; set; }

        [Required]
        public string date { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string transportid { get; set; }

    }

    public class GetDcrLocalConveyanceLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<GetFinalLists> data { get; set; }
       
    }

    public class GetFinalLists
    {
        public List<GetDcrLocalConveyanceList> LocalConveyanceList { get; set; }
        public List<GetexeccheckinoutlistdtwisesumList> listdtwisesumList { get; set; }
    }

    public class GetDcrLocalConveyanceList
    {
        public string orgid { get; set; }
        public string orgcat { get; set; }
        public string traveldistance { get; set; }
        public string orgnm { get; set; }
        public string travelduration { get; set; }
        
    }

    public class GetexeccheckinoutlistdtwisesumList
    {
        public string grossdistance { get; set; }
        public string claimablediastance { get; set; }
        public string odomtrkm { get; set; }
        public string BikeRate { get; set; }
        public string CarRate { get; set; }
        public string balancekm { get; set; }
        public string balanceamt { get; set; }

    }
}