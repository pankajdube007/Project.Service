using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers
{
    public class VendorUserProfileController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getVendorUserProfile")]
        public HttpResponseMessage GetDetails(ListOfVendorprofile ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<GetVendorprofileDetails> alldcr = new List<GetVendorprofileDetails>();
                    List<GetVendorprofileDetail> alldcr1 = new List<GetVendorprofileDetail>();
                    var dr = g1.return_dr("dbo.spVUserProfileapp '" + ula.vendorID + "','" + ula.email + "','" + ula.Cat + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new GetVendorprofileDetail
                            {
                                email = Convert.ToString(dr["email"].ToString()),
                                mobile = Convert.ToString(dr["mobile"].ToString()),
                                name = Convert.ToString(dr["name"].ToString()),
                                dealnm = Convert.ToString(dr["dealnm"].ToString()),
                                addline1 = Convert.ToString(dr["addline1"].ToString()),
                                panno = Convert.ToString(dr["panno"].ToString()),
                                GSTNo = Convert.ToString(dr["GSTNo"].ToString()),
                                countrynm = Convert.ToString(dr["countrynm"].ToString()),
                                statenm = Convert.ToString(dr["statenm"].ToString()),
                                citynm = Convert.ToString(dr["citynm"].ToString()),
                                areanm = Convert.ToString(dr["areanm"].ToString()),



                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new GetVendorprofileDetails
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
                        response.Content = new StringContent(cm.StatusTime(true, "No  Data available"), Encoding.UTF8, "application/json");

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