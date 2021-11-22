using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Configuration;
using System.Web.Http;

namespace Project.Service.Controllers
{
    public class SchemeDownloadExcutiveController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getSchemeDownloadExcutive")]
        public HttpResponseMessage GetAllUserLatLong(ListofSchemeDownloadEx ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;
                    List<SchemeDownloadExs> alldcr = new List<SchemeDownloadExs>();
                    List<SchemeDownloadEx> alldcr1 = new List<SchemeDownloadEx>();
                    var dr = g1.return_dr("AppSchemeUploadEx " + ula.ExId + ",'" + ula.CIN + "'," + Convert.ToBoolean(ula.Hierarchy));
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new SchemeDownloadEx
                            {
                                partynm = Convert.ToString(dr["displaynm"].ToString()),
                                exnm = Convert.ToString(dr["salesexname"].ToString()),
                                withamturl = WebConfigurationManager.AppSettings["ErpUrl"] + "Partywiseactiveschemeprint2.aspx?cin=" + Convert.ToString(dr["cin"].ToString()),
                                withoutamturl = WebConfigurationManager.AppSettings["ErpUrl"] + "Partywiseactiveschemeprint.aspx?cin=" + Convert.ToString(dr["cin"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new SchemeDownloadExs
                        {
                            result = "True",
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
                        response.Content = new StringContent(cm.StatusTime(false, "No Items available"), Encoding.UTF8, "application/json");

                        return response;
                    }
                }
                catch (Exception ex)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!" + ex.ToString()), Encoding.UTF8, "application/json");

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