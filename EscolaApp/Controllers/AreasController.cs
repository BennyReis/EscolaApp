using EscolaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EscolaApp.Controllers
{
    public class AreasController : Controller
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        // GET: Area
        public ActionResult Index()
        {
            return View(db.Areas);
        }

        // GET: Area/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Area areaDetalhe = db.Areas.FirstOrDefault(a => a.IdArea == id);
           
            
            return View(areaDetalhe);
        }

        // GET: Area/Create
        public ActionResult Create()
        {
            return View();
        }
        /// <summary>
        /// Criação de uma nova área.
        /// Tendo em atenção o ID, caso não exista deve ganhar o valor 0 + 1
        /// </summary>
        /// <param name="novaArea"></param>
        /// <returns></returns>
        // POST: Area/Create
        [HttpPost]
        public ActionResult Create(Area novaArea)
        {
            if (ModelState.IsValid)
            {
                db.Areas.InsertOnSubmit(novaArea);
            }
            try
            {
                // TODO: Add insert logic here               

                novaArea.CreateDate = DateTime.Now;
                novaArea.LastUpdate = DateTime.Now;
                db.SubmitChanges();
                

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        /// <summary>
        /// Edição de uma área.
        /// O Parametro ID deve ser opcional, para que a app não parta caso não receba nenhum ID, p.ex: URL
        /// HTTP GET é para o utilizador pedir ao servidor qual o objeto a ser alterado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Area/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Area area = db.Areas.FirstOrDefault(a => a.IdArea == id);

            if (area == null)
            {
                return HttpNotFound();
            }
            return View(area);
        }

        /// <summary>
        /// Edição de uma área
        /// HTTP POST, envia as alterações efetuadas pelo utilizador para a database
        /// O parametro Area aAlterar é o objeto passado pelo utilizador através dos formulários
        /// De seguida é alterado no objeto existente todos as suas propriedades alteradas de acordo com o que foi passado pelo utilizador
        /// </summary>
        /// <param name="id">chave primária do objeto que está a ser alterado</param>
        /// <param name="aAlterar">Objeto passado pelo utilizador</param>
        /// <returns></returns>
        // POST: Area/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Area aAlterar)
        {
            Area areaAlterada = db.Areas.FirstOrDefault(a => a.IdArea == id);
            if (areaAlterada == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                areaAlterada.Designação = aAlterar.Designação;
                areaAlterada.Detalhes = aAlterar.Detalhes;
                areaAlterada.LastUpdate = DateTime.Now;
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
        /// Apagar uma área
        /// HTTP GET, para o utilizador pedir ao servidor a área que quer apagar
        /// 
        /// </summary>
        /// <param name="id">Parametro opcional, chave primária do objeto a ser pedido ao servidor, para precaver caso não seja passado nenhum ID é retornado um erro HTTP</param>
        /// <returns></returns>
        // GET: Area/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Area area = db.Areas.FirstOrDefault(a => a.IdArea == id);

            if (area == null)
            {
                return HttpNotFound();
            }

            return View(area);
        }
        /// <summary>
        /// Apagar uma área
        /// HTTP POST, para que o utilizador envie para o servidor qual a área a apagar
        /// </summary>
        /// <param name="id">Parametro com a chave primária do objeto a ser pesquisado</param>
        /// <returns></returns>
        // POST: Area/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Area aApagar = db.Areas.First(a => a.IdArea == id);

            if (aApagar == null)
            {
                return HttpNotFound();
            }

            db.Areas.DeleteOnSubmit(aApagar);

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
