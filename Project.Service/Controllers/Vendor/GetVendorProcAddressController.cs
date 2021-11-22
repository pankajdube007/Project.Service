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
    public class GetVendorProcAddressController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getVendorProcAddress")]
        public HttpResponseMessage GetDetails(ListofVendorAdress ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<GetVendorAdressDetails> alldcr = new List<GetVendorAdressDetails>();
                    List<GetVendorAdressDetail> alldcr1 = new List<GetVendorAdressDetail>();
                    var dr = g1.return_dr("dbo.spVendorProcAdressapp '" + ula.vendorID + "','" + ula.Cat + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new GetVendorAdressDetail
                            {
                                contactnm = Convert.ToString(dr["contactnm"].ToString()),
                                mobile = Convert.ToString(dr["mobile"].ToString()),
                                email = Convert.ToString(dr["email"].ToString()),
                                dealnm = Convert.ToString(dr["dealnm"].ToString()),
                                designm = Convert.ToString(dr["designm"].ToString()),
                                DOB = Convert.ToString(dr["DOB"].ToString()),
                                addline1 = Convert.ToString(dr["addline1"].ToString()),
                                panno = Convert.ToString(dr["panno"].ToString()),
                                GSTNo = Convert.ToString(dr["GSTNo"].ToString()),
                                pinno = Convert.ToString(dr["pinno"].ToString()),
                                countrynm = Convert.ToString(dr["countrynm"].ToString()),
                                statenm = Convert.ToString(dr["statenm"].ToString()),
                                citynm = Convert.ToString(dr["citynm"].ToString()),
                                areanm = Convert.ToString(dr["areanm"].ToString()),
                             


                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new GetVendorAdressDetails
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