using BizWizard.Domain.Data.Repository;
using BizWizard.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BizWizard.MVC.Controllers
{
    public class MailMessageController : Controller
    {
        private readonly IMailMessageRepository mymodel;
        
        public MailMessageController(IMailMessageRepository model)
        {
            this.mymodel = model;
        }

        // GET: MailMessages
        public ActionResult Index()
        {
            return View(mymodel.GetAll());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(String name)
        {
            if (!String.IsNullOrEmpty(name))
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        IEnumerable<MailMessage> mailMessages = mymodel.Find(x => x.Name.ToLower().Contains(name.ToLower()));
                        if (mailMessages.Any())
                        {
                            return View(mailMessages);
                        }
                        else
                        {
                            throw new Exception("The name " + name + " was not found. Please try again!");
                        }
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Error = e.Message;
                }
            }
            return View(mymodel.GetAll());
        }

        // GET: MailMessage/Details/5
        public ActionResult View(int id)
        {
            MailMessage mailMessage = mymodel.GetById(id);
            if (mailMessage == null)
            {
                return HttpNotFound();
            }
            return View(mailMessage);
        }
        
        // GET: MailMessage/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }

            var mailMessage = mymodel.GetById(id.Value);

            if (mailMessage == null)
            {
                return HttpNotFound();
            }

            return View( "Delete", mailMessage);
        }
        
        // POST: MailMessage/DeleteConfirmed/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var mailMessageToDelete = mymodel.GetById(id);
            try
            {
                if (ModelState.IsValid)
                {
                    if (!mymodel.Delete(mailMessageToDelete))
                    {
                        throw new Exception();
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
        }
    }
}
