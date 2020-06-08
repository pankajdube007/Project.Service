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
    public class ExpenseChildAllSubChildController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getExpenseChildAllSubChild")]
        public HttpResponseMessage GetDetails(ListofExpenseChildAllSubChild ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<ExpenseChildAllSubChilds> alldcr = new List<ExpenseChildAllSubChilds>();
                    List<ExpenseChildAllSubChild> alldcr1 = new List<ExpenseChildAllSubChild>();
                    var dr = g1.return_dr("ExpenseChildAllsubchild '" + ula.fromdate + "','" + ula.todate + "','" + ula.CIN + "','" + ula.Category + "'," + ula.ledgerid + ',' + ula.branchid + ",'" + ula.suppliername + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new ExpenseChildAllSubChild
                            {
                                vocherno = Convert.ToString(dr["voucherno"]),
                                date = Convert.ToString(dr["vdate"]),
                                amount = Convert.ToString(dr["amt"]),
                                narration = Convert.ToString(dr["narration"]),
                                paymentmode = Convert.ToString(dr["instrumenttype"]),
                                type = Convert.ToString(dr["typo"]),
                                link = string.IsNullOrEmpty(dr["UploadFiles"].ToString().Trim(',')) ? "" : getimageurl(dr["UploadFiles"].ToString().Trim(',')),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new ExpenseChildAllSubChilds
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
                        response.Content = new StringContent(cm.StatusTime(true, "No  Data available"), Encoding.UTF8, "application/json");

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
        public string dateformat(string input)
        {
            string output = string.Empty;

            string[] words = input.Split('/');

            if (words.Length == 3)
            {
                output = words[1] + "/" + words[0] + "/" + words[2];
            }

            return output;
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


