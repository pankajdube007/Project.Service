using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers
{
    public class DCRExecutiveController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/InsertDCRExecutive")]
        public HttpResponseMessage GetDetails(ListofDCRExecutive ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    //  string data1;

                    //List<DCRExecutives> alldcr = new List<DCRExecutives>();
                    //List<DCRExecutive> alldcr1 = new List<DCRExecutive>();

                    var dr = g2.return_dr("App_dcraddforlead " + ula.ExId + "," + ula.slno + ",'" + ula.dcrdate + "','" + ula.dcrtime + "','" + ula.dcrduration + "'," + ula.modeid + "," + ula.catid + "," + ula.orgid + "," + ula.addressid + ",'" + ula.contactperson + "','" +
                        ula.purposeid + "','" + ula.productid + "','" + ula.priority + "','" + ula.remark + "'," + ula.reschduled +
                        ",'" + ula.redcrdate + "','" + ula.redcrtime + "'," + ula.remodeid + "," + ula.readdressid + ",'" + ula.recontactperson + "','"
                        + ula.repurposeid + "','" + ula.reproductid + "','" + ula.repriority + "','" + ula.reason + "',"+ula.transportid+",'"+ula.journeydistance+"','"+ula.systemdistance+"'");

                    if (dr.HasRows)
                    {
                        g2.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(true, "Data Inserted!!!"), Encoding.UTF8, "application/json");

                        return response;
                    }
                    else
                    {
                        g2.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "Something Went Wrong,Data not innserted!!!"), Encoding.UTF8, "application/json");

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