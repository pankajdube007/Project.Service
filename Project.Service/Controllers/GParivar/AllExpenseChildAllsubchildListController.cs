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
using System.Linq;

namespace Project.Service.Controllers
{
    public class AllExpenseChildAllsubchildListController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getallExpenseChildAllSubChildList")]
        public HttpResponseMessage GetDetails(ListAllExpenseChildAllsubchildAll ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<AllExpenseChildAllsubchildHeadLists> alldcr = new List<AllExpenseChildAllsubchildHeadLists>();
                    List<AllExpenseChildAllsubchildHead> alldcr1 = new List<AllExpenseChildAllsubchildHead>();
                    List<AllExpenseChildAllsubchildHeadSupplier> alldcr2 = new List<AllExpenseChildAllsubchildHeadSupplier>();
                    List<AllExpenseChildAllsubchildHeadLedger> alldcr3 = new List<AllExpenseChildAllsubchildHeadLedger>();

                    var dr = g1.return_dt("ExpenseChildAllsubchildList'" + ula.Fromdate + "','" + ula.Todate + "','" + ula.CIN + "','" + ula.Cat + "'");

                    if (dr.Rows.Count>0)
                    {
                        var dr1 = (from Rows in dr.AsEnumerable()
                                  select Rows["name"]).Distinct().ToList();

                        var dr2 = (from Rows in dr.AsEnumerable()
                                   select Rows["paidtoledgname"]).Distinct().ToList();
                        dr2.Sort();
                      

                        if (dr1.Count > 0)
                        {
                            foreach (var item in dr1)
                            {
                                alldcr2.Add(new AllExpenseChildAllsubchildHeadSupplier
                                {
                                    name = item.ToString()
                                });

                            }

                        }


                        if (dr2.Count > 0)
                        {
                            foreach (var item in dr2)
                            {
                                alldcr3.Add(new AllExpenseChildAllsubchildHeadLedger
                                {
                                    name = item.ToString()
                                });

                            }

                        }



                        alldcr1.Add(new AllExpenseChildAllsubchildHead
                            {
                                Supplier=alldcr2,
                                ledger=alldcr3
                            });
                    
                        g1.close_connection();
                        alldcr.Add(new AllExpenseChildAllsubchildHeadLists
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
                        response.Content = new StringContent(cm.StatusTime(false, "No Data Found"), Encoding.UTF8, "application/json");

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
        public string getimageurl(string url)
        {
            string imgurl = string.Empty;
            GoldMedia _goldMedia = new GoldMedia();
            string baseurl = _goldMedia.MapPathToPublicUrl(string.Empty);
            if (!string.IsNullOrEmpty(url))
            {
                string[] split = url.Split(',');

                foreach (var item in split)
                {
                    if (item != string.Empty)
                    {
                        imgurl = imgurl + baseurl + "billbook/" + item.ToString() + ",";
                    }


                }
            }


            return imgurl.TrimEnd(',');
        }
    }


}