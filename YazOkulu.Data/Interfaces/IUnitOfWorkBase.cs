using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YazOkulu.Data.Interfaces
{
    public interface IUnitOfWorkBase
    {
        void BeginTransaction();
        void CommitTransaction();
        void DisposeTransaction();
        void CommitAndDisposeTransaction();
        void RollBack();
        void StartAuditLog();
    }
}
