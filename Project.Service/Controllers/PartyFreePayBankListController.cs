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
    public class PartyFreePayBankListController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetPartyFreePayBank")]
        public HttpResponseMessage GetDetails(ListPartyFreePayBnak ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            GoldMedia _goldMedia = new GoldMedia();
            Common cm = new Common();
            if (ula.CIN != 0)
            {
                try
                {
                    string data1;

                    List<PartyFreePayBnaks> alldcr = new List<PartyFreePayBnaks>();
                    List<PartyFreePayBnak> alldcr1 = new List<PartyFreePayBnak>();

                    var dr = g1.return_dr("App_PartFreePayBankList '" + ula.CIN + "'");

                    if (dr.HasRows)
                    {
                        string baseurl = _goldMedia.MapPathToPublicUrl("");
                        while (dr.Read())
                        {
                            alldcr1.Add(new PartyFreePayBnak
                            {
                                PartyName = Convert.ToString(dr["displaynmwitharea"].ToString()),
                                BankCode = Convert.ToString(dr["bankcode"].ToString()),
                                BankName = Convert.ToString(dr["bankname"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new PartyFreePayBnaks
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
                        response.Content = new StringContent(cm.StatusTime(true, "No Data"), Encoding.UTF8, "application/json");

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