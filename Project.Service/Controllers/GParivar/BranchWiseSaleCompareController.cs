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
    public class BranchWiseSaleCompareController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetBranchwiseSalesCompare")]
        public HttpResponseMessage GetDetails(ListofBranchWiseSalesCompare ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<BranchWiseSalesCompares> alldcr = new List<BranchWiseSalesCompares>();
                    List<BranchWiseSalesCompare> alldcr1 = new List<BranchWiseSalesCompare>();

                    var dr = g1.return_dr("BranchwiseSaleCompare '"+ula.CIN+ "','" + ula.Category + "' ");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new BranchWiseSalesCompare
                            {
                                branchid = Convert.ToString(dr["branchid"].ToString()),
                                branchnm = Convert.ToString(dr["branchnm"].ToString()),
                                currentyearsale = Convert.ToString(dr["currentyearsale"].ToString()),
                                previousyearsale = Convert.ToString(dr["previousyearsale"].ToString()),
                                previoustwoyearsale = Convert.ToString(dr["previou2yearsale"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new BranchWiseSalesCompares
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
                        response.Content = new StringContent(cm.StatusTime(true, "No Data Found"), Encoding.UTF8, "application/json");

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