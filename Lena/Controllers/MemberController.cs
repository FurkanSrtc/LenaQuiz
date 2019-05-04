using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lena.Models;
using Lena.Models.ViewModels;

namespace Lena.Controllers
{

    [Authorize]
    public class MemberController : Controller
    {
        LenaDBManagerEntities dbm = new LenaDBManagerEntities();
        FormViewModel dbf = new FormViewModel();

        public ActionResult Index()
        {
            return View(dbm.Forms.Where(x => x.createdBy == User.Identity.Name).ToList());
        }

        public ActionResult CreateForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateForm(Forms f)
        {
            f.createdAt = DateTime.Now.Date;
            f.createdBy = User.Identity.Name;
            dbm.Forms.Add(f);
            dbm.SaveChanges();
            Session["formOwner"] = dbm.Forms.Where(x => x.createdBy == f.createdBy && x.createdAt == f.createdAt).Max(x => x.id);
            return RedirectToAction("Create");
        }

        public ActionResult Create()
        {
            List<FormNames> FormNames = dbm.FormNames.ToList();
            ViewBag.NameList = new SelectList(FormNames, "name", "name");
            return View();
        }
        [HttpPost]
        public ActionResult Create(FieldsViewModel field, string submit)
        {
            Fields f1 = new Fields();
            f1.Name = field.Name;
            f1.Required = field.Required;
            f1.DataType = field.DataType;
            f1.UserId = ((int)Session["formOwner"]);
            dbm.Fields.Add(f1);
            dbm.SaveChanges();
            switch (submit)
            {
                case "Oluştur":
                    return RedirectToAction("Index");
            }
            return RedirectToAction("Create");
        }

        public ActionResult CreateTable()
        {
            List<FormNames> FormNames = dbm.FormNames.ToList();
            ViewBag.NameList = new SelectList(FormNames, "name", "name");
            return View();
        }


        [Route("forms/{id}")]
        public ActionResult DetailsForm(int? id)
        {
            List<Fields> fields = dbm.Fields.Where(x=>x.UserId== id).ToList();
            return View(fields);
        }

        public ActionResult DeleteForm(int? id)
        {
            List<Fields> fields = dbm.Fields.Where(x => x.UserId == id).ToList();


            foreach (var item in fields)
            {
                dbm.Fields.Remove(item);
            }

            dbm.Forms.Remove(dbm.Forms.Where(x=>x.id==id).FirstOrDefault());
          
            dbm.SaveChanges();
            return RedirectToAction("Index");
        }
        

    }
}