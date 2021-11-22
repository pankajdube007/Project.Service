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
    public class GetEnquiryListController : ApiController
    {

        [HttpPost]
        [ValidateModel]
        [Route("api/getlistofvisitorenquiry")]
        public HttpResponseMessage GetDetails(ListofEnquiryDetails ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<GetListofEnquiryDetails> alldcr = new List<GetListofEnquiryDetails>();
                    List<GetListofEnquiryDetail> alldcr1 = new List<GetListofEnquiryDetail>();
                    var dr = g1.return_dr("dbo.GetEnquiryDetails '" + ula.ExId + "','" + ula.SearchBy + "','" + ula.Visitorid + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new GetListofEnquiryDetail
                            {

                                ExId = Convert.ToInt32(dr["ExId"].ToString()),
                                enquiryCode = Convert.ToString(dr["eqcode"].ToString()),
                                enquiryStatus = Convert.ToString(dr["enqstatus"].ToString()),
                                salesexnm = Convert.ToString(dr["salesexnm"].ToString()),
                                enquirytypeID = Convert.ToInt32(dr["enquirytypeID"].ToString()),
                                enquirytypename = Convert.ToString(dr["enquirytypename"].ToString()),
                                cityid = Convert.ToInt32(dr["cityid"].ToString()),
                                citynm = Convert.ToString(dr["citynm"].ToString()),
                                onsitepersonname = Convert.ToString(dr["onsitepersonname"].ToString()),
                                contactno = Convert.ToString(dr["contactno"].ToString()),
                                pincoide = Convert.ToString(dr["pincoide"].ToString()),
                                Addressline1 = Convert.ToString(dr["Addressline1"].ToString()),
                                Addressline2 = Convert.ToString(dr["Addressline2"].ToString()),
                                comment = Convert.ToString(dr["comment"].ToString()),


                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new GetListofEnquiryDetails
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