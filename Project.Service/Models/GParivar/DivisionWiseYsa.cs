using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class DivisionWiseYsaAction
    {
        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public int divisionid { get; set; }

        
        public int ExecId { get; set; }
    }

    public class DivisionWiseYsas
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<DivisionWiseYsa> data { get; set; }
    }

    public class DivisionWiseYsa
    {
        public List<DivisionWiseDetails> DivisionWiseSale { get; set; }
        public List<DivisionWiseTotal> DivisionWiseSaleTotal { get; set; }
    }

    public class DivisionWiseDetails
    {
        public string categorynm { get; set; }
        public string sale { get; set; }
    }

    public class DivisionWiseTotal
    {
        public string TotalSale { get; set; }
    }
}