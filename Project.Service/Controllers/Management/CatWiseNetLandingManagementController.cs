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
    public class CatWiseNetLandingManagementController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getcatwisenetlandingmanagement")]
        public HttpResponseMessage GetDetails(Listsofcatwisenetlandingmanagement ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.Cin != "")
            {
                try
                {
                    string data1;

                    List<catwisenetlandingmanagementLists> alldcr = new List<catwisenetlandingmanagementLists>();
                    List<catwisenetlandingmanagementList> alldcr1 = new List<catwisenetlandingmanagementList>();

                    var dr = g1.return_dr("catwisenetlandingmanagement '"+ula.Cin + "','"+ula.Category+"','"+ula.CatId+"'");


                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new catwisenetlandingmanagementList
                            {

                                ItemId = Convert.ToInt32(dr["slno"].ToString()),
                                ItemName = Convert.ToString(dr["ProductCode"].ToString()),
                                SubCat = Convert.ToString(dr["rangenm"].ToString()),
                                Netvalue = Convert.ToString(dr["netland"].ToString()),



                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new catwisenetlandingmanagementLists
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