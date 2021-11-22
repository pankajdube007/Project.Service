using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using Project.Service.Models.hrm;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers.hrm
{
    public class AttendenceSyncController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/SyncAttendence")]
        public HttpResponseMessage GetDetails(ListofAttendenceSync ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            GoldMedia _goldMedia = new GoldMedia();
            Common cm = new Common();
            if (!string.IsNullOrEmpty(ula.jsondata))
            {
                try
                {
                  
                    var dr = g1.return_dr("synattendencebydevice '" + "[" + ula.jsondata.TrimStart(',') + "]" + "'");
                   
                    if (dr.HasRows)
                    {
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(true, "Success!!!!!!!!"), Encoding.UTF8, "application/json");

                        return response;
                    }

                    else
                    {
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!"), Encoding.UTF8, "application/json");

                        return response;
                    }
                  
                }
                catch (Exception ex)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false,  ex.Message), Encoding.UTF8, "application/json");

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
        [Route("api/SyncDevices")]
        public HttpResponseMessage GetDetails1(ListofAttendenceSync ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            GoldMedia _goldMedia = new GoldMedia();
            Common cm = new Common();
            if (!string.IsNullOrEmpty(ula.jsondata))
            {
                try
                {

                    var dr = g1.return_dt("syndevices '" + "["+ula.jsondata.TrimStart(',')+"]" + "'");

                    if (dr.Rows.Count>0)
                    {
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(true, "Success!!!!!!!!"), Encoding.UTF8, "application/json");

                        return response;
                    }

                    else
                    {
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!"), Encoding.UTF8, "application/json");

                        return response;
                    }

                }
                catch (Exception ex)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, ex.Message), Encoding.UTF8, "application/json");

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