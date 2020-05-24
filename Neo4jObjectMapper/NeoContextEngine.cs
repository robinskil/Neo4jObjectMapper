using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Neo4jObjectMapper
{
    public class NeoContextEngine
    {
        private readonly Dictionary<Type, Dictionary<string, MethodInfo>> cachedSettersForType;
        private readonly Dictionary<Type, Dictionary<string, MethodInfo>> cachedGettersForType;

        public NeoContextEngine()
        {
            cachedSettersForType = new Dictionary<Type, Dictionary<string, MethodInfo>>();
            cachedGettersForType = new Dictionary<Type, Dictionary<string, MethodInfo>>();
        }

        private Dictionary<string, MethodInfo> GetSetProperties(Type type)
        {
            if (!cachedSettersForType.ContainsKey(type))
            {
                var properties = type.GetProperties().Where(p => p.CanWrite);
                var propertiesAndSetters = new Dictionary<string, MethodInfo>();
                foreach (var property in properties)
                {
                    var setter = property.GetSetMethod();
                    if (setter != null)
                    {
                        propertiesAndSetters.Add(property.Name.ToLower(), setter);
                    }
                }
                cachedSettersForType.Add(type, propertiesAndSetters);
            }
            return cachedSettersForType[type];
        }

        private Dictionary<string,MethodInfo> GetGetProperties(Type type)
        {
            if (!cachedGettersForType.ContainsKey(type))
            {
                var properties = type.GetProperties().Where(p => p.CanRead && p.PropertyType.IsValueType || p.PropertyType == typeof(string));
                var propertiesAndGetters = new Dictionary<string, MethodInfo>();
                foreach (var property in properties)
                {
                    var getter = property.GetGetMethod();
                    if (getter != null)
                    {
                        propertiesAndGetters.Add(property.Name, getter);
                    }
                }
                cachedGettersForType.Add(type, propertiesAndGetters);
            }
            return cachedGettersForType[type];
        }

        internal T ConvertNodeToSingleObject<T>(IRecord record) where T : class, new()
        {
            if (record.Values.Count != 1)
            {
                throw new NeoContextException("More than 1 variable was returned in a single row.");
            }
            var objectNode = record.Values.First().Value as INode;
            return CreateTFromNode<T>(objectNode);
        }

        internal T CreateTFromNode<T>(INode node) where T : class, new()
        {
            T result = new T();
            var setters = GetSetProperties(typeof(T));
            foreach (var nodeProperty in node.Properties)
            {
                if (setters.ContainsKey(nodeProperty.Key.ToLower()))
                {
                    setters[nodeProperty.Key.ToLower()].Invoke(result, new[] { nodeProperty.Value });
                }
            }
            return result;
        }

        internal TResult ConvertNodeToObjectsAndMap<TResult, TInclude>(IEnumerable<IRecord> records, Func<TResult, TInclude, TResult> mapFunc)
            where TResult : class, new()
            where TInclude : class, new()
        {
            TResult result = new TResult();
            foreach (var record in records)
            {
                TResult res = null;
                TInclude include = null;
                foreach (var recordValue in record.Values)
                {
                    var node = recordValue.Value as INode;
                    if (node.Labels.First().ToLower() == typeof(TResult).Name.ToLower())
                    {
                        res = CreateTFromNode<TResult>(node);
                    }
                    else if (node.Labels.First().ToLower() == typeof(TInclude).Name.ToLower())
                    {
                        include = CreateTFromNode<TInclude>(node);
                    }
                    else
                    {
                        throw new NeoContextException("Unrecognized node inside the result of the query.");
                    }
                }
                result = mapFunc(res, include);
            }
            return result;
        }
        internal TResult ConvertNodeToObjectsAndMap<TResult, TInclude, TInclude2>(List<IRecord> records, Func<TResult, TInclude, TInclude2, TResult> mapFunc)
            where TResult : class, new()
            where TInclude : class, new()
            where TInclude2 : class, new()
        {
            TResult result = new TResult();
            foreach (var record in records)
            {
                TResult res = null;
                TInclude include = null;
                TInclude2 include2 = null;
                foreach (var recordValue in record.Values)
                {
                    var node = recordValue.Value as INode;
                    if (node.Labels.First().ToLower() == typeof(TResult).Name.ToLower())
                    {
                        res = CreateTFromNode<TResult>(node);
                    }
                    else if (node.Labels.First().ToLower() == typeof(TInclude).Name.ToLower())
                    {
                        include = CreateTFromNode<TInclude>(node);
                    }
                    else if (node.Labels.First().ToLower() == typeof(TInclude2).Name.ToLower())
                    {
                        include2 = CreateTFromNode<TInclude2>(node);
                    }
                    else
                    {
                        throw new NeoContextException("Unrecognized node inside the result of the query.");
                    }
                }
                result = mapFunc(res, include, include2);
            }
            return result;
        }
        internal TResult ConvertNodeToObjectsAndMap<TResult, TInclude, TInclude2, TInclude3>(List<IRecord> records, Func<TResult, TInclude, TInclude2, TInclude3, TResult> mapFunc)
            where TResult : class, new()
            where TInclude : class, new()
            where TInclude2 : class, new()
            where TInclude3 : class, new()
        {
            TResult result = new TResult();
            foreach (var record in records)
            {
                TResult res = null;
                TInclude include = null;
                TInclude2 include2 = null;
                TInclude3 include3 = null;
                foreach (var recordValue in record.Values)
                {
                    var node = recordValue.Value as INode;
                    if (node.Labels.First().ToLower() == typeof(TResult).Name.ToLower())
                    {
                        res = CreateTFromNode<TResult>(node);
                    }
                    else if (node.Labels.First().ToLower() == typeof(TInclude).Name.ToLower())
                    {
                        include = CreateTFromNode<TInclude>(node);
                    }
                    else if (node.Labels.First().ToLower() == typeof(TInclude2).Name.ToLower())
                    {
                        include2 = CreateTFromNode<TInclude2>(node);
                    }
                    else if (node.Labels.First().ToLower() == typeof(TInclude3).Name.ToLower())
                    {
                        include3 = CreateTFromNode<TInclude3>(node);
                    }
                    else
                    {
                        throw new NeoContextException("Unrecognized node inside the result of the query.");
                    }
                }
                result = mapFunc(res, include, include2, include3);
            }
            return result;
        }
        internal TResult ConvertNodeToObjectsAndMap<TResult, TInclude, TInclude2, TInclude3, TInclude4>(List<IRecord> records, Func<TResult, TInclude, TInclude2, TInclude3, TInclude4, TResult> mapFunc)
            where TResult : class, new()
            where TInclude : class, new()
            where TInclude2 : class, new()
            where TInclude3 : class, new()
            where TInclude4 : class, new()
        {
            TResult result = new TResult();
            foreach (var record in records)
            {
                TResult res = null;
                TInclude include = null;
                TInclude2 include2 = null;
                TInclude3 include3 = null;
                TInclude4 include4 = null;
                foreach (var recordValue in record.Values)
                {
                    var node = recordValue.Value as INode;
                    if (node.Labels.First().ToLower() == typeof(TResult).Name.ToLower())
                    {
                        res = CreateTFromNode<TResult>(node);
                    }
                    else if (node.Labels.First().ToLower() == typeof(TInclude).Name.ToLower())
                    {
                        include = CreateTFromNode<TInclude>(node);
                    }
                    else if (node.Labels.First().ToLower() == typeof(TInclude2).Name.ToLower())
                    {
                        include2 = CreateTFromNode<TInclude2>(node);
                    }
                    else if (node.Labels.First().ToLower() == typeof(TInclude3).Name.ToLower())
                    {
                        include3 = CreateTFromNode<TInclude3>(node);
                    }
                    else if (node.Labels.First().ToLower() == typeof(TInclude4).Name.ToLower())
                    {
                        include4 = CreateTFromNode<TInclude4>(node);
                    }
                    else
                    {
                        throw new NeoContextException("Unrecognized node inside the result of the query.");
                    }
                }
                result = mapFunc(res, include, include2, include3, include4);
            }
            return result;
        }

        internal IEnumerable<TResult> ConvertNodeToObjectsAndMap<TResult, TInclude>(List<IRecord> records, Func<TResult, TInclude, IEnumerable<TResult>> mapFunc)
            where TResult : class, new()
            where TInclude : class, new()
        {
            IEnumerable<TResult> result = default;
            foreach (var record in records)
            {
                TResult res = null;
                TInclude include = null;
                foreach (var recordValue in record.Values)
                {
                    var node = recordValue.Value as INode;
                    if (node.Labels.First().ToLower() == typeof(TResult).Name.ToLower())
                    {
                        res = CreateTFromNode<TResult>(node);
                    }
                    else if (node.Labels.First().ToLower() == typeof(TInclude).Name.ToLower())
                    {
                        include = CreateTFromNode<TInclude>(node);
                    }
                    else
                    {
                        throw new NeoContextException("Unrecognized node inside the result of the query.");
                    }
                }
                result = mapFunc(res, include);
            }
            return result;
        }
        internal IEnumerable<TResult> ConvertNodeToObjectsAndMap<TResult, TInclude, TInclude2>(List<IRecord> records, Func<TResult, TInclude, TInclude2, IEnumerable<TResult>> mapFunc)
            where TResult : class, new()
            where TInclude : class, new()
            where TInclude2 : class, new()
        {
            IEnumerable<TResult> result = default;
            foreach (var record in records)
            {
                TResult res = null;
                TInclude include = null;
                TInclude2 include2 = null;
                foreach (var recordValue in record.Values)
                {
                    var node = recordValue.Value as INode;
                    if (node.Labels.First().ToLower() == typeof(TResult).Name.ToLower())
                    {
                        res = CreateTFromNode<TResult>(node);
                    }
                    else if (node.Labels.First().ToLower() == typeof(TInclude).Name.ToLower())
                    {
                        include = CreateTFromNode<TInclude>(node);
                    }
                    else if (node.Labels.First().ToLower() == typeof(TInclude2).Name.ToLower())
                    {
                        include2 = CreateTFromNode<TInclude2>(node);
                    }
                    else
                    {
                        throw new NeoContextException("Unrecognized node inside the result of the query.");
                    }
                }
                result = mapFunc(res, include, include2);
            }
            return result;
        }
        internal IEnumerable<TResult> ConvertNodeToObjectsAndMap<TResult, TInclude, TInclude2, TInclude3>(List<IRecord> records, Func<TResult, TInclude, TInclude2, TInclude3, IEnumerable<TResult>> mapFunc)
            where TResult : class, new()
            where TInclude : class, new()
            where TInclude2 : class, new()
            where TInclude3 : class, new()
        {
            IEnumerable<TResult> result = default;
            foreach (var record in records)
            {
                TResult res = null;
                TInclude include = null;
                TInclude2 include2 = null;
                TInclude3 include3 = null;
                foreach (var recordValue in record.Values)
                {
                    var node = recordValue.Value as INode;
                    if (node.Labels.First().ToLower() == typeof(TResult).Name.ToLower())
                    {
                        res = CreateTFromNode<TResult>(node);
                    }
                    else if (node.Labels.First().ToLower() == typeof(TInclude).Name.ToLower())
                    {
                        include = CreateTFromNode<TInclude>(node);
                    }
                    else if (node.Labels.First().ToLower() == typeof(TInclude2).Name.ToLower())
                    {
                        include2 = CreateTFromNode<TInclude2>(node);
                    }
                    else if (node.Labels.First().ToLower() == typeof(TInclude3).Name.ToLower())
                    {
                        include3 = CreateTFromNode<TInclude3>(node);
                    }
                    else
                    {
                        throw new NeoContextException("Unrecognized node inside the result of the query.");
                    }
                }
                result = mapFunc(res, include, include2, include3);
            }
            return result;
        }
        internal IEnumerable<TResult> ConvertNodeToObjectsAndMap<TResult, TInclude, TInclude2, TInclude3, TInclude4>(List<IRecord> records, Func<TResult, TInclude, TInclude2, TInclude3, TInclude4, IEnumerable<TResult>> mapFunc)
            where TResult : class, new()
            where TInclude : class, new()
            where TInclude2 : class, new()
            where TInclude3 : class, new()
            where TInclude4 : class, new()
        {
            IEnumerable<TResult> result = default;
            foreach (var record in records)
            {
                TResult res = null;
                TInclude include = null;
                TInclude2 include2 = null;
                TInclude3 include3 = null;
                TInclude4 include4 = null;
                foreach (var recordValue in record.Values)
                {
                    var node = recordValue.Value as INode;
                    if (node.Labels.First().ToLower() == typeof(TResult).Name.ToLower())
                    {
                        res = CreateTFromNode<TResult>(node);
                    }
                    else if (node.Labels.First().ToLower() == typeof(TInclude).Name.ToLower())
                    {
                        include = CreateTFromNode<TInclude>(node);
                    }
                    else if (node.Labels.First().ToLower() == typeof(TInclude2).Name.ToLower())
                    {
                        include2 = CreateTFromNode<TInclude2>(node);
                    }
                    else if (node.Labels.First().ToLower() == typeof(TInclude3).Name.ToLower())
                    {
                        include3 = CreateTFromNode<TInclude3>(node);
                    }
                    else if (node.Labels.First().ToLower() == typeof(TInclude4).Name.ToLower())
                    {
                        include4 = CreateTFromNode<TInclude4>(node);
                    }
                    else
                    {
                        throw new NeoContextException("Unrecognized node inside the result of the query.");
                    }
                }
                result = mapFunc(res, include, include2, include3, include4);
            }
            return result;
        }

        internal NOMDataRecord ConvertRecordToNeoRecord(IRecord record)
        {
            Dictionary<string, IReadOnlyDictionary<string, object>> nodes = new Dictionary<string, IReadOnlyDictionary<string, object>>();
            foreach (var node in record.Values)
            {
                INode values = node.Value as INode;
                nodes.Add(node.Key, values.Properties);
            }
            return new NOMDataRecord(nodes);
        }

        internal Query CreateInsertQuery<TNode>(TNode entity)
        {
            var properties = GetGetProperties(typeof(TNode));
            var query = BuildNodeQuery<TNode>(properties.Keys.ToArray());
            return new Query(query,BuildParameters(properties,entity));
        }
        internal Query CreateInsertQuery<TNode>(IEnumerable<TNode> entities)
        {
            var queryStringBuilder = new StringBuilder();
            var properties = GetGetProperties(typeof(TNode));
            var propertyArray = properties.Keys.ToArray();
            var entityArray = entities.ToArray();
            List<KeyValuePair<string, object>> parameterList = new List<KeyValuePair<string, object>>();
            for (int i = 0; i < entityArray.Length; i++)
            {
                queryStringBuilder.AppendLine(BuildNodeQuery<TNode>(propertyArray, $"entity{i}"));
                parameterList.AddRange(BuildParameters(properties, entityArray[i], $"entity{i}"));
            }
            var parameterDic = new Dictionary<string, object>();
            foreach (var kv in parameterList)
            {
                parameterDic.Add(kv.Key, kv.Value);
            }
            return new Query(queryStringBuilder.ToString(), new Dictionary<string,object>(parameterDic));
        }
        internal Query CreateInsertQuery<TNode, TRelation, TNode2>(TNode node, TRelation relation, TNode2 node2)
        {
            var queryBuilder = new StringBuilder();
            var propertiesNode1 = GetGetProperties(typeof(TNode));
            var propertiesRel1 = GetGetProperties(typeof(TRelation));
            var propertiesNode2 = GetGetProperties(typeof(TNode2));
            queryBuilder.AppendLine(BuildNodeQuery<TNode>(propertiesNode1.Keys.ToArray(), "node1prop", "node1"));
            queryBuilder.AppendLine(BuildNodeQuery<TNode2>(propertiesNode2.Keys.ToArray(), "node2prop", "node2"));
            queryBuilder.AppendLine(BuildRelationQuery<TRelation>(propertiesRel1.Keys.ToArray(), "node1", "node2","relProp"));
            List<KeyValuePair<string, object>> parameterList = new List<KeyValuePair<string, object>>();
            parameterList.AddRange(BuildParameters(propertiesNode1, node, "node1prop"));
            parameterList.AddRange(BuildParameters(propertiesNode2, node2, "node2prop"));
            parameterList.AddRange(BuildParameters(propertiesRel1, relation, "relProp"));
            var parameterDic = new Dictionary<string, object>();
            foreach (var kv in parameterList)
            {
                parameterDic.Add(kv.Key, kv.Value);
            }            
            return new Query(queryBuilder.ToString(), new Dictionary<string, object>(parameterDic));
        }
        internal Query CreateInsertRelationQuery<TRelation>(string cypherMatchQuery, string variableNodeFrom, string variableNodeTo, TRelation relation)
        {
            var queryBuilder = new StringBuilder();
            var properties = GetGetProperties(typeof(TRelation));
            queryBuilder.AppendLine(cypherMatchQuery);
            queryBuilder.AppendLine(BuildRelationQuery<TRelation>(properties.Keys.ToArray(),variableNodeFrom,variableNodeTo,"neo_rel_"));
            return new Query(queryBuilder.ToString(), BuildParameters(properties, relation));
        }
        internal Dictionary<string,object> BuildParameters(IDictionary<string,MethodInfo> properties,object entityObject, string propertyPrefix = "")
        {
            var result = new Dictionary<string, object>();
            foreach (var property in properties)
            {
                result.Add(propertyPrefix + property.Key, property.Value.Invoke(entityObject, null));
            }
            return result;
        }
        internal string BuildRelationQuery<TRelation>(string[] properties, string nodeBase, string nodeTo ,  string propertyPrefix = "" ,string nodeName = "")
        {
            var builder = new StringBuilder();
            builder.Append($"CREATE({nodeBase})-[");
            builder.Append($"{nodeName}:{typeof(TRelation).Name} ");
            builder.Append("{");
            for (int i = 0; i < properties.Length; i++)
            {
                if (i == properties.Length - 1)
                {
                    builder.Append($"{properties[i]}:${propertyPrefix}{properties[i]}");
                }
                else
                {
                    builder.Append($"{properties[i]}:${propertyPrefix}{properties[i]} , ");
                }
            }
            builder.Append("}");
            builder.Append($"]->({nodeTo})");
            return builder.ToString();
        }
        internal string BuildNodeQuery<TNode>(string[] properties, string propertyPrefix = "", string nodeName = "")
        {
            var builder = new StringBuilder();
            builder.Append("CREATE(");
            builder.Append($"{nodeName}:{typeof(TNode).Name} ");
            builder.Append("{");
            for (int i = 0; i < properties.Length; i++)
            {
                if(i == properties.Length - 1)
                {
                    builder.Append($"{properties[i]}:${propertyPrefix}{properties[i]}");
                }
                else
                {
                    builder.Append($"{properties[i]}:${propertyPrefix}{properties[i]} , ");
                }
            }
            builder.Append("}");
            builder.Append(")");
            return builder.ToString();
        }


    }
}
