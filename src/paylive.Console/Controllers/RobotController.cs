using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using paylive.Console.Core;
using paylive.Console.DbContext;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace paylive.Console.Controllers
{
    //飞信维持登录状态
    public class RobotController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public async Task GetSmsPwd()
        {
            await HttpService.Post<object>("uname=13718607977", Config._GetSmsPwd);
        }

        public async Task Login(string pwd)
        {
            var ret = await HttpService.Post<object>($"UserName=13718607977&Pwd={pwd}&OnlineStatus=400&AccountType=1&Ccp=", Config._Login);
            if (ret.CookieCollection.Count > 0&& ret.CookieCollection["webim_sessionid"]!=null)
            {
                var webimsessionid = ret.CookieCollection["webim_sessionid"].Value;
                using (var live = new LiveContext())
                {
                    var im = live.WebImConfig.FirstOrDefault();
                    if (im == null)
                    {
                        live.WebImConfig.Add(new WebImConfig() { UserName = "213129474", Ssid = webimsessionid });
                    }
                    else
                    {
                        im.UserName = "213129474";
                        im.Ssid = webimsessionid;
                    }
                    live.SaveChanges();
                }
                GetConnect(webimsessionid);
            }
        }

        public async Task GetConnect(string webimsessionid)
        {
            //心跳计数
            int i = 10;

            while (true)
            {
                try
                {
                    var ret = await HttpService.Post<List<ConnectBody>>($"ssid={webimsessionid}&reported=", string.Format(Config._GetConnect, i));
                    if ((ret.StringBody.rc != "305" && ret.StringBody.rc != "200") || i == 36000)
                    {
                        Core.Core.WriteTextLog("GetConnect" + webimsessionid, ret.StringBody.rc, DateTime.Now);
                        GetSmsPwd();
                        break;
                    }
                    i++;
                }
                catch (Exception ex)
                {
                    Core.Core.WriteTextLog("GetConnect" + webimsessionid, ex.Message, DateTime.Now);
                    GetSmsPwd();
                }
                Thread.Sleep(5000);
            }
        }
    }
}
