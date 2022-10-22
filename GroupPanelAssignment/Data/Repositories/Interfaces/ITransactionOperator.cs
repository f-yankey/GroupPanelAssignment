using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupPanelAssignment.Data.Repositories.Interfaces
{
    public interface ITransactionOperator
    {
        IDbContextTransaction BeginTransaction(string transactionName);
        Task<KeyValuePair<bool, string>> CommitTransactionAsync(IDbContextTransaction transactionObject);
        Task<KeyValuePair<bool, string>> RollbackTransactionAsync(IDbContextTransaction transactionObject, string transactionName);

    }
}
