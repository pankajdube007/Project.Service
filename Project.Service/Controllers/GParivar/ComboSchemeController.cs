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
    public class ComboSchemeController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getComboSchemes")]
        public HttpResponseMessage GetDetails(ListsofComboScheme ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<ComboSchemes> alldcr = new List<ComboSchemes>();
                    List<ComboSchemeFinal> ComboSchemeFinal = new List<ComboSchemeFinal>();
                    List<ComboScheme> ComboScheme = new List<ComboScheme>();

                    var dr = g1.return_dr("AppComboMaster '" + ula.CIN + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            ComboScheme.Add(new ComboScheme
                            {
                                slno = Convert.ToInt32(dr["slno"].ToString()),
                                combogrpname = Convert.ToString(dr["ComboGrpName"].ToString()),
                                comboname = Convert.ToString(dr["ComboName"].ToString()),
                                qty = Convert.ToString(dr["bookedqty"].ToString()),
                                amount = Convert.ToString(dr["amount"].ToString()),
                            });
                        }

                        ComboSchemeFinal.Add(new ComboSchemeFinal
                        {
                            ComboSchemeDetails = ComboScheme,
                            ComboSchemeUrl = "https://goldmedalblob.blob.core.windows.net/goldappdata/goldapp/base/erp/schemefiles/new/GFD_Combo.pdf",
                            ComboSchemeBooking = true,
                        });

                        g1.close_connection();
                        alldcr.Add(new ComboSchemes
                        {
                            result = true,
                            message = string.Empty,
                            servertime = DateTime.Now.ToString(),
                            data = ComboSchemeFinal,
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
                        response.Content = new StringContent(cm.StatusTime(false, "No  Data available"), Encoding.UTF8, "application/json");

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