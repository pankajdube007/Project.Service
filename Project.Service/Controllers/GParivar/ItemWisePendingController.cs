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
    public class ItemWisePendingController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getitemwisependingamount")]
        public HttpResponseMessage GetAllUserdetails(Listofitemwisepending ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;
                    List<itemwisependingLists> alldcr = new List<itemwisependingLists>();
                    List<itemwisependingList> alldcr1 = new List<itemwisependingList>();
                    var dr = g1.return_dr("managementitemwisepending '" + ula.Catid + "','" + ula.BranchId + "','" + ula.Potype + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new itemwisependingList
                            {
                                SubCatId = Convert.ToString(dr["rangeid"].ToString()),
                                SubCat = Convert.ToString(dr["rangenm"].ToString()),
                                ItemId = Convert.ToString(dr["itemid"].ToString()),
                                ItemName = Convert.ToString(dr["itemnm"].ToString()),
                                Color = Convert.ToString(dr["colornm"].ToString()),
                                //DispatchFrom = Convert.ToString(dr["locnm"].ToString()),
                                Qty = Convert.ToString(dr["penqty"].ToString()),
                                Amount = Convert.ToString(dr["amt"].ToString()),
                                pendingsince = Convert.ToString(dr["pendingsince"].ToString()),
                                Branch = Convert.ToString(dr["Branch"].ToString()),
                                //materialissuefrom = Convert.ToString(dr["materialissuefrom"].ToString()),
                                Potype = Convert.ToString(dr["Potype"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new itemwisependingLists
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
                        response.Content = new StringContent(cm.StatusTime(false, "No Data available"), Encoding.UTF8, "application/json");

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
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(cm.StatusTime(false, "Please Log In"), Encoding.UTF8, "application/json");

                return response;
            }
        }
    }
}