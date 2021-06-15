using EscolaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EscolaApp.Controllers
{
    public class Alunos_TelefonesController : Controller
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        // GET: Alunos_Emails
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        // GET: Alunos_Emails/Details/5
        public ActionResult Details(int? id)
        {

            return View();
        }

        // GET: Alunos_Emails/Create
        /// <summary>
        /// Executado a partir dos detalhes do aluno.
        /// 
        /// </summary>
        /// <param name="idAluno"></param>
        /// <returns></returns>
        public ActionResult Create(int? idAluno)
        {
            if (idAluno == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Alunos_Telefone contacto = new Alunos_Telefone();
            contacto.Aluno = db.Alunos.FirstOrDefault(a => a.IdAluno == idAluno);

            Telefone acriar = new Telefone();
            contacto.Telefone = acriar;

            return View(contacto);
        }

        // POST: Alunos_Emails/Create
        /// <summary>
        /// Recebe o email, cria o registo da relação aluno email e cria tambem o registo de email
        /// </summary>
        /// <param name="idAluno"></param>
        /// <param name="telefone"></param>
        /// <param name="acriar"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(int idAluno, Alunos_Telefone telefone, Telefone acriar, FormCollection collection)
        {
            if (collection == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            acriar.CreateDate = DateTime.Now;
            acriar.LastUpdate = DateTime.Now;
            acriar.Telefone1 = collection["Telefone.Telefone1"];

            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    db.Telefones.InsertOnSubmit(acriar);
                    db.SubmitChanges();

                    telefone.IdAluno = idAluno;
                    telefone.IdTelefone = acriar.IdTelefone;
                    telefone.CreateDate = DateTime.Now;
                    telefone.LastUpdate = DateTime.Now;
                    db.Alunos_Telefones.InsertOnSubmit(telefone);
                    db.SubmitChanges();
                }
                return RedirectToAction("Details", "Alunos", new { id = idAluno });
            }
            catch
            {
                return View();
            }
        }

        // GET: Alunos_Emails/Edit/5
        public ActionResult Edit(int? idAlunosTel, Telefone aAlterar)
        {
            if (idAlunosTel == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Alunos_Telefone telefone = db.Alunos_Telefones.FirstOrDefault(t => t.IdContactoTelefone == idAlunosTel);

            if (telefone == null)
            {
                return HttpNotFound();
            }

            //Email alterado = email.Email;

            return View(telefone);
        }

        // POST: Alunos_Emails/Edit/5
        [HttpPost]
        public ActionResult Edit(int idAlunosTel, Alunos_Telefone aEditar)
        {
            Alunos_Telefone editado = db.Alunos_Telefones.FirstOrDefault(e => e.IdContactoTelefone == idAlunosTel);
            Telefone nrTel = db.Telefones.FirstOrDefault(t => t.IdTelefone == editado.IdTelefone);
            if (editado == null || nrTel == null)
            {
                return HttpNotFound();
            }
            if (editado.Telefone.Telefone1 == aEditar.Telefone.Telefone1)
            {
                return RedirectToAction("Details", "Alunos", new { id = editado.IdAluno });
            }
            var checktelefone = true;
            checktelefone = db.Telefones.Any(e => e.IdTelefone != editado.IdTelefone && e.Telefone1 == aEditar.Telefone.Telefone1);
            if (ModelState.IsValid && !checktelefone)
            {
                try
                {
                    // TODO: Add update logic here
                    nrTel.Telefone1 = aEditar.Telefone.Telefone1;
                    nrTel.LastUpdate = DateTime.Now;
                    editado.LastUpdate = DateTime.Now;
                    db.SubmitChanges();

                    return RedirectToAction("Details", "Alunos", new { id = editado.IdAluno });
                }
                catch (Exception e)
                {
                    ViewBag.Erro = e.Message;
                    return View();
                }
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

        }

        // GET: Alunos_Emails/Delete/5
        public ActionResult Delete(int? idAlunosTel)
        {
            if (idAlunosTel == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Alunos_Telefone nrTel = db.Alunos_Telefones.FirstOrDefault(e => e.IdContactoTelefone == idAlunosTel);

            if (nrTel == null)
            {
                return HttpNotFound();
            }

            //Email alterado = email.Email;

            return View(nrTel);
        }

        // POST: Alunos_Emails/Delete/5
        [HttpPost]
        public ActionResult Delete(int idAlunosTel)
        {
            Alunos_Telefone apagado = db.Alunos_Telefones.FirstOrDefault(e => e.IdContactoTelefone == idAlunosTel);
            Telefone nrTel = db.Telefones.FirstOrDefault(t => t.IdTelefone == apagado.IdTelefone);

            if (apagado == null || nrTel == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add delete logic here
                    db.Telefones.DeleteOnSubmit(nrTel);
                    db.Alunos_Telefones.DeleteOnSubmit(apagado);
                    db.SubmitChanges();
                    return RedirectToAction("Details", "Alunos", new { id = apagado.IdAluno });
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
