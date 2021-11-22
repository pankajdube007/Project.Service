using System;

namespace Project.Service.Models.Citi
{
    public class CitiPayoutTransaction
    {
        //public string Value { get; set; }
        public string Description { get; set; }
        public string Remarks { get; set; }
        public string CitiUTR { get; set; }
        public string TransferType { get; set; }
        public DateTime ValueDate { get; set; }
        public DateTime? EntryDate { get; set; }
        public string FundsCode { get; set; }
        public string Amount { get; set; }
        public string TransactionType { get; set; }
        public string GoldmedalTransactionReferenceId { get; set; }
        public string DebitCredit { get; set; }
        public string Account { get; set; }
        public string DetailsDescription { get; set; }
        public string Name { get; set; }
        //public string SupplementaryDetails { get; set; }
        public string AccountServicingReference { get; set; }
    }
}