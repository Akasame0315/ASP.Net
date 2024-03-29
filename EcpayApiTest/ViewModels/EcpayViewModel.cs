﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.ViewModels
{
    public class EcpayViewModel
    {
        public string ChoosePayment { get; set; }
        public string MerchantID { get; set; }
        public string MerchantTradeNo { get; set; }
        public string MerchantTradeDate { get; set; }
        public string PaymentType { get; set; }
        public int TotalAmount { get; set; }
        public string TradeDesc { get; set; }
        public string ItemName { get; set; }
        public string ReturnURL { get; set; }
        public string CheckMacValue { get; set; }
        public string CheckMacValue2 { get; set; }
        public int EncryptType { get; set; }
    }
}