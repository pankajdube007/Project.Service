using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Service.Models.GParivar
{
    public class AddRaiseTicket
    {
    }

    public class ListofAddRaiseTicket
    {

        [Required]
        public string CIN { get; set; }

        [Required]
        public string ClientSecret { get; set; }


        public long slno { get; set; }


        public long slnoCopy { get; set; }

        public string TktNo { get; set; }


        public string Tktdt { get; set; }


        public long TicketOwnerID { get; set; }


        public int ServiceCenterID { get; set; }


        public string CustName { get; set; }


        public string CustContactNo { get; set; }


        public string ContactPersonContactNo { get; set; }

        public string EmailID { get; set; }


        public string CustAddress { get; set; }


        public string Address2 { get; set; }


        public string Address3 { get; set; }


        public string Pincode { get; set; }


        //public long PincodeID { get; set; }


        public int StateID { get; set; }


        public int DistrictID { get; set; }


        public string City { get; set; }


        public string PersonCallingName { get; set; }


        public string ContactPersonName { get; set; }

        public int ProductInputType { get; set; }

        public string ItemSerialNo { get; set; }

        public string ItemEANNo { get; set; }

        public int QRInputType { get; set; }

        public string ItemQRCode { get; set; }


        public int DivisionID { get; set; }


        public int CategoryID { get; set; }


        public long ItemID { get; set; }


        public string ProductDescription { get; set; }


        public string PurchaseDt { get; set; }

        public string WarrantyUpToDt { get; set; }

        public int IsPincodeAvailable { get; set; }

        public int PartyType { get; set; }
        public long PartyID { get; set; }
        public int PartyAddressID { get; set; }


        public bool IsProductWarnty { get; set; }

        public bool IsDealerCall { get; set; }


        public string ProductIssue { get; set; }


        public string ProductIssueDesc { get; set; }

        public string CloseRemark { get; set; }


        public long StatusID { get; set; }


        public int TktPriority { get; set; }


        public int TktSource { get; set; }


        public long CustomerID { get; set; }

        public int IsProcessedTicket { get; set; }

        public int TktStatusID { get; set; }


        public long CustomerProductID { get; set; }

        public long AddressID { get; set; }

        public int createuid { get; set; }

        public long logno { get; set; }

        public Guid Custuniquekey { get; set; }

        public Guid Itemuniquekey { get; set; }

        public Guid uniquekey { get; set; }

        public int ApplicationID { get; set; }

        public int CompanyID { get; set; }
    }

    public class AddRaiseTicketDetails
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<AddRaiseTicketDetail> data { get; set; }
    }

    public class AddRaiseTicketDetail
    {
        public string output { get; set; }
    }
}