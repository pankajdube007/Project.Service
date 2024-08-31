using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers
{
    public class SetAadharDetailsController : ApiController
    {
        private object _goldMedia;

        [HttpPost]
        [ValidateModel]
        [Route("api/SetAadharDetailsForLedgerConfirmation")]
        public HttpResponseMessage GetDetails(ListSetAadharNoMapping ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            GoldMedia _goldMedia = new GoldMedia();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<AddListSetAadharNoMappingDetails> alldcr = new List<AddListSetAadharNoMappingDetails>();
                    List<AddListSetAadharNoMappingDetail> alldcr1 = new List<AddListSetAadharNoMappingDetail>();
                   
                    var dr = g2.return_dr("dbo.AadharNoMappingForLedgerConfirmationMasterAddUpd_APP '" + ula.CIN + "','" + ula.EmailID + "','" + ula.MobileNo + "','" + ula.AadharCardNo + "','" + ula.Name + "','" + ula.PancardNo + "','" + ula.NameAsPerPancard + "'");

                    if (dr.HasRows)
                    {
                        alldcr1.Add(new AddListSetAadharNoMappingDetail
                        {
                            output = "Data Sucessfully Inserted"
                        });

                        g2.close_connection();
                        alldcr.Add(new AddListSetAadharNoMappingDetails
                        {
                            result = true,
                            message = "Data Sucessfully Inserted",
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
                        alldcr.Add(new AddListSetAadharNoMappingDetails
                        {
                            result = false,
                            message = " This CIN No. '" + ula.CIN + "' Already Mapped",
                            servertime = DateTime.Now.ToString(),
                            data = alldcr1,
                        });

                        data1 = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                        response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                        return response;
                    }
                }
                catch (Exception ex)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    // response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!" + ex.Message), Encoding.UTF8, "application/json");
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

    }
}