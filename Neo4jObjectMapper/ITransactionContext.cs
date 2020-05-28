using System.Threading.Tasks;

namespace Neo4jObjectMapper
{
    public interface ITransactionContext : IQueryContext, IInsertContext, IHelperContext, IExecuteContext
    {
        Task CommitTransaction();
        Task RollbackTransaction();
    }
}
