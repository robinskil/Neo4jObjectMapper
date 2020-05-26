using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection.Emit;
using System.Reflection;
using System.Linq;
using System.Net.Http.Headers;
using System.ComponentModel;

namespace Neo4jObjectMapper
{

    internal class Neo4jCustomTypeConverter
    {
        private delegate T ConvertTo<T>(IReadOnlyDictionary<string, object> neo4jProperties);
        private readonly Dictionary<Type, Dictionary<string, MethodInfo>> cachedPropertiesForType;
        private readonly Dictionary<Type, Delegate> cachedConverters;
        internal Neo4jCustomTypeConverter()
        {
            cachedConverters = new Dictionary<Type, Delegate>();
            cachedPropertiesForType = new Dictionary<Type, Dictionary<string, MethodInfo>>();
        }

        internal IEnumerable<string> GetGetProperties<T>()
        {
            if (!cachedPropertiesForType.ContainsKey(typeof(T)))
            {
                AddGetPropertiesToCache<T>();
            }
            return cachedPropertiesForType[typeof(T)].Keys;
        }

        internal IEnumerable<KeyValuePair<string,object>> GenerateParametersWithValuesFromT<T>(T obj)
        {
            if (!cachedPropertiesForType.ContainsKey(typeof(T)))
            {
                AddGetPropertiesToCache<T>();
            }
            foreach (var kv in cachedPropertiesForType[typeof(T)])
            {
                Type propType = kv.Value.ReturnType;
                object val;
                if(propType == typeof(Guid))
                {
                    Guid valHolder = (Guid)kv.Value.Invoke(obj, null);
                    val = valHolder.ToString();
                }
                else
                {
                    val = kv.Value.Invoke(obj, null);
                }
                yield return new KeyValuePair<string, object>(kv.Key,val);
            }
        }

        internal IDictionary<string, object> ConvertParameterTypes(IDictionary<string,object> parameters)
        {
            var dicConvertParameters = new Dictionary<string, object>();
            foreach (var kv in parameters)
            {
                if(kv.Value.GetType() == typeof(Guid))
                {
                    Guid guid = (Guid)kv.Value;
                    dicConvertParameters.Add(kv.Key, guid.ToString());
                }
                else
                {
                    dicConvertParameters.Add(kv.Key,kv.Value);
                }
            }
            return dicConvertParameters;
        }

