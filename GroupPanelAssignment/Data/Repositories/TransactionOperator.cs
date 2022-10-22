using GroupPanelAssignment.Data.Models;
using GroupPanelAssignment.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupPanelAssignment.Data.Repositories
{
    public class TransactionOperator : ITransactionOperator
    {
        private GroPanDbContext _dbContext;

        public TransactionOperator(GroPanDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IDbContextTransaction BeginTransaction(string transactionName)
        {
            var transaction = _dbContext.Database.BeginTransaction();
            transaction.CreateSavepoint(transactionName);
            return transaction;
        }


        public async Task<KeyValuePair<bool, string>> CommitTransactionAsync(IDbContextTransaction transactionObject)
        {
            await transactionObject.CommitAsync();
            return new KeyValuePair<bool, string>(true, $"Transaction committed!");
        }

        public async Task<KeyValuePair<bool, string>> RollbackTransactionAsync(IDbContextTransaction transactionObject, string transactionName)
        {
            await transactionObject.RollbackToSavepointAsync(transactionName);
            return new KeyValuePair<bool, string>(true, $"{transactionName} rolled back!");
        }

    }
}
