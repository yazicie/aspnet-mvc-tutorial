using System.Linq;
using System.Web.Mvc;
using Notes.Models;

namespace Notes.Controllers
{
  public class UsersController : Controller
  {
    private readonly NotesDBEntities2 db = new NotesDBEntities2();

    // GET: Users
    public ActionResult Index()
    {
      return View(db.Users.ToList());
    }

    // GET: Users/Create
    public ActionResult Create()
    {
      return View();
    }

    // POST: Users/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "Id,Username,Password,PasswordConfirm")] Users user)
    {

      var isUserExists = db.Users.Any(x => x.Username == user.Username);
      if (isUserExists)
      {
        user.LoginErrorMessage = "Bu kullanıcı adı daha önce alınmış.";
        return View("Create", user);
      }

      if (ModelState.IsValid)
      {
        db.Users.Add(user);
        db.SaveChanges();

        return RedirectToAction("Index", "Notes", new { userId = user.Id });
      }

      return View(user);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        db.Dispose();
      }
      base.Dispose(disposing);
    }

  }
}
