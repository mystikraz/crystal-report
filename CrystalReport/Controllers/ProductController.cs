using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using CrystalReport.Models;


namespace CrystalReport.Controllers
{
    public class ProductController : Controller
    {

        private MyDemoEntities mde = new MyDemoEntities();
        public ActionResult Index()
        {
            ViewBag.ListProducts = mde.Products.ToList();

            return View();
        }

        public ActionResult Export()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/CrystalReportProduct.rpt")));
            rd.SetDataSource(mde.Products.Select(p => new
            {
                id = p.id,
                Name = p.Name,
                Price = p.Price.Value,
                Quantity = p.Quantity.Value
            }).ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "ListProducts.pdf");
        }
        public ActionResult ExportGrouping()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports/CrystalReportGrouping.rpt")));
            rd.SetDataSource(mde.Products.Select(p => new
            {
                id = p.id,
                Name = p.Name,
                Price = p.Price.Value,
                Quantity = p.Quantity.Value,
                CategoryId=p.Category.id,
                CategoryName=p.Category.name
            }).ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "ListProductsGrouping.pdf");
        }
    }
}