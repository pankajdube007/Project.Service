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
    public class vendorsalependingorderpowiseController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getvendorsalependingorderpowise")]
        public HttpResponseMessage GetDetails(Listofvendorsalependingorderpowise ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<vendorsalependingorderpowiseLists> alldcr = new List<vendorsalependingorderpowiseLists>();
                    List<vendorsalependingorderpowiseList> alldcr1 = new List<vendorsalependingorderpowiseList>();
                    var dr = g1.return_dr("Appvendorsalependingorderpowise " + ula.vendorid + ",'" + ula.Category + "','" + ula.Date + "'," + ula.ItemId + "");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new vendorsalependingorderpowiseList
                            {


                                Itemslno = Convert.ToString(dr["itemid"].ToString()),
                                ItemName = Convert.ToString(dr["itemnm"].ToString()),
                                ColorName = Convert.ToString(dr["colornm"].ToString()),
                                Subcategory = Convert.ToString(dr["rangenm"].ToString()),
                                PendingQty = Convert.ToString(dr["pendingQty"].ToString()),
                                PendingDays = Convert.ToString(dr["datesince"].ToString()),
                                Ponum = Convert.ToString(dr["ponum"].ToString()),
                                Podate = Convert.ToString(dr["podttime"].ToString()),
                                OrderQty = Convert.ToString(dr["orderqty"].ToString()),
                                AprQty = Convert.ToString(dr["approvqty"].ToString()),
                                //download = WebConfigurationManager.AppSettings["ErpUrl"].ToString() + Convert.ToString(dr["download"]),

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new vendorsalependingorderpowiseLists
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