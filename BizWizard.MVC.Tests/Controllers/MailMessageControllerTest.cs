using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BizWizard.MVC.Controllers;
using System.Web.Mvc;
using BizWizard.Domain.Data.Repository;
using System.Collections.Generic;
using BizWizard.Domain.Entity;
using BizWizard.MVC.Models.Repositories;
using System.Linq;
using System.Net;
using System.Web;
using System.IO;
using System.Web.Routing;

namespace BizWizard.MVC.Tests.Controllers
{
    [TestClass]
    public class MailMessageControllerTest
    {
        private IMailMessageRepository mymodel;
        private MailMessageController controller;
        private MailMessage mailMessageToTest;
        
        [TestInitialize]
        public void Initalize()
        {
            this.mymodel = new MailMessageRepository();
            this.controller = new MailMessageController(this.mymodel);
            this.mailMessageToTest = this.GetMailMessageIdToTest();
        }

        [TestMethod]
        public void TestDelete_WithExistingMailMessage()
        {
            // Assert
            // Returns mailmessage
            Assert.IsInstanceOfType(mymodel.GetById(this.mailMessageToTest.Id), typeof(MailMessage));
            
            // Act
            ViewResult result = (ViewResult)controller.Delete(this.mailMessageToTest.Id);
            
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Delete", result.ViewName);
            Assert.IsNotNull(result.Model);
            Assert.IsInstanceOfType(result.Model, typeof(MailMessage));
            Assert.AreEqual(mailMessageToTest, result.Model);
        }

        [TestMethod]
        public void TestDeleteConfirmed_WithExistingMailMessage()
        {
            ActionResult result = controller.DeleteConfirmed(this.mailMessageToTest.Id);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            RedirectToRouteResult routeResult = result as RedirectToRouteResult;
            Assert.AreEqual(routeResult.RouteValues["action"], "Index");
        }

        [TestMethod]
        public void TestDeleteWithNotExistingMailMessage_ReturnNotFound_404()
        {
            // Assert
            // Returns mailmessage
            Assert.IsInstanceOfType(mymodel.GetById(this.mailMessageToTest.Id), typeof(MailMessage));

            // Arrange
            controller.DeleteConfirmed(this.mailMessageToTest.Id);

            // Assert
            // Mailmessage is gone
            Assert.AreEqual(null, mymodel.GetById(this.mailMessageToTest.Id));

            // Act
            var result = controller.Delete(this.mailMessageToTest.Id) as HttpStatusCodeResult;

            // Assert
            // The action method returned an action result
            Assert.IsNotNull(result);
            // The action result is an HttpStatusCodeResult object
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
            // The HTTP code is 404 (not found)
            Assert.AreEqual(404, result.StatusCode);
        }

        [TestMethod]
        public void TestDeleteConfirmedWithNotExistingMailMessage_ReturnNotFound_404()
        {
            // Assert
            // Returns mailmessage
            Assert.IsInstanceOfType(mymodel.GetById(this.mailMessageToTest.Id), typeof(MailMessage));

            // Arrange
            controller.DeleteConfirmed(this.mailMessageToTest.Id);

            // Assert
            // Mailmessage is gone
            Assert.AreEqual(null, mymodel.GetById(this.mailMessageToTest.Id));

            // Act
            var result = controller.DeleteConfirmed(this.mailMessageToTest.Id) as HttpStatusCodeResult;

            // Assert
            // The action method returned an action result
            Assert.IsNotNull(result);
            // The action result is an HttpStatusCodeResult object
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
            // The HTTP code is 404 (not found)
            Assert.AreEqual(404, result.StatusCode);
        }

        private MailMessage GetMailMessageIdToTest()
        {
            List<MailMessage> mailMessages = this.mymodel.GetAll().ToList();
            Random random = new Random();
            int myRandom = random.Next(0, mailMessages.Count - 1);
            return mailMessages[myRandom];
        }
    }
}
