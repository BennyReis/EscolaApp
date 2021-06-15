using EscolaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EscolaApp.Controllers
{
    public class CursosController : Controller
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        // GET: Cursos
        public ActionResult Index()
        {
            return View(db.Cursos);
        }

        /// <summary>
        /// Detalhes do curso. Após adicionar as UFCD's deve-se alterar o controlador
        /// Tem como objetivo mostrar todas as UFCD's de um curso, bem como a soma das suas horas ---> duração do curso
        /// </summary>
        /// <param name="id">Opcional para prevenir possiveis erros </param>
        /// <returns></returns>
        // GET: Cursos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Curso curso = db.Cursos.FirstOrDefault(c => c.IdCurso == id);

            if (curso == null)
            {
                return HttpNotFound();
            }

            var pesquisa = from Cursos in db.Cursos
                           join Referenciai in db.Referenciais
                           on Cursos.IdCurso equals Referenciai.IdCurso
                           join UFCD in db.UFCDs
                           on Referenciai.IdUFCD equals UFCD.IdUFCD
                           select UFCD.Duração;
            ViewBag.Duracao = pesquisa.Sum();
            //Area area = db.Areas.FirstOrDefault(a => a.IdArea == curso.IdArea);

            ////utilização de um view bag para passar a informação sobre a que área o curso pertence
            //if (area != null)
            //{
            //    ViewBag.Area = area.Designação;
            //}

            return View(curso);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: Cursos/Create
        public ActionResult Create()
        {
            //passagem da das áreas para que o utilizador possa escolher a que área pertence um curso
            ViewBag.IdArea = new SelectList(db.Areas.OrderBy(a => a.Designação),"IdArea","Designação");

            return View();
        }

        /// <summary>
        /// Adicionar um curso
        /// Método que envia para o servidor o curso adiconado pelo utilizador
        /// </summary>
        /// <param name="novoCurso"></param>
        /// <returns></returns>
        // POST: Cursos/Create
        [HttpPost]
        public ActionResult Create(Curso novoCurso)
        {
            if (ModelState.IsValid)
            {
                db.Cursos.InsertOnSubmit(novoCurso);
            }
            try
            {
                // TODO: Add insert logic here
                novoCurso.CreateDate = DateTime.Now;
                novoCurso.LastUpdate = DateTime.Now;
                db.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                ViewBag.Erro = e;
                return View();
            }
        }

        // GET: Cursos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Curso curso = db.Cursos.FirstOrDefault(c => c.IdCurso == id);

            if (curso == null)
            {
                return HttpNotFound();
            }

            ViewBag.IdArea = new SelectList(db.Areas.OrderBy(a => a.Designação), "IdArea", "Designação");

            return View(curso);
        }

        // POST: Cursos/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Curso aEditar)
        {
            Curso cursoEditado = db.Cursos.FirstOrDefault(c => c.IdCurso == id);

            if (cursoEditado == null)
            {
                return HttpNotFound();
            }
            Area area = db.Areas.FirstOrDefault(a => a.IdArea == aEditar.IdArea);

            if (ModelState.IsValid && area != null)
            {
                cursoEditado.IdCurso = aEditar.IdCurso;
                cursoEditado.IdArea = aEditar.IdArea;
                cursoEditado.Detalhes = aEditar.Detalhes;
                cursoEditado.Designação = aEditar.Designação;
                cursoEditado.LastUpdate = DateTime.Now;
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

        /// <summary>
        /// Apagar um curso
        /// HTTP GET, onde o utilizador pede ao servidor as informações sobre o curso a apagar
        /// </summary>
        /// <param name="id">Parametro opcional chave primaria do curso a apagar</param>
        /// <returns></returns>
        // GET: Cursos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Curso curso = db.Cursos.FirstOrDefault(c => c.IdCurso == id);

            if (curso == null)
            {
                return HttpNotFound();
            }

            Area area = db.Areas.FirstOrDefault(a => a.IdArea == curso.IdArea);

            if (area != null)
            {
                ViewBag.Area = area.Designação;
            }
            return View(curso);
        }

        /// <summary>
        /// HTTP POST, o utilizador envia para o servidor qual o curso a apagar
        /// </summary>
        /// <param name="id">Chave primaria do registo a apagar na base de dados</param>
        /// <returns></returns>
        // POST: Cursos/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Curso aApagar = db.Cursos.First(c => c.IdCurso == id);

            if (aApagar == null)
            {
                return HttpNotFound();
            }
            db.Cursos.DeleteOnSubmit(aApagar);
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
