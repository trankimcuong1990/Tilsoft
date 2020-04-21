using System.Web.Mvc;

namespace Frontend.Controllers
{
    [Authorize]
    public class ReceiptNoteMngController : Controller
    {
        private string ModuleCode = "ReceiptNoteMng";
        public ActionResult Init()
        {
            ViewBag.ModuleCode = ModuleCode;
            return View();
        }

        public ActionResult Index()
        {
            ViewBag.ModuleCode = ModuleCode;
            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.ModuleCode = ModuleCode;
            ViewBag.ID = id;
            return View();
        }

        public ActionResult View(int id)
        {
            ViewBag.ModuleCode = ModuleCode;
            ViewBag.ID = id;
            return View();
        }

        public ActionResult _PurchasingInvoiceItem()
        {
            ViewBag.ModuleCode = ModuleCode;
            return PartialView("~/Views/ReceiptNoteMng/_Edit/_PurchasingInvoiceItem.cshtml");
        }
        public ActionResult _General()
        {
            ViewBag.ModuleCode = ModuleCode;
            return PartialView("~/Views/ReceiptNoteMng/_Edit/_General.cshtml");
        }
        public ActionResult _FactorySaleInvoiceItem()
        {
            ViewBag.ModuleCode = ModuleCode;
            return PartialView("~/Views/ReceiptNoteMng/_Edit/_FactorySaleInvoiceItem.cshtml");
        }
    }
}