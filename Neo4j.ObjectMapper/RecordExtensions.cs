using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Neo4j.ObjectMapper
{
    public static class RecordExtensions
    {
        public static T ConvertFirstRecordValueTo<T>(this IRecord record)
            where T : class, new()
        {
            if (record.Values.Count > 0)
            {
                var entity = record.Values.First().Value as IEntity;
                if (entity != null)
                {
                    return entity.ConvertEntityTo<T>();
                }
            }
            return default;
        }

        public static T MapRecordTo<T>(this IRecord record)
            where T : class, new()
        {
            T t = null;
            foreach (var recordValue in record.Values)
            {
                var objectName = GetObjectName(recordValue);
                var entity = recordValue.Value as IEntity;
                if (objectName.Equals(typeof(T).Name, StringComparison.OrdinalIgnoreCase))
                {
                    t = entity.ConvertEntityTo<T>();
                }
            }
            return t;
        }
        public static (T, T1) MapRecordTo<T, T1>(this IRecord record)
            where T : class, new()
            where T1 : class, new()
        {
            T t = null;
            T1 t1 = null;
            foreach (var recordValue in record.Values)
            {
                var objectName = GetObjectName(recordValue);
                var entity = recordValue.Value as IEntity;
                if (objectName.Equals(typeof(T).Name, StringComparison.OrdinalIgnoreCase))
                {
                    t = entity.ConvertEntityTo<T>();
                }
                else if (objectName.Equals(typeof(T1).Name, StringComparison.OrdinalIgnoreCase))
                {
                    t1 = entity.ConvertEntityTo<T1>();
                }
            }
            return (t, t1);
        }

        public static T MapRecordTo<T, T1>(this IRecord record, Func<T, T1, T> map)
            where T : class, new()
            where T1 : class, new()
        {
            T t = null;
            T1 t1 = null;
            foreach (var recordValue in record.Values)
            {
                var objectName = GetObjectName(recordValue);
                var entity = recordValue.Value as IEntity;
                if (objectName.Equals(typeof(T).Name, StringComparison.OrdinalIgnoreCase))
                {
                    t = entity.ConvertEntityTo<T>();
                }
                else if (objectName.Equals(typeof(T1).Name, StringComparison.OrdinalIgnoreCase))
                {
                    t1 = entity.ConvertEntityTo<T1>();
                }
            }
            return map(t, t1);
        }
        public static T MapRecordTo<T, T1, T2>(this IRecord record, Func<T, T1, T2, T> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
        {
            T t = null;
            T1 t1 = null;
            T2 t2 = null;
            foreach (var recordValue in record.Values)
            {
                var objectName = GetObjectName(recordValue);
                var entity = recordValue.Value as IEntity;
                if (objectName.Equals(typeof(T).Name, StringComparison.OrdinalIgnoreCase))
                {
                    t = entity.ConvertEntityTo<T>();
                }
                else if (objectName.Equals(typeof(T1).Name, StringComparison.OrdinalIgnoreCase))
                {
                    t1 = entity.ConvertEntityTo<T1>();
                }
                else if (objectName.Equals(typeof(T2).Name, StringComparison.OrdinalIgnoreCase))
                {
                    t2 = entity.ConvertEntityTo<T2>();
                }
            }
            return map(t, t1, t2);
        }
        public static T MapRecordTo<T, T1, T2, T3>(this IRecord record, Func<T, T1, T2, T3, T> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            T t = null;
            T1 t1 = null;
            T2 t2 = null;
            T3 t3 = null;
            foreach (var recordValue in record.Values)
            {
                var objectName = GetObjectName(recordValue);
                var entity = recordValue.Value as IEntity;
                if (objectName.Equals(typeof(T).Name, StringComparison.OrdinalIgnoreCase))
                {
                    t = entity.ConvertEntityTo<T>();
                }
                else if (objectName.Equals(typeof(T1).Name, StringComparison.OrdinalIgnoreCase))
                {
                    t1 = entity.ConvertEntityTo<T1>();
                }
                else if (objectName.Equals(typeof(T2).Name, StringComparison.OrdinalIgnoreCase))
                {
                    t2 = entity.ConvertEntityTo<T2>();
                }
                else if (objectName.Equals(typeof(T3).Name, StringComparison.OrdinalIgnoreCase))
                {
                    t3 = entity.ConvertEntityTo<T3>();
                }
            }
            return map(t, t1, t2, t3);
        }
        public static T MapRecordTo<T, T1, T2, T3, T4>(this IRecord record, Func<T, T1, T2, T3, T4, T> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            T t = null;
            T1 t1 = null;
            T2 t2 = null;
            T3 t3 = null;
            T4 t4 = null;
            foreach (var recordValue in record.Values)
            {
                var objectName = GetObjectName(recordValue);
                var entity = recordValue.Value as IEntity;
                if (objectName.Equals(typeof(T).Name, StringComparison.OrdinalIgnoreCase))
                {
                    t = entity.ConvertEntityTo<T>();
                }
                else if (objectName.Equals(typeof(T1).Name, StringComparison.OrdinalIgnoreCase))
                {
                    t1 = entity.ConvertEntityTo<T1>();
                }
                else if (objectName.Equals(typeof(T2).Name, StringComparison.OrdinalIgnoreCase))
                {
                    t2 = entity.ConvertEntityTo<T2>();
                }
                else if (objectName.Equals(typeof(T3).Name, StringComparison.OrdinalIgnoreCase))
                {
                    t3 = entity.ConvertEntityTo<T3>();
                }
                else if (objectName.Equals(typeof(T4).Name, StringComparison.OrdinalIgnoreCase))
                {
                    t4 = entity.ConvertEntityTo<T4>();
                }
            }
            return map(t, t1, t2, t3, t4);
        }

        public static IList<T> MapRecordTo<T, T1>(this IRecord record, Func<T, T1, IList<T>> map)
            where T : class, new()
            where T1 : class, new()
        {
            T t = null;
            T1 t1 = null;
            foreach (var recordValue in record.Values)
            {
                var objectName = GetObjectName(recordValue);
                var entity = recordValue.Value as IEntity;
                if (objectName.Equals(typeof(T).Name, StringComparison.OrdinalIgnoreCase))
                {
                    t = entity.ConvertEntityTo<T>();
                }
                else if (objectName.Equals(typeof(T1).Name, StringComparison.OrdinalIgnoreCase))
                {
                    t1 = entity.ConvertEntityTo<T1>();
                }
            }
            return map(t, t1);
        }
        public static IList<T> MapRecordTo<T, T1, T2>(this IRecord record, Func<T, T1, T2, IList<T>> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
        {
            T t = null;
            T1 t1 = null;
            T2 t2 = null;
            foreach (var recordValue in record.Values)
            {
                var objectName = GetObjectName(recordValue);
                var entity = recordValue.Value as IEntity;
                if (objectName.Equals(typeof(T).Name, StringComparison.OrdinalIgnoreCase))
                {
                    t = entity.ConvertEntityTo<T>();
                }
                else if (objectName.Equals(typeof(T1).Name, StringComparison.OrdinalIgnoreCase))
                {
                    t1 = entity.ConvertEntityTo<T1>();
                }
                else if (objectName.Equals(typeof(T2).Name, StringComparison.OrdinalIgnoreCase))
                {
                    t2 = entity.ConvertEntityTo<T2>();
                }
            }
            return map(t, t1, t2);
        }
        public static IList<T> MapRecordTo<T, T1, T2, T3>(this IRecord record, Func<T, T1, T2, T3, IList<T>> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            T t = null;
            T1 t1 = null;
            T2 t2 = null;
            T3 t3 = null;
            foreach (var recordValue in record.Values)
            {
                var objectName = GetObjectName(recordValue);
                var entity = recordValue.Value as IEntity;
                if (objectName.Equals(typeof(T).Name, StringComparison.OrdinalIgnoreCase))
                {
                    t = entity.ConvertEntityTo<T>();
                }
                else if (objectName.Equals(typeof(T1).Name, StringComparison.OrdinalIgnoreCase))
                {
                    t1 = entity.ConvertEntityTo<T1>();
                }
                else if (objectName.Equals(typeof(T2).Name, StringComparison.OrdinalIgnoreCase))
                {
                    t2 = entity.ConvertEntityTo<T2>();
                }
                else if (objectName.Equals(typeof(T3).Name, StringComparison.OrdinalIgnoreCase))
                {
                    t3 = entity.ConvertEntityTo<T3>();
                }
            }
            return map(t, t1, t2, t3);
        }
        public static IList<T> MapRecordTo<T, T1, T2, T3, T4>(this IRecord record, Func<T, T1, T2, T3, T4, IList<T>> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            T t = null;
            T1 t1 = null;
            T2 t2 = null;
            T3 t3 = null;
            T4 t4 = null;
            foreach (var recordValue in record.Values)
            {
                var objectName = GetObjectName(recordValue);
                var entity = recordValue.Value as IEntity;
                if (objectName.Equals(typeof(T).Name, StringComparison.OrdinalIgnoreCase))
                {
                    t = entity.ConvertEntityTo<T>();
                }
                else if (objectName.Equals(typeof(T1).Name, StringComparison.OrdinalIgnoreCase))
                {
                    t1 = entity.ConvertEntityTo<T1>();
                }
                else if (objectName.Equals(typeof(T2).Name, StringComparison.OrdinalIgnoreCase))
                {
                    t2 = entity.ConvertEntityTo<T2>();
                }
                else if (objectName.Equals(typeof(T3).Name, StringComparison.OrdinalIgnoreCase))
                {
                    t3 = entity.ConvertEntityTo<T3>();
                }
                else if (objectName.Equals(typeof(T4).Name, StringComparison.OrdinalIgnoreCase))
                {
                    t4 = entity.ConvertEntityTo<T4>();
                }
            }
            return map(t, t1, t2, t3, t4);
        }

        private static string GetObjectName(KeyValuePair<string, object> recordValue)
        {
            string objectName;
            if (typeof(INode).IsAssignableFrom(recordValue.Value.GetType()))
            {
                objectName = (recordValue.Value as INode).Labels[0];
            }
            else
            {
                objectName = (recordValue.Value as IRelationship).Type;
            }
            return objectName.ToLower();
        }
    }
}
