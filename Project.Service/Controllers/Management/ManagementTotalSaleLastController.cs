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
    public class ManagementTotalSaleLastController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetTotalSaleBranchWiseLast")]
        public HttpResponseMessage GetDetails(ListofManagementTotalSaleLast ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<ManagementTotalSaleLasts> alldcr = new List<ManagementTotalSaleLasts>();
                    List<ManagementTotalSaleLast> alldcr1 = new List<ManagementTotalSaleLast>();

                    var dr = g1.return_dr("InvoiceReportManagementBranchLast '" + ula.CurFromDate + "','" + ula.CurToDate + "','"+ula.LastFromDate+"','"+ula.LastToDate+ "','" + ula.CIN + "','" + ula.Category + "'");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new ManagementTotalSaleLast
                            {
                                branchid = Convert.ToString(dr["branchid"].ToString()),
                                branchnm = Convert.ToString(dr["branchnm"].ToString()),
                                curwiringdevices = Convert.ToString(dr["wiring"].ToString()),
                                curlights = Convert.ToString(dr["lights"].ToString()),
                                curwireandcable = Convert.ToString(dr["wire"].ToString()),
                                curpipesandfittings = Convert.ToString(dr["pipes"].ToString()),
                                curmcbanddbs = Convert.ToString(dr["mcb"].ToString()),
                                curtotalsale = Convert.ToString(dr["Curtotalsale"].ToString()),
                                curbranchcontribution = Convert.ToString(dr["Curbranchcontribution"].ToString()),
                                curbranchcontributionpercentage = Convert.ToString(dr["Curcontribypercent"].ToString()),
                                lastwiringdevices = Convert.ToString(dr["lstwiring"].ToString()),
                                lastlights = Convert.ToString(dr["lstlights"].ToString()),
                                lastwireandcable = Convert.ToString(dr["lstwire"].ToString()),
                                lastpipesandfittings = Convert.ToString(dr["lstpipes"].ToString()),
                                lastmcbanddbs = Convert.ToString(dr["lstmcb"].ToString()),
                                lasttotalsale = Convert.ToString(dr["Lasttotalsale"].ToString()),
                                lastbranchcontribution = Convert.ToString(dr["Lastbranchcontribution"].ToString()),
                                lastbranchcontributionpercentage = Convert.ToString(dr["Lastcontribypercent"].ToString()),
                                curfan = Convert.ToString(dr["fan"].ToString()),
                                lastfan = Convert.ToString(dr["lstfan"].ToString()),
                                curhealthcare = Convert.ToString(dr["HEALTHCARE"].ToString()),
                                lasthealthcare = Convert.ToString(dr["lstHEALTHCARE"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new ManagementTotalSaleLasts
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