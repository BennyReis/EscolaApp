using EscolaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EscolaApp.Controllers
{
    public class NotasController : Controller
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        // GET: Notas
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Aluno aluno = db.Alunos.FirstOrDefault(a => a.IdAluno == id);

            if (aluno == null)
            {
                return HttpNotFound();
            }

            var notas = db.Notas.Where(n => n.IdAluno == id);

            //var ufcdsIDs = notas.Select(n => n.UFCD);
            //var ufcds = db.UFCDs;
            return View(notas);
        }

        // GET: Notas/Details/5
        /// <summary>
        /// Detalhes da nota acedida pelo aluno
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Nota nota = db.Notas.FirstOrDefault(n => n.IdNota == id);

            if (nota == null)
            {
                return HttpNotFound();
            }

            return View(nota);
        }

        // GET: Notas/Create
        /// <summary>
        /// Tem de receber o aluno e a turma, para aceder ao curso e apenas apresentar as notas referentes as ufcds desse curso
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateViaAlunos(int? idAluno, int? idTurma)
        {
            
            Aluno aluno = db.Alunos.FirstOrDefault(a => a.IdAluno == idAluno);
            ViewBag.Aluno = aluno;
            var pesquisaUFCDs = from UFCD in db.UFCDs
                                join Referenciai in db.Referenciais
                                on UFCD.IdUFCD equals Referenciai.IdUFCD
                                join Curso in db.Cursos
                                on Referenciai.IdCurso equals Curso.IdCurso
                                join Turma in db.Turmas
                                on Curso.IdCurso equals Turma.IdCurso
                                where Turma.IdTurma == idTurma
                                select UFCD;

            ViewBag.IdUFCD = pesquisaUFCDs.Select(a => new SelectListItem()
            {
                Value = a.IdUFCD.ToString(),
                Text = a.Designação,
            });

            return View();
        }

        // POST: Notas/Create
        [HttpPost]
        public ActionResult CreateViaAlunos(Nota novaNota)
        {
            Aluno aluno = db.Alunos.FirstOrDefault(a=>a.IdAluno == novaNota.IdAluno);
            UFCD ufcd = db.UFCDs.FirstOrDefault(u => u.IdUFCD == novaNota.IdUFCD);

            
            if (ufcd == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            //verificar se a nota já existe, exceto a propria, para evitar ter dois ou mais registos do mesmo aluno para a mesma ufcd
            var duplicatedUfcds = db.Notas.Any(nota => nota.IdNota != novaNota.IdNota && ufcd.IdUFCD == nota.IdUFCD && aluno.IdAluno == nota.IdAluno);

            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid && !duplicatedUfcds)
                {
                    db.Notas.InsertOnSubmit(novaNota);
                    novaNota.CreateDate = DateTime.Now;
                    novaNota.LastUpdate = DateTime.Now;
                    db.SubmitChanges();
                }

                return RedirectToAction("Details", "Alunos", new { id = novaNota.IdAluno});
            }
            catch (Exception e)
            {
                ViewBag.Erro = e;
                return View();
            }
        }

        // GET: Notas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Nota nota = db.Notas.FirstOrDefault(c => c.IdNota == id);

            if (nota == null)
            {
                return HttpNotFound();
            }

            //passar para a view o nome completo do aluno
            ViewBag.IdAluno = db.Alunos.Select(a => new SelectListItem()
            {
                Value = a.IdAluno.ToString(),
                Text = a.Nome + " " + a.Apelido,
            });
            //ViewBag.IdAluno = new SelectList(db.Alunos.OrderBy(a => a.ToString()), "IdAluno", "Nome");
            ViewBag.IdUFCD = new SelectList(db.UFCDs.OrderBy(u => u.Designação), "IdUFCD", "Designação");
            return View(nota);
        }

        // POST: Notas/Edit/5
        /// <summary>
        /// Inicialmente o registo podia trocar de aluno e UFCD, mas isso nao faz sentido
        /// </summary>
        /// <param name="id"></param>
        /// <param name="aEditar"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(int id, Nota aEditar)
        {

            Nota notaEditada = db.Notas.FirstOrDefault(c => c.IdNota == id);

            if (notaEditada == null)
            {
                return HttpNotFound();
            }
            ;

            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    notaEditada.Nota1 = aEditar.Nota1;
                    notaEditada.LastUpdate = DateTime.Now;
                    db.SubmitChanges();
                }
                return RedirectToAction("Details", "Alunos", new { id = notaEditada.IdAluno });
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// Apagar nota? vamos ver se apaga
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Notas/Delete/5
        public ActionResult Delete(int? id)
        {
            Nota aApagar = db.Notas.FirstOrDefault(n => n.IdNota == id);
            return View(aApagar);
        }

        // POST: Notas/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Nota aApagar = db.Notas.FirstOrDefault(n => n.IdNota == id);
            if (aApagar == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add delete logic here
                    db.Notas.DeleteOnSubmit(aApagar);
                    db.SubmitChanges();
                    return RedirectToAction("Details", "Alunos", new { id = aApagar.IdAluno });
                }
                catch
                {
                    return View();
                }
            }

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
