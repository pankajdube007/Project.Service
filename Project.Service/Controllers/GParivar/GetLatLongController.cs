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
    public class GetLatLongController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetLatLong")]
        public HttpResponseMessage GetDetails(ListofGetLatLongEx ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();

            try
            {
                string data1;

                List<GetLatLongExs> alldcr = new List<GetLatLongExs>();
                List<GetLatLongEx> alldcr1 = new List<GetLatLongEx>();
               // List<GetLatLongExFinal> Final = new List<GetLatLongExFinal>();
                var dr = g1.return_dt("AppGetlatlongInsert " + ula.ExId + ",'" + ula.Date + ' ' + ula.FromTime + "','" + ula.Date + ' ' + ula.ToTime + "'");

                if (dr.Rows.Count>0)
                {
                    for (int i = 0; i < dr.Rows.Count; i++)
                    {
                        alldcr1.Add(new GetLatLongEx
                        {
                            lat = dr.Rows[i]["lat"].ToString(),
                            Long = dr.Rows[i]["long"].ToString(),
                            Date = dr.Rows[i]["LDate"].ToString(),
                            Address = dr.Rows[i]["addresss"].ToString(),
                            WorkAddress = dr.Rows[i]["workaddresss"].ToString(),
                        });
                    }
                   

                    //Final.Add(new GetLatLongExFinal
                    //{
                    //    latdata = alldcr1,
                    //    Address = dr.Rows[0]["addresss"].ToString()
                    //});



                    g1.close_connection();
                    alldcr.Add(new GetLatLongExs
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
                    response.Content = new StringContent(cm.StatusTime(true, "Oops! Data is not Available"), Encoding.UTF8, "application/json");

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
    }
}