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
    public class AppDownloadController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/DownloadGFamily")]
        public HttpResponseMessage GetDetails(ListAppDownload ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.ClientSecret != "")
            {
                try
                {
                    string data1;

                    List<AppDownloadLists> alldcr = new List<AppDownloadLists>();
                    List<AppDownload> alldcr1 = new List<AppDownload>();

                    alldcr1.Add(new AppDownload
                    {


                        AppUrl = "https://erp.goldmedalindia.in/GoldmedalAppDownload.aspx",

                    });


                    alldcr.Add(new AppDownloadLists
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


        [HttpPost]
        [ValidateModel]
        [Route("api/DownloadCRM")]
        public HttpResponseMessage GetDetailscrm(ListAppDownload ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.ClientSecret != "")
            {
                try
                {
                    string data1;

                    List<AppDownloadLists> alldcr = new List<AppDownloadLists>();
                    List<AppDownload> alldcr1 = new List<AppDownload>();

                    alldcr1.Add(new AppDownload
                    {


                        AppUrl = "https://erp.goldmedalindia.in/GoldmedalAppCrmDownload.aspx",

                    });


                    alldcr.Add(new AppDownloadLists
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


        [HttpPost]
        [ValidateModel]
        [Route("api/UpdateGFamily")]
        public HttpResponseMessage GetDetails1(ListAppUpdate ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.Url != "")
            {
                try
                {
                    string data1;

                    List<AppUpdateLists> alldcr = new List<AppUpdateLists>();
                    List<AppUpdate> alldcr1 = new List<AppUpdate>();


                    var dr = g1.return_dr("updateGfamilyUrl '" + ula.Url + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {

                            alldcr1.Add(new AppUpdate
                            {
                                AppUrl = "https://erp.goldmedalindia.in/GoldmedalAppDownload.aspx",

                            });
                        }

                        alldcr.Add(new AppUpdateLists
                        {
                            result = true,
                            message = "Updated Sucessfully",
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
                        response.Content = new StringContent(cm.StatusTime(false, "Please Try Again after some time Its not updated"), Encoding.UTF8, "application/json");

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

        [HttpPost]
        [ValidateModel]
        [Route("api/UpdateCRM")]
        public HttpResponseMessage GetDetailscrm1(ListAppUpdate ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.Url != "")
            {
                try
                {
                    string data1;

                    List<AppUpdateLists> alldcr = new List<AppUpdateLists>();
                    List<AppUpdate> alldcr1 = new List<AppUpdate>();


                    var dr = g1.return_dr("updateCRMUrl '" + ula.Url + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {

                            alldcr1.Add(new AppUpdate
                            {
                                AppUrl = "https://erp.goldmedalindia.in/GoldmedalAppCrmDownload.aspx",

                            });
                        }

                        alldcr.Add(new AppUpdateLists
                        {
                            result = true,
                            message = "Updated Sucessfully",
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
                        response.Content = new StringContent(cm.StatusTime(false, "Please Try Again after some time Its not updated"), Encoding.UTF8, "application/json");

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