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
    public class BrandingGetBranchwiseJobCountController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/brandinggetbranchwisejobcount")]
        public HttpResponseMessage GetDetails(ListBrandingGetBranchwiseJobCount ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<BrandingGetBranchwiseJobCountLists> alldcr = new List<BrandingGetBranchwiseJobCountLists>();
                    List<BrandingGetBranchwiseJobCountList> alldcr1 = new List<BrandingGetBranchwiseJobCountList>();

                    var dr = g1.return_dr("BrandingGetBranchwiseJobCount '" + ula.CIN + "','" + ula.Cat + "','" + ula.FromDate + "','" + ula.ToDate + "'");


                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new BrandingGetBranchwiseJobCountList
                            {

                                BranchId = Convert.ToString(dr["BranchId"].ToString()),
                                BranchName = Convert.ToString(dr["BranchName"].ToString()),
                                Total = Convert.ToString(dr["Total"].ToString()),
                                Pending = Convert.ToString(dr["Pending"].ToString()),
                                Approved = Convert.ToString(dr["Approved"].ToString()),
                                DisApproved = Convert.ToString(dr["DisApproved"].ToString()),
                                Cancel = Convert.ToString(dr["Cancel"].ToString()),
                                Assigned = Convert.ToString(dr["Assigned"].ToString()),
                                DesignComplete = Convert.ToString(dr["DesignComplete"].ToString()),
                                DesignApproved = Convert.ToString(dr["DesignApproved"].ToString()),
                                SentToPrinter = Convert.ToString(dr["SentToPrinter"].ToString()),
                                ClosedJobs = Convert.ToString(dr["ClosedJobs"].ToString()),




                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new BrandingGetBranchwiseJobCountLists
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