using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.IO;

namespace Project.Service.Controllers
{
    public class PresentsController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/IsExecutivepresent")]
        public HttpResponseMessage Ispresent(PresentAction pa)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();
            if (pa.ExId != 0)
            {
                try
                {
                    string uploadVehicleImage = string.Empty;
                    if (pa.img != "") uploadVehicleImage = GetImage(pa.img, 1);
                    DateTime presentdates = DateTime.Now;
                    int row = g2.ExecDB("exec AppExecutiveAttendance " + pa.ExId + ",2,'" + pa.Present + "','" + pa.Remark + "','" + presentdates + "','" + pa.IP + "','" + pa.Lat + "','" + pa.Long + "','" + pa.DeviceId + "'," + pa.Type + ",'" + pa.time + "','"+Convert.ToBoolean(pa.IsTimeMismatch)+"','"+ uploadVehicleImage+"','"+pa.odoMeter+"'");
                    g2.close_connection();

                    if (row > 0 )
                    {
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(true, "Attendence Sucesssfully submitted"), Encoding.UTF8, "application/json");
                        return response;
                    }
                  else
                    {
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong or Attendence Already submitted"), Encoding.UTF8, "application/json");
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
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
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
                uniquefoldernm = "punchinoutimage";
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
                result = _goldMedia.MapPathToPublicUrl("punchinoutimage/"+FileName + ".jpg");
            }

            return result;
        }
    }
}