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
    public class DivisionWiseSubCatImgController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getsubcatimgdivwise")]
        public HttpResponseMessage GetDetails(ListDivisionWiseSubCatImg ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<DivisionWiseSubCatImgs> alldcr = new List<DivisionWiseSubCatImgs>();
                    List<DivisionWiseSubCatImg> alldcr1 = new List<DivisionWiseSubCatImg>();

                    var dr = g1.return_dr("Api_GeTechnicalSpecificationSubCatImagesList '"+ula.Search+"'");


                    if (dr.HasRows)
                    {
                        string baseurl = _goldMedia.MapPathToPublicUrl("");
                        while (dr.Read())
                        {
                            alldcr1.Add(new DivisionWiseSubCatImg
                            {
                                Division = Convert.ToString(dr["divisionnm"].ToString()),
                                SubCat = Convert.ToString(dr["rangenm"].ToString()),
                                urlimg = string.IsNullOrEmpty(dr["Images"].ToString().Trim(',')) ? "" : (baseurl + "technicalspecification/" + dr["Images"].ToString().Trim(',')),
                                urlpdf = string.IsNullOrEmpty(dr["FileNm"].ToString().Trim(',')) ? "" : (baseurl + "technicalspecification/" + dr["FileNm"].ToString().Trim(','))
                            });
                        }

                        g1.close_connection();
                        alldcr.Add(new DivisionWiseSubCatImgs
                        {
                            result = true,
                            message = "",
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