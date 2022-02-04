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
    public class FanCatController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getfancategory")]
        public HttpResponseMessage GetDetails(FanCat ula)
        {
            DataConection g1 = new DataConection();
            GoldMedia _goldMedia = new GoldMedia();
            Common cm = new Common();

            if (ula.Cin != "")
            {
                try
                {
                    string data1;

                    List<FanCatLists> alldcr = new List<FanCatLists>();
                    List<FanCatList> alldcr1 = new List<FanCatList>();
                    List<FanCatListfinal> alldcr2 = new List<FanCatListfinal>();

                    var dr = g1.return_dt("fancatlist '" + ula.Cin + "'");
                    var dr1 = g1.return_dr("fancombocinwisedetals '"+ula.Cin+"'");

                    if (dr.Rows.Count > 0)
                    {

                    
                        for (int i = 0; i < dr.Rows.Count; i++)
                        {
                            alldcr1.Add(new FanCatList
                            {
                                CatID = Convert.ToInt32(dr.Rows[i]["SlNo"]),
                                Cat = Convert.ToString(dr.Rows[i]["categorynm"]),
                                Minqty = Convert.ToString(dr.Rows[i]["Minqty"]),
                                OrderQty = Convert.ToString(dr.Rows[i]["OrderQty"]),

                            });
                        }

                        if (dr1.HasRows)
                        {
                            string baseurl = _goldMedia.MapPathToPublicUrl("");
                            while (dr1.Read())
                            {
                                alldcr2.Add(new FanCatListfinal
                                {
                                    data = alldcr1,
                                    startdate = Convert.ToString(dr1["startdate"]),
                                    endsin = Convert.ToString(dr1["endsin"]),
                                    ComboQtyBooked = Convert.ToString(dr1["ComboQtyBooked"]),
                                    comboPrice = Convert.ToString(dr1["comboPrice"]),
                                    comboQty = Convert.ToString(dr1["comboQty"]),
                                    maxcomboQty = Convert.ToString(dr1["maxcomboQty"]),
                                    viewScheme = string.IsNullOrEmpty(dr1["scheme"].ToString().Trim(',')) ? "" : (baseurl + "fancomboscheme/" + dr1["scheme"].ToString().Trim(',')),
                                    isactive = Convert.ToString(dr1["isactive"]),
                                    isbranchactive = Convert.ToString(dr1["isbranchactive"]),
                                    isasiatourselect=Convert.ToString(dr1["isasiatourselect"]),
                                    msg = Convert.ToString(dr1["msg"]),
                                });
                            }

                        }

                            g1.close_connection();
                        alldcr.Add(new FanCatLists
                        {
                            result = true,
                            message = string.Empty,
                            servertime = DateTime.Now.ToString(),
                            data = alldcr2,
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