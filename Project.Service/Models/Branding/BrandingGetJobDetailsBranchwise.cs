using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{




    public class ListBrandingGetJobDetailsBranchwise
    {
        [Required]
        public string CIN { get; set; }
        [Required]
        public string Cat { get; set; }
        [Required]
        public string BranchId { get; set; }
        [Required]
        public string FromDate { get; set; }
        [Required]
        public string ToDate { get; set; }
        [Required]
        public string ClientSecret { get; set; }

    }
    public class BrandingGetJobDetailsBranchwiseLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<BrandingGetJobDetailsBranchwiseList> data { get; set; }
    }
    public class BrandingGetJobDetailsBranchwiseList
    {
        public string slno { get; set; }
        public string JobReqNo { get; set; }
        public string JobReqdt { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string BranchName { get; set; }
        public string Address { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string SubName { get; set; }
        public string SubAddress { get; set; }
        public string SubContactPerson { get; set; }
        public string SubContact { get; set; }
        public string SubEmail { get; set; }
        public string JRCreateBy { get; set; }
        public string JRCreatedt { get; set; }
        public string JRGivenBy { get; set; }
        public string JobType { get; set; }
        public string SubJob { get; set; }
        public string SubSubJob { get; set; }
        public string DesignType { get; set; }
        public string OldProductType { get; set; }
        public string Quantity { get; set; }
        public string TotalAmount { get; set; }
        public string JRImageLink { get; set; }
        public string JRImage { get; set; }
        public string JRDelBy { get; set; }
        public string JRDeldt { get; set; }
        public string JRApproveBy { get; set; }
        public string JRApprovedt { get; set; }
        public string JRDisApproveBy { get; set; }
        public string JRDisApprovedt { get; set; }
        public string JRApprovestatus { get; set; }
        public string JRCDelBy { get; set; }
        public string JRCDeldt { get; set; }
        public string JobAssignRequestNo { get; set; }
        public string AssignTo { get; set; }
        public string Deadline { get; set; }
        public string Assigndate { get; set; }
        public string AssignBy { get; set; }
        public string AssignDelBy { get; set; }
        public string AssignDeldt { get; set; }
        public string SizeType { get; set; }
        public string SubmitImage { get; set; }
        public string DesignSubmitLink { get; set; }
        public string JobSubmitBy { get; set; }
        public string DesignSubmitDate { get; set; }
        public string JSApproveBy { get; set; }
        public string JSApprovedt { get; set; }
        public string JSDisApproveBy { get; set; }
        public string JSDisApprovedt { get; set; }
        public string DesignSubmitStatus { get; set; }
        public string PrinterRequestNo { get; set; }
        public string AssignToPrinterBy { get; set; }
        public string AssignToPrinterdt { get; set; }
        public string ASPDelBy { get;  set; }
        public string ASPDeldt { get; set; }
        public string PrinterSubmitImage { get; set; }
        public string PrinterSubmitLink { get; set; }
        public string PrinterReceivedDate { get; set; }
        public string PrinterReceiveStatus { get; set; }
        public string PrinterJobApprBy { get; set; }
        public string PrinterJobApprdt { get; set; }
        public string PrinterApprStatus { get; set; }
        public string FabricatorRequestNo { get; set; }
        public string AssignToFabricatorBy { get; set; }
        public string AssignToFabricatordt { get; set; }
        public string ASFDelBy { get; set; }
        public string ASFDeldt { get; set; }
        public string FabricatorSubmitImage { get; set; }
        public string FabricatorSubmitLink { get; set; }
        public string FabricatorSubmitDate { get; set; }
        public string FabricatorSubmitStatus { get; set; }
        public string FabricatorJobApprBy { get; set; }
        public string FabricatorJobApprdt { get; set; }
        public string FabricatorApprStatus { get; set; }
        public string FinalLink { get; set; }
        public string FinalImage { get; set; }
        public string FinalAsmBy { get; set; }
        public string FinalAsdt { get; set; }
        public string JobCloseBy { get; set; }
        public string JobCloseDate { get; set; }
        public string JobCloseStatus { get; set; }
        public string JobCloseRemark { get; set; }
        public string JRHeadSlno { get; set; }
        public string JRChildSlno { get; set; }
        public string ASJSlno { get; set; }
        public string DSSlno { get; set; }
        public string ASPSlno { get; set; }
        public string ASFSlno { get; set; }
        public string FASlno { get; set; }
        public string NewProductType { get; set; }
        public string PrinterName { get; set; }
        public string PrinterEmail { get; set; }
        public string PrinterMobile { get; set; }
        public string PrinterContact { get; set; }
        public string FabricatorName { get; set; }
        public string FabricatorEmail { get; set; }
        public string FabricatorContact { get; set; }
        public string FabricatorMobile { get; set; }
        public string sendemail { get; set; }
        public string sendemaildt { get; set; }
        public string isapproveparty { get; set; }
        public string isapprovepartydt { get; set; }
        public string ismailsend { get; set; }
        public string uplodepartyimg { get; set; }
        public string ispayment { get; set; }
        public string managementapr { get; set; }
        public string finalapr { get; set; }
        public string Unit { get; set; }
        public string GivenByName { get; set; }
        public string BoardType { get; set; }
        public string PrintLocation { get; set; }
        public string FabricatorLocation { get; set; }
        public string Priority { get; set; }
        public string ReopenByName { get; set; }
        public string Reopendt { get; set; }
        public string JobSendTypeName { get; set; }
        public string PartyTypeName { get; set; }
        public string SendToName { get; set; }
        public string LRNumber { get; set; }
        public string LRDate { get; set; }
        public string TranspoterName { get; set; }
        public string JobSendToAddress { get; set; }
        public string JobSendByName { get; set; }
        public string JobReceiveByName { get; set; }
        public string JobReceiveDate { get; set; }
        public string JobReceiveCreateDate { get; set; }
        public string ApprovalGivenByName { get; set; }
        public string MgmRemark { get; set;  }
        public string MgmApprovalDate { get; set; }
        public string PrintCost { get; set; }
        public string FabricationCost { get; set; }

    }
}