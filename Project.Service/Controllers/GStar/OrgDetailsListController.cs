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
    public class OrgDetailsListController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getOrgDetailsList")]
        public HttpResponseMessage GetDetails(ListOrgDetails ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<GetOrgDetailsLists> alldcr = new List<GetOrgDetailsLists>();
                    List<GetOrgDetailsList> alldcr1 = new List<GetOrgDetailsList>();
                    var dr = g1.return_dr("dbo.ExecOrgDetail '" + ula.orgid + "','" + ula.orgcat + "'");
                    if (dr.HasRows)
                    {
                        string baseurl = _goldMedia.MapPathToPublicUrl("");
                        while (dr.Read())
                        {
                            alldcr1.Add(new GetOrgDetailsList
                            {

                                orgid = Convert.ToString(dr["orgid"].ToString()),
                                compname = Convert.ToString(dr["compname"].ToString()),
                                orgcat = Convert.ToString(dr["orgcat"].ToString()),
                                partycatnm = Convert.ToString(dr["partycatnm"].ToString()),
                                contact = Convert.ToString(dr["contact"].ToString()),
                                email = Convert.ToString(dr["email"].ToString()),
                                regaddress = Convert.ToString(dr["regaddress"].ToString()),
                                img1 = string.IsNullOrEmpty(dr["img1"].ToString().TrimEnd(',')) ? string.Empty : ( Convert.ToString(dr["img1"]).ToString().TrimEnd(',')),
                                img2 = string.IsNullOrEmpty(dr["img2"].ToString().TrimEnd(',')) ? string.Empty : ( Convert.ToString(dr["img2"]).ToString().TrimEnd(',')),
                                img3 = string.IsNullOrEmpty(dr["img3"].ToString().TrimEnd(',')) ? string.Empty : ( Convert.ToString(dr["img3"]).ToString().TrimEnd(',')),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new GetOrgDetailsLists
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