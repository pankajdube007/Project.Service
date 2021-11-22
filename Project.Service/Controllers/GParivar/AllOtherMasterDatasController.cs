using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using System.Web.Hosting;
using System.IO;
using Newtonsoft.Json.Serialization;
using System.Text;
using Project.Service.Filters;
using Project.Service.Models;

namespace Project.Service.Controllers
{
    public class AllOtherMasterDatasController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetAllOtherMasterData")]
        public HttpResponseMessage GetAllOtherMasterData(OtherAction oa)
        {
            Common cm = new Common();
            if (cm.Validate(oa.uniquekey))
            {

                try
                {
                    string data1;
                    string data11;
                    List<DumpOtherMasterData> othermast = new List<DumpOtherMasterData>();
                    List<Urls> uri = new List<Urls>();
                    othermast.Add(new DumpOtherMasterData
                    {
                        result = true,
                        message = "",
                        data = cm.GetOtherMaster(oa.lastsyncdates),
                    });

                    data1 = JsonConvert.SerializeObject(othermast);
                    if (cm.check)
                    {
                        ////code for txt generation and send the link
                        //string fileName1 = string.Format("{0}_DumpAllOtherMaster.txt", oa.userid);
                        //string fileName = string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt}_" + fileName1, DateTime.Now);
                        //string theFileName = Path.Combine(Server.MapPath("~/Files/"), fileName);
                        //if (!File.Exists(theFileName))
                        //{
                        //    // Create a file to write to.

                        //    File.WriteAllText(theFileName, data1);
                        //}
                        string dirUrl = "Files/" + oa.userid;
                        string dirPath = HostingEnvironment.MapPath("~/" + dirUrl);
                        if (!Directory.Exists(dirPath))
                        {
                            Directory.CreateDirectory(dirPath);
                        }
                        string fileName = string.Format("{0}_DumpAllOtherMaster.txt", oa.userid);
                        string filePath = dirPath + "/" + fileName;
                        if (File.Exists(filePath))
                        {
                            // Date a file if exits.                          
                            File.Delete(filePath);
                        }
                        if (!File.Exists(filePath))
                        {
                            // Create a file to write to.
                            File.WriteAllText(filePath, data1);
                        }
                        uri.Add(new Urls
                        {
                            result = true,
                            message = "Download data from this URL",
                            servertime = DateTime.Now.ToString(),
                            data = new Url
                            {

                                //urldnld = theFileName,
                                urldnld = "http://110.173.184.31:90//Files//" + oa.userid + "//" + fileName,
                            }
                        });
                        data1 = JsonConvert.SerializeObject(uri, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                        response.Content = new StringContent(data1, Encoding.Unicode);

                        return response;
                    }
                    else
                    {
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "No avilable Other Master data for you"), Encoding.Unicode);

                        return response;

                    }
                }
                catch (Exception ex)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!"), Encoding.Unicode);

                    return response;
                }
            }
            else
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(cm.StatusTime(false, "Please Log In"), Encoding.Unicode);

                return response;
            }

        }

    }
}
