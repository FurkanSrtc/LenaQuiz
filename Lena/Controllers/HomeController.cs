using Lena.App_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Lena.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
           
            MembershipUserCollection users = Membership.GetAllUsers();
            return View(users);
        }

        [AllowAnonymous]
        public ActionResult Register()
        {

            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(Kullanici k)
        {
            MembershipCreateStatus durum;
            Membership.CreateUser(k.KullaniciAdi, k.Parola, k.Email, k.GizliSoru, k.GizliCevap, true, out durum);
            string mesaj="";
            switch (durum)
            {
                case MembershipCreateStatus.Success:
                    mesaj += "Kayıt Başarılı!";
                    break;
                case MembershipCreateStatus.InvalidUserName:
                    mesaj += "Geçersiz Kullanıcı Adı";

                    break;
                case MembershipCreateStatus.InvalidPassword:
                    mesaj += "Geçersiz Parola";
                    break;
                case MembershipCreateStatus.InvalidQuestion:
                    mesaj += "Geçersiz Soru";
                    break;
                case MembershipCreateStatus.InvalidAnswer:
                    mesaj += "Geçersiz Cevap";
                    break;
                case MembershipCreateStatus.InvalidEmail:
                    mesaj += "Geçersiz Mail";
                    break;
                case MembershipCreateStatus.DuplicateUserName:
                    mesaj += "Bu kullanıcı adı daha önce kullanıldı.";
                    break;
                case MembershipCreateStatus.DuplicateEmail:
                    mesaj += "Bu email daha önce kullanıldı.";
                    break;
                case MembershipCreateStatus.UserRejected:
                    mesaj += "Kullanıcı engel hatası";
                    break;
                case MembershipCreateStatus.InvalidProviderUserKey:
                    mesaj += "Geçerssiz key hatası";
                    break;
                case MembershipCreateStatus.DuplicateProviderUserKey:
                    mesaj += "Bu key daha önce kullanıldı";
                    break;
                case MembershipCreateStatus.ProviderError:
                    mesaj += "Sistem hatası";
                    break;
                default:
                    break;
            }
            ViewBag.Mesaj = mesaj;
            if (durum==MembershipCreateStatus.Success)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Kullanici k, string Hatirla)
        {
            bool sonuc = Membership.ValidateUser(k.KullaniciAdi, k.Parola);
            if (sonuc)
            {
                if (Hatirla == "on")
                    FormsAuthentication.RedirectFromLoginPage(k.KullaniciAdi, true);

                else
                    FormsAuthentication.RedirectFromLoginPage(k.KullaniciAdi, false);
                return RedirectToAction("Index", "Member");
            }
            else
                ViewBag.Uyari = "Kullanıcı Adı veya Parola Hatalı";
                return View();
          
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }



    }
}