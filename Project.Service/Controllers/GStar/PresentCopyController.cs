﻿using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers
{
    public class PresentCopyController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/IsExecutivepresentCopy")]
        public HttpResponseMessage Ispresent(PresentActionCopy pa)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();
            if (pa.ExId != 0)
            {
                try
                {
                    DateTime presentdates = DateTime.Now;
                    int row = g2.ExecDB("exec AppExecutiveAttendancecopy1 " + pa.ExId + ",2,'" + pa.Present + "','" + pa.Remark + "','" + presentdates + "','" + pa.IP + "','" + pa.Lat + "','" + pa.Long + "','" + pa.DeviceId + "'," + pa.Type + ",'" + pa.time + "','" + Convert.ToBoolean(pa.IsTimeMismatch) + "','" + pa.Distance + "','" + pa.Address + "','" + pa.odoimg + "','" + pa.odokm + "','"+ pa.EmpType + "','"+ pa.vehicleId + "'");
                    g2.close_connection();

                    if (row > 0)
                    {
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(true, "Attendence Sucesssfully submitted"), Encoding.UTF8, "application/json");

                        return response;
                    }
                    else
                    {
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong or Attendence Already submitted or odometer issue "), Encoding.UTF8, "application/json");

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
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(cm.StatusTime(false, "Please Log In"), Encoding.UTF8, "application/json");

                return response;
            }
        }
    }
}