using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using paylive.wxpay.Services;
using QRCoder;
using WxPayAPI;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace paylive.wxpay.Controllers
{
    public class WxController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return Content("wechat pay!");
        }

        public async Task<IActionResult> NativePay()
        {
            Log.Info(GetType().ToString(), "page load");
            var nativePay = new NativePay();
            var url = await nativePay.GetPayUrl("123456789");
            var imageUrl = "MakeQrCode?data=" + UrlEncoder.Default.Encode(url);
            return View("NativePay", imageUrl);
        }

        public FileContentResult MakeQrCode(string data)
        {
            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new BitmapByteQRCode(qrCodeData);
            var qrCodeImage = qrCode.GetGraphic(20);
            return File(qrCodeImage, "image/gif");
        }
    }
}