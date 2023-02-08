﻿using Newtonsoft.Json;
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
    public class OrganizationSearchController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetOrganationSearchList")]
        public HttpResponseMessage GetDetails(ListsofOrganizationSearch ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<OrganizationSearchs> alldcr = new List<OrganizationSearchs>();
                    List<OrganizationSearch> alldcr1 = new List<OrganizationSearch>();
                    var dr = g1.return_dr("AppOrganizaionSearch '" + ula.searchtxt + "','" + ula.ExId + "','"+ ula.EmpType + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new OrganizationSearch
                            {
                                orgid = Convert.ToInt32(dr["slno"].ToString()),
                                orgname = Convert.ToString(dr["compname"].ToString()),
                                catid = Convert.ToString(dr["categoryid"].ToString()),
                                catname = Convert.ToString(dr["partycatnm"].ToString()),
                                areaid = Convert.ToString(dr["areaid"].ToString()),
                                areaname = Convert.ToString(dr["areanm"].ToString()),
                                orgaddress = Convert.ToString(dr["regaddress"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new OrganizationSearchs
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
                        response.Content = new StringContent(cm.StatusTime(false, "No Data available"), Encoding.UTF8, "application/json");

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