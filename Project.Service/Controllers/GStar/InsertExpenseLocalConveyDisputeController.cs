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
using NLog;

namespace Project.Service.Controllers.GStar
{
    public class InsertExpenseLocalConveyDisputeController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/AddDispute")]
        public HttpResponseMessage GetDetails(ListofInsertExpenseLocalConveyDispute ula)
        {
            ILogger logger = LogManager.GetCurrentClassLogger();
            DataConnectionTrans g2 = new DataConnectionTrans();
            GoldMedia _goldMedia = new GoldMedia();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<AddInsertExpenseLocalConveyDisputeLists> alldcr = new List<AddInsertExpenseLocalConveyDisputeLists>();
                    List<AddInsertExpenseLocalConveyDisputeList> alldcr1 = new List<AddInsertExpenseLocalConveyDisputeList>();
                    string uploadImage = string.Empty;
                    

                    if (ula.upldfile != "") uploadImage = GetImage(ula.upldfile, 1);
                   
                    
                    var dr = g2.return_dr("dbo.AddExpenseLocalConveyDispute '" + ula.typeid + "','" + ula.refid + "','" + ula.disputetypeid + "','" + uploadImage + "','" + ula.remark + "'");

                    if (dr.HasRows)
                    {
                        alldcr1.Add(new AddInsertExpenseLocalConveyDisputeList
                        {
                            output = "Data Sucessfully inserted"
                        });

                        g2.close_connection();
                        alldcr.Add(new AddInsertExpenseLocalConveyDisputeLists
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
                        response.Content = new StringContent(cm.StatusTime(false, "Add Trip Not Created!!!!!!!!"), Encoding.UTF8, "application/json");

                        return response;
                    }
                }
                catch (Exception ex)
                {
                    g2.close_connection();
                    logger.Error($"Error occours! API : {RequestContext.Url}  Input : '{JsonConvert.SerializeObject(ula)}' Exception : {ex}");
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!! ExecutiveId:" + ula.ExId), Encoding.UTF8, "application/json");

                    return response;

                    //HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    //response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!"), Encoding.UTF8, "application/json");

                    //return response;
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
                uniquefoldernm = "disputeimg";
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