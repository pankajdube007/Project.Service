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
    public class BrandingGetDesignersJobDetailsController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getbrandinggetdesignersjobdetails")]
        public HttpResponseMessage GetDetails(ListBrandingGetDesignersJobDetails ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<BrandingGetDesignersJobDetailsLists> alldcr = new List<BrandingGetDesignersJobDetailsLists>();
                    List<BrandingGetDesignersJobDetailsList> alldcr1 = new List<BrandingGetDesignersJobDetailsList>();

                    var dr = g1.return_dr("BrandingGetDesignersJobDetails '" + ula.CIN + "','" + ula.Cat + "','" + ula.Type + "','" + ula.DesignerID + "'");


                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new BrandingGetDesignersJobDetailsList
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
                                DesignType = Convert.ToString(dr["DesignType"].ToString()),
                                OldProductType = Convert.ToString(dr["OldProductType"].ToString()),
                                Quantity = Convert.ToString(dr["Quantity"].ToString()),
                                TotalAmount = Convert.ToString(dr["TotalAmount"].ToString()),
                                JRImageLink = Convert.ToString(dr["JRImageLink"].ToString()),
                                JRImage = Convert.ToString(dr["JRImage"].ToString()),
                                Unit = Convert.ToString(dr["Unit"].ToString()),
                                GivenByName = Convert.ToString(dr["GivenByName"].ToString()),
                                BoardType = Convert.ToString(dr["BoardType"].ToString()),
                                PrintLocation = Convert.ToString(dr["PrintLocation"].ToString()),
                                FabricatorLocation = Convert.ToString(dr["FabricatorLocation"].ToString()),
                                Priority = Convert.ToString(dr["Priority"].ToString()),
                                ReopenByName = Convert.ToString(dr["ReopenByName"].ToString()),
                                Reopendt = Convert.ToString(dr["Reopendt"].ToString()),
                                JRApproveBy = Convert.ToString(dr["JRApproveBy"].ToString()),
                                JRApprovedt = Convert.ToString(dr["JRApprovedt"].ToString()),
                                JRDisApproveBy = Convert.ToString(dr["JRDisApproveBy"].ToString()),
                                JRDisApprovedt = Convert.ToString(dr["JRDisApprovedt"].ToString()),
                                JRApprovestatus = Convert.ToString(dr["JRApprovestatus"].ToString()),
                                JobAssignRequestNo = Convert.ToString(dr["JobAssignRequestNo"].ToString()),
                                AssignTo = Convert.ToString(dr["AssignTo"].ToString()),
                                Deadline = Convert.ToString(dr["Deadline"].ToString()),
                                Assigndate = Convert.ToString(dr["Assigndate"].ToString()),
                                AssignBy = Convert.ToString(dr["AssignBy"].ToString()),
                                SizeType = Convert.ToString(dr["SizeType"].ToString()),
                                SubmitImage = Convert.ToString(dr["SubmitImage"].ToString()),
                                DesignSubmitLink = Convert.ToString(dr["DesignSubmitLink"].ToString()),
                                DesignSubmitDate = Convert.ToString(dr["DesignSubmitDate"].ToString()),
                                DesignSubmitStatus = Convert.ToString(dr["DesignSubmitStatus"].ToString()),


                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new BrandingGetDesignersJobDetailsLists
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