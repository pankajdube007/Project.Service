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
    public class BrandingGetLastJobOfDesignerController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getbrandinglastjobofdesigner")]
        public HttpResponseMessage GetDetails(ListBrandingGetLastJobOfDesigner ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<BrandingGetLastJobOfDesignerLists> alldcr = new List<BrandingGetLastJobOfDesignerLists>();
                    List<BrandingGetLastJobOfDesignerList> alldcr1 = new List<BrandingGetLastJobOfDesignerList>();

                    var dr = g1.return_dr("BrandingGetLastJobOfDesigner '" + ula.AssignTo + "'");


                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new BrandingGetLastJobOfDesignerList
                            {

                                DesignerID = Convert.ToString(dr["DesignerID"].ToString()),
                                DesignerName = Convert.ToString(dr["DesignerName"].ToString()),
                                DaysFromToday = Convert.ToString(dr["DaysFromToday"].ToString()),
                       
                              




                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new BrandingGetLastJobOfDesignerLists
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
                        response.Content = new StringContent(cm.StatusTime(false, "No Data Found"), Encoding.UTF8, "application/json");

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