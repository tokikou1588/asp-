using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace BookShop.Web.AliPay
{
    public class Pay
    {
        public Pay(string subject, string body, string out_trade_no, decimal total_fee)
        {
            partner = ConfigurationManager.AppSettings["partner"];
            return_url = ConfigurationManager.AppSettings["return_url"];
            seller_email = ConfigurationManager.AppSettings["seller_email"];
            key = ConfigurationManager.AppSettings["key"];
            payGateUrl = ConfigurationManager.AppSettings["payGateUrl"];
            this.subject = subject;
            this.body = body;
            this.out_trade_no = out_trade_no;
            this.total_fee = total_fee;
            //为按顺序连接     总金额、 商户编号、订单号、商品名称、商户密钥的MD5值。(小写值)
            this.sign = Common.WebCommon.GetMd5String(total_fee + partner + out_trade_no + subject + key).ToLower();

        }
        public string GoPay()
        {
            return string.Format("{0}?partner={1}&return_url={2}&subject={3}&body={4}&out_trade_no={5}&total_fee={6}&seller_email={7}&sign={8}", payGateUrl,partner,return_url,subject,body,out_trade_no,total_fee,seller_email,sign);
        }

        private string partner;//商户编号    1   --

        public string Partner
        {
            get { return partner; }
            set { partner = value; }
        }
        private string return_url;//回调商户地址（通过商户网站的哪个页面来通知支付成功！）1  --  支付宝将数据返回给这个属性所指定的我们商城网站中的某个页面。

        public string Return_url
        {
            get { return return_url; }
            set { return_url = value; }
        }
        private string subject;//商品名称

        public string Subject
        {
            get { return subject; }
            set { subject = value; }
        }
        private string body;//商品描述

        public string Body
        {
            get { return body; }
            set { body = value; }
        }
        private string out_trade_no;//订单号！！！(由商户网站生成，支付宝不确保正确性，只负责转发。)

        public string Out_trade_no
        {
            get { return out_trade_no; }
            set { out_trade_no = value; }
        }
        private decimal total_fee;//总金额 

        public decimal Total_fee
        {
            get { return total_fee; }
            set { total_fee = value; }
        }
        private string seller_email;//卖家邮箱1--

        public string Seller_email
        {
            get { return seller_email; }
            set { seller_email = value; }
        }
        private string sign;//数字签名。为按顺序连接     总金额、 商户编号、订单号、商品名称、商户密钥的MD5值。(小写值)支付时,请将上述参数以get形式传给接入地址.

        public string Sign
        {
            get { return sign; }
            set { sign = value; }
        }
        private string key;//密钥    --1

        public string Key
        {
            get { return key; }
            set { key = value; }
        }

        private string payGateUrl;//支付地址 1

        public string PayGateUrl
        {
            get { return payGateUrl; }
            set { payGateUrl = value; }
        }



    }

}