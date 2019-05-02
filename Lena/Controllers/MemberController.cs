using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lena.Models;

namespace Lena.Controllers
{

    [Authorize]
    public class MemberController : Controller
    {
        LenaDBManagerEntities dbm = new LenaDBManagerEntities();
        public ActionResult Index()
        {
           
            return View(dbm.Forms.Where(x => x.createdBy == User.Identity.Name).ToList());
          
        }

        public ActionResult Create()
        {
            List<FormNames> FormNames = dbm.FormNames.ToList();
            ViewBag.NameList = new SelectList(FormNames, "id", "name");

          
            return View();
        }



    }
}