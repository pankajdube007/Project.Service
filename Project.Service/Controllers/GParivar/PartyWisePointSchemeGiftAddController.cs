using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using Project.Service.Models.GParivar;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Configuration;
using System.Web.Http;

namespace Project.Service.Controllers.GParivar
{
    public class PartyWisePointSchemeGiftAddController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/PartyWisePointSchemeGiftAdd")]
        public HttpResponseMessage GetDetails(PartyWisePointSchemeGiftAdd ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();

            if (ula.CIN != "")
            {

                try
                {
                    string data1;

                    List<PartyWisePointSchemeGiftAddLists> alldcr = new List<PartyWisePointSchemeGiftAddLists>();
                    List<PartyWisePointSchemeGiftAddList> alldcr1 = new List<PartyWisePointSchemeGiftAddList>();

                    var dr = g1.return_dt("partyWisepointschemeGiftAdd '" + ula.CIN + "','" + ula.Points + "','" + ula.PriceDetails.ToString() + "','"+ula.address+"',"+ula.SchemeID);

                    if (dr.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dr.Rows[0]["Out"]) == 1)
                        {
                            alldcr.Add(new PartyWisePointSchemeGiftAddLists
                            {
                                result = true,
                                message = "Data Updated Sucessfully",
                                servertime = DateTime.Now.ToString(),
                                data = alldcr1,
                            });
                            data1 = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                            response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                            return response;
                        }

                        if (Convert.ToInt32(dr.Rows[0]["Out"]) == 2)
                        {
                            alldcr.Add(new PartyWisePointSchemeGiftAddLists
                            {
                                result = true,
                                message = "Data Insterted Sucessfully ",
                                servertime = DateTime.Now.ToString(),
                                data = alldcr1,
                            });
                            data1 = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                            response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                            return response;
                        }

                        if (Convert.ToInt32(dr.Rows[0]["Out"]) == 3)
                        {
                            alldcr.Add(new PartyWisePointSchemeGiftAddLists
                            {
                                result = true,
                                message = "Something is wrong",
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
                            alldcr.Add(new PartyWisePointSchemeGiftAddLists
                            {
                                result = false,
                                message = "",
                                servertime = DateTime.Now.ToString(),
                                data = alldcr1,
                            });
                            data1 = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                            response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                            return response;
                        }
                    }
                    else
                    {
                        g1.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, Data not Inserted"), Encoding.UTF8, "application/json");

                        return response;
                    }
                }
                catch (Exception ex)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!"+ex.Message.ToString()), Encoding.UTF8, "application/json");

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