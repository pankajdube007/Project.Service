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
    public class NewArrivalDetailsController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getNewArrivalDetailsData")]
        public HttpResponseMessage GetDetails(NewArrivalDetails ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();

            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<NewArrivalAppDetails> alldcr = new List<NewArrivalAppDetails>();
                    List<NewArrivalAppDetail> alldcr1 = new List<NewArrivalAppDetail>();
                    var dr = g1.return_dr("NewArrivaldetailsapp '" + ula.Slno + "'");

                    if (dr.HasRows)
                    {
                        string baseurl = _goldMedia.MapPathToPublicUrl("");
                        while (dr.Read())
                        {
                            alldcr1.Add(new NewArrivalAppDetail
                            {

                                Slno = Convert.ToInt32(dr["slno"].ToString()),
                                //Image = string.IsNullOrEmpty(dr["Images"].ToString().Trim(',')) ? "" : (baseurl + "showroom/" + dr["Images"].ToString().Trim(',')),
                                ProductCode = dr["ProductCode"].ToString(),
                                StartDate = dr["StartDate"].ToString(),
                                EndDate = dr["EndDate"].ToString(),
                                Images = getimageurl(dr["Images"].ToString()),


                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new NewArrivalAppDetails
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
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!"), Encoding.UTF8, "application/json");

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
                        imgurl = imgurl + baseurl + "showroom/" + item.ToString() + ",";
                    }


                }
            }


            return imgurl.TrimEnd(',');
        }
    }
}