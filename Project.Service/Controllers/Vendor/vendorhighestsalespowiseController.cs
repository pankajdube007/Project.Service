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
    public class vendorhighestsalespowiseController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getvendorhighestsalespowise")]
        public HttpResponseMessage GetDetails(Listofvendorhighestsalespowise ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<vendorhighestsalespowiseLists> alldcr = new List<vendorhighestsalespowiseLists>();
                    List<vendorhighestsalespowiseList> alldcr1 = new List<vendorhighestsalespowiseList>();
                    var dr = g1.return_dr("appHighestSalespowise  " + ula.vendorid + ",'" + ula.Category + "','" + ula.FromDate + "','" + ula.ToDate + "'," + ula.ItemnId + "");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new vendorhighestsalespowiseList
                            {

                                ItemCode = Convert.ToString(dr["itemcode"].ToString()),
                                Itemslno = Convert.ToString(dr["itemid"].ToString()),
                                ItemDescription = Convert.ToString(dr["itemnm"].ToString()),
                                Subcategory = Convert.ToString(dr["rangenm"].ToString()),
                                Quantity = Convert.ToString(dr["approvqty"].ToString()),
                                BasicAmt = Convert.ToString(dr["amount"].ToString()),
                                FinalAmt = Convert.ToString(dr["finalamt"].ToString()),
                                ponum = Convert.ToString(dr["ponum1"].ToString()),
                                date = Convert.ToString(dr["podt"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new vendorhighestsalespowiseLists
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