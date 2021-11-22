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
    public class ComboCompareController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getComboCompare")]
        public HttpResponseMessage GetDetails(ListsofComboCompare ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;
                    string comboname = string.Empty;

                    List<ComboCompares> alldcr = new List<ComboCompares>();
                    List<ComboCompare> alldcr1 = new List<ComboCompare>();
                    List<ComboComparefinal> ComboComparefinal = new List<ComboComparefinal>();
                    var dr = g1.return_dr("AppCombocompare '" + ula.ComboIds + "','" + ula.CIN + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new ComboCompare
                            {
                                itemid = Convert.ToString(dr["slno"]),
                                combogrpname = Convert.ToString(dr["ComboGrpName"]),
                                comboname = Convert.ToString(dr["ComboName"]),
                                itemnm = Convert.ToString(dr["ItemName"]),
                                qty = Convert.ToString(dr["Qty"]),
                            });
                            comboname = Convert.ToString(dr["allcombonm"].ToString());
                        }

                        ComboComparefinal.Add(new ComboComparefinal
                        {
                            combocomparedetails = alldcr1,
                            allcombonm = comboname
                        });

                        g1.close_connection();
                        alldcr.Add(new ComboCompares
                        {
                            result = true,
                            message = string.Empty,
                            servertime = DateTime.Now.ToString(),
                            data = ComboComparefinal,
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
                        response.Content = new StringContent(cm.StatusTime(false, "No  Data available"), Encoding.UTF8, "application/json");

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