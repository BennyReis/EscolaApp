using EscolaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EscolaApp.Controllers
{
    public class TurmasController : Controller
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        // GET: Turmas
        public ActionResult Index()
        {
            ViewBag.IdCurso = new SelectList(db.Cursos.OrderBy(c => c.Designação), "IdCurso", "Designação");
            return View(db.Turmas);
        }

        /// <summary>
        /// Falta linkar os alunos da turma nos detalhes!
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Turmas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Turma turma = db.Turmas.FirstOrDefault(t => t.IdTurma== id);
            return View(turma);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: Turmas/Create
        public ActionResult Create()
        {
            //passagem da das áreas para que o utilizador possa escolher a que área pertence um curso
            ViewBag.IdCurso = new SelectList(db.Cursos.OrderBy(c => c.Designação), "IdCurso", "Designação");

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="novaTurma"></param>
        /// <returns></returns>
        // POST: Turmas/Create
        [HttpPost]
        public ActionResult Create(Turma novaTurma)
        {
            if (ModelState.IsValid)
            {
                db.Turmas.InsertOnSubmit(novaTurma);
            }
            try
            {
                // TODO: Add insert logic here
                novaTurma.CreateDate = DateTime.Now;
                novaTurma.LastUpdate = DateTime.Now;
                db.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.Erro = e;
                return View();
            }
        }

        public ActionResult CreateViaCurso(int? idCurso)
        {
            if (idCurso == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Curso curso = db.Cursos.FirstOrDefault(t => t.IdCurso == idCurso);
            ViewBag.Curso = curso;
            return View();
        }
        [HttpPost]
        public ActionResult CreateViaCurso(int idCurso, Turma novaTurma)
        {
            if (ModelState.IsValid)
            {
                db.Turmas.InsertOnSubmit(novaTurma);
            }
            try
            {
                // TODO: Add insert logic here
                novaTurma.CreateDate = DateTime.Now;
                novaTurma.LastUpdate = DateTime.Now;
                db.SubmitChanges();

                return RedirectToAction("Details", "Cursos", new { id = idCurso });
            }
            catch (Exception e)
            {
                ViewBag.Erro = e;
                return View();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Chave primaria da turma a editar</param>
        /// <returns></returns>
        // GET: Turmas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Turma turma = db.Turmas.FirstOrDefault(c => c.IdTurma == id);

            if (turma == null)
            {
                return HttpNotFound();
            }

            ViewBag.IdCurso = new SelectList(db.Cursos.OrderBy(c => c.Designação), "IdCurso", "Designação");

            return View(turma);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Chave primaria da turma a editar</param>
        /// <param name="aEditar">Objeto passado pelo utilizador, que vai ser atribuido no objeto existente (respetivos campos)</param>
        /// <returns></returns>
        // POST: Turmas/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Turma aEditar)
        {
            Turma turmaEditada = db.Turmas.FirstOrDefault(t => t.IdTurma == id);

            if (turmaEditada == null)
            {
                return HttpNotFound();
            }

            Curso cursoTurma = db.Cursos.FirstOrDefault(c => c.IdCurso == aEditar.IdCurso);

            if (ModelState.IsValid && cursoTurma != null)
            {
                turmaEditada.IdTurma = aEditar.IdTurma;
                turmaEditada.IdCurso = aEditar.IdCurso;
                turmaEditada.DataInicio = aEditar.DataInicio;
                turmaEditada.DataFim = aEditar.DataFim;
                turmaEditada.Designação = aEditar.Designação;
                turmaEditada.LastUpdate = DateTime.Now;
            }

            try
            {
                // TODO: Add update logic here
                db.SubmitChanges();
                return RedirectToAction("Details", new { id = turmaEditada.IdTurma });
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Turmas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Turma turma = db.Turmas.FirstOrDefault(c => c.IdTurma == id);

            if (turma == null)
            {
                return HttpNotFound();
            }

            Curso curso = db.Cursos.FirstOrDefault(c => c.IdCurso == turma.IdCurso );
            if (curso != null)
            {
                ViewBag.Curso = curso.Designação;
            }

            return View(turma);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // POST: Turmas/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Turma aApagar = db.Turmas.FirstOrDefault(t => t.IdTurma == id);
            if (aApagar == null)
            {
                return HttpNotFound();
            }
            db.Turmas.DeleteOnSubmit(aApagar);

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
