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
    public class UpdateLatLonOrganationController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/LatLonOrganationUpdate")]
        public HttpResponseMessage GetDetails(ListupdatelatlonOrganisation ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<updatelatlonOrganisations> alldcr = new List<updatelatlonOrganisations>();
                    List<updatelatlonOrganisation> alldcr1 = new List<updatelatlonOrganisation>();
                    var dr = g2.return_dr("updatelatlonOrganisation '" + ula.OrgId + "','" + ula.CatId + "','" + ula.Lat + "','" + ula.Lon + "','"+ula.address+"','"+ ula.ExId + "','"+ula.EmpType + "'");

                    if (dr.HasRows)
                    {
                        alldcr1.Add(new updatelatlonOrganisation
                        {
                            output = "Data Sucessfully updated"
                        });

                        g2.close_connection();
                        alldcr.Add(new updatelatlonOrganisations
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
                        g2.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "Organation Not updated or may be not exists!!!!!!!!"), Encoding.UTF8, "application/json");

                        return response;
                    }
                }
                catch (Exception ex)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    // response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!" + ex.Message), Encoding.UTF8, "application/json");
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