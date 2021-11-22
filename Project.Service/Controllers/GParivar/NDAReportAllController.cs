using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
namespace Project.Service.Controllers
{
    public class NDAReportAllController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetNDAReportAll")]
        public HttpResponseMessage GetDetails(ListsofNDAReportsAll ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<NDAReportLists> alldcr = new List<NDAReportLists>();
                    List<NDAReportList> alldcr1 = new List<NDAReportList>();

                    var dr = g1.return_dr("AppGetNDAReportAll '" + ula.Month + "','" + ula.Year + "','"+ula.BranchId+ "','" + ula.CIN + "','" + ula.Category + "'");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new NDAReportList
                            {
                                Name = Convert.ToString(dr["displaynmwitharea"].ToString()),
                                cin = Convert.ToString(dr["cin"].ToString()),
                                SalesExecutive = Convert.ToString(dr["salesexname"].ToString()),
                                Amount = Convert.ToString(dr["amount"].ToString()),
                                JoinDate = Convert.ToString(dr["createdt"].ToString()),
                               
                            });
                        }
                        g1.close_connection();              
                        alldcr.Add(new NDAReportLists
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