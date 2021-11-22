using System;
using CsvHelper.Configuration;

namespace Project.Service.Models.Citi
{
    public class CitiBenResponse
    {
        public string PreformatCode { get; set; }
        public string LastModifiedDateUserName { get; set; }
        public string LastUsedDate { get; set; }
        public string Status { get; set; }
        //public string BeneficiaryNameAddress { get; set; }
        //public string BeneficiaryAccountorOtherID { get; set; }
        //public string BeneficiaryBankRoutingCode { get; set; }
        //public string BeneficiaryBankAddress { get; set; }
        //public string BeneficiaryBankName { get; set; }
        public string BeneficiaryBankCode { get; set; }
        public string DebitPartyAccountNumber { get; set; }
        public string DebitPartyName { get; set; }
        public string DebitBankName { get; set; }
        public string DebitBankAddress { get; set; }
        public string BeneficiaryAccountNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string EmailAddres { get; set; }
    }

    internal sealed class CitiBenResponseMap : ClassMap<CitiBenResponse>
    {
        public CitiBenResponseMap()
        {
            Map(x => x.PreformatCode).Name("Preformat Code");
            Map(x => x.LastModifiedDateUserName).Name("Last Modified Date/User Name");
            Map(x => x.LastUsedDate).Name("Last Used Date");
            Map(x => x.Status).Name("Status");
            //Map(x => x.BeneficiaryNameAddress).Name("Beneficiary Name/Address").Index(1);;
            //Map(x => x.BeneficiaryAccountorOtherID).Name("Beneficiary Account or Other ID");
            //Map(x => x.BeneficiaryBankRoutingCode).Name("Beneficiary Bank Routing Code");
            //Map(x => x.BeneficiaryBankAddress).Name("Beneficiary Bank Address");
            //Map(x => x.BeneficiaryBankName).Name("Beneficiary Bank Name");
            Map(x => x.BeneficiaryBankCode).Name("Beneficiary Bank Code");
            Map(x => x.DebitPartyAccountNumber).Name("Debit Party Account Number");
            Map(x => x.DebitPartyName).Name("Debit Party Name");
            Map(x => x.DebitBankName).Name("Debit Bank Name");
            Map(x => x.DebitBankAddress).Name("Debit Bank Address");
            Map(x => x.BeneficiaryAccountNumber).Name("Beneficiary Account Number");
            Map(x => x.ExpiryDate).Name("Expiry Date");
            Map(x => x.EmailAddres).Name("Email Address");
            //Map(m => m.FirstName).Name("Name").NameIndex(0);
            //Map(m => m.LastName).Name("Name").NameIndex(1);
        }
    }
}