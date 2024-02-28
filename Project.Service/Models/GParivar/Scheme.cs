using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class SchemeAction
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int Type { get; set; }

       
        public int Execid { get; set; }

       
        public int Apptype { get; set; }


    }

    public class Schemes
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<Scheme> data { get; set; }
    }

    public class Scheme
    {
        //public List<RegularS> regularscheme { get; set; }
        //public List<QuantityS> quantityscheme { get; set; }
        //public List<GroupS> groupsscheme { get; set; }
        //public List<ValueS> valuesscheme { get; set; }

        public string schemename { get; set; }
        public string netsale { get; set; }
        public string curslab { get; set; }
        public string nextslab { get; set; }
    }

    //public class RegularS
    //{
    //    public string schemename { get; set; }
    //    public string netsale { get; set; }
    //    public string curslab { get; set; }
    //    public string nextslab { get; set; }
    //}

    //public class QuantityS
    //{
    //    public string schemename { get; set; }
    //    public string netsale { get; set; }
    //    public string curslab { get; set; }
    //    public string nextslab { get; set; }
    //}

    //public class GroupS
    //{
    //    public string schemename { get; set; }
    //    public string netsale { get; set; }
    //    public string curslab { get; set; }
    //    public string nextslab { get; set; }
    //}

    //public class ValueS
    //{
    //    public string schemename { get; set; }
    //    public string netsale { get; set; }
    //    public string curslab { get; set; }
    //    public string nextslab { get; set; }
    //}
}