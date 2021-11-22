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
    public class AllExpenseChildAllsubchildController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getallExpenseChildAllSubChild")]
        public HttpResponseMessage GetDetails(ListAllExpenseChildAllsubchild ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<AllExpenseChildAllsubchildLists> alldcr = new List<AllExpenseChildAllsubchildLists>();
                    List<AllExpenseChildAllsubchildList> alldcr1 = new List<AllExpenseChildAllsubchildList>();
                    List<AllExpenseChildAllsubchildListFinal> Final = new List<AllExpenseChildAllsubchildListFinal>();

                    var dr = g1.return_dt("ExpenseChildAllsubchildall'" + ula.Fromdate + "','" + ula.Todate + "','" + ula.CIN + "','" + ula.Cat + "','"+ula.SupplierName+"','"+ula.LedgerName + "','" + ula.BranchName + "',"+ula.Index+","+ula.Count);
                    bool more = false;
                    if (dr.Rows.Count>0)
                    {

                        if (Convert.ToInt32(dr.Rows[0]["TotalCount"].ToString()) > (ula.Count + ula.Index))
                        {
                            more = true;
                        }
                        else
                        {
                            more = false;
                        }
                        for (int i = 0; i < dr.Rows.Count; i++)
                        {
                            alldcr1.Add(new AllExpenseChildAllsubchildList
                            {
                                slno = Convert.ToInt32(dr.Rows[i]["slno"].ToString()),
                                Branch = Convert.ToString(dr.Rows[i]["branchname"].ToString()),
                                SupplierName = Convert.ToString(dr.Rows[i]["name"].ToString()),
                                LedgerName = Convert.ToString(dr.Rows[i]["paidtoledgname"].ToString()),
                                VoucherNo = Convert.ToString(dr.Rows[i]["voucherno"].ToString()),
                                Date = Convert.ToString(dr.Rows[i]["vdate"].ToString()),
                                Amount = Convert.ToString(dr.Rows[i]["amt"].ToString()),
                                Narration = Convert.ToString(dr.Rows[i]["narration"].ToString()),
                                Paymentmode = Convert.ToString(dr.Rows[i]["instrumenttype"].ToString()),
                                Type = Convert.ToString(dr.Rows[i]["typo"].ToString()),
                                link = string.IsNullOrEmpty(dr.Rows[i]["UploadFiles"].ToString().Trim(',')) ? "" : getimageurl(dr.Rows[i]["UploadFiles"].ToString().Trim(',')),


                            });
                        }


                        Final.Add(new AllExpenseChildAllsubchildListFinal
                        {
                            ExpenseChilddata = alldcr1,
                            ismore = more
                        });

                        g1.close_connection();
                        alldcr.Add(new AllExpenseChildAllsubchildLists
                        {
                            result = true,
                            message = string.Empty,
                            servertime = DateTime.Now.ToString(),
                            data = Final,
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