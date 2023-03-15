using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using Project.Service.Models.GStar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers.GStar
{
    public class DcrAddLocalConveyancenewController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/addDcrLocalConveyancenew")]
        public HttpResponseMessage GetDetails(ListofAddDcrLocalConveyancenew ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            GoldMedia _goldMedia = new GoldMedia();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<AddDcrLocalConveyanceListsnew> alldcr = new List<AddDcrLocalConveyanceListsnew>();
                    List<AddDcrLocalConveyanceListnew> alldcr1 = new List<AddDcrLocalConveyanceListnew>();

                    var dr = g2.return_dr("dbo.AddDcrLocalConveyancenew '" + ula.ExId + "','" + ula.trvldt + "','" + ula.grossdistance + "','" + ula.claimabledistance + "','" + ula.personaltravl + "','" + ula.odomtrkm + "','" + ula.trvlmodeq + "','" + ula.claimableamt + "','" + ula.self + "','" + ula.train + "','" + ula.metro + "','" + ula.rentalcar + "','" + ula.bus + "','" + ula.auto + "','" + ula.tollparking + "','" + ula.totalpayble + "','" + ula.IsClaimable + "', '" + ula.AppRemark + "','" + ula.slno + "','" + ula.food + "','" + ula.outstation + "' ,'" + ula.fixamt + "','" + ula.samedayamt + "','" + ula.isfix + "','"+ ula.trainimg + "','"+ ula.metroimg + "','"+ ula.rentalcarimg + "','"+ ula.tollparkingimg + "','" + ula.autoimg + "','" + ula.busimg + "','" + ula.outstationimg + "'");

                    if (dr.HasRows)
                    {
                        alldcr1.Add(new AddDcrLocalConveyanceListnew
                        {
                            output = "Data Sucessfully inserted"
                        });

                        g2.close_connection();
                        alldcr.Add(new AddDcrLocalConveyanceListsnew
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
                        response.Content = new StringContent(cm.StatusTime(false, "Add Trip Not Created!!!!!!!!"), Encoding.UTF8, "application/json");

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