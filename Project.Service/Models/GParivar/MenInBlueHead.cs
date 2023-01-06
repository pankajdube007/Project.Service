using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace Project.Service.Models
{
   


    public class ListMenInBlueHead
    {

        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class MenInBlueHeads
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<MenInBlueHead> data { get; set; }

    }

    public class MenInBlueHead
    {

       
        public string Name { get; set; }
        public string Branch { get; set; }
        public string TotalPoint { get; set; }
        public string AusPoint { get; set; }
        public string PendingPoints { get; set; }

        public string address { get; set; }
        public bool isselection { get; set; }
        public bool isEditable { get; set; }
      
    }

   


}