using BizWizard.Domain.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace BizWizard.MVC.Models.Repositories
{
    public class MailMessageRepository : MailMessageRepositoryBase
    {
        private static readonly String path = Domain.Properties.Resources.MailMessages;

        public MailMessageRepository()
        {
            if (mailMessages == null)
            {
                mailMessages = QueryMailMessages();
            }
        }
        public override IQueryable<MailMessage> QueryMailMessages()
        {
            XElement xelement = XElement.Parse(path);
            IEnumerable<XElement> xmlMailMessages = xelement.Elements();

            // Cast to MailMessage
            List<MailMessage> mailMessages = new List<MailMessage>();
            foreach (var xmlMailMessage in xmlMailMessages)
            {
                MailMessage newMailMessage = new MailMessage
                {
                    Id = Convert.ToInt16(xmlMailMessage.Element("MailMessageId").Value),
                    PublicationId = Convert.ToInt16(xmlMailMessage.Element("PublicationId").Value),
                    Name = xmlMailMessage.Element("Name").Value,
                    ModifiedDate = XmlConvert.ToDateTime(xmlMailMessage.Element("ModifiedDate").Value, XmlDateTimeSerializationMode.Utc),
                    IsSent = (bool)xmlMailMessage.Element("IsSent")
                };
                mailMessages.Add(newMailMessage);
            }
            return mailMessages.AsQueryable();
        }
    }
}
