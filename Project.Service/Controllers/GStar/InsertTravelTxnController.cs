using Project.Service.Filters;
using Project.Service.Models;
using Project.Service.Models.GStar;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers
{
    public class InsertTravelTxnController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/InsertTravelTxnData")]
        public HttpResponseMessage GetDetails(TravelTxn ula)
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

                    var dr = g2.return_dr("insertTravelTxnData " + ula.ExId + "," + ula.TotalTravelDays + ",'" +ula.TravelFromDate + "','" + ula.TravelToDate + "','" + ula.Source + "','" + ula.Destination + "','" + ula.Stop1 + "','" + ula.Stop2 + "','" + ula.ReturnSource + "','" + ula.ReturnDestination + "','" + ula.PersonalTravel + "'," + ula.PersonalTravelDays +",'" + ula.PersonalTFromDate + "','" + ula.PersonalTToDate + "'," + ula.ModeOfTransport +"," + ula.AccomodationDays +"," + ula.Purpose +"," + ula.ApprovedBy1 +",'" + ula.ApprovedBy1Date + "'," + ula.ApprovedBy2 + ",'" + ula.ApprovedBy2Date +"','" + ula.ApprovedStatus + "'");

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
                        response.Content = new StringContent(cm.StatusTime(true, "Something Went Wrong,Data not innserted!!!"), Encoding.UTF8, "application/json");

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