using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Diagnostics;

namespace XObjectSerializer.Strategy.Code
{
    internal class ProxyCollection
    {
        public Type Type { get; private set; }
        public Type ItemType { get; private set; }
        internal object CollectionObject { get; private set; }
        private IList innerList;
        private IDictionary innerDictionary;
        private ConstructorInfo proxyConstructor;
        private int capacity;
        private int count = 0;

        internal ProxyCollection(Type type,int capacity)
        {
            this.capacity = capacity;
            Type = type;
            FindType();
        }
        private void FindType() {
            if (Type.IsArray)
            {
                Array();
            }
            else
            {
                Collection();
            }
        }
        private void Array()
        {
            innerList = (IList)Activator.CreateInstance(Type, new object[] { capacity });
            ItemType = Type.GetElementType();

            CollectionObject = innerList;
        }
        private void Collection()
        {
            CollectionObject = Activator.CreateInstance(Type);
            Type type = Type;
            Type em = typeof(IEnumerable);
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Public;
            do
            {
                proxyConstructor = type.GetConstructors(flags)
                    .FirstOrDefault(f => f.GetParameters().Count() == 1 && em.IsAssignableFrom(f.GetParameters()[0].ParameterType));
                if (proxyConstructor != null)
                {
                    Type innerEm = proxyConstructor.GetParameters()[0].ParameterType;
                    if (innerEm.GetGenericTypeDefinition() == typeof(IDictionary<,>))
                    {
                        ItemType = typeof(KeyValuePair<,>).MakeGenericType(innerEm.GetTypeInfo().GetGenericArguments()[0], innerEm.GetTypeInfo().GetGenericArguments()[1]);
                        var innerCollectionType = typeof(Dictionary<,>);
                        var innerColectionCtorType = innerCollectionType.MakeGenericType(innerEm.GetTypeInfo().GetGenericArguments()[0], innerEm.GetTypeInfo().GetGenericArguments()[1]);
                        innerDictionary = Activator.CreateInstance(innerColectionCtorType) as IDictionary;
                    }
                    else
                    {
                        if (innerEm.IsGenericType)
                        {
                            ItemType = innerEm.GetTypeInfo().GetGenericArguments()[0];
                        }
                        else
                        {
                            ItemType = typeof(object);
                        }
                        var innerCollectionType = typeof(List<>);
                        var innerColectionCtorType = innerCollectionType.MakeGenericType(ItemType);
                        innerList = Activator.CreateInstance(innerColectionCtorType) as IList;
                    }
                    break;
                };
            } while ((type = type.BaseType) != null);

            if (proxyConstructor == null) throw new Exception();
        }
        internal void Push(object value)
        {
            if (Type.IsArray)
            {
                innerList[count++] = value;
            }
            else
            {
                if (innerDictionary != null)
                {
                    Type type = value.GetType();
                    PropertyInfo k = type.GetProperty("Key");
                    PropertyInfo v = type.GetProperty("Value");
                    innerDictionary.Add(k.GetValue(value), v.GetValue(value));
                }
                else
                {
                    innerList.Add(value);
                }
            }
        }
        internal void CreateProxy()
        {
            if(!Type.IsArray)
            {
                if (innerDictionary != null)
                {
                    proxyConstructor.Invoke(CollectionObject, new[] { innerDictionary as IEnumerable });
                }
                else
                {
                    proxyConstructor.Invoke(CollectionObject, new[] { innerList as IEnumerable });
                }
            }
        }
    }
}
