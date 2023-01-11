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
    public class updateinspectionbyuexecController : ApiController
    {
       

        [HttpPost]
        [ValidateModel]
        [Route("api/updateinspectionbyuexec")]
        public HttpResponseMessage GetDetails(updateinspectionbyuexec ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();

            if (ula.ExId != 0)
            {

                try
                {
                    string data1;

                    List<updateinspectionbyuexecs> alldcr = new List<updateinspectionbyuexecs>();
                    List<updateinspectionbyuexeces> alldcr1 = new List<updateinspectionbyuexeces>();
                    var dr = g1.return_dt("updateinspectionbyuexec '" + ula.ExId + "'," + ula.RefID + ",'" + ula.Details.ToString() + "'");

                    if (dr.Rows.Count > 0)
                    {
                        
                        if (Convert.ToInt32(dr.Rows[0]["Out"]) == 2)
                        {
                            alldcr.Add(new updateinspectionbyuexecs
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
                            alldcr.Add(new updateinspectionbyuexecs
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
                            alldcr.Add(new updateinspectionbyuexecs
                            {
                                result = false,
                                message = "Something is wrong",
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
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!" + ex.Message.ToString()), Encoding.UTF8, "application/json");

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