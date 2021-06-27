using EscolaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EscolaApp.Controllers
{
    public class ReferenciaisController : Controller
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        // GET: Referenciais
        public ActionResult Index()
        {


            return View();
        }

        // GET: Referenciais/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            List<Referenciai> lista = new List<Referenciai>();

            var cursos = lista.GroupBy(curso => curso.IdCurso)
                        .Select(grp => grp.First());

            Referenciai refe = db.Referenciais.FirstOrDefault(r => r.IdReferencial == id);

            return View(refe);
        }

        // GET: Referenciais/Create
        /// <summary>
        /// Poderia-se verificar se a ufcd nao está ja associada ao curso
        /// </summary>
        /// <param name="idCurso"></param>
        /// <returns></returns>
        public ActionResult CreateViaCurso(int? idCurso)
        {
            if (idCurso == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }


            ViewBag.IdUFCD = db.UFCDs.Select(a => new SelectListItem()
            {
                Value = a.IdUFCD.ToString(),
                Text = a.Designação,
            });

            ViewBag.Curso = db.Cursos.FirstOrDefault(i => i.IdCurso == idCurso);

            return View();
        }

        // POST: Referenciais/Create
        [HttpPost]
        public ActionResult CreateViaCurso(FormCollection collection, Referenciai referencial, int idCurso)
        {
            List<Referenciai> referenciais = new List<Referenciai>();
            string[] IdsUFCD = collection["IdUFCD"].Split(',');

            foreach (var item in IdsUFCD)
            {
                var checkRepeated = true;
                referencial = new Referenciai();

                referencial.IdCurso = idCurso;
                referencial.IdUFCD = int.Parse(item);
                referencial.CreateDate = DateTime.Now;
                referencial.LastUpdate = DateTime.Now;

                checkRepeated = db.Referenciais.Any(r => r.IdReferencial != referencial.IdReferencial && r.IdCurso == referencial.IdCurso && r.IdUFCD == referencial.IdUFCD);

                if (!checkRepeated)
                {
                    referenciais.Add(referencial);
                }
            }

            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid && referenciais.Count > 0)
                {
                    db.Referenciais.InsertAllOnSubmit(referenciais);
                    db.SubmitChanges();
                }
                else
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }
                return RedirectToAction("Details", "Cursos", new { id = idCurso });
            }
            catch (Exception e)
            {
                ViewBag.Erro = e.Message;
                return View();
            }
        }

        // GET: Referenciais/Edit/5
        /// <summary>
        /// O curso pode mudar uma UFCD, a UFCD não pode mudar de curso!
        /// Passagem de todas as ufcds
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Referenciai referencial = db.Referenciais.FirstOrDefault(r => r.IdReferencial == id);

            if (referencial == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdUFCD = new SelectList(db.UFCDs.OrderBy(u => u.Designação), "IdUFCD", "Designação");

            return View(referencial);
        }

        // POST: Referenciais/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Referenciai aEditar)
        {
            Referenciai editado = db.Referenciais.FirstOrDefault(r => r.IdReferencial == aEditar.IdReferencial);

            if (editado == null)
            {
                return HttpNotFound();
            }
            if (editado.IdUFCD == aEditar.IdUFCD)
            {
                return RedirectToAction("Details", "Cursos", new { id = editado.IdCurso });
            }
            var checkRepeated = true;
            checkRepeated = db.Referenciais.Any(r => r.IdReferencial != editado.IdReferencial &&  r.IdCurso == editado.IdCurso && r.IdUFCD == aEditar.IdUFCD );
            if (ModelState.IsValid && !checkRepeated)
            {
                editado.IdUFCD = aEditar.IdUFCD;
                editado.LastUpdate = DateTime.Now;
                try
                {
                    db.SubmitChanges();

                    return RedirectToAction("Details", "Cursos", new { id = editado.IdCurso });
                }
                catch (Exception e)
                {
                    ViewBag.Erro = e.Message;
                    return View();
                }
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        }

        // GET: Referenciais/Delete/5
        /// <summary>
        /// Acedido atraves dos cursos
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Referenciai refe = db.Referenciais.FirstOrDefault(r => r.IdReferencial == id);
            return View(refe);
        }

        // POST: Referenciais/Delete/5
        /// <summary>
        /// Acedido atraves dos cursos
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Referenciai aApagar = db.Referenciais.FirstOrDefault(r => r.IdReferencial == id);
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {

                    db.Referenciais.DeleteOnSubmit(aApagar);
                    db.SubmitChanges();
                }
                return RedirectToAction("Details", "Cursos", new { id = aApagar.IdCurso });
            }
            catch (Exception e)
            {

                ViewBag.Erro = e.Message;
                return View();
            }
        }
    }
}
