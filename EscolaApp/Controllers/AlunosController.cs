using EscolaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EscolaApp.Controllers
{
    public class AlunosController : Controller
    {
        DataClasses1DataContext db = new DataClasses1DataContext();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: Alunos
        public ActionResult Index()
        {
            return View(db.Alunos);
        }

        /// <summary>
        /// Pedir ao servidor os detalhes de cada aluno
        /// Falta poder criar e editar notas nos detalhes de cada aluno
        /// </summary>
        /// <param name="id">Chave primaria de aluno</param>
        /// <returns></returns>
        // GET: Alunos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Aluno aluno = db.Alunos.FirstOrDefault(a => a.IdAluno == id);
            var mails = from Alunos in db.Alunos
                        join Alunos_Email in db.Alunos_Emails
                        on Alunos.IdAluno equals Alunos_Email.IdAluno
                        join Email in db.Emails
                        on Alunos_Email.IdEmail equals Email.IdEmail
                        select Email;

            //ViewBag.Email = mails.Sele
            return View(aluno);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: Alunos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Alunos/Create
        [HttpPost]
        public ActionResult Create(Aluno novoAluno)
        {
            if (ModelState.IsValid)
            {
                db.Alunos.InsertOnSubmit(novoAluno);
            }
            try
            {
                // TODO: Add insert logic here
                novoAluno.CreateDate = DateTime.Now;
                novoAluno.LastUpdate = DateTime.Now;
                db.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.Erro = e;
                return View();
            }

        }

        // GET: Alunos/Edit/5
        /// <summary>
        /// verificaçao da existencia de um aluno com aquele id
        /// </summary>
        /// <param name="id">id de aluno</param>
        /// <returns></returns>
        public ActionResult Edit(int? id)
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
        
            return View(aluno);
        }

        // POST: Alunos/Edit/5
        /// <summary>
        /// verificação da existencia do aluno
        /// atribuição dos novos dados
        /// alteração na base de dados
        /// </summary>
        /// <param name="id">id de aluno atual</param>
        /// <param name="aEditar">novos atributos do aluno</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(int id, Aluno aEditar)
        {
            Aluno editado = db.Alunos.FirstOrDefault(a => a.IdAluno == id);

            if (editado == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                editado.IdAluno = aEditar.IdAluno;
                editado.Nome = aEditar.Nome;
                editado.Apelido = aEditar.Apelido;
                editado.DataNascimento = aEditar.DataNascimento;
                editado.NIF = aEditar.NIF;
                editado.LastUpdate = DateTime.Now;
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

        // GET: Alunos/Delete/5
        public ActionResult Delete(int? id)
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
            return View(aluno);
        }

        // POST: Alunos/Delete/5
        /// <summary>
        /// verificação da existencia do aluno
        /// remoção do aluno da base de dados
        /// </summary>
        /// <param name="id">id de aluno a apagar</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Aluno aApagar = db.Alunos.FirstOrDefault(a => a.IdAluno == id);
            if (aApagar == null)
            {
                return HttpNotFound();
            }
            db.Alunos.DeleteOnSubmit(aApagar);
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
