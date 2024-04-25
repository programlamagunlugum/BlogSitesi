using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MvcBlogSitesi.Controllers
{
    public class RecaptchaController : Controller
    {
        public JsonResult Verify(string response)
        {
            string secretKey = ConfigurationManager.AppSettings["recaptchaSecretKey"];
            string apiUrl = "https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}";
            string requestUrl = string.Format(apiUrl, secretKey, response);

            using (WebClient client = new WebClient())
            {
                string responseJson = client.DownloadString(requestUrl);
                JObject jsonResponse = JObject.Parse(responseJson);
                bool success = jsonResponse.Value<bool>("success");

                return Json(new { success });
            }
        }
    }
}