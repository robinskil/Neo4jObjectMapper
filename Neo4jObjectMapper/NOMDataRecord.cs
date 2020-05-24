using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Neo4jObjectMapper
{
    public class NOMDataRecord
    {
        #region static
        private static readonly Dictionary<Type, Dictionary<string, MethodInfo>> cachedSettersForType;
        static NOMDataRecord()
        {
            cachedSettersForType = new Dictionary<Type, Dictionary<string, MethodInfo>>();
        }
        private static Dictionary<string, MethodInfo> GetSetProperties(Type type)
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
        #endregion static

        private readonly IReadOnlyDictionary<string, IReadOnlyDictionary<string, object>> rawNodeData;
        internal NOMDataRecord(Dictionary<string, IReadOnlyDictionary<string, object>> rawNodeData)
        {
            this.rawNodeData = rawNodeData;
        }

        private dynamic ConvertDictionaryToDynamic(IReadOnlyDictionary<string, object> rawNodeData)
        {
            var expandoObj = new ExpandoObject();
            var expandoObjCollection = (ICollection<KeyValuePair<String, Object>>)expandoObj;

            foreach (var keyValuePair in rawNodeData)
            {
                expandoObjCollection.Add(keyValuePair);
            }
            return expandoObj;
        }

        public IEnumerable<string> GetNodeVariables()
        {
            return rawNodeData.Keys;
        }

        public dynamic this[string nodevariable]
        {
            get
            {
                return ConvertDictionaryToDynamic(rawNodeData[nodevariable]);
            }
        }

        public TProp GetSingleProperty<TProp>(string nodevariable , string propName)
        {
            return (TProp)rawNodeData[nodevariable][propName];
        }

        public object GetSingleProperty(string nodevariable, string propName)
        {
            return rawNodeData[nodevariable][propName];
        }

        public T GetAndConvert<T>(string nodevariable) where T : class, new()
        {
            T t = new T();
            var obj = rawNodeData[nodevariable];
            var propertiesToSet = GetSetProperties(typeof(T));
            foreach (var property in obj)
            {
                propertiesToSet[property.Key.ToLower()].Invoke(t, new[] { property.Value });
            }
            return t;
        }
    }
}
