using EscolaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EscolaApp.Controllers
{
    public class Alunos_EmailsController : Controller
    {
        DataClasses1DataContext db = new DataClasses1DataContext();

        // GET: Alunos_Emails/Create
        /// <summary>
        /// Executado a partir dos detalhes do aluno. 
        /// É criado ja um objeto email de modo a fazer a relação com o aluno na view, sem a necessidade de viewbags ---> Pode ser uma melhoria noutros controladores
        /// <param name="idAluno"></param>
        /// <returns></returns>
        public ActionResult Create(int? idAluno)
        {
            if (idAluno == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Alunos_Email email = new Alunos_Email();
            email.Aluno = db.Alunos.FirstOrDefault(a => a.IdAluno == idAluno);

            Email acriar = new Email();
            email.Email = acriar;

            return View(email);
        }

        // POST: Alunos_Emails/Create
        /// <summary>
        /// Recebe o email, cria o registo da relação aluno email e cria tambem o registo de email
        /// </summary>
        /// <param name="idAluno"></param>
        /// <param name="email"></param>
        /// <param name="acriar"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(int idAluno, Alunos_Email email, Email acriar, FormCollection collection)
        {
            if (collection == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            acriar.CreateDate = DateTime.Now;
            acriar.LastUpdate = DateTime.Now;
            acriar.Email1 = collection["Email.Email1"];

            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    db.Emails.InsertOnSubmit(acriar);
                    db.SubmitChanges();

                    email.IdAluno = idAluno;
                    email.IdEmail = acriar.IdEmail;
                    email.CreateDate = DateTime.Now;
                    email.LastUpdate = DateTime.Now;
                    db.Alunos_Emails.InsertOnSubmit(email);
                    db.SubmitChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Alunos_Emails/Edit/5
        /// <summary>
        /// Edição do registo mail a partir da relação mail - aluno.
        /// Passagem para a view da relação aluno-email atual
        /// </summary>
        /// <param name="idAlunosMail">relação aluno-email</param>
        /// <returns></returns>
        public ActionResult Edit(int? idAlunosMail)
        {
            if (idAlunosMail == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Alunos_Email email = db.Alunos_Emails.FirstOrDefault(e => e.IdContactoMail == idAlunosMail);

            if (email == null)
            {
                return HttpNotFound();
            }

            //Email alterado = email.Email;

            return View(email);
        }

        // POST: Alunos_Emails/Edit/5
        /// <summary>
        /// Edição do registo mail a partir da relação mail - aluno.
        /// Verificação do conteudo do mail de forma a nao existirem mails repetidos
        /// </summary>
        /// <param name="idAlunosMail">relação aluno-email</param>
        /// <param name="aEditar">relação passada pela view, com o email novo</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(int idAlunosMail, Alunos_Email aEditar)
        {
            Alunos_Email editado = db.Alunos_Emails.FirstOrDefault(e => e.IdContactoMail == idAlunosMail);
            Email email = db.Emails.FirstOrDefault(e => e.IdEmail == editado.IdEmail);
            if (editado == null || email == null)
            {
                return HttpNotFound();
            }
            if (editado.Email.Email1 == aEditar.Email.Email1)
            {
                return RedirectToAction("Details", "Alunos", new { id = editado.IdAluno });
            }
            var checkemail = true;
            checkemail = db.Emails.Any(e => e.IdEmail != editado.IdEmail && e.Email1 == aEditar.Email.Email1);
            if (ModelState.IsValid && !checkemail)
            {
                try
                {
                    // TODO: Add update logic here
                    email.Email1 = aEditar.Email.Email1;
                    email.LastUpdate = DateTime.Now;
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
        public ActionResult Delete(int? idAlunosMail)
        {
            if (idAlunosMail == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Alunos_Email email = db.Alunos_Emails.FirstOrDefault(e => e.IdContactoMail == idAlunosMail);

            if (email == null)
            {
                return HttpNotFound();
            }

            //Email alterado = email.Email;

            return View(email);
        }

        // POST: Alunos_Emails/Delete/5
        /// <summary>
        ///  verificação dos dados, remoção da base de dados do mail e da relação aluno-mail
        /// </summary>
        /// <param name="idAlunosMail">relação aluno-email</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int idAlunosMail)
        {
            Alunos_Email apagado = db.Alunos_Emails.FirstOrDefault(e => e.IdContactoMail == idAlunosMail);
            Email email = db.Emails.FirstOrDefault(e => e.IdEmail == apagado.IdEmail);

            if (apagado == null || email == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add delete logic here
                    db.Emails.DeleteOnSubmit(email);
                    db.Alunos_Emails.DeleteOnSubmit(apagado);
                    db.SubmitChanges();
                    return RedirectToAction("Index");
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
