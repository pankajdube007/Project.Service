using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
  
    public class ListBrandingGetDesignersJobDetails
    {
        [Required]
        public string CIN { get; set; }
        [Required]
        public string Cat { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string DesignerID { get; set; }
        [Required]
        public string ClientSecret { get; set; }

    }
    public class BrandingGetDesignersJobDetailsLists
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string servertime { get; set; }
        public List<BrandingGetDesignersJobDetailsList> data { get; set; }
    }
    public class BrandingGetDesignersJobDetailsList
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
        public string Unit { get; set; }
        public string GivenByName { get; set; }
        public string BoardType { get; set; }
        public string PrintLocation { get; set; }
        public string FabricatorLocation { get; set; }
        public string Priority { get; set; }
        public string ReopenByName { get; set; }
        public string Reopendt { get; set; }
        public string JRApproveBy { get; set; }
        public string JRApprovedt { get; set; }
        public string JRDisApproveBy { get; set; }
        public string JRDisApprovedt { get; set; }
        public string JRApprovestatus { get; set; }
        public string JobAssignRequestNo { get; set; }
        public string AssignTo { get; set; }
        public string Deadline { get; set; }
        public string Assigndate { get; set; }
        public string AssignBy { get; set; }
        public string SizeType { get; set; }
        public string SubmitImage { get; set; }
        public string DesignSubmitLink { get; set; }
        public string DesignSubmitDate { get; set; }
        public string DesignSubmitStatus { get; set; }


    }
}