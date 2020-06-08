using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Project.Service.Models
{
    internal class Common
    {
        private DataConection g1 = new DataConection();
        private DataConnectionTrans g2 = new DataConnectionTrans();
        GoldMedia _goldMedia = new GoldMedia();

        internal bool check = false;

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [Valid]
        public bool Validate(string key)
        {
            DataConection g1 = new DataConection();
            Authen auth = new Authen();
            try
            {
                byte[] uniq = Convert.FromBase64String(key);
                if (uniq.Length == 96)
                {
                    var recovered = auth.DecryptString(auth.EncryptionKey, uniq);
                    Guid guidOutput;
                    bool isValid = Guid.TryParse(recovered, out guidOutput);

                    if (isValid)
                    {
                        var dt1 = g1.return_dt("exec dcrlogindetlcheck '" + guidOutput + "'");
                        if (dt1.Rows.Count > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [Valid]
        public bool Validateo(string key)
        {
            DataConection g1 = new DataConection();
            Authen auth = new Authen();
            bool isValid = false, isValids = false;
            string guidOutputstring = string.Empty, getGuidkeystring = string.Empty;
            try
            {
                byte[] uniq = Convert.FromBase64String(key);
                if (uniq.Length == 96)
                {
                    var recovered = auth.DecryptString(auth.EncryptionKey, uniq);
                    guidOutputstring = recovered;
                }
                byte[] uniqval = Convert.FromBase64String(ConfigurationManager.AppSettings["orderkey"].ToString());
                if (uniq.Length == 96)
                {
                    var getkeys = auth.DecryptString(auth.EncryptionKey, uniqval);
                    getGuidkeystring = getkeys;
                }
                if (!string.IsNullOrEmpty(guidOutputstring) && !string.IsNullOrEmpty(getGuidkeystring))
                {
                    Guid guidOutput, getGuidkeys;
                    isValid = Guid.TryParse(guidOutputstring, out guidOutput);
                    isValids = Guid.TryParse(getGuidkeystring, out getGuidkeys);
                    if (isValid == true && isValids == true)
                    {
                        int result = getGuidkeys.CompareTo(guidOutput);
                        if (result == 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public List<UserValidation.Holiday> Getholiday()
        {
            List<UserValidation.Holiday> holi = new List<UserValidation.Holiday>();
            for (int i = 0; i < 2; i++)
            {
                if (i == 0)
                {
                    holi.Add(new UserValidation.Holiday
                    {
                        slno = 1,
                        holilist = "1/26/2016"
                    });
                }
                if (i == 1)
                {
                    holi.Add(new UserValidation.Holiday
                    {
                        slno = 2,
                        holilist = "8/15/2016"
                    });
                }
            }
            return holi;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="orgid"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public List<OrgAddr> GetOrgaddr(int orgid, int category)
        {
            List<OrgAddr> totaladdr = new List<OrgAddr>();
            try
            {
                var dt11 = g1.return_dt("exec OrganizationAddressMastselectbyorgid " + orgid + "," + category);
                if (dt11.Rows.Count > 0)
                {
                    for (int i1 = 0; i1 < dt11.Rows.Count; i1++)
                    {
                        totaladdr.Add(new OrgAddr
                        {
                            slno = Convert.ToInt32(dt11.Rows[i1]["slno"].ToString()),
                            address = Convert.ToString(dt11.Rows[i1]["address"].ToString()),
                            areaid = Convert.ToInt32(dt11.Rows[i1]["areaid"].ToString()),
                            Cont = GetOrgCont(orgid, Convert.ToString(dt11.Rows[i1]["slno"].ToString()), Convert.ToInt32(dt11.Rows[i1]["category"].ToString())),
                        });
                    }
                    return totaladdr;
                }
                else
                {
                    totaladdr.Add(new OrgAddr
                    {
                        slno = 0,
                        address = string.Empty,
                        areaid = 0,
                        Cont = GetOrgCont1(orgid, category),
                    });
                    return totaladdr;
                }
            }
            catch (Exception ex)
            {
                totaladdr.Add(new OrgAddr
                {
                    slno = 0,
                    address = string.Empty,
                    areaid = 0,
                    Cont = GetOrgCont1(orgid, category),
                });
                return totaladdr;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="orgid"></param>
        /// <param name="orgaddid"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public List<OrgContact> GetOrgCont(int orgid, string orgaddid, int category)
        {
            List<OrgContact> totalconct = new List<OrgContact>();
            try
            {
                var dt12 = g1.return_dt("exec OrganizationContactMastselectbyorgid " + orgid + ",'" + orgaddid + "'," + category);
                if (dt12.Rows.Count > 0)
                {
                    for (int i2 = 0; i2 < dt12.Rows.Count; i2++)
                    {
                        totalconct.Add(new OrgContact
                        {
                            slno = Convert.ToInt32(dt12.Rows[i2]["slno"].ToString()),
                            contactperson = Convert.ToString(dt12.Rows[i2]["contactperson"].ToString()),
                        });
                    }
                    return totalconct;
                }
                else
                {
                    totalconct.Add(new OrgContact
                    {
                        slno = 0,
                        contactperson = string.Empty,
                    });
                    return totalconct;
                }
            }
            catch (Exception ex)
            {
                totalconct.Add(new OrgContact
                {
                    slno = 0,
                    contactperson = string.Empty,
                });
                return totalconct;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="orgid"></param>
        /// <returns></returns>
        public List<OrgContact> GetOrgCont1(int orgid, int category)//, int orgaddid)
        {
            List<OrgContact> totalconct = new List<OrgContact>();
            try
            {
                var dt12 = g1.return_dt("exec OrganizationContactMastselectbyorgid1 " + orgid + "," + category);
                if (dt12.Rows.Count > 0)
                {
                    for (int i2 = 0; i2 < dt12.Rows.Count; i2++)
                    {
                        totalconct.Add(new OrgContact
                        {
                            slno = Convert.ToInt32(dt12.Rows[i2]["slno"].ToString()),
                            contactperson = Convert.ToString(dt12.Rows[i2]["contactperson"].ToString()),
                        });
                    }
                    return totalconct;
                }
                else
                {
                    totalconct.Add(new OrgContact
                    {
                        slno = 0,
                        contactperson = string.Empty,
                    });
                    return totalconct;
                }
            }
            catch (Exception ex)
            {
                totalconct.Add(new OrgContact
                {
                    slno = 0,
                    contactperson = string.Empty,
                });
                return totalconct;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        public byte[] GetImageFile(string link)
        {
            try
            {
                if (!string.IsNullOrEmpty(link))
                    return System.IO.File.ReadAllBytes(link);
                else
                    return new byte[] { 0 };
            }
            catch (Exception ex)
            {
                return new byte[] { 0 };
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="link"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public string SaveImage(string link, int userid)
        {
            try
            {
                string s = link.Trim().Replace(' ', '+');//.Replace("-","+").Replace("_", "/");
                if (s.Length % 4 > 0)
                {
                    s = s.PadRight(s.Length + 4 - s.Length % 4, '=');
                }
                byte[] binImage = Convert.FromBase64String(s);
                string dirUrl = "ClientImage/" + userid;
                // string dirPath = HostingEnvironment.MapPath("~/" + dirUrl);
                string dirPath = Path.Combine("D:/goldmedalorder/", dirUrl);// ("~/" + dirUrl);

                // Check for Directory, If not exist, then create it
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
                string fileName = string.Format("{0}_Client.png", Guid.NewGuid());
                string filePath = dirPath + "/" + fileName;
                MemoryStream ms = new MemoryStream(binImage);
                FileStream fs = new FileStream(filePath, FileMode.Create);
                ms.WriteTo(fs);
                // clean up
                ms.Close();
                fs.Close();
                fs.Dispose();
                return filePath;
            }
            catch (Exception ex)
            {
                string exp = ex.ToString();
                return string.Empty;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="issucess"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public string StatusTime(bool issucess, string text)
        {
            string data1;
            bool issucess1 = issucess;
            List<SuccessTime> logstatus = new List<SuccessTime>();
            logstatus.Add(new SuccessTime
            {
                result = issucess1,
                message = text,
                servertime = DateTime.Now.ToString(),
                data = new Succes { },
            });
            // data1 = JsonConvert.SerializeObject(logstatus);
            data1 = JsonConvert.SerializeObject(logstatus, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            return data1;
        }

        public class SuccessTime
        {
            public bool result { get; set; }
            public string message { get; set; }
            public string servertime { get; set; }
            public Succes data { get; set; }
        }

        public class Succes
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="lastsyncdate"></param>
        /// <returns></returns>
        public List<DumpOtherMasterDatas> GetOtherMaster(DateTime lastsyncdate)
        {
            List<DumpOtherMasterDatas> other = new List<DumpOtherMasterDatas>();

            other.Add(new DumpOtherMasterDatas
            {
                Category = GetAllCategory(lastsyncdate),
                Product = GetAllProduct(lastsyncdate),
                ContactMode = GetAllContact(lastsyncdate),
                Purpose = GetAllPurpose(lastsyncdate),
                Area = GetAllArea(lastsyncdate),
                City = GetAllCity(lastsyncdate),
                State = GetAllState(lastsyncdate),
                Country = GetAllCountry(lastsyncdate),
            });
            return other;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="lastsyncdate"></param>
        /// <returns></returns>
        public List<ContactModeDetail> GetAllContact(DateTime lastsyncdate)
        {
            List<ContactModeDetail> contact = new List<ContactModeDetail>();
            try
            {
                var dt11 = g1.return_dt("exec contactmodeselectfordcr '" + lastsyncdate + "'");
                if (dt11.Rows.Count > 0)
                {
                    check = true;
                    for (int i1 = 0; i1 < dt11.Rows.Count; i1++)
                    {
                        contact.Add(new ContactModeDetail
                        {
                            Val = Convert.ToInt32(dt11.Rows[i1]["slno"].ToString()),
                            Txt = Convert.ToString(dt11.Rows[i1]["contactmodename"].ToString()),
                            flag = Convert.ToBoolean(dt11.Rows[i1]["flag"].ToString()),
                        });
                    }
                    return contact;
                }
                else
                {
                    contact.Add(new ContactModeDetail
                    {
                        Val = 0,
                        Txt = string.Empty,
                        flag = false,
                    });
                    return contact;
                }
            }
            catch (Exception ex)
            {
                contact.Add(new ContactModeDetail
                {
                    Val = 0,
                    Txt = string.Empty,
                    flag = false,
                });
                return contact;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="lastsyncdate"></param>
        /// <returns></returns>
        public List<ContactModeDetail> GetAllProduct(DateTime lastsyncdate)
        {
            List<ContactModeDetail> product = new List<ContactModeDetail>();
            try
            {
                var dt11 = g1.return_dt("categoryselectfordcr '" + lastsyncdate + "'");
                if (dt11.Rows.Count > 0)
                {
                    check = true;
                    for (int i1 = 0; i1 < dt11.Rows.Count; i1++)
                    {
                        product.Add(new ContactModeDetail
                        {
                            Val = Convert.ToInt32(dt11.Rows[i1]["slno"].ToString()),
                            Txt = Convert.ToString(dt11.Rows[i1]["categorynm"].ToString()),
                            flag = Convert.ToBoolean(dt11.Rows[i1]["flag"].ToString()),
                        });
                    }
                    return product;
                }
                else
                {
                    product.Add(new ContactModeDetail
                    {
                        Val = 0,
                        Txt = string.Empty,
                        flag = false,
                    });
                    return product;
                }
            }
            catch (Exception ex)
            {
                product.Add(new ContactModeDetail
                {
                    Val = 0,
                    Txt = string.Empty,
                    flag = false,
                });
                return product;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="lastsyncdate"></param>
        /// <returns></returns>
        public List<ContactModeDetail> GetAllPurpose(DateTime lastsyncdate)
        {
            List<ContactModeDetail> purpose = new List<ContactModeDetail>();
            try
            {
                var dt11 = g1.return_dt("purposeselect1 '" + lastsyncdate + "'");
                if (dt11.Rows.Count > 0)
                {
                    check = true;
                    for (int i1 = 0; i1 < dt11.Rows.Count; i1++)
                    {
                        purpose.Add(new ContactModeDetail
                        {
                            Val = Convert.ToInt32(dt11.Rows[i1]["slno"].ToString()),
                            Txt = Convert.ToString(dt11.Rows[i1]["name"].ToString()),
                            flag = Convert.ToBoolean(dt11.Rows[i1]["flag"].ToString()),
                        });
                    }
                    return purpose;
                }
                else
                {
                    purpose.Add(new ContactModeDetail
                    {
                        Val = 0,
                        Txt = string.Empty,
                        flag = false,
                    });
                    return purpose;
                }
            }
            catch (Exception ex)
            {
                purpose.Add(new ContactModeDetail
                {
                    Val = 0,
                    Txt = string.Empty,
                    flag = false,
                });
                return purpose;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="lastsyncdate"></param>
        /// <returns></returns>
        public List<ContactModeDetail> GetAllCategory(DateTime lastsyncdate)
        {
            List<ContactModeDetail> category = new List<ContactModeDetail>();
            try
            {
                var dt11 = g1.return_dt("partycatselectfordcr'" + lastsyncdate + "'");
                if (dt11.Rows.Count > 0)
                {
                    check = true;
                    for (int i1 = 0; i1 < dt11.Rows.Count; i1++)
                    {
                        category.Add(new ContactModeDetail
                        {
                            Val = Convert.ToInt32(dt11.Rows[i1]["slno"].ToString()),
                            Txt = Convert.ToString(dt11.Rows[i1]["partycatnm"].ToString()),
                            flag = Convert.ToBoolean(dt11.Rows[i1]["flag"].ToString()),
                        });
                    }
                    return category;
                }
                else
                {
                    category.Add(new ContactModeDetail
                    {
                        Val = 0,
                        Txt = string.Empty,
                        flag = false,
                    });
                    return category;
                }
            }
            catch (Exception ex)
            {
                category.Add(new ContactModeDetail
                {
                    Val = 0,
                    Txt = string.Empty,
                    flag = false,
                });
                return category;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="lastsyncdate"></param>
        /// <returns></returns>
        public List<Areamast> GetAllArea(DateTime lastsyncdate)
        {
            List<Areamast> area = new List<Areamast>();
            try
            {
                var dt11 = g1.return_dt("areamastselectfordcr '" + lastsyncdate + "'");
                if (dt11.Rows.Count > 0)
                {
                    check = true;
                    for (int i1 = 0; i1 < dt11.Rows.Count; i1++)
                    {
                        area.Add(new Areamast
                        {
                            slno = Convert.ToInt32(dt11.Rows[i1]["slno"].ToString()),
                            areaname = Convert.ToString(dt11.Rows[i1]["areanm"].ToString()),
                            cityid = Convert.ToInt32(dt11.Rows[i1]["cityid"].ToString()),
                            flag = Convert.ToBoolean(dt11.Rows[i1]["flag"].ToString()),
                        });
                    }
                    return area;
                }
                else
                {
                    area.Add(new Areamast
                    {
                        slno = 0,
                        areaname = string.Empty,
                        cityid = 0,
                        flag = false,
                    });
                    return area;
                }
            }
            catch (Exception ex)
            {
                area.Add(new Areamast
                {
                    slno = 0,
                    areaname = string.Empty,
                    cityid = 0,
                    flag = false,
                });
                return area;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="lastsyncdate"></param>
        /// <returns></returns>
        public List<Citymast> GetAllCity(DateTime lastsyncdate)
        {
            List<Citymast> city = new List<Citymast>();
            try
            {
                var dt11 = g1.return_dt("citymastselectfordcr '" + lastsyncdate + "'");
                if (dt11.Rows.Count > 0)
                {
                    check = true;
                    for (int i1 = 0; i1 < dt11.Rows.Count; i1++)
                    {
                        city.Add(new Citymast
                        {
                            slno = Convert.ToInt32(dt11.Rows[i1]["slno"].ToString()),
                            cityname = Convert.ToString(dt11.Rows[i1]["citynm"].ToString()),

                            stateid = Convert.ToInt32(dt11.Rows[i1]["stateid"].ToString()),
                            flag = Convert.ToBoolean(dt11.Rows[i1]["flag"].ToString()),
                        });
                    }
                    return city;
                }
                else
                {
                    city.Add(new Citymast
                    {
                        slno = 0,
                        cityname = string.Empty,

                        stateid = 0,
                        flag = false,
                    });
                    return city;
                }
            }
            catch (Exception ex)
            {
                city.Add(new Citymast
                {
                    slno = 0,
                    cityname = string.Empty,

                    stateid = 0,
                    flag = false,
                });
                return city;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="lastsyncdate"></param>
        /// <returns></returns>
        public List<Statemast> GetAllState(DateTime lastsyncdate)
        {
            List<Statemast> state = new List<Statemast>();
            try
            {
                var dt11 = g1.return_dt("stateselectfordcr '" + lastsyncdate + "'");
                if (dt11.Rows.Count > 0)
                {
                    check = true;
                    for (int i1 = 0; i1 < dt11.Rows.Count; i1++)
                    {
                        state.Add(new Statemast
                        {
                            slno = Convert.ToInt32(dt11.Rows[i1]["slno"].ToString()),
                            statename = Convert.ToString(dt11.Rows[i1]["statenm"].ToString()),
                            countryid = Convert.ToInt32(dt11.Rows[i1]["countryid"].ToString()),
                            flag = Convert.ToBoolean(dt11.Rows[i1]["flag"].ToString()),
                        });
                    }
                    return state;
                }
                else
                {
                    state.Add(new Statemast
                    {
                        slno = 0,
                        statename = string.Empty,
                        countryid = 0,
                        flag = false,
                    });
                    return state;
                }
            }
            catch (Exception ex)
            {
                state.Add(new Statemast
                {
                    slno = 0,
                    statename = string.Empty,
                    countryid = 0,
                    flag = false,
                });
                return state;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="lastsyncdate"></param>
        /// <returns></returns>
        public List<Countrymast> GetAllCountry(DateTime lastsyncdate)
        {
            List<Countrymast> country = new List<Countrymast>();
            try
            {
                var dt11 = g1.return_dt("countryselectfordcr '" + lastsyncdate + "'");
                if (dt11.Rows.Count > 0)
                {
                    check = true;
                    for (int i1 = 0; i1 < dt11.Rows.Count; i1++)
                    {
                        country.Add(new Countrymast
                        {
                            slno = Convert.ToInt32(dt11.Rows[i1]["slno"].ToString()),
                            countryname = Convert.ToString(dt11.Rows[i1]["countrynm"].ToString()),
                            flag = Convert.ToBoolean(dt11.Rows[i1]["flag"].ToString()),
                        });
                    }
                    return country;
                }
                else
                {
                    country.Add(new Countrymast
                    {
                        slno = 0,
                        countryname = string.Empty,
                        flag = false,
                    });
                    return country;
                }
            }
            catch (Exception ex)
            {
                country.Add(new Countrymast
                {
                    slno = 0,
                    countryname = string.Empty,
                    flag = false,
                });
                return country;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [Valid]
        public string GetValidKey(string key)
        {
            DataConection g1 = new DataConection();
            Authen auth = new Authen();
            try
            {
                byte[] uniq = Convert.FromBase64String(key);
                if (uniq.Length == 96)
                {
                    var recovered = auth.DecryptString(auth.EncryptionKey, uniq);
                    Guid guidOutput;
                    bool isValid = Guid.TryParse(recovered, out guidOutput);

                    if (isValid)
                    {
                        return guidOutput.ToString();
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public bool ValidateEmail(string EmailId)
        {
            bool isEmail = Regex.IsMatch(EmailId.Trim(), @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);

            return isEmail;
        }

        public bool ValidateMobile(string Mobile)
        {
            bool isMobile = Regex.IsMatch(Mobile.TrimStart('0'), @"\d{10}", RegexOptions.IgnoreCase);

            return isMobile;
        }

        public string GenerateRandomString(int length)
        {
            Random random = new Random();
            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(characters[random.Next(characters.Length)]);
            }
            return result.ToString();
        }

        public string GenerateRandomNo(int length)
        {
            Random random = new Random();
            string characters = "0123456789";
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(characters[random.Next(characters.Length)]);
            }
            return result.ToString();
        }


        public string SaveBlogImage(string imagebyte, string extension, string uniquefoldernm, string FileName)
        {
            string s = imagebyte.Trim().Replace(' ', '+').Replace("-", "+").Replace("_", "/");
            if (s.Length % 4 > 0)
            {
                s = s.PadRight(s.Length + 4 - s.Length % 4, '=');
            }
            byte[] binPdf = Convert.FromBase64String(s);

            Stream stream = new MemoryStream(binPdf);
            string uploadfilename = string.Empty;


            if (extension == "pdf")
            {
                var retStr = _goldMedia.GoldMediaUpload(FileName, uniquefoldernm, ".pdf", stream, "application/pdf", false, false, false);
                uploadfilename = FileName + ".pdf";
            }
            else if (extension == "jpeg" || extension == "jpg")
            {
                var retStr = _goldMedia.GoldMediaUpload(FileName, uniquefoldernm, ".jpg", stream, "image/jpeg", false, false, false);
                uploadfilename = FileName + ".jpg";
            }
            if (extension == "png")
            {
                var retStr = _goldMedia.GoldMediaUpload(FileName, uniquefoldernm, ".png", stream, "image/png", false, false, false);
                uploadfilename = FileName + ".png";
            }

            return uploadfilename;
        }


        public string GetEwayTokanno()
        {
            var key = string.Empty;
            var dr = g2.return_dt("EwayBillKeySelect '"
                + ConfigurationManager.AppSettings["Gold.Eway.AppId"] + "','"
                + ConfigurationManager.AppSettings["Gold.Eway.AppSecret"]
                + "'");

            if (dr.Rows.Count > 0)
            {
                key = dr.Rows[0][0].ToString();
            }
            else
            {
                Dictionary<string, string> header = new Dictionary<string, string>
                    {
                        { "gspappid", ConfigurationManager.AppSettings["Gold.Eway.AppId"] },
                        { "gspappsecret",ConfigurationManager.AppSettings["Gold.Eway.AppSecret"]},
                        //{ "Accept", "application/json" },
                        //{ "Content-type", "application/json" }
                    };


                var baseurl = "https://gsp.adaequare.com/gsp/authenticate?grant_type=token";
                RemoteStatus res;
                using (var remoteClient = new RemoteClient())
                {
                    res = remoteClient.PostAsync(url: baseurl,
                       customsHeader: header).Result;
                }

                dynamic _output = JsonConvert.DeserializeObject(res.Response);




                if (res.StatusCode == 200)
                {

                    var dr2 = g2.return_dr("EwayBillKeyInsert '"
                        + ConfigurationManager.AppSettings["Gold.Eway.AppId"] + "','"
            + ConfigurationManager.AppSettings["Gold.Eway.AppSecret"] + "','"
            + _output.access_token + "','"
            + _output.token_type + "','"
            + _output.expires_in + "','"
            + _output.scope + "','"
            + _output.jti + "'"

            );
                    if (dr2.HasRows)
                    {
                        key = _output.access_token.ToString();
                    }

                }


            }
            return key;
        }


    }
}