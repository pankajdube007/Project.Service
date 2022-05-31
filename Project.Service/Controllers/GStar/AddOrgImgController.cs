using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using Project.Service.Models.GStar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers.GStar
{
    public class AddOrgImgController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/AddOrgImg")]
        public HttpResponseMessage GetDetails(ListOrgImg ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            GoldMedia _goldMedia = new GoldMedia();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<AddOrgImgLists> alldcr = new List<AddOrgImgLists>();
                    List<AddOrgImgList> alldcr1 = new List<AddOrgImgList>();
                    string uploadImage = string.Empty;
                   
                    if (ula.img != "") uploadImage = GetImage(ula.img, 1);
                    
                    var dr = g2.return_dr("dbo.addorgimg '" + ula.orgid + "','" + ula.orgcat + "','" + ula.imgtype + "','" + uploadImage + "'");

                    if (dr.HasRows)
                    {
                        alldcr1.Add(new AddOrgImgList
                        {
                            output = "Data Sucessfully inserted"
                        });

                        g2.close_connection();
                        alldcr.Add(new AddOrgImgLists
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
                        response.Content = new StringContent(cm.StatusTime(false, "Add Org Img Not Created!!!!!!!!"), Encoding.UTF8, "application/json");

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



        protected string GetImage(string img, int folderCreation)
        {
            var _goldMedia = new GoldMedia();
            var result = "";
            string uniquefoldernm = "";
            if (folderCreation == 1)
            {
                uniquefoldernm = "orgimg";
            }


            if (!string.IsNullOrEmpty(img))
            {
                var s = img.Trim().Replace(' ', '+').Replace("-", "+").Replace("_", "/");
                if (s.Length % 4 > 0) s = s.PadRight(s.Length + 4 - s.Length % 4, '=');
                var binPdf = Convert.FromBase64String(s);
                Stream stream = new MemoryStream(binPdf);
                var FileName = Guid.NewGuid().ToString();

                var retStr = _goldMedia.GoldMediaUpload(FileName, uniquefoldernm, ".jpg", stream, "image/jpeg", false,
                    false, true);
                result = _goldMedia.MapPathToPublicUrl(uniquefoldernm + '/' + FileName + ".jpg");
            }
            return result;
        }
    }
}