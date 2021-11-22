using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers
{
    public class BrandingGetJobDetailsBranchwiseController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getbrandinggetjobdetailsbranchwise")]
        public HttpResponseMessage GetDetails(ListBrandingGetJobDetailsBranchwise ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<BrandingGetJobDetailsBranchwiseLists> alldcr = new List<BrandingGetJobDetailsBranchwiseLists>();
                    List<BrandingGetJobDetailsBranchwiseList> alldcr1 = new List<BrandingGetJobDetailsBranchwiseList>();

                    var dr = g1.return_dr("BrandingGetJobDetailsBranchwise '" + ula.CIN + "','" + ula.Cat + "','" + ula.BranchId + "','" + ula.FromDate + "','" + ula.ToDate + "'");


                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new BrandingGetJobDetailsBranchwiseList
                            {

                                 slno = Convert.ToString(dr["slno"].ToString()),
                                JobReqNo = Convert.ToString(dr["JobReqNo"].ToString()),
                                JobReqdt = Convert.ToString(dr["JobReqdt"].ToString()),
                                Type = Convert.ToString(dr["Type"].ToString()),
         Name = Convert.ToString(dr["Name"].ToString()),
         BranchName = Convert.ToString(dr["BranchName"].ToString()),
         Address = Convert.ToString(dr["Address"].ToString()),
         ContactPerson = Convert.ToString(dr["ContactPerson"].ToString()),
         ContactNo = Convert.ToString(dr["ContactNo"].ToString()),
                                Email = Convert.ToString(dr["Email"].ToString()),
                            
         SubName = Convert.ToString(dr["SubName"].ToString()),
         SubAddress = Convert.ToString(dr["SubAddress"].ToString()),
         SubContactPerson = Convert.ToString(dr["SubContactPerson"].ToString()),
         SubContact = Convert.ToString(dr["SubContact"].ToString()),
         SubEmail = Convert.ToString(dr["SubEmail"].ToString()),
         JRCreateBy = Convert.ToString(dr["JRCreateBy"].ToString()),
         JRCreatedt = Convert.ToString(dr["JRCreatedt"].ToString()),
         JRGivenBy = Convert.ToString(dr["JRGivenBy"].ToString()),
         JobType = Convert.ToString(dr["JobType"].ToString()),
         SubJob = Convert.ToString(dr["SubJob"].ToString()),
         SubSubJob = Convert.ToString(dr["SubSubJob"].ToString()),
                                DesignType= Convert.ToString(dr["DesignType"].ToString()),
         Quantity = Convert.ToString(dr["Quantity"].ToString()),
                                TotalAmount = Convert.ToString(dr["TotalAmount"].ToString()),
                                JRImageLink = Convert.ToString(dr["JRImageLink"].ToString()),
                                JRImage = Convert.ToString(dr["JRImage"].ToString()),
         JRDelBy = Convert.ToString(dr["JRDelBy"].ToString()),
         JRDeldt = Convert.ToString(dr["JRDeldt"].ToString()),
         JRApproveBy = Convert.ToString(dr["JRApproveBy"].ToString()),
         JRApprovedt = Convert.ToString(dr["JRApprovedt"].ToString()),
         JRDisApproveBy = Convert.ToString(dr["JRDisApproveBy"].ToString()),
                                JRDisApprovedt = Convert.ToString(dr["JRDisApprovedt"].ToString()),
                                JRApprovestatus = Convert.ToString(dr["JRApprovestatus"].ToString()),
                                JRCDelBy = Convert.ToString(dr["JRCDelBy"].ToString()),
         JRCDeldt = Convert.ToString(dr["JRCDeldt"].ToString()),
         JobAssignRequestNo = Convert.ToString(dr["JobAssignRequestNo"].ToString()),
                                AssignTo = Convert.ToString(dr["AssignTo"].ToString()),
                                Deadline = Convert.ToString(dr["Deadline"].ToString()),
                                Assigndate = Convert.ToString(dr["Assigndate"].ToString()),
                                AssignBy = Convert.ToString(dr["AssignBy"].ToString()),
         AssignDelBy = Convert.ToString(dr["AssignDelBy"].ToString()),
                                AssignDeldt = Convert.ToString(dr["AssignDeldt"].ToString()),
                                SizeType = Convert.ToString(dr["SizeType"].ToString()),
         SubmitImage = Convert.ToString(dr["SubmitImage"].ToString()),
                                DesignSubmitLink = Convert.ToString(dr["DesignSubmitLink"].ToString()),
                                JobSubmitBy = Convert.ToString(dr["JobSubmitBy"].ToString()),
                                DesignSubmitDate = Convert.ToString(dr["DesignSubmitDate"].ToString()),
                                JSApproveBy = Convert.ToString(dr["JSApproveBy"].ToString()),
                                JSApprovedt = Convert.ToString(dr["JSApprovedt"].ToString()),
                                JSDisApproveBy = Convert.ToString(dr["JSDisApproveBy"].ToString()),
                                JSDisApprovedt = Convert.ToString(dr["JSDisApprovedt"].ToString()),
                                DesignSubmitStatus = Convert.ToString(dr["DesignSubmitStatus"].ToString()),
                                PrinterRequestNo = Convert.ToString(dr["PrinterRequestNo"].ToString()),
                                AssignToPrinterBy = Convert.ToString(dr["AssignToPrinterBy"].ToString()),
                                AssignToPrinterdt = Convert.ToString(dr["AssignToPrinterdt"].ToString()),
                                ASPDelBy = Convert.ToString(dr["ASPDelBy"].ToString()),
                                ASPDeldt = Convert.ToString(dr["ASPDeldt"].ToString()),
                                PrinterSubmitImage = Convert.ToString(dr["PrinterSubmitImage"].ToString()),
                                PrinterSubmitLink = Convert.ToString(dr["PrinterSubmitLink"].ToString()),
                                PrinterReceivedDate = Convert.ToString(dr["PrinterReceivedDate"].ToString()),
                                PrinterReceiveStatus = Convert.ToString(dr["PrinterReceiveStatus"].ToString()),
                                PrinterJobApprBy = Convert.ToString(dr["PrinterJobApprBy"].ToString()),
                                PrinterJobApprdt = Convert.ToString(dr["PrinterJobApprdt"].ToString()),
                                PrinterApprStatus = Convert.ToString(dr["PrinterApprStatus"].ToString()),
                                FabricatorRequestNo = Convert.ToString(dr["FabricatorRequestNo"].ToString()),
         AssignToFabricatorBy = Convert.ToString(dr["AssignToFabricatorBy"].ToString()),
         AssignToFabricatordt = Convert.ToString(dr["AssignToFabricatordt"].ToString()),
         ASFDelBy = Convert.ToString(dr["ASFDelBy"].ToString()),
         ASFDeldt = Convert.ToString(dr["ASFDeldt"].ToString()),
         FabricatorSubmitImage = Convert.ToString(dr["FabricatorSubmitImage"].ToString()),
         FabricatorSubmitLink = Convert.ToString(dr["FabricatorSubmitLink"].ToString()),
         FabricatorSubmitDate = Convert.ToString(dr["FabricatorSubmitDate"].ToString()),
         FabricatorSubmitStatus = Convert.ToString(dr["FabricatorSubmitStatus"].ToString()),
         FabricatorJobApprBy = Convert.ToString(dr["FabricatorJobApprBy"].ToString()),
         FabricatorJobApprdt = Convert.ToString(dr["FabricatorJobApprdt"].ToString()),
         FabricatorApprStatus = Convert.ToString(dr["FabricatorApprStatus"].ToString()),
         FinalLink = Convert.ToString(dr["FinalLink"].ToString()),
                                FinalImage = Convert.ToString(dr["FinalImage"].ToString()),
                                FinalAsmBy = Convert.ToString(dr["FinalAsmBy"].ToString()),
                                FinalAsdt = Convert.ToString(dr["FinalAsdt"].ToString()),
                                JobCloseBy = Convert.ToString(dr["JobCloseBy"].ToString()),
                                JobCloseDate = Convert.ToString(dr["JobCloseDate"].ToString()),
                                JobCloseStatus = Convert.ToString(dr["JobCloseStatus"].ToString()),
                                JobCloseRemark = Convert.ToString(dr["JobCloseRemark"].ToString()),
                                JRHeadSlno = Convert.ToString(dr["JRHeadSlno"].ToString()),
                                JRChildSlno = Convert.ToString(dr["JRChildSlno"].ToString()),
                                ASJSlno = Convert.ToString(dr["ASJSlno"].ToString()),
                                DSSlno = Convert.ToString(dr["DSSlno"].ToString()),
                                ASFSlno = Convert.ToString(dr["ASFSlno"].ToString()),
                                FASlno = Convert.ToString(dr["FASlno"].ToString()),
                                NewProductType = Convert.ToString(dr["NewProductType"].ToString()),
                                PrinterName = Convert.ToString(dr["PrinterName"].ToString()),
                                PrinterEmail = Convert.ToString(dr["PrinterEmail"].ToString()),
                                PrinterMobile = Convert.ToString(dr["PrinterMobile"].ToString()),
                                PrinterContact = Convert.ToString(dr["PrinterContact"].ToString()),
                                FabricatorName = Convert.ToString(dr["FabricatorName"].ToString()),
                                FabricatorEmail = Convert.ToString(dr["FabricatorEmail"].ToString()),
                                FabricatorContact = Convert.ToString(dr["FabricatorContact"].ToString()),
                                FabricatorMobile = Convert.ToString(dr["FabricatorMobile"].ToString()),
                                sendemail = Convert.ToString(dr["sendemail"].ToString()),
                                sendemaildt = Convert.ToString(dr["sendemaildt"].ToString()),
                                isapproveparty = Convert.ToString(dr["isapproveparty"].ToString()),
                                isapprovepartydt = Convert.ToString(dr["isapprovepartydt"].ToString()),
                                ismailsend = Convert.ToString(dr["ismailsend"].ToString()),
                                uplodepartyimg = Convert.ToString(dr["uplodepartyimg"].ToString()),
                                ispayment = Convert.ToString(dr["ispayment"].ToString()),
                                managementapr = Convert.ToString(dr["managementapr"].ToString()),
                                finalapr = Convert.ToString(dr["finalapr"].ToString()),
                                Unit = Convert.ToString(dr["Unit"].ToString()),
                                GivenByName = Convert.ToString(dr["GivenByName"].ToString()),
                                BoardType = Convert.ToString(dr["BoardType"].ToString()),
                                PrintLocation = Convert.ToString(dr["PrintLocation"].ToString()),
                                FabricatorLocation = Convert.ToString(dr["FabricatorLocation"].ToString()),
                                Priority = Convert.ToString(dr["Priority"].ToString()),
                                ReopenByName = Convert.ToString(dr["ReopenByName"].ToString()),
                                Reopendt = Convert.ToString(dr["Reopendt"].ToString()),
                                JobSendTypeName = Convert.ToString(dr["JobSendTypeName"].ToString()),
                                PartyTypeName = Convert.ToString(dr["PartyTypeName"].ToString()),
                                SendToName = Convert.ToString(dr["SendToName"].ToString()),
                                LRNumber = Convert.ToString(dr["LRNumber"].ToString()),
                                LRDate = Convert.ToString(dr["LRDate"].ToString()),
                                TranspoterName = Convert.ToString(dr["TranspoterName"].ToString()),
                                JobSendToAddress = Convert.ToString(dr["JobSendToAddress"].ToString()),
                                JobSendByName = Convert.ToString(dr["JobSendByName"].ToString()),
                                JobReceiveByName = Convert.ToString(dr["JobReceiveByName"].ToString()),
                                JobReceiveDate = Convert.ToString(dr["JobReceiveDate"].ToString()),
                                JobReceiveCreateDate = Convert.ToString(dr["JobReceiveCreateDate"].ToString()),
                                ApprovalGivenByName = Convert.ToString(dr["ApprovalGivenByName"].ToString()),
                                MgmRemark = Convert.ToString(dr["MgmRemark"].ToString()),
                                MgmApprovalDate = Convert.ToString(dr["MgmApprovalDate"].ToString()),
                                PrintCost = Convert.ToString(dr["PrintCost"].ToString()),
                                FabricationCost = Convert.ToString(dr["FabricationCost"].ToString()),




                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new BrandingGetJobDetailsBranchwiseLists
                        {
                            result = true,
                            message = string.Empty,
                            servertime = DateTime.Now.ToString(),
                            data = alldcr1,
                        });
                        data1 = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(data1, Encoding.UTF8, "application/json");
                        return response;
                    }
                    else
                    {
                        g1.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "No Data Found"), Encoding.UTF8, "application/json");

                        return response;
                    }
                }
                catch (Exception ex)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!" + ex.Message), Encoding.UTF8, "application/json");

                    return response;
                }
            }
            else
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Unauthorized);
                response.Content = new StringContent(cm.StatusTime(false, "Please Log In"), Encoding.UTF8, "application/json");

                return response;
            }
        }
    }
}