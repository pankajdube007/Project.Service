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
    public class SchemeUploadDetailsExController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getSchemeUploaddetails")]
        public HttpResponseMessage GetDetails(ListsofSchemeUploadDetails ula)
        {
            DataConection g1 = new DataConection();
            GoldMedia _goldMedia = new GoldMedia();
            Common cm = new Common();
            if (ula.ClientSecret != "")
            {
                try
                {
                    string data1;

                    List<SchemeUploadDetailss> alldcr = new List<SchemeUploadDetailss>();
                    List<SchemeUploadDetailFinal> DetailFinal = new List<SchemeUploadDetailFinal>();
                    List<SchemeUploadDetailCompany> DetailCompany = new List<SchemeUploadDetailCompany>();
                    List<SchemeUploadDetailRange> DetailRange = new List<SchemeUploadDetailRange>();
                    List<SchemeUploadDetailType> DetailType = new List<SchemeUploadDetailType>();

                    var dr = g1.return_dr("AppOtherCompanyName");
                    var dr1 = g1.return_dr("AppOtherCompanyRangeName");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            DetailCompany.Add(new SchemeUploadDetailCompany
                            {
                                companyname = Convert.ToString(dr["CompanyName"].ToString()),
                            });
                        }

                        if (dr1.HasRows)
                        {
                            while (dr1.Read())
                            {
                                DetailRange.Add(new SchemeUploadDetailRange
                                {
                                    rangename = Convert.ToString(dr1["Range"].ToString()),
                                });
                            }
                        }

                        DetailType.Add(new SchemeUploadDetailType { type = "Scheme Discount", });
                        DetailType.Add(new SchemeUploadDetailType { type = "Discount", });
                        DetailType.Add(new SchemeUploadDetailType { type = "Price List", });
                        DetailType.Add(new SchemeUploadDetailType { type = "Catalogue", });
                        DetailType.Add(new SchemeUploadDetailType { type = "Free Scheme", });

                        DetailFinal.Add(new SchemeUploadDetailFinal
                        {
                            allcompanyname = DetailCompany,
                            allrangename = DetailRange,
                            alltype = DetailType,
                        }

                            );

                        g1.close_connection();
                        alldcr.Add(new SchemeUploadDetailss
                        {
                            result = true,
                            message = string.Empty,
                            servertime = DateTime.Now.ToString(),
                            data = DetailFinal,
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