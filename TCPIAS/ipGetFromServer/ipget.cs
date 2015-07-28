using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;

//namespace TCPIAS.ipGetFromServer
//{
//    class ipget
//    {
//        protected string IpAddress(out bool isProxy)

//        {

//            var request =  HttpContinueDelegate.Request;

//            const string noIp = "Unknow";

//            //if (current == null)

//            //{

//            //    isProxy = false;

//            //    return noIp;

//            //}

//            //Note:HTTP_X_FORWARDED_FOR(如果是代理这个是代理的IP)是可由客户端修改IP地址的

//            var forIp = request.ServerVariables["HTTP_X_FORWARDED_FOR"];  //本机真实IP,1层代理IP,2层代理IP,.....

//            isProxy = request.ServerVariables["HTTP_VIA"] != null ? true : false;

//            if (!string.IsNullOrEmpty(forIp))

//            {

//                if (!isProxy)

//                    isProxy = forIp.IndexOf(',') > -1 ? true : false;

//                if (forIp.IndexOf('.') > 0)

//                    return forIp;

//            }

//            return request.UserHostAddress ?? noIp;

//        }
//    }
//}
