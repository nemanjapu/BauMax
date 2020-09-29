using BauMax.Core.Interfaces;
using BauMax.Core.Models;
using BauMax.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BauMax.Core.Repositories
{
    public class LeadsRepository : ILeadsRepository
    {
        private readonly ApplicationDbContext _ctx;

        public LeadsRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public void AddNewLead(Lead lead)
        {
            _ctx.Leads.Add(lead);
        }

        public void DeleteLead(Guid id)
        {
            var lead = GetLeadById(id);
            _ctx.Leads.Remove(lead);
        }

        public IEnumerable<Lead> GetAllLeads()
        {
            return _ctx.Leads.OrderByDescending(l => l.Date);
        }

        public Lead GetLeadById(Guid id)
        {
            return _ctx.Leads.Where(l => l.Id.ToString().Contains(id.ToString())).FirstOrDefault();
        }
    }
}