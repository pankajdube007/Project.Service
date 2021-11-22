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
    public class ItemPoWisePendingListController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getitempowisependingamount")]
        public HttpResponseMessage GetAllUserdetails(ListofitemPOwisepending ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;
                    List<itemPOwisependingLists> alldcr = new List<itemPOwisependingLists>();
                    List<itemPOwisependingList> alldcr1 = new List<itemPOwisependingList>();
                    var dr = g1.return_dr("managementpowisepending '" + ula.itemid + "','" + ula.BranchId + "','" + ula.Potype + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new itemPOwisependingList
                            {

                                Ponum = Convert.ToString(dr["ponum"].ToString()),
                                SubCat = Convert.ToString(dr["rangenm"].ToString()),
                                ItemName = Convert.ToString(dr["itemnm"].ToString()),
                                Color = Convert.ToString(dr["colornm"].ToString()),
                                DispatchFrom = Convert.ToString(dr["locnm"].ToString()),
                                Qty = Convert.ToString(dr["penqty"].ToString()),
                                Amount = Convert.ToString(dr["amt"].ToString()),
                                pendingsince = Convert.ToString(dr["pendingsince"].ToString()),
                               
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new itemPOwisependingLists
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