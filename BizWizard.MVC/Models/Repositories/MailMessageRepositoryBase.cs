using BizWizard.Domain.Data.Repository;
using BizWizard.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace BizWizard.MVC.Models.Repositories
{
    public abstract class MailMessageRepositoryBase : IMailMessageRepository
    {
        public static IQueryable<MailMessage> mailMessages;

        public abstract IQueryable<MailMessage> QueryMailMessages();
        
        public bool Delete(MailMessage mailMessage)
        {
            List<MailMessage> MailMessagesList = mailMessages.ToList<MailMessage>();
            if (MailMessagesList.Remove(mailMessage))
            {
                mailMessages = MailMessagesList.AsQueryable();
                return true;
            }
            return false;
        }

        public IEnumerable<MailMessage> Find(Expression<Func<MailMessage, bool>> predicate)
        {
            return mailMessages.Where(predicate);
        }

        public IEnumerable<MailMessage> GetAll()
        {
            return mailMessages.Take(count: 100).ToList();
        }
        
        public MailMessage GetById(int id)
        {
            return mailMessages.SingleOrDefault(item => item.Id == id);
        }
    }
}