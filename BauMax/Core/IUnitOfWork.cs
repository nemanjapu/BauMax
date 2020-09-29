using BauMax.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BauMax.Core
{
    public interface IUnitOfWork
    {
        ILeadsRepository Leads { get; }
        void Complete();
    }
}
