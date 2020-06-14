using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Notes.Models;

namespace Notes.Controllers
{
  public class NotesController : Controller
  {
    private readonly NotesDBEntities2 db = new NotesDBEntities2();

    // GET: Notes
    public ActionResult Index(int? userId)
    {

      if (userId == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }

      ViewData["Id"] = userId;
      var notes = db.Notes.Where(a => a.UserId == userId);
      if (notes == null)
      {
        return HttpNotFound();
      }
      return View(notes);
    }

    // GET: Notes/Create
    public ActionResult Create(int? userId)
    {
      ViewData["Id"] = userId;
      return View();
    }

    // POST: Notes/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "Id,UserId,Content")] Models.Notes note)
    {

      if (ModelState.IsValid)
      {
        db.Notes.Add(note);
        db.SaveChanges();
        return RedirectToAction("Index", "Notes", new { userId = note.UserId });
      }

      return View(note);
    }

    // GET: Notes/Edit/5
    public ActionResult Edit(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }

      var notes = db.Notes.Find(id);

      if (notes == null)
      {
        return HttpNotFound();
      }
      return View(notes);
    }

    // POST: Notes/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "Id,UserId,Content")] Models.Notes note)
    {
      if (ModelState.IsValid)
      {
        db.Entry(note).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Index", "Notes", new { userId = note.UserId });
      }
      return View(note);
    }

    // GET: Notes/Delete/5
    public ActionResult Delete(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }

      var notes = db.Notes.Find(id);

      if (notes == null)
      {
        return HttpNotFound();
      }
      return View(notes);
    }

    // POST: Notes/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
      var notes = db.Notes.Find(id);
      db.Notes.Remove(notes);
      db.SaveChanges();
      return RedirectToAction("Index", "Notes", new { userId = notes.UserId });
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
