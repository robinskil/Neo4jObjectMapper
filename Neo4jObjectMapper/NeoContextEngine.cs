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
        private readonly Neo4jCustomTypeConverter typeConverter;

        public NeoContextEngine()
        {
            typeConverter = new Neo4jCustomTypeConverter();
        }

        internal T ConvertRecordToObject<T>(IRecord record) where T : class, new()
        {
            if (record.Values.Count > 0)
            {
                var objectNode = record.Values.First().Value as INode;
                return typeConverter.ConvertPropertiesTo<T>(objectNode.Properties);
            }
            return default;
        }

        private string GetObjectName(KeyValuePair<string,object> recordValue)
        {
            string objectName;
            if (typeof(INode).IsAssignableFrom(recordValue.Value.GetType()))
            {
                objectName = (recordValue.Value as INode).Labels.First();
            }
            else
            {
                objectName = (recordValue.Value as IRelationship).Type;
            }
            return objectName.ToLower();
        }

        internal TResult ConvertRecordToObjects<TResult, TInclude>(IEnumerable<IRecord> records, Func<TResult, TInclude, TResult> mapFunc)
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
                    string objectName = GetObjectName(recordValue);
                    var entity = recordValue.Value as IEntity;
                    if (objectName.ToLower() == typeof(TResult).Name.ToLower())
                    {
                        res = typeConverter.ConvertPropertiesTo<TResult>(entity.Properties);
                    }
                    else if (objectName.ToLower() == typeof(TInclude).Name.ToLower())
                    {
                        include = typeConverter.ConvertPropertiesTo<TInclude>(entity.Properties);
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
        internal TResult ConvertRecordToObjects<TResult, TInclude, TInclude2>(List<IRecord> records, Func<TResult, TInclude, TInclude2, TResult> mapFunc)
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
                    var objectName = GetObjectName(recordValue);
                    var entity = recordValue.Value as IEntity;
                    if (objectName == typeof(TResult).Name.ToLower())
                    {
                        res = typeConverter.ConvertPropertiesTo<TResult>(entity.Properties);
                    }
                    else if (objectName == typeof(TInclude).Name.ToLower())
                    {
                        include = typeConverter.ConvertPropertiesTo<TInclude>(entity.Properties);
                    }
                    else if (objectName == typeof(TInclude2).Name.ToLower())
                    {
                        include2 = typeConverter.ConvertPropertiesTo<TInclude2>(entity.Properties);
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
        internal TResult ConvertRecordToObjects<TResult, TInclude, TInclude2, TInclude3>(List<IRecord> records, Func<TResult, TInclude, TInclude2, TInclude3, TResult> mapFunc)
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
                    var objectName = GetObjectName(recordValue);
                    var entity = recordValue.Value as IEntity;
                    if (objectName == typeof(TResult).Name.ToLower())
                    {
                        res = typeConverter.ConvertPropertiesTo<TResult>(entity.Properties);
                    }
                    else if (objectName == typeof(TInclude).Name.ToLower())
                    {
                        include = typeConverter.ConvertPropertiesTo<TInclude>(entity.Properties);
                    }
                    else if (objectName == typeof(TInclude2).Name.ToLower())
                    {
                        include2 = typeConverter.ConvertPropertiesTo<TInclude2>(entity.Properties);
                    }
                    else if (objectName == typeof(TInclude3).Name.ToLower())
                    {
                        include3 = typeConverter.ConvertPropertiesTo<TInclude3>(entity.Properties);
                    }
                    else
                    {
                        throw new NeoContextException("Unrecognized node inside the result of the query.");
                    }
                }
                result = mapFunc(res, include, include2,include3);
            }
            return result;
        }
        internal TResult ConvertRecordToObjects<TResult, TInclude, TInclude2, TInclude3, TInclude4>(List<IRecord> records, Func<TResult, TInclude, TInclude2, TInclude3, TInclude4, TResult> mapFunc)
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
                    var objectName = GetObjectName(recordValue);
                    var entity = recordValue.Value as IEntity;
                    if (objectName == typeof(TResult).Name.ToLower())
                    {
                        res = typeConverter.ConvertPropertiesTo<TResult>(entity.Properties);
                    }
                    else if (objectName == typeof(TInclude).Name.ToLower())
                    {
                        include = typeConverter.ConvertPropertiesTo<TInclude>(entity.Properties);
                    }
                    else if (objectName == typeof(TInclude2).Name.ToLower())
                    {
                        include2 = typeConverter.ConvertPropertiesTo<TInclude2>(entity.Properties);
                    }
                    else if (objectName == typeof(TInclude3).Name.ToLower())
                    {
                        include3 = typeConverter.ConvertPropertiesTo<TInclude3>(entity.Properties);
                    }
                    else if (objectName == typeof(TInclude4).Name.ToLower())
                    {
                        include4 = typeConverter.ConvertPropertiesTo<TInclude4>(entity.Properties);
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

        internal IEnumerable<TResult> ConvertRecordsToObjects<TResult, TInclude>(List<IRecord> records, Func<TResult, TInclude, IEnumerable<TResult>> mapFunc)
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
                    string objectName = GetObjectName(recordValue);
                    var entity = recordValue.Value as IEntity;
                    if (objectName.ToLower() == typeof(TResult).Name.ToLower())
                    {
                        res = typeConverter.ConvertPropertiesTo<TResult>(entity.Properties);
                    }
                    else if (objectName.ToLower() == typeof(TInclude).Name.ToLower())
                    {
                        include = typeConverter.ConvertPropertiesTo<TInclude>(entity.Properties);
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
        internal IEnumerable<TResult> ConvertRecordsToObjects<TResult, TInclude, TInclude2>(List<IRecord> records, Func<TResult, TInclude, TInclude2, IEnumerable<TResult>> mapFunc)
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
                    var objectName = GetObjectName(recordValue);
                    var entity = recordValue.Value as IEntity;
                    if (objectName == typeof(TResult).Name.ToLower())
                    {
                        res = typeConverter.ConvertPropertiesTo<TResult>(entity.Properties);
                    }
                    else if (objectName == typeof(TInclude).Name.ToLower())
                    {
                        include = typeConverter.ConvertPropertiesTo<TInclude>(entity.Properties);
                    }
                    else if (objectName == typeof(TInclude2).Name.ToLower())
                    {
                        include2 = typeConverter.ConvertPropertiesTo<TInclude2>(entity.Properties);
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
        internal IEnumerable<TResult> ConvertRecordsToObjects<TResult, TInclude, TInclude2, TInclude3>(List<IRecord> records, Func<TResult, TInclude, TInclude2, TInclude3, IEnumerable<TResult>> mapFunc)
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
                    var objectName = GetObjectName(recordValue);
                    var entity = recordValue.Value as IEntity;
                    if (objectName == typeof(TResult).Name.ToLower())
                    {
                        res = typeConverter.ConvertPropertiesTo<TResult>(entity.Properties);
                    }
                    else if (objectName == typeof(TInclude).Name.ToLower())
                    {
                        include = typeConverter.ConvertPropertiesTo<TInclude>(entity.Properties);
                    }
                    else if (objectName == typeof(TInclude2).Name.ToLower())
                    {
                        include2 = typeConverter.ConvertPropertiesTo<TInclude2>(entity.Properties);
                    }
                    else if (objectName == typeof(TInclude3).Name.ToLower())
                    {
                        include3 = typeConverter.ConvertPropertiesTo<TInclude3>(entity.Properties);
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
        internal IEnumerable<TResult> ConvertRecordsToObjects<TResult, TInclude, TInclude2, TInclude3, TInclude4>(List<IRecord> records, Func<TResult, TInclude, TInclude2, TInclude3, TInclude4, IEnumerable<TResult>> mapFunc)
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
                    var objectName = GetObjectName(recordValue);
                    var entity = recordValue.Value as IEntity;
                    if (objectName == typeof(TResult).Name.ToLower())
                    {
                        res = typeConverter.ConvertPropertiesTo<TResult>(entity.Properties);
                    }
                    else if (objectName == typeof(TInclude).Name.ToLower())
                    {
                        include = typeConverter.ConvertPropertiesTo<TInclude>(entity.Properties);
                    }
                    else if (objectName == typeof(TInclude2).Name.ToLower())
                    {
                        include2 = typeConverter.ConvertPropertiesTo<TInclude2>(entity.Properties);
                    }
                    else if (objectName == typeof(TInclude3).Name.ToLower())
                    {
                        include3 = typeConverter.ConvertPropertiesTo<TInclude3>(entity.Properties);
                    }
                    else if (objectName == typeof(TInclude4).Name.ToLower())
                    {
                        include4 = typeConverter.ConvertPropertiesTo<TInclude4>(entity.Properties);
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
            Dictionary<string, IReadOnlyDictionary<string, object>> resultObjects = new Dictionary<string, IReadOnlyDictionary<string, object>>();
            foreach (var resultObject in record.Values)
            {
                IEntity values = resultObject.Value as IEntity;
                resultObjects.Add(resultObject.Key, values.Properties);
            }
            return new NOMDataRecord(resultObjects);
        }

        internal Query CreateInsertNodeQuery<TNode>(TNode entity)
        {
            var properties = typeConverter.GetGetProperties<TNode>();
            var query = BuildNodeQuery<TNode>(properties.ToArray());
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            foreach (var parameter in BuildParameters<TNode>(entity))
            {
                parameters.Add(parameter.Key,parameter.Value);
            }
            return new Query(query, parameters);
        }
        internal Query CreateInsertNodesQuery<TNode>(IEnumerable<TNode> entities)
        {
            var queryStringBuilder = new StringBuilder();
            var properties = typeConverter.GetGetProperties<TNode>();
            var propertyArray = properties.ToArray();
            var entityArray = entities.ToArray();
            List<KeyValuePair<string, object>> parameterList = new List<KeyValuePair<string, object>>();
            for (int i = 0; i < entityArray.Length; i++)
            {
                queryStringBuilder.AppendLine(BuildNodeQuery<TNode>(propertyArray, $"entity{i}"));
                parameterList.AddRange(BuildParameters(entityArray[i], $"entity{i}"));
            }
            var parameterDic = new Dictionary<string, object>();
            foreach (var kv in parameterList)
            {
                parameterDic.Add(kv.Key, kv.Value);
            }
            return new Query(queryStringBuilder.ToString(), new Dictionary<string, object>(parameterDic));
        }
        internal Query CreateInsertNodesWithRelationQuery<TNode, TRelation, TNode2>(TNode node, TRelation relation, TNode2 node2)
        {
            var queryBuilder = new StringBuilder();
            var propertiesNode1 = typeConverter.GetGetProperties<TNode>();
            var propertiesRel1 = typeConverter.GetGetProperties<TRelation>();
            var propertiesNode2 = typeConverter.GetGetProperties<TNode2>();
            queryBuilder.AppendLine(BuildNodeQuery<TNode>(propertiesNode1.ToArray(), "node1prop", "node1"));
            queryBuilder.AppendLine(BuildNodeQuery<TNode2>(propertiesNode2.ToArray(), "node2prop", "node2"));
            queryBuilder.AppendLine(BuildRelationQuery<TRelation>(propertiesRel1.ToArray(), "node1", "node2", "relProp"));
            List<KeyValuePair<string, object>> parameterList = new List<KeyValuePair<string, object>>();
            parameterList.AddRange(BuildParameters(node, "node1prop"));
            parameterList.AddRange(BuildParameters(node2, "node2prop"));
            parameterList.AddRange(BuildParameters(relation, "relProp"));
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
            var properties = typeConverter.GetGetProperties<TRelation>();
            queryBuilder.AppendLine(cypherMatchQuery);
            queryBuilder.AppendLine(BuildRelationQuery<TRelation>(properties.ToArray(), variableNodeFrom, variableNodeTo, "neo_rel_"));
            return new Query(queryBuilder.ToString(), BuildParameters(relation, "neo_rel_"));
        }

        internal T CreateTFromEntity<T>(IEntity entities) where T : class, new()
        {
            return typeConverter.ConvertPropertiesTo<T>(entities.Properties);
        }

        internal IEnumerable<KeyValuePair<string, object>> BuildParameters<T>(T obj, string propertyPrefix = "")
        {
            foreach (var property in typeConverter.GenerateParametersWithValuesFromT<T>(obj))
            {
                yield return new KeyValuePair<string,object>(propertyPrefix + property.Key, property.Value);
            }
        }
        internal string BuildRelationQuery<TRelation>(string[] properties, string nodeBase, string nodeTo, string propertyPrefix = "", string nodeName = "")
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
            builder.Append(")");
            return builder.ToString();
        }

        internal IDictionary<string, object> ParameterConverter(IDictionary<string,object> parameters)
        {
            return typeConverter.ConvertParameterTypes(parameters);
        }
    }
}
