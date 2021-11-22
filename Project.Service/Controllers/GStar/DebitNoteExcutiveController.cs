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
    public class DebitNoteExcutiveController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getDebitNoteExcutive")]
        public HttpResponseMessage GetDetails(ListsofDebitNoteEx ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<DebitNoteExs> alldcr = new List<DebitNoteExs>();
                    List<DebitNoteEx> alldcr1 = new List<DebitNoteEx>();
                    var dr = g1.return_dr("AppDebitNoteExcutive " + ula.ExId + ",'" + ula.CIN + "','" + ula.FinYear + "','" + ula.searchtxt + "'," + Convert.ToBoolean(ula.Hierarchy) + "," + ula.ReportType + "," + ula.ReportValue);
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new DebitNoteEx
                            {
                                partynm = Convert.ToString(dr["displaynm"].ToString()),
                                referenceno = Convert.ToString(dr["referenceno"].ToString()),
                                date = Convert.ToString(dr["date"].ToString()),
                                amount = Convert.ToString(dr["totalamount"].ToString()),
                                ledgerdec = Convert.ToString(dr["LedgerDesc"].ToString()),
                                url = WebConfigurationManager.AppSettings["ErpUrl"].ToString() + "Debit_Note-Print1.aspx?id=" + Convert.ToString(dr["SlNo"]),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new DebitNoteExs
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
    }
}