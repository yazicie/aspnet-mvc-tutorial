using Notes.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Notes.Controllers
{
  public class HomeController : Controller
  {
    public ActionResult Index()
    {
      if (Session["Id"] != null)
      {
        var userId = Convert.ToInt32(Session["Id"]);
        return RedirectToAction("Index", "Notes", new { userId = userId });
      }
      return View();
    }

    public ActionResult Logout()
    {
      Session.Abandon();
      return RedirectToAction("Index", "Home");
    }

    public ActionResult Contact()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Authorize(Users user)
    {
      using (var db = new NotesDBEntities2())
      {
        var userDetail = db.Users.Where(x => x.Username == user.Username && x.Password == user.Password).FirstOrDefault();
        if (userDetail == null)
        {
          user.LoginErrorMessage = "Geçersiz kullanıcı adı ya da şifre";
          return View("Index", user);
        }
        else
        {
          Session["Id"] = userDetail.Id;
          return RedirectToAction("Index", "Notes", new { userId = userDetail.Id });
        }
      }
    }

  }
}