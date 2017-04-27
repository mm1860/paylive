using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using paylive.alipay;
using paylive.core;
using paylive.wxpay;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace paylive.Controllers
{
    public class HomeController : Controller
    {
        public List<Plug> TypePlugs;

        public HomeController(List<Plug> plugs)
        {
            TypePlugs = plugs;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var ua = Request.Headers["User-Agent"].ToString();
            var service = TypePlugs.FirstOrDefault(i => ua.ToLower().Contains(i.Ua));
            if (service == null)
            {
                return View();
            }
            return Redirect(service.Url);
        }
    }
}