        internal T ConvertPropertiesTo<T>(IReadOnlyDictionary<string, object> properties)
        {
            if(!cachedConverters.ContainsKey(typeof(T)))
            {
                Type[] methodArgTypes = { typeof(IReadOnlyDictionary<string,object>) };
                DynamicMethod dm = new DynamicMethod("Convert", typeof(T),methodArgTypes, Assembly.GetExecutingAssembly().GetType().Module);
                ILGenerator il = dm.GetILGenerator();

                il.DeclareLocal(typeof(T));
                il.Emit(OpCodes.Newobj, typeof(T).GetConstructor(Type.EmptyTypes));
                il.Emit(OpCodes.Stloc_0);

                var writeableProperties = typeof(T).GetProperties().Where(p => PropertyIsWriteable(p));
                var propertyKeysFromDic = properties.Keys.ToList();
                foreach (var property in writeableProperties)
                {
                    var propName = property.Name;
                    if(propertyKeysFromDic.Any(a => a.ToLower() == property.Name.ToLower()))
                    {
                        propName = propertyKeysFromDic.FirstOrDefault(a => a.ToLower() == property.Name.ToLower());
                    }
                    Label continueNotFound = il.DefineLabel();
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Ldstr, propName);
                    il.Emit(OpCodes.Callvirt, typeof(IReadOnlyDictionary<string, object>).GetMethod("ContainsKey", new Type[] { typeof(string) }));
                    //if property is in dictionary else go to label continueNotFound
                    il.Emit(OpCodes.Brfalse_S, continueNotFound);

                    il.Emit(OpCodes.Ldloc_0);
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Ldstr, propName);
                    il.Emit(OpCodes.Callvirt, typeof(IReadOnlyDictionary<string, object>).GetMethod("get_Item", new Type[] { typeof(string) }));
                    switch (property.PropertyType.Name)
                    {
                        case "Int32":
                            il.Emit(OpCodes.Unbox_Any, typeof(Int64));
                            il.Emit(OpCodes.Callvirt, typeof(T).GetMethod("set_" + property.Name, new Type[] { typeof(Int32) }));
                            break;
                        case "Int64":
                            il.Emit(OpCodes.Unbox_Any, typeof(long));
                            il.Emit(OpCodes.Callvirt, typeof(T).GetMethod("set_" + property.Name, new Type[] { typeof(Int64) }));
                            break;
                        case "Double":
                            il.Emit(OpCodes.Unbox_Any, typeof(double));
                            il.Emit(OpCodes.Callvirt, typeof(T).GetMethod("set_" + property.Name, new Type[] { property.PropertyType }));
                            break;
                        case "Single":
                            il.Emit(OpCodes.Unbox_Any, typeof(float));
                            il.Emit(OpCodes.Callvirt, typeof(T).GetMethod("set_" + property.Name, new Type[] { property.PropertyType }));
                            break;
                        case "String":
                            il.Emit(OpCodes.Unbox_Any, typeof(string));
                            il.Emit(OpCodes.Callvirt, typeof(T).GetMethod("set_" + property.Name, new Type[] { property.PropertyType }));
                            break;
                        case "Boolean":
                            il.Emit(OpCodes.Unbox_Any, typeof(bool));
                            il.Emit(OpCodes.Callvirt, typeof(T).GetMethod("set_" + property.Name, new Type[] { property.PropertyType }));
                            break;
                        case "Guid":
                            il.Emit(OpCodes.Call, typeof(Neo4jCustomTypeConverter).GetMethod("ConvertStringToGuid", BindingFlags.Static | BindingFlags.NonPublic));
                            il.Emit(OpCodes.Callvirt, typeof(T).GetMethod("set_" + property.Name, new Type[] { property.PropertyType }));
                            break;
                        case "DateTime":
                            il.Emit(OpCodes.Call, typeof(Neo4jCustomTypeConverter).GetMethod("ConvertLocalDateTime", BindingFlags.Static | BindingFlags.NonPublic));
                            il.Emit(OpCodes.Callvirt, typeof(T).GetMethod("set_" + property.Name, new Type[] { property.PropertyType }));
                            break;
                        case "LocalDate":
                            il.Emit(OpCodes.Callvirt, typeof(T).GetMethod("set_" + property.Name, new Type[] { property.PropertyType }));
                            break;
                        case "OffsetTime":
                            il.Emit(OpCodes.Callvirt, typeof(T).GetMethod("set_" + property.Name, new Type[] { property.PropertyType }));
                            break;
                        case "LocalTime":
                            il.Emit(OpCodes.Callvirt, typeof(T).GetMethod("set_" + property.Name, new Type[] { property.PropertyType }));
                            break;
                        case "ZonedDateTime":
                            il.Emit(OpCodes.Callvirt, typeof(T).GetMethod("set_" + property.Name, new Type[] { property.PropertyType }));
                            break;
                        case "LocalDateTime":
                            il.Emit(OpCodes.Callvirt, typeof(T).GetMethod("set_" + property.Name, new Type[] { property.PropertyType }));
                            break;
                        case "Duration":
                            il.Emit(OpCodes.Callvirt, typeof(T).GetMethod("set_" + property.Name, new Type[] { property.PropertyType }));
                            break;
                        case "Point":
                            il.Emit(OpCodes.Callvirt, typeof(T).GetMethod("set_" + property.Name, new Type[] { property.PropertyType }));
                            break;
                        case "Byte[]":
                            il.Emit(OpCodes.Callvirt, typeof(T).GetMethod("set_" + property.Name, new Type[] { property.PropertyType }));
                            break;
                        case "IList`1":
                            if (!typeof(IList<object>).IsAssignableFrom(property.PropertyType))
                            {
                                goto default;
                            }
                            il.Emit(OpCodes.Callvirt, typeof(T).GetMethod("set_" + property.Name, new Type[] { property.PropertyType }));
                            break;
                        case "IDictionary`2":
                            if (!typeof(IDictionary<string,object>).IsAssignableFrom(property.PropertyType))
                            {
                                goto default;
                            }
                            il.Emit(OpCodes.Callvirt, typeof(T).GetMethod("set_" + property.Name, new Type[] { property.PropertyType }));
                            break;
                        default:
                            il.Emit(OpCodes.Pop);
                            il.Emit(OpCodes.Pop);
                            break;
                    }
                    il.MarkLabel(continueNotFound);
                }

                il.Emit(OpCodes.Ldloc_0);
                // Return
                il.Emit(OpCodes.Ret);
                cachedConverters.Add(typeof(T), dm.CreateDelegate(typeof(ConvertTo<T>)));
            }
            ConvertTo<T> virtualConvertCall = (ConvertTo<T>)cachedConverters[typeof(T)];
            return virtualConvertCall(properties);
        }
        private void AddGetPropertiesToCache<T>()
        {
            cachedPropertiesForType.Add(typeof(T), new Dictionary<string, MethodInfo>());
            foreach (var property in typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(p => p.CanRead && (p.PropertyType.IsValueType || p.PropertyType == typeof(string))))
            {
                cachedPropertiesForType[typeof(T)].Add(property.Name, property.GetMethod);
            }
        }
        private DateTime ConvertZonedDateTime(ZonedDateTime zonedDateTime)
        {
            return zonedDateTime.ToDateTimeOffset().DateTime;
        }
        private static DateTime ConvertLocalDateTime(LocalDateTime localDateTime)
        {
            return localDateTime.ToDateTime();
        }
        private static Guid ConvertStringToGuid(string guidString)
        {
            return Guid.Parse(guidString);
        }

        private bool PropertyIsWriteable(PropertyInfo propertyInfo)
        {
            var readonlyAtt = Attribute.GetCustomAttribute(propertyInfo, typeof(ReadOnlyAttribute));
            return propertyInfo.CanWrite && readonlyAtt == null;
        }
    }
}
