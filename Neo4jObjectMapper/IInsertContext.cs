using Neo4j.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Neo4jObjectMapper
{
    public interface IInsertContext
    {
        Task<IResultSummary> Insert(string cypherQuery);
        Task<IResultSummary> Insert(string cypherQuery, IDictionary<string, object> parameters);
        Task<IResultSummary> InsertNode<TNode>(TNode node);
        Task<IResultSummary> InsertNodes<TNode>(IEnumerable<TNode> node);
        Task<IResultSummary> InsertNodeWithRelation<TNode, TRelation, TNode2>(TNode node, TRelation relation, TNode2 node2);
        Task<IResultSummary> InsertRelation<TRelation>(string cypherQuery, string variableNodeFrom, string variableNodeTo, TRelation relation);
    }
}
