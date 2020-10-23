using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IS7.Models;

namespace IS7.Controllers
{
    public class InterviewsController : Controller
    {
        private IS7_DBEntities db = new IS7_DBEntities();
        public ActionResult Index(string searchBy, string search)
        {
            if (searchBy == "InterviewCompany")
            {
                return View(db.Interviews.Where(x => x.InterviewCompany.StartsWith(search) || search == null).ToList());
            }
            else
            {
                return View(db.Interviews.Where(x => x.MNumber.EndsWith(search) || search == null).ToList());
            }
        }
        // GET: Interviews
        //public async Task<ActionResult> Index()
        //{
        //    var interviews = db.Interviews.Include(i => i.User);
        //    return View(await interviews.ToListAsync());
        //}

        // GET: Interviews/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Interview interview = await db.Interviews.FindAsync(id);
            if (interview == null)
            {
                return HttpNotFound();
            }
            return View(interview);
        }

        // GET: Interviews/Create
        public ActionResult Create()
        {
            ViewBag.MNumber = new SelectList(db.Users, "MNumber", "LastName");
            return View();
        }

        // POST: Interviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MNumber,InterviewCompany,InterviewDate,Offer")] Interview interview)
        {
            if (ModelState.IsValid)
            {
                db.Interviews.Add(interview);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MNumber = new SelectList(db.Users, "MNumber", "LastName", interview.MNumber);
            return View(interview);
        }

        // GET: Interviews/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Interview interview = await db.Interviews.FindAsync(id);
            if (interview == null)
            {
                return HttpNotFound();
            }
            ViewBag.MNumber = new SelectList(db.Users, "MNumber", "LastName", interview.MNumber);
            return View(interview);
        }

        // POST: Interviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MNumber,InterviewCompany,InterviewDate,Offer")] Interview interview)
        {
            if (ModelState.IsValid)
            {
                db.Entry(interview).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MNumber = new SelectList(db.Users, "MNumber", "LastName", interview.MNumber);
            return View(interview);
        }

        // GET: Interviews/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Interview interview = await db.Interviews.FindAsync(id);
            if (interview == null)
            {
                return HttpNotFound();
            }
            return View(interview);
        }

        // POST: Interviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Interview interview = await db.Interviews.FindAsync(id);
            db.Interviews.Remove(interview);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
