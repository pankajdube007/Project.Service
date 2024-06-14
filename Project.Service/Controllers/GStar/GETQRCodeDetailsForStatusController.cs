using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using Project.Service.Models.GStar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers.GStar
{
    public class GETQRCodeDetailsForStatusController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GETQRCodeDetailsForStatus")]
        public HttpResponseMessage GetDetails(GETQRCodeDetailsForStatus ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;
                    List<GETQRCodeDetailsForStatusLists> alldcr = new List<GETQRCodeDetailsForStatusLists>();
                    List<GETQRCodeDetailsForStatusList> alldcr1 = new List<GETQRCodeDetailsForStatusList>();
                    var dr = g1.return_dr("GETQRCodeDetailsForStatus '" + ula.QRCODE+"'");
                    if (dr.HasRows)
                    {

                        while (dr.Read())
                        {
                            alldcr1.Add(new GETQRCodeDetailsForStatusList
                            {
                                QRType = Convert.ToString(dr["QRType"].ToString()),
                                QRCode = Convert.ToString(dr["QRCode"].ToString()),
                                IBranch = Convert.ToString(dr["IBranch"].ToString()),
                                CBranch = Convert.ToString(dr["CBranch"].ToString()),
                                ProductName = Convert.ToString(dr["ProductName"].ToString()),
                                DCID = Convert.ToString(dr["DCID"].ToString()),
                                POMappedDate = Convert.ToString(dr["POMappedDate"].ToString()),
                                DCNo = Convert.ToString(dr["DCNo"].ToString()),
                                DCDate = Convert.ToString(dr["DCDate"].ToString()),
                                DCPartyName = Convert.ToString(dr["DCPartyName"].ToString()),

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new GETQRCodeDetailsForStatusLists
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
                        response.Content = new StringContent(cm.StatusTime(true, "No  Data available !!!"), Encoding.UTF8, "application/json");

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
