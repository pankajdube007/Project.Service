using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;

namespace Project.Service.Controllers.PayU
{
    public class PayUCheckSumHashController : ApiController
    {
        private readonly string _salt;
        public PayUCheckSumHashController()
        {
            _salt = ConfigurationManager.AppSettings["Gold.PayU.Salt"];
        }

        [HttpPost]
        [ValidateModel]
        [Route("api/GetPayUHash")]
        public HttpResponseMessage GetHash(PayUHashModel hashModel)
        {
            var cm = new Common();
            var payUHashStrings = new List<PayUHashStrings>();
            var hashStrings = new List<PayUHashString>();
            HttpResponseMessage response;
            if (hashModel.CIN != 0)
                try
                {
                    byte[] hash;
                    var datab = Encoding.UTF8.GetBytes(hashModel.HashString + _salt);
                    using (SHA512 shaM = new SHA512Managed())
                    {
                        hash = shaM.ComputeHash(datab);
                    }

                    var result = BitConverter.ToString(hash).Replace("-", "");


                    hashStrings.Add(new PayUHashString
                    {
                        Hash = result
                    });

                    payUHashStrings.Add(new PayUHashStrings
                    {
                        result = true,
                        message = string.Empty,
                        servertime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        data = hashStrings
                    });
                    var data1 = JsonConvert.SerializeObject(payUHashStrings,
                        new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

                    response = Request.CreateResponse(HttpStatusCode.OK);

                    response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                    return response;
                }
                catch (Exception ex)
                {
                    response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content =
                        new StringContent(
                            cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!" + ex.Message),
                            Encoding.UTF8, "application/json");

                    return response;
                }


            response = Request.CreateResponse(HttpStatusCode.Unauthorized);
            response.Content =
                new StringContent(cm.StatusTime(false, "Please Log In"), Encoding.UTF8, "application/json");

            return response;
        }

        [HttpPost]
        [ValidateModel]
        [Route("api/GetPayUFromHash")]
        public HttpResponseMessage GetStringFromHash(PayUHashModel hashModel)
        {
            var cm = new Common();
            var payUHashStrings = new List<PayUHashStrings>();
            var hashStrings = new List<PayUHashString>();
            HttpResponseMessage response;
            if (hashModel.CIN != 0)
                try
                {
                    var hash = Encoding.UTF8.GetBytes(hashModel.HashString);
                    var result = new StringBuilder();
                    foreach (var h in hash) result.Append(h.ToString("X2").ToLower());

                    hashStrings.Add(new PayUHashString
                    {
                        Hash = result.ToString()
                    });


                    payUHashStrings.Add(new PayUHashStrings
                    {
                        result = true,
                        message = string.Empty,
                        servertime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        data = hashStrings
                    });
                    var data1 = JsonConvert.SerializeObject(payUHashStrings,
                        new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

                    response = Request.CreateResponse(HttpStatusCode.OK);

                    response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                    return response;
                }
                catch (Exception ex)
                {
                    response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content =
                        new StringContent(
                            cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!" + ex.Message),
                            Encoding.UTF8, "application/json");

                    return response;
                }


            response = Request.CreateResponse(HttpStatusCode.Unauthorized);
            response.Content =
                new StringContent(cm.StatusTime(false, "Please Log In"), Encoding.UTF8, "application/json");

            return response;
        }
    }
}