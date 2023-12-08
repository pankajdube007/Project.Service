﻿using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Project.Service.Filters;
using Project.Service.Models.GParivar;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers.GParivar
{
    public class GetNewYearDhamakaOfferPointListController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetNewYearDhamakaOfferPointList")]
        public HttpResponseMessage GetDetails(GetNewYearDhamakaOfferPointList ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<GetNewYearDhamakaOfferPointListe> alldcr = new List<GetNewYearDhamakaOfferPointListe>();
                    List<GetNewYearDhamakaOfferPointLists> alldcr1 = new List<GetNewYearDhamakaOfferPointLists>();
                    var dr = g1.return_dr($"dbo.usp_saleQuantity  '{ula.CIN}' ");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new GetNewYearDhamakaOfferPointLists
                            {
                                ItemName = Convert.ToString(dr["ProductCode1"].ToString()),
                                InvoiceNo = Convert.ToString(dr["invoiceno"].ToString()),
                                Quantity = Convert.ToString(dr["salequantity"].ToString()),
                                Point = Convert.ToString(dr["SaleQuantityPoint"].ToString()),

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new GetNewYearDhamakaOfferPointListe
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