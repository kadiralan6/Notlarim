using Notlarim102.BusinessLayer;
using Notlarim102.Entity;
using Notlarim102.Entity.Messages;
using Notlarim102.Entity.ValueObject;
using Notlarim102.WebApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Notlarim102.WebApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //16.12 /2 datayi list note gibi gönder
            //if (TempData["mm"] != null)
            //{
            //    return View(TempData["mm"] as List<Note>);
            //}
            //Test test = new Test();
            ////asağıdakileri test de yazdıklarımızdan sonra gelip yapıyoz
            ////  test.InsertTest(); bunu UpdateTest yazınca kapattık
            ////  test.UpdateTest();
            ////  test.DeleteTest();
            //test.CommentTest();

            NoteManager nm = new NoteManager();
            return View(nm.GetAllNotes().OrderByDescending(x => x.ModifiedOn).ToList());
            //return View(nm.GetAllNoteQueryable().OrderByDescending(x=>x.ModifiedOn));

        }
        public ActionResult ByCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryManager cm = new CategoryManager();

            Category cat = cm.GetCategoriesById(id.Value);

            if (cat == null)
            {
                return HttpNotFound();
            }
            // TempData["mm"] = cat.Notes;

            return View("Index", cat.Notes.OrderByDescending(x => x.ModifiedOn).ToList());
        }

        //en çok beğeni alanları sıralıyor
        public ActionResult MostLiked()
        {
            NoteManager nm = new NoteManager();
            return View("Index", nm.GetAllNotes().OrderByDescending(x => x.LikeCount).ToList());
        }
        /*  public ActionResult isim()
          {
              NoteManager nm = new NoteManager();
              return View("Index", nm.GetAllNotes().OrderByDescending(x => x.ModifiedUserName=="candan").ToList());
          }*/
        //20//12/1
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            //21,12/6
            if (ModelState.IsValid)
            {
                NotlarimUserManager num = new NotlarimUserManager();
                BusinessLayerResult<NotlarimUser> res = num.LoginUser(model);
                if (res.Errors.Count>0) //bir hata da olur birden fazla
                {//22,12/8
                    if (res.Errors.Find(x=>x.Code==Entity.Messages.ErrorMessageCode.UserIsNotActive)!=null)
                    {
                        ViewBag.SetLink = $"https://localhost:44300/Home/UserActivate/{res.Result.ActivateGuid}";
                    }
                    res.Errors.ForEach(s => ModelState.AddModelError("",s.Message));
                    return View(model);
                }

                //yonlendirme ve session

                Session["login"] = res.Result;//oturum açma demek eski bir tekniktir.kayıt olduğgnzda bilgiler arka planda saklanıtor. doğrulugu sessiondan onaylanıyor. oturum sonlandırma da.

                return RedirectToAction("Index"); //yonlendirme

            }

            return View();
        }
        //20//12/4
        public ActionResult Register()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            //kullanici adinin uygunluğu
            //email kontrolü
            //aktivasyon işlemi

            //  bool hasError = false;

            if (ModelState.IsValid)
            {
                //21,12 de burayı kapattık
                // if (model.UserName=="aaa")
                // {
                //     ModelState.AddModelError("","Bu kollanici adi uygun değil..");
                ////     hasError = true;
                // }
                // if (model.Email=="aaa@aaa.com")
                // {
                //     ModelState.AddModelError("", "Email kullanılıyor.");
                //  //   hasError = true;
                // }
                // /* if (hasError == true)
                //  {
                //      return View(model);
                //  }
                //  else
                //  {
                //      return RedirectToAction("RegisterOk");
                //  }*/

                //


                //21,12// 2

                //NotlarimUserManager num = new NotlarimUserManager();
                //NotlarimUser user = null;

                //try
                //{
                //    user = num.RegisterUser(model);
                //}
                //catch (Exception e)
                //{
                //    ModelState.AddModelError("", e.Message);
                //}
                //foreach (var item in ModelState)
                //{
                //    if (item.Value.Errors.Count > 0) //true demek hoca  göstermişti
                //    {
                //        return View(model);
                //    }
                //}

                //21,12/4
                NotlarimUserManager num = new NotlarimUserManager();
                BusinessLayerResult<NotlarimUser> res = num.RegisterUser(model);
                if (res.Errors.Count>0)
                {
                    res.Errors.ForEach(s => ModelState.AddModelError("", s.Message));
                    return View(model);
                }


                return RedirectToAction("RegisterOk");
            }

            return View(model);
        }
        public ActionResult RegisterOk()
        {
            return View();
        }
        // 22,12- 20
        public ActionResult UserActivate(Guid id)
        {
            NotlarimUserManager num = new NotlarimUserManager();
            BusinessLayerResult<NotlarimUser> res = num.ActivateUser(id);
            if (res.Errors.Count>0)
            {
                TempData["errors"] = res.Errors;
                return RedirectToAction("UserActivateCancel");
            }
            return  RedirectToAction("UserActivateOk");
        }
        // 22,12- 21 alltakilerin viewi oluşturup registerok den kopyala yapıstr aldık.
        public ActionResult UserActivateOk()
        {

            return View();
        }
        public ActionResult UserActivateCancel()
        {
            List<ErrorMessageObject> errors = null;
            if (TempData["errors"]!=null)
            {
                errors= TempData["errors"] as List<ErrorMessageObject>;

            }
            return View(errors);
        }
    }

}