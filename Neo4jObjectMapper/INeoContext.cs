using Neo4j.Driver;
using System;
using System.Threading.Tasks;

namespace Neo4jObjectMapper
{
    public interface INeoContext : IQueryContext, IInsertContext, IHelperContext , IExecuteContext
    {
        Task UseTransaction(Action<ITransactionContext> transactionBody);
        Task ReadTransaction(Action<IAsyncTransaction> transactionFunctionBlock);
        Task InsertWithWriteTransaction(Action<IAsyncTransaction> transactionFunctionBlock);
    }
}
