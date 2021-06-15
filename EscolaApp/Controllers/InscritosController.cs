using EscolaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EscolaApp.Controllers
{
    public class InscritosController : Controller
    {
        
        DataClasses1DataContext db = new DataClasses1DataContext();
        // GET: Inscritos
        //Este provavelmente nao faz nada nem vai fazer
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        //// GET: Inscritos/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: Inscritos/CreateViaTurma
        //so pode ser acedido através da turma, de forma a adicionar alunos a essa turma
        /// <summary>
        /// Create inscritos, inscreve alunos na turma, acedido através dos detalhes da turma.
        /// </summary>
        /// <param name="idTurma"></param>
        /// <returns></returns>
        public ActionResult CreateViaTurma(int? idTurma)
        {
            if (idTurma == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }


            ViewBag.IdAluno = db.Alunos.Select(a => new SelectListItem()
            {
                Value = a.IdAluno.ToString(),
                Text = a.Nome + " " + a.Apelido,
            });

            Turma turma = db.Turmas.FirstOrDefault(i => i.IdTurma == idTurma);
            ViewBag.Turma = turma;

            //ViewBag.IdTurma = db.Turmas.Select(t => new SelectListItem()
            //{
            //    Value = t.IdTurma.ToString(),
            //    Text = t.Designação,
            //});
            return View();
        }

        // POST: Inscritos/CreateViaTurma
        /// <summary>
        /// Envia para o servidor os alunos adicionados à turma
        /// Verifica, através do checkrepeated se já existe algum registo com o mesmo aluno para a mesma turma, caso nao exista, esse registo é descartado
        /// </summary>
        /// <param name="form">dados introduzidos no formulário pelo user</param>
        /// <param name="inscrito">aluno a inscrever na turma</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateViaTurma(FormCollection form, Inscrito inscrito, int idTurma)
        {

            List<Inscrito> inscritos = new List<Inscrito>();
            string[] IdsAlunos = form["IdAluno"].Split(',');
            for (int i = 0; i < IdsAlunos.Count(); i++)
            {
                var checkRepeated = true;
                inscrito = new Inscrito();

                inscrito.IdAluno = int.Parse(IdsAlunos[i]);
                inscrito.IdTurma = idTurma;
                inscrito.CreateDate = DateTime.Now;
                inscrito.LastUpdate = DateTime.Now;
                checkRepeated = db.Inscritos.Any(ins => ins.IdInscricao != inscrito.IdInscricao && ins.IdTurma == inscrito.IdTurma && ins.IdAluno == inscrito.IdAluno);
                if (!checkRepeated)
                {
                    inscritos.Add(inscrito);
                }
            }

            try
            {

                // TODO: Add insert logic here

                if (ModelState.IsValid  && inscritos.Count > 0)
                {

                    db.Inscritos.InsertAllOnSubmit(inscritos);
                    db.SubmitChanges();
                }
                return RedirectToAction("Details", "Turmas", new {id = idTurma });
            }
            catch (Exception e)
            {

                ViewBag.Erro = e.Message;
                return View();
            }

        }

        //GET:Inscritos/CreateViaAlunos
        /// <summary>
        /// Adicionar alunos a turma, via detalhes do aluno 
        /// </summary>
        /// <param name="idAluno"></param>
        /// <returns></returns>
        public ActionResult CreateViaAlunos(int? idAluno)
        {
            if (idAluno == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }


            ViewBag.IdTurma = db.Turmas.Select(t => new SelectListItem()
            {
                Value = t.IdTurma.ToString(),
                Text = t.Designação
            });

            Aluno aluno = db.Alunos.FirstOrDefault(i => i.IdAluno == idAluno);
            ViewBag.Aluno = aluno;
            //ViewBag.IdTurma = db.Turmas.Select(t => new SelectListItem()
            //{
            //    Value = t.IdTurma.ToString(),
            //    Text = t.Designação,
            //});
            return View();
        }

        //POST:Inscritos/CreateViaAlunos
        /// <summary>
        /// Adicionar alunos a turma, via detalhes do aluno 
        /// </summary>
        /// <param name="inscrito"></param>
        /// <param name="idAluno"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateViaAlunos(Inscrito inscrito, int? idAluno)
        {
            var checkRepeated = true;
            checkRepeated = db.Inscritos.Any(ins => ins.IdInscricao != inscrito.IdInscricao && ins.IdTurma == inscrito.IdTurma && ins.IdAluno == inscrito.IdAluno);
            if (ModelState.IsValid && !checkRepeated)
            {
                inscrito.CreateDate = DateTime.Now;
                inscrito.LastUpdate = DateTime.Now;
                db.Inscritos.InsertOnSubmit(inscrito);
                try
                {
                    db.SubmitChanges();

                    return RedirectToAction("Details", "Alunos", new { id = idAluno });
                }
                catch(Exception e)
                {
                    ViewBag.Erro = e.Message;
                    return View();
                }
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            
        }

        /// <summary>
        /// Edit é apenas acedivel através do aluno!
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Inscritos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Inscrito inscrito = db.Inscritos.FirstOrDefault(i => i.IdInscricao == id);

            if (inscrito == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdTurma = db.Turmas.Select(t => new SelectListItem()
            {
                Value = t.IdTurma.ToString(),
                Text = t.Designação
            });
            return View(inscrito);

        }

        // POST: Inscritos/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Inscrito aEditar)
        {
            Inscrito editado = db.Inscritos.FirstOrDefault(i => i.IdInscricao == id);

            if (editado == null)
            {
                return HttpNotFound();
            }
            var checkRepeated = true;
            checkRepeated = db.Inscritos.Any(ins => ins.IdInscricao != editado.IdInscricao && ins.IdTurma == editado.IdTurma && ins.IdAluno == editado.IdAluno);
            if (ModelState.IsValid && !checkRepeated)
            {
                editado.IdTurma = aEditar.IdTurma;
                editado.LastUpdate = DateTime.Now;
                try
                {
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


        /// <summary>
        /// Faz a pesquisa previa dos alunos que a turma tem.
        /// </summary>
        /// <param name="idTurma"></param>
        /// <returns></returns>
        // GET: Inscritos/Delete/5
        [HttpGet]
        public ActionResult DeleteViaTurma(int? idTurma)
        {
            if (idTurma == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var pesquisa = from Turma in db.Turmas
                           join Inscrito in db.Inscritos
                           on Turma.IdTurma equals Inscrito.IdTurma
                           join Aluno in db.Alunos
                           on Inscrito.IdAluno equals Aluno.IdAluno
                           where Turma.IdTurma == idTurma
                           select Aluno;

            ViewBag.IdAluno = pesquisa.Select(a => new SelectListItem()
            {
                Value = a.IdAluno.ToString(),
                Text = a.Nome + " " + a.Apelido,
            });

            Turma turma = db.Turmas.FirstOrDefault(i => i.IdTurma == idTurma);
            ViewBag.Turma = turma;
            return View();
        }

        // POST: Inscritos/Delete/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idTurma"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteViaTurma(int? idTurma, FormCollection form)
        {
            Inscrito aApagar;

            if (form["IdAluno"] == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            List<Inscrito> listaAapagar = new List<Inscrito>();
            string[] IdsAlunos = form["IdAluno"].Split(',');
            for (int i = 0; i < IdsAlunos.Count(); i++)
            {
                
                var pesquisa = from Inscrito in db.Inscritos
                              where Inscrito.IdAluno == int.Parse(IdsAlunos[i]) && Inscrito.IdTurma == idTurma
                              select Inscrito;
                aApagar = pesquisa.FirstOrDefault();
                listaAapagar.Add(aApagar);
            }

            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid && listaAapagar.Count > 0)
                {

                    db.Inscritos.DeleteAllOnSubmit(listaAapagar);
                    db.SubmitChanges();
                }
                return RedirectToAction("Details", "Turmas", new { id = idTurma });
            }
            catch(Exception e)
            {

                ViewBag.Erro = e.Message;
                return View();
            }
        }
    }
}
