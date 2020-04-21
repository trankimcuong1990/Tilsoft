using System.Web.Mvc;

namespace Frontend.Controllers
{
    [Authorize]
    public class ECommercialInvoiceController : Controller
    {
        private string moduleCode = "ECommercialInvoiceMng";

        // GET: Model
        public ActionResult Index()
        {
            ViewBag.Icon = "fa-search";
            ViewBag.ModuleCode = moduleCode;
            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Icon = "fa-pencil-square-o";
            ViewBag.ID = id;
            ViewBag.InvoiceTypeID = Request.QueryString["invoiceTypeID"];
            ViewBag.InternalCompanyID = Request.QueryString["internalCompanyID"];
            ViewBag.ClientID = Request.QueryString["clientID"];
            ViewBag.ParentID = Request.QueryString["parentID"];
            ViewBag.ModuleCode = moduleCode;
            return View();
        }

        public ActionResult Init()
        {
            ViewBag.Icon = "fa-search";
            ViewBag.ModuleCode = moduleCode;
            return View();
        }

        public ActionResult Init2()
        {
            ViewBag.Icon = "fa-search";
            ViewBag.InvoiceTypeID = Request.QueryString["invoiceTypeID"];
            ViewBag.InternalCompanyID = Request.QueryString["internalCompanyID"];
            ViewBag.ModuleCode = moduleCode;
            return View();
        }

        public ActionResult CreditNote(int id)
        {
            ViewBag.Icon = "fa-pencil-square-o";
            ViewBag.ID = id;
            ViewBag.ECommercialInvoiceID = Request.QueryString["eCommercialInvoiceID"];
            ViewBag.ModuleCode = moduleCode;
            return View();
        }

        public ActionResult Overview(int id)
        {
            ViewBag.Icon = "fa-eye";
            ViewBag.ID = id;
            ViewBag.InvoiceTypeID = Request.QueryString["invoiceTypeID"];
            ViewBag.InternalCompanyID = Request.QueryString["internalCompanyID"];
            ViewBag.ClientID = Request.QueryString["clientID"];
            ViewBag.ParentID = Request.QueryString["parentID"];
            ViewBag.ModuleCode = moduleCode;
            return View();
        }
    }
}