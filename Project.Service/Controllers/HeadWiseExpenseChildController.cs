using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Configuration;
using System.Web.Http;


namespace Project.Service.Controllers
{
    public class HeadWiseExpenseChildController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getManagementHeadwiseExpenseChild")]
        public HttpResponseMessage GetDetails(ListofHeadWiseExpenseChild ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<HeadWiseExpenseChilds> alldcr = new List<HeadWiseExpenseChilds>();
                    List<HeadWiseExpenseChild> alldcr1 = new List<HeadWiseExpenseChild>();
                    var dr = g1.return_dr("APPLedgerwiseExpanseReportChild '" + ula.fromdate + "','" + ula.todate + "','" + ula.branchid + "','" + ula.headnm+"'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new HeadWiseExpenseChild
                            {
                                partynm = Convert.ToString(dr["paidto"]),
                                voucherno = Convert.ToString(dr["voucherno"]),
                                date = Convert.ToString(dr["vdate"]),
                                instrumenttype = Convert.ToString(dr["instrumenttype"]),
                                chequeno = Convert.ToString(dr["chequeno"]),
                                chequedt = Convert.ToString(dr["chequedate"]),
                                amount = Convert.ToString(dr["amount"]),
                                narration = Convert.ToString(dr["narration"]),
                                remark = Convert.ToString(dr["remarks"]),
                               links= WebConfigurationManager.AppSettings["ErpUrl"].ToString() + "/Party-Ledger-Report3.aspx?partyid=" + Convert.ToString(dr["paidtoid"])+ "&fromdate="+ dateformat(ula.fromdate)+ "&todate="+ dateformat(ula.todate),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new HeadWiseExpenseChilds
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
                        response.Content = new StringContent(cm.StatusTime(true, "No  Data available"), Encoding.UTF8, "application/json");

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


        public string dateformat(string input)
        {
            string output = string.Empty;

            string[] words = input.Split('/');

            if(words.Length==3)
            {
                output = words[1]+"/"+words[0]+"/"+words[2];
            }

            return output;
        }
    }



}