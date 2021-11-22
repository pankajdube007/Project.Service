using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers
{
    public class ItemListForProblemController : ApiController
    {


        [HttpPost]
        [ValidateModel]
        [Route("api/GetItemListForProblem")]
        public HttpResponseMessage GetItemDetails(ItemListForProblem ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();

            
                try
                {
                    string data1;

                    List<GetItemList> alldcr = new List<GetItemList>();
                    List<ItemList> alldcr1 = new List<ItemList>();

                    var dr = g2.return_dr("getItemlistforProblem ");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new ItemList
                            {

                                slno = Convert.ToInt32(dr["slno"].ToString()),
                                Item = Convert.ToString(dr["productcode"].ToString()),
                               
                            });
                        }
                        g2.close_connection();

                        alldcr.Add(new GetItemList
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
                        g2.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(true, "No data available."), Encoding.UTF8, "application/json");

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
    }
}