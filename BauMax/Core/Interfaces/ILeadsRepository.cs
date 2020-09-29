using BauMax.Core.Models;
using System;
using System.Collections.Generic;

namespace BauMax.Core.Interfaces
{
    public interface ILeadsRepository
    {
        IEnumerable<Lead> GetAllLeads();
        Lead GetLeadById(Guid id);
        void AddNewLead(Lead lead);
        void DeleteLead(Guid id);
    }
}
