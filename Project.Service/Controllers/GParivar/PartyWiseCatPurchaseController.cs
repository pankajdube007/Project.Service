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
    public class PartyWiseCatPurchaseController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getpartywisepurchase")]
        public HttpResponseMessage GetDetails(ListofFinYearWisePartyWisepurchase ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<FinYearWisePartyWisepurchaselists> alldcr = new List<FinYearWisePartyWisepurchaselists>();
                    List<FinYearWisePartyWisepurchaselist> alldcr1 = new List<FinYearWisePartyWisepurchaselist>();
                    string baseurl = _goldMedia.MapPathToPublicUrl(string.Empty);
                    var dr = g1.return_dr("getpartywisepurchase '" + ula.CIN + "','" + ula.Category + "','" + ula.Finyear + "','"+ula.catId+"'");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new FinYearWisePartyWisepurchaselist
                            {
                                displaynmwitharea = Convert.ToString(dr["displaynmwitharea"].ToString()),
                                PartyId = Convert.ToString(dr["partyid"].ToString()),
                                TypeCat = Convert.ToString(dr["typecat"].ToString()),
                                Amount = Convert.ToString(dr["amount"].ToString()),

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new FinYearWisePartyWisepurchaselists
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