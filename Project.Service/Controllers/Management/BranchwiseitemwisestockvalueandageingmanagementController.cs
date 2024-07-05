﻿using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Project.Service.Filters;
using Project.Service.Models.Management;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers.Management
{
    public class BranchwiseitemwisestockvalueandageingmanagementController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/Branchwiseitemwisestockvalueandageingmanagement")]
        public HttpResponseMessage GetDetails(Branchwiseitemwisestockvalueandageingmanagement ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<BranchwiseitemwisestockvalueandageingmanagementLists> alldcr = new List<BranchwiseitemwisestockvalueandageingmanagementLists>();
                    List<BranchwiseitemwisestockvalueandageingmanagementList> alldcr1 = new List<BranchwiseitemwisestockvalueandageingmanagementList>();

                    var dr = g1.return_dr("Branchwiseitemwisestockvalueandageingmanagement '" + ula.CIN + "','" + ula.Category + "'," + ula.BranchID + "," + ula.RangeId);

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new BranchwiseitemwisestockvalueandageingmanagementList
                            {

                                ItemId = Convert.ToString(dr["itemid"].ToString()),
                                ItemName = Convert.ToString(dr["itemnm"].ToString()),
                                Stock = Convert.ToString(dr["stock"].ToString()),
                                Stockvale = Convert.ToString(dr["stockvale"].ToString()),
                                to2monthstock = Convert.ToString(dr["to2monthstock"].ToString()),
                                to2monthvalue = Convert.ToString(dr["to2monthvalue"].ToString()),
                                to6monthstock = Convert.ToString(dr["to6monthstock"].ToString()),
                                to6monthvalue = Convert.ToString(dr["to6monthvalue"].ToString()),
                                to12monthstock = Convert.ToString(dr["to12monthstock"].ToString()),
                                to12monthvalue = Convert.ToString(dr["to12monthvalue"].ToString()),
                                to1to2yearstock = Convert.ToString(dr["to1to2yearstock"].ToString()),
                                to1to2yearstockvalue = Convert.ToString(dr["to1to2yearstockvalue"].ToString()),
                                morethan2yearstock = Convert.ToString(dr["morethan2yearstock"].ToString()),
                                morethan2yearstockvalue = Convert.ToString(dr["morethan2yearstockvalue"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new BranchwiseitemwisestockvalueandageingmanagementLists
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