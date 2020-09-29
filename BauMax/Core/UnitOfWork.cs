using BauMax.Core.Interfaces;
using BauMax.Core.Repositories;
using BauMax.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BauMax.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _ctx;

        public ILeadsRepository Leads { get; private set; }

        public UnitOfWork(ApplicationDbContext ctx)
        {
            _ctx = ctx;
            Leads = new LeadsRepository(_ctx);
        }

        public void Complete()
        {
            _ctx.SaveChanges();
        }
    }
}