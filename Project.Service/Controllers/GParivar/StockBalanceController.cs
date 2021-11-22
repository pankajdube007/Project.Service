using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers
{
    public class StockBalanceController : ApiController
    {
        [HttpPost]
        [ValidateModel]
     //  [Route("api/StockBalance")]
        [Route("api/StockBalanceStop")]
        public HttpResponseMessage GetAllUserLatLong(StockBalanceAction ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (cm.Validate(ula.uniquekey))
            {
                try
                {
                    string data1;

                    List<StockBalances> alldcr = new List<StockBalances>();
                    List<StockBalance> alldcr1 = new List<StockBalance>();
                    DataTable dtTable = g1.return_dt("App_stockbalance " + ula.BranchId + "," + ula.WareHouseId + "," + ula.ItemId
                       + "," + ula.CatId + ",'" + ula.Searchtext + "'," + ula.index + "," + ula.Count);

                    bool more = false;
                    if (dtTable.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dtTable.Rows[0]["TotalCount"]) > ula.Count)
                        {
                            more = true;
                        }
                    }

                    DataTable dr = new DataTable();
                    dr = dtTable;

                    if (dr.Rows.Count > 0)
                    {
                        for (int i = 0; i < dr.Rows.Count; i++)
                        {
                            alldcr1.Add(new StockBalance
                            {
                                slno = Convert.ToInt32(dr.Rows[i]["SlNo"].ToString()),
                                itemcode = Convert.ToString(dr.Rows[i]["itemcode"].ToString()),
                                ItemName = Convert.ToString(dr.Rows[i]["ItemName"].ToString()),
                                colornm = Convert.ToString(dr.Rows[i]["colornm"].ToString()),
                                balanceqty = Convert.ToString(dr.Rows[i]["balanceqty"].ToString()),
                            });
                        }
                        g1.close_connection();

                        alldcr.Add(new StockBalances
                        {
                            result = true,
                            message = string.Empty,
                            Ismore = more,
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
                        response.Content = new StringContent(cm.StatusTime(false, "No  Stock available"), Encoding.UTF8, "application/json");

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