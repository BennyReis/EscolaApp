using EscolaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EscolaApp.Controllers
{
    public class UFCDController : Controller
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        // GET: UFCD
        public ActionResult Index()
        {
            return View(db.UFCDs);
        }

        // GET: UFCD/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            UFCD uf = db.UFCDs.FirstOrDefault(u => u.IdUFCD == id);

            return View(uf);
        }

        // GET: UFCD/Create
        /// <summary>
        /// Provavelmente deve ser acedido através dos cursos?
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        // POST: UFCD/Create
        [HttpPost]
        public ActionResult Create(UFCD ufcd)
        {
            if (ModelState.IsValid)
            {
                db.UFCDs.InsertOnSubmit(ufcd);
            }
            try
            {
                // TODO: Add insert logic here
                ufcd.CreateDate = DateTime.Now;
                ufcd.LastUpdate = DateTime.Now;
                db.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.Erro = e;
                return View();
            }
        }

        // GET: UFCD/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            UFCD ufcd = db.UFCDs.FirstOrDefault(u => u.IdUFCD == id);

            if (ufcd == null)
            {
                return HttpNotFound();
            }

            return View(ufcd);
        }

        // POST: UFCD/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, UFCD aEditar)
        {
            UFCD editada = db.UFCDs.FirstOrDefault(u => u.IdUFCD == id);

            if (editada == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                editada.Duração = aEditar.Duração;
                editada.Designação = aEditar.Designação;
                editada.Detalhes = aEditar.Detalhes;
                editada.LastUpdate = DateTime.Now;
            }

            try
            {
                // TODO: Add update logic here
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: UFCD/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            UFCD uf = db.UFCDs.FirstOrDefault(u => u.IdUFCD == id);

            if (uf == null)
            {
                return HttpNotFound();
            }
            return View(uf);
        }

        // POST: UFCD/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            UFCD aApagar = db.UFCDs.FirstOrDefault(u => u.IdUFCD == id);
            if (aApagar == null)
            {
                return HttpNotFound();
            }
            db.UFCDs.DeleteOnSubmit(aApagar);
            try
            {
                // TODO: Add delete logic here
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
