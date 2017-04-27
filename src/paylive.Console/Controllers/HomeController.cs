using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using paylive.Console.DbContext;
using paylive.Console.Core;

namespace paylive.Console.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            List<Receivers> list;
            using (var live = new LiveContext())
            {
                list = live.Receivers.ToList();
            }
            return View(list);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string msg,string imno)
        {
            WebImConfig im;
            using (var live = new LiveContext())
            {
                im = live.WebImConfig.FirstOrDefault();
            }

            var body = $"UserName={im.UserName}&Msg={msg}&Receivers={imno}&ssid={im.Ssid}";
            var result = await HttpService.Post<object>(body, Config._Sendsms);
            using (var live = new LiveContext())
            {
                live.SmsQu.Add(new SmsQu()
                {
                    Msg = msg,
                    Receivers = imno,
                    Completed = result.StringBody.rc == "200",
                    AddTime = DateTime.Now
                });
                live.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Contact()
        {
            List<Receivers> list;
            using (var live = new LiveContext())
            {
                list = live.Receivers.ToList();
            }
            return View(list);
        }

        [HttpPost]
        public IActionResult Contact(string mobile, string imno)
        {
            using (var live = new LiveContext())
            {
                var rece = live.Receivers.Any(i => i.Mobile == mobile && i.ImNo == imno);
                if (!rece)
                    live.Receivers.Add(new Receivers()
                    {
                        Mobile = mobile,
                        ImNo = imno,
                        AddTime = DateTime.Now
                    });
                live.SaveChanges();
            }

            return RedirectToAction("Contact");
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult H5()
        {
            return View();
        }
    }
}
