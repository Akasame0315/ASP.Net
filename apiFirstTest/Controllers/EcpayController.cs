using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class EcpayController : Controller
    {
        // GET: Ecpay
        public ActionResult Index()
        {
            WebApplication1.ViewModels.EcpayViewModel model = new WebApplication1.ViewModels.EcpayViewModel();
            var str = "HashKey=pwFHCqoQZGmho4w6&ChoosePayment=Credit&EncryptType=1&ItemName=product&MerchantID=3002607&MerchantTradeDate=2022/12/02 14:06:55&MerchantTradeNo=a0ade2e6-fc30-471d-89e2-b5494b41c56e&PaymentType=aio&ReturnURL=https://4dd5-101-3-2-97.jp.ngrok.io/Ecpay/Index&TotalAmount=500&TradeDesc=test&HashIV=EkRm7iFT261dpevs";
            var encode = "";
            var lowerEncode = "";
            var encode256 = "";
            var upper256 = "";
            encode = HttpUtility.UrlEncode(str);
            lowerEncode = encode.ToLower();
            encode256 = Sha256Helper.Sha256(lowerEncode);
            upper256 = encode256.ToUpper();
            model.ChoosePayment = "Credit";
            model.MerchantID = "3002607";
            model.MerchantTradeNo = "a0ade2e6-fc30-471d-89e2-b5494b41c56e";
            model.MerchantTradeDate = "2022/12/02 14:06:55";
            model.PaymentType = "aio";
            model.TotalAmount = 500;
            model.TradeDesc = "test";
            model.ItemName = "product";
            model.ReturnURL = "https://4dd5-101-3-2-97.jp.ngrok.io/Ecpay/Index";
            model.CheckMacValue = upper256;
            model.EncryptType = 1;

            return View(model);
        }

        public static class Sha256Helper
        {
            /// <summary>
            /// Get SHA256 hash
            /// </summary>
            /// <param name="str"></param>
            /// <example>
            /// For example:
            /// <code>
            ///    Sha256("Ruyut");
            /// </code>
            ///  results in <c>e5a47100b733b86f6fc82b9b614c4829bc9042ca3f24a65c5c2783c699ed6625</c>
            /// </example>
            public static string Sha256(string str)
            {
                byte[] sha256Bytes = Encoding.UTF8.GetBytes(str);
                SHA256Managed sha256 = new SHA256Managed();
                byte[] bytes = sha256.ComputeHash(sha256Bytes);
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }
    }
}