using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using Project.Service.Models.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers.Management
{
    public class BranchWiseSaleCOGSsubitemwiseController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/BranchWiseSaleCOGSsubitemwise")]
        public HttpResponseMessage GetDetails(BranchWiseSaleCOGSsubitemwise ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<BranchWiseSaleCOGSsubitemwiseLists> alldcr = new List<BranchWiseSaleCOGSsubitemwiseLists>();
                    List<BranchWiseSaleCOGSsubitemwiseList> alldcr1 = new List<BranchWiseSaleCOGSsubitemwiseList>();

                    var dr = g1.return_dr("BranchWiseSaleCOGSsubitemwise '" + ula.fromdate + "','" + ula.todate + "'," + ula.branchid + "," + ula.rangeid + ",'" + ula.Category + "','" + ula.CIN + "'");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new BranchWiseSaleCOGSsubitemwiseList
                            {

                                ProductCode = Convert.ToString(dr["ProductCode1"].ToString()),
                                Sale = Convert.ToString(dr["sale"].ToString()),
                                SaleMep = Convert.ToString(dr["salemep"].ToString()),
                                Rtn = Convert.ToString(dr["rtn"].ToString()),
                                cnamt = Convert.ToString(dr["cnamt"].ToString()),
                                Profit = Convert.ToString(dr["Profit"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new BranchWiseSaleCOGSsubitemwiseLists
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