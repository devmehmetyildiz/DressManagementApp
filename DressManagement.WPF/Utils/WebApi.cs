using DressManagement.WPF.DataAccess;
using DressManagement.WPF.Models;
using DressManagement.WPF.Models.Auth;
using DressManagement.WPF.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DressManagement.WPF.Utils
{
    public class WebApi
    {
        HttpAttributes dataAccess;
        private const string Controller = "";
        private const string Methodname = "";
        public WebApi()
        {
            dataAccess = new HttpAttributes();
        }
        public static string access_token { get; set; }

        public static string token_type { get; set; }

        public static string expires_in { get; set; }

        public async Task<TokenModel> GetToken()
        {
            TokenModel obj = new TokenModel();
            LoginModel objuser = new LoginModel
            {
                Username = ActiveUser.Username,
                Password = ActiveUser.Password
            };
            try
            {
                objuser = dataAccess.DoPost(objuser, Controller, Methodname);
            }
            catch (Exception ex)
            {
                ActiveUser.Logs.Add(new LogModel { Id = ActiveUser.Logs.Count + 1, Jobname = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + System.Reflection.MethodBase.GetCurrentMethod().Name, Message = ex.Message, Jobvalue = "Error" });
            }
            return obj;
        }

        public static TokenModel GetTokensync()
        {
            ServicePointManager
     .ServerCertificateValidationCallback +=
     (sender, cert, chain, sslPolicyErrors) => true;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            TokenModel obj = new TokenModel();
            UserCredential objuser = new UserCredential
            {
                UserName = UserUtils.ActiveUser,
                Password = UserUtils.Password
            };
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["TokenURL"].ToString() + "Login");

                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(objuser);

                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    obj.access_token = result.ToString();
                }
            }
            catch (Exception ex)
            {

                LogVM.Addlog("WebapiUtils", System.Reflection.MethodBase.GetCurrentMethod().Name, "ERROR", "Token Çekme Hatası", ex.Message);
            }
            return obj;
        }

        public static bool apitest()
        {
            try
            {
                ServicePointManager
                .ServerCertificateValidationCallback +=
                (sender, cert, chain, sslPolicyErrors) => true;
                HttpClient client;
                string controller = "Home/";
                client = new HttpClient();
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["baseURL"].ToString() + controller);
                TokenModel tk = new TokenModel();
                tk = Task.Run(async () => await WebapiUtils.GetToken()).Result;
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + tk.access_token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                HttpResponseMessage response = client.GetAsync("Test").Result;
                var result = response.Content.ReadAsStringAsync().Result;
                if (result.ToString() == "\"OK\"")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                return false;
            }

        }

        public static bool Dbtest()
        {
            try
            {
                ServicePointManager
                .ServerCertificateValidationCallback +=
                (sender, cert, chain, sslPolicyErrors) => true;
                HttpClient client;
                string controller = "Home/";
                client = new HttpClient();
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["baseURL"].ToString() + controller);
                TokenModel tk = new TokenModel();
                tk = Task.Run(async () => await WebapiUtils.GetToken()).Result;
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + tk.access_token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                HttpResponseMessage response = client.GetAsync("DBTest").Result;
                var result = response.Content.ReadAsStringAsync().Result;
                if (result.ToString() == "\"OK\"")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                return false;
            }

        }
    }
}