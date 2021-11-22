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
    public class vendorhighestpurchaseitemwiseController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getvendorhighestpurchaseitemwise")]
        public HttpResponseMessage GetDetails(Listofvendorhighestpurchaseitemwise ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<vendorhighestpurchaseitemwiseLists> alldcr = new List<vendorhighestpurchaseitemwiseLists>();
                    List<vendorhighestpurchaseitemwiseList> alldcr1 = new List<vendorhighestpurchaseitemwiseList>();
                    var dr = g1.return_dr("apphighestpurchaseitemwise " + ula.vendorid + ",'" + ula.Category + "','" + ula.FromDate + "','" + ula.ToDate + "'," + ula.cnt + "");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new vendorhighestpurchaseitemwiseList
                            {
                                Itemslno = Convert.ToString(dr["slno"].ToString()),
                                ItemCode = Convert.ToString(dr["itemcode"].ToString()),
                                ItemDescription = Convert.ToString(dr["itemnm"].ToString()),
                                TotalQty = Convert.ToString(dr["quantity"].ToString()),
                                Subcategory = Convert.ToString(dr["rangenm"].ToString()),
                                BasicAmt = Convert.ToString(dr["amount"].ToString()),
                                FinalAmount = Convert.ToString(dr["FinalAmount"].ToString()),


                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new vendorhighestpurchaseitemwiseLists
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
                        response.Content = new StringContent(cm.StatusTime(true, "No  Division available"), Encoding.UTF8, "application/json");

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