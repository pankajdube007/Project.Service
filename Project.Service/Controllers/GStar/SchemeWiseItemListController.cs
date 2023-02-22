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
    public class SchemeWiseItemListController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getSchemeWiseItemList")]
        public HttpResponseMessage GetDetails(ListSchemeWiseItemList ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<SchemeWiseItemList> alldcr = new List<SchemeWiseItemList>();
                    List<SchemeWiseItemLists> alldcr1 = new List<SchemeWiseItemLists>();
                    var dr = g1.return_dr("GCSSchemeWiseItemList '" + ula.schemeid + "'");
                    if (dr.HasRows)
                    {
                        string baseurl = _goldMedia.MapPathToPublicUrl("");
                        while (dr.Read())
                        {
                            alldcr1.Add(new SchemeWiseItemLists
                            {

                                Itemid = Convert.ToString(dr["itemid"].ToString()),
                                ItemName = Convert.ToString(dr["itemname"].ToString()),
                                Divisionid = Convert.ToString(dr["divisionid"].ToString()),
                                Divisionnm = Convert.ToString(dr["divisionnm"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new SchemeWiseItemList
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