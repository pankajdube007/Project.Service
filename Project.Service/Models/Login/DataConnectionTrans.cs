using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;

namespace Project.Service.Models
{
    public class DataConnectionTrans
    {
        public SqlConnection sql;
        public SqlBulkCopy sqlBulkCopy;
        public SqlDataAdapter da;
        public SqlCommand cmd;



        /// <summary>
        /// To send an email
        /// </summary>
        /// <param name="toeamilid"></param>
        /// <param name="emailbody"></param>
        /// <param name="emailsubject"></param>
        /// <param name="displayname"></param>
        /// <param name="file_name"></param>
        public void sendmail(string toeamilid, string emailbody, string emailsubject, string displayname, string file_name)
        {
            try
            {
                using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtpClient.Credentials = new System.Net.NetworkCredential("arun.g@goldmedalindia.com", "231015@itArun");
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.EnableSsl = true;
                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress("arun.g@goldmedalindia.com", displayname);
                        mail.To.Add(new MailAddress(toeamilid));
                        mail.IsBodyHtml = true;
                        mail.Priority = MailPriority.High;
                        mail.Body = emailbody;
                        mail.Subject = emailsubject;
                        smtpClient.Send(mail);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// open a sql connection
        /// </summary>
        public void open_connection()
        {
            try
            {
                //string str1 = "Data Source=.;Initial Catalog=GoldmedalErp07March;Integrated Security=True";
                string str1 = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;
                sql = new SqlConnection(str1);
                // sql.ConnectionString = str1;


                cmd = new SqlCommand();
                cmd.Connection = sql;
                cmd.CommandTimeout = 2000;
                sqlBulkCopy = new SqlBulkCopy(sql);

                sql.Open();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Close a Connection
        /// </summary>
        public void close_connection()
        {
            try
            {
                sql.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Return a data Table
        /// </summary>
        /// <param name="s1"></param>
        /// <returns></returns>
        public DataTable return_dt(string s1)
        {
            open_connection();
            try
            {
                DataTable dt = new DataTable();
                using (SqlCommand cmd1 = new SqlCommand(s1, sql))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd1))
                    {
                        da.Fill(dt);
                    }
                }
                close_connection();
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Return a datareader obj
        /// </summary>
        /// <param name="s1"></param>
        /// <returns></returns>
        public SqlDataReader return_dr(string s1)
        {
            open_connection();
            try
            {
                SqlDataReader dr;
                using (SqlCommand cmd1 = new SqlCommand(s1, sql))
                {
                    dr = cmd1.ExecuteReader();
                }

                return dr;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Execute a store procedure
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public int ExecDB(String strSql)
        {
            int retVal = 0;
            open_connection();
            try
            {
                using (SqlCommand objCmd = new SqlCommand(strSql, sql))
                {
                    objCmd.CommandType = CommandType.Text;
                    retVal = objCmd.ExecuteNonQuery();
                    close_connection();
                }

                if (retVal > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Return a Scaler value
        /// </summary>
        /// <param name="s1"></param>
        /// <returns></returns>
        public string reterive_val(string s1)
        {
            try
            {
                open_connection();
                using (SqlCommand cmd = new SqlCommand(s1, sql))
                {
                    string genValue = Convert.ToString(cmd.ExecuteScalar());
                    close_connection();
                    if (genValue == string.Empty)
                    {
                        return string.Empty;
                    }
                    else
                    {
                        //int id = Convert.ToInt32(genValue.PadRight(  genValue.Substring(4, genValue.Length - 4));
                        return genValue;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        #region Dataset Methods With Parameter
        public DataSet FillDataSet(string spName, SqlParameter[] sqlparam, String ConnectionStringCode)
        {
            DataSet DataSetToFill = new DataSet();
            try
            {
                open_connection();
                if (sqlparam != null)
                    Add_Parameter_In_Command(sqlparam);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = spName;
                da = new SqlDataAdapter(cmd);
                da.Fill(DataSetToFill);
                return DataSetToFill;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                close_connection();
                da.Dispose();
            }
        }
        #endregion

        #region Dataset Methods withOut Parameter
        public DataSet FillDataSet(string spName)
        {
            try
            {
                open_connection();
                DataSet DataSetToFill = new DataSet();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = spName;
                da = new SqlDataAdapter(cmd);
                da.Fill(DataSetToFill, spName);
                return DataSetToFill;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                close_connection();
                da.Dispose();
            }
        }
        #endregion

        private void Add_Parameter_In_Command(SqlParameter[] oParameterArray)
        {
            if (cmd != null)
            {
                cmd.Parameters.Clear();
                if (oParameterArray != null)
                {
                    foreach (SqlParameter Param in oParameterArray)
                    {
                        cmd.Parameters.Add(Param);
                    }
                }
            }
        }

        public String BulkInsert(DataTable dtBulk, String strTableName)
        {

            string retValue = "";
            try
            {
                open_connection();
                sqlBulkCopy.DestinationTableName = strTableName;
                try
                {
                    sqlBulkCopy.WriteToServer(dtBulk);
                    retValue = "True";
                }
                catch (Exception ex)
                {
                    retValue = "False";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                close_connection();
            }
            return retValue;
        }

    }
}