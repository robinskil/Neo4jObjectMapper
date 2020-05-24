using Neo4j.Driver;

namespace Neo4jObjectMapper
{
    public interface IHelperContext
    {
        T ConvertNodeToT<T>(INode node) where T : class, new();
    }
}
