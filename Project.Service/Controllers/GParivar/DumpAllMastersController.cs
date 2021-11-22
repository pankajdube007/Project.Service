using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Hosting;
using System.Web.Http;

namespace Project.Service.Controllers
{
    public class DumpAllMastersController : ApiController
    {
        /// <summary>
        /// /
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="lastdate"></param>
        /// <param name="uniquekey"></param>
        /// <param name="lat"></param>
        /// <param name="longi"></param>
        /// <param name="latlongtmdt"></param>
        /// <param name="img1"></param>
        /// <param name="img2"></param>
        /// <param name="remarks"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        [Route("api/DumpAllMaster")]
        public HttpResponseMessage AllMasterDumping(AllMaster am)
        {
            Common cm = new Common();
            DataConection g1 = new DataConection();
            if (cm.Validate(am.uniquekey))
            {
                if (am.flag == 3)
                {
                    try
                    {
                        string data1;
                       
                        List<DumpMasterData> totalmast = new List<DumpMasterData>();
                        List<Urls> uri = new List<Urls>();
                        List<Url> uridnld = new List<Url>();
                        List<OrgName> totalmastnm = new List<OrgName>();
                        //List<OrgAddr> totaladdr = new List<OrgAddr>();
                        //List<OrgContact> totalconct = new List<OrgContact>();
                        var dt = g1.return_dt("exec orgmast_pager " + am.userid + ",'" + am.lastdate + "'");
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                totalmastnm.Add(new OrgName
                                {
                                    slno = Convert.ToInt32(dt.Rows[i]["slno"].ToString()),
                                    compname = Convert.ToString(dt.Rows[i]["compname"].ToString()),
                                    categoryid = Convert.ToInt32(dt.Rows[i]["categoryid"].ToString()),
                                    flag = Convert.ToBoolean(dt.Rows[i]["flag"].ToString()),
                                    Addr = cm.GetOrgaddr(Convert.ToInt32(dt.Rows[i]["slno"].ToString()), Convert.ToInt32(dt.Rows[i]["categoryid"].ToString())),
                                });
                            }
                            totalmast.Add(new DumpMasterData
                            {
                                result = true,
                                message = "Only Master data,and will give you shortly",
                                data = totalmastnm,
                            });
                            data1 = JsonConvert.SerializeObject(totalmast);
                            string dirUrl = "Files/" + am.userid;
                            string dirPath = HostingEnvironment.MapPath("~/" + dirUrl);
                            if (!Directory.Exists(dirPath))
                            {
                                Directory.CreateDirectory(dirPath);
                            }
                            string fileName = string.Format("{0}_DumpAllMaster.txt", am.userid);
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
                                    urldnld = "http://110.173.184.31:90//Files//" + am.userid + "//" + fileName,
                                    // urldnld = filePath,
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
                            response.Content = new StringContent(cm.StatusTime(false, "No avilable Master data for you"), Encoding.Unicode);

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
                else if (am.flag == 2)
                {
                    try
                    {
                        int row = g1.ExecDB("exec AddLatLong " + am.userid + ",'" + am.lat + "','" + am.longi + "','" + am.latlongtmdt + "'");
                        if (row > 0)
                        {
                            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                            response.Content = new StringContent(cm.StatusTime(true, "Successfully Sumbmitted Lat/Long"), Encoding.Unicode);

                            return response;
                        }
                        else
                        {
                            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                            response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!"), Encoding.Unicode);

                            return response;
                        }
                    }
                    catch (Exception ex)
                    {
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!" + ex.ToString()), Encoding.Unicode);

                        return response;
                    }
                }
                else if (am.flag == 1)
                {
                    try
                    {
                        string pic1 = string.Empty;
                        string pic2 = string.Empty;
                        if (!(string.IsNullOrEmpty(am.img1.Trim())))// && IsBase64String(img1))
                        {
                            pic1 = cm.SaveImage(am.img1, am.userid);
                        }
                        //if (!(string.IsNullOrEmpty(img2.Trim())) && IsBase64String(img2))
                        if (!(string.IsNullOrEmpty(am.img2.Trim())))// && IsBase64String(img2))
                        {
                            pic2 = cm.SaveImage(am.img2, am.userid);
                        }
                        if (!string.IsNullOrEmpty(pic1) || !string.IsNullOrEmpty(pic2))
                        {
                            int row = g1.ExecDB("exec AddClient " + am.userid + ",'" + pic1 + "','" + pic2 + "','" + am.remarks + "'");
                            if (row > 0)
                            {
                                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                                response.Content = new StringContent(cm.StatusTime(true, "Client Succesfully Submitted"), Encoding.Unicode);

                                return response;
                            }
                            else
                            {
                                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                                response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!need delete logic here"), Encoding.Unicode);

                                return response;
                            }
                        }
                        else
                        {
                            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                            response.Content = new StringContent(cm.StatusTime(false, "Image not uploaded! try again later"), Encoding.Unicode);

                            return response;
                        }
                    }
                    catch (Exception ex)
                    {
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!" + ex.ToString()), Encoding.Unicode);

                        return response;
                    }
                }
                else
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Flag should be 1,2,3"), Encoding.Unicode);

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