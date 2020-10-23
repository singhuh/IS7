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

    public class WorkHistoriesController : Controller
    {
        private IS7_DBEntities db = new IS7_DBEntities();

        // GET: WorkHistories
        public ActionResult Index(string searchBy, string search)
        { if (searchBy == "CompanyName")
            {
                return View(db.WorkHistories.Where(x => x.CompanyName.StartsWith(search) || search == null).ToList());
            }        
            else 
            { 
             return View(db.WorkHistories.Where(x => x.TitleName.StartsWith(search) || search == null).ToList());
            }
        }

        //{
        //    var workHistories = db.WorkHistories.Include(w => w.Company).Include(w => w.Title).Include(w => w.User);
        //    return View(await workHistories.ToListAsync());
        //}

        // GET: WorkHistories/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkHistory workHistory = await db.WorkHistories.FindAsync(id);
            if (workHistory == null)
            {
                return HttpNotFound();
            }
            return View(workHistory);
        }

        // GET: WorkHistories/Create
        public ActionResult Create()
        {
            ViewBag.CompanyName = new SelectList(db.Companies, "CompanyName", "CompanyName");
            ViewBag.TitleName = new SelectList(db.Titles, "TitleName", "TitleName");
            ViewBag.MNumber = new SelectList(db.Users, "MNumber", "LastName");
            return View();
        }

        // POST: WorkHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MNumber,CompanyName,TitleName,StartDate,EndDate,FTE")] WorkHistory workHistory)
        {
            if (ModelState.IsValid)
            {
                db.WorkHistories.Add(workHistory);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CompanyName = new SelectList(db.Companies, "CompanyName", "CompanyName", workHistory.CompanyName);
            ViewBag.TitleName = new SelectList(db.Titles, "TitleName", "TitleName", workHistory.TitleName);
            ViewBag.MNumber = new SelectList(db.Users, "MNumber", "LastName", workHistory.MNumber);
            return View(workHistory);
        }

        // GET: WorkHistories/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkHistory workHistory = await db.WorkHistories.FindAsync(id);
            if (workHistory == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyName = new SelectList(db.Companies, "CompanyName", "CompanyName", workHistory.CompanyName);
            ViewBag.TitleName = new SelectList(db.Titles, "TitleName", "TitleName", workHistory.TitleName);
            ViewBag.MNumber = new SelectList(db.Users, "MNumber", "LastName", workHistory.MNumber);
            return View(workHistory);
        }

        // POST: WorkHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MNumber,CompanyName,TitleName,StartDate,EndDate,FTE")] WorkHistory workHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workHistory).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyName = new SelectList(db.Companies, "CompanyName", "CompanyName", workHistory.CompanyName);
            ViewBag.TitleName = new SelectList(db.Titles, "TitleName", "TitleName", workHistory.TitleName);
            ViewBag.MNumber = new SelectList(db.Users, "MNumber", "LastName", workHistory.MNumber);
            return View(workHistory);
        }

        // GET: WorkHistories/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkHistory workHistory = await db.WorkHistories.FindAsync(id);
            if (workHistory == null)
            {
                return HttpNotFound();
            }
            return View(workHistory);
        }

        // POST: WorkHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            WorkHistory workHistory = await db.WorkHistories.FindAsync(id);
            db.WorkHistories.Remove(workHistory);
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
