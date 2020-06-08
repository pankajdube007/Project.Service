using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{




    public class ListAllExpenseChildAllsubchild
    {
        [Required]
        public string Cat { get; set; }
        [Required]
        public string CIN { get; set; }
        [Required]
        public string Fromdate { get; set; }
        [Required]
        public string Todate { get; set; }
        [Required]
        public string ClientSecret { get; set; }

        public string SupplierName { get; set; }

        public string LedgerName { get; set; }

        public string BranchName { get; set; }

        [Required]
        public int Index { get; set; }

        [Required]
        public int Count { get; set; }
    }


    public class ListAllExpenseChildAllsubchildAll
    {
        [Required]
        public string Cat { get; set; }
        [Required]
        public string CIN { get; set; }
        [Required]
        public string Fromdate { get; set; }
        [Required]
        public string Todate { get; set; }
        [Required]
        public string ClientSecret { get; set; }

       
    }
    public class AllExpenseChildAllsubchildLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<AllExpenseChildAllsubchildListFinal> data { get; set; }
    }


    public class AllExpenseChildAllsubchildListFinal
    {
        public List<AllExpenseChildAllsubchildList> ExpenseChilddata { get; set; }
        public bool ismore { get; set; }
    }


        public class AllExpenseChildAllsubchildList
    {
        public int slno { get; set; }
        public string Branch { get; set; }
        public string SupplierName { get; set; }
        public string LedgerName { get; set; }
        public string VoucherNo { get; set; }
        public string Date { get; set; }
        public string Amount { get; set; }
        public string Narration { get; set; }
        public string Paymentmode { get; set; }
        public string Type { get; set; }
        public string link { get; set; }
    }


    public class AllExpenseChildAllsubchildHeadLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<AllExpenseChildAllsubchildHead> data { get; set; }
    }


    public class AllExpenseChildAllsubchildHead
    {

        public List<AllExpenseChildAllsubchildHeadSupplier> Supplier { get; set; }
        public List<AllExpenseChildAllsubchildHeadLedger> ledger { get; set; }
    }


    public class AllExpenseChildAllsubchildHeadSupplier
    {
        public string name { get; set; }

    }

    public class AllExpenseChildAllsubchildHeadLedger
    {
        public string name { get; set; }

    }
}