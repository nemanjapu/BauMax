using BauMax.Core;
using BauMax.Core.Models;
using BauMax.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace BauMax.Controllers
{
    [RoutePrefix("kontakt")]
    public class ContactController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContactController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Route]
        [OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, Duration = 86400)]
        public ActionResult Index()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SubmitForm(ContactViewModel model)
        {
            //var lead = new Lead()
            //{
            //    Id = new Guid(),
            //    EmailAddress = model.EmailAddress,
            //    FirstName = model.FirstName,
            //    LastName = model.FirstName,
            //    PhoneNumber = model.PhoneNumber,
            //    Note = model.Note,
            //    Date = DateTime.Now
            //};

            //_unitOfWork.Leads.AddNewLead(lead);
            //_unitOfWork.Complete();

            string HostMail = "postmaster@struix.co";
            string HostPassword = "StruiXTeaM12#$";
            string DisplayName = "Info";
            MailMessage eMailMessage = new MailMessage();
            eMailMessage.To.Add("baumaxdoo@gmail.com");
            eMailMessage.From = new MailAddress(HostMail, DisplayName);

            string subject = "Novi kontakt - BauMax DOO";
            string body = "<p>Ime: <b>" + model.FirstName + " " + model.LastName + "</b></p><p>Email: <b>" + model.EmailAddress + "</b></p><p>Telefon: <b>" + model.PhoneNumber + "</b></p><p>Bilješka: <b>" + model.Note + "</b></p>";

            eMailMessage.Subject = subject;
            eMailMessage.Body = body;
            eMailMessage.IsBodyHtml = true;

            using (SmtpClient smtp = new SmtpClient())
            {
                smtp.EnableSsl = false;
                smtp.Host = "mail.struix.co";
                smtp.Port = 8889;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(HostMail, HostPassword);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.SendCompleted += (s, e) => { smtp.Dispose(); };
                smtp.Send(eMailMessage);
            };

            return Content("<b class='m-0'>Hvala na poruci, uskoro ćete biti kontaktirani.</b>", "text/html");
        }
    }
}