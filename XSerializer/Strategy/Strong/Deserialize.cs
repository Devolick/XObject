using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections;
using System.Diagnostics;
using XObjectSerializer.Helpers;
using XObjectSerializer.Interfaces;
using XObjectSerializer.Exceptions;
using XObjectSerializer.Strategy.Code;
using XObjectSerializer.Attributes;

namespace XObjectSerializer.Strategy.Strong
{
    internal class Deserialize : Weak.Deserialize
    {
        internal Deserialize(Mechanism machanism)
            : base(machanism)
        {

        }
        internal Deserialize(Mechanism machanism, string dateFormat)
            : base(machanism,dateFormat) { }
        internal Deserialize(Mechanism machanism, string dateFormat, IFormatProvider dateFormatProvider)
            : base(machanism,dateFormat, dateFormatProvider) { }

        protected override object ComplexBuilder(Type type, string x)
        {
            XIgnoreClassAttribute ignoreClass = type.GetCustomAttribute<XIgnoreClassAttribute>(false);
            object o = Activator.CreateInstance(type);
            Regex rx = new Regex($"{Queries.OBJECT}|{Queries.VALUE}", RegexOptions.Compiled);
            MatchCollection matches = rx.Matches(x);

            foreach (PropertyInfo pi in ReflectionHelper.EachProps(o))
            {
                if (pi.GetCustomAttribute(typeof(XIgnorePropertyAttribute), false) != null ||
                    (ignoreClass != null && ignoreClass.Properties.Length > 0 && ignoreClass.Properties.Contains(pi.Name)))
                {
                    continue;
                }

                string value = matches.FirstDefault(w => Regex.IsMatch(w, Queries.GETPREFIX, RegexOptions.Compiled));
                if (!string.IsNullOrEmpty(value)) continue;
                pi.SetValue(o, Build(pi.PropertyType, Regex.Match(value, Queries.GETDATA, RegexOptions.Compiled).Value));
            }
            return o;
        }
        protected override object CollectionBuilder(Type type, string x)
        {
            XIgnoreClassAttribute ignoreClass = type.GetCustomAttribute<XIgnoreClassAttribute>(false);

            Regex rx = new Regex($"{Queries.OBJECT}|{Queries.VALUE}", RegexOptions.Compiled);
            MatchCollection matches = rx.Matches(x);
            string anyQ = string.Format(Queries.PREFIX, "I");
            IEnumerable<string> collectionItems = matches.Where(a => Regex.IsMatch(a, anyQ, RegexOptions.Compiled));
            ProxyCollection proxyCollection = new ProxyCollection(type, collectionItems.Count());
            foreach (PropertyInfo pi in ReflectionHelper.EachProps(proxyCollection.Collection))
            {
                if (pi.GetCustomAttribute(typeof(XIgnorePropertyAttribute), false) != null ||
                    (ignoreClass != null && ignoreClass.Properties.Length > 0 && ignoreClass.Properties.Contains(pi.Name)))
                {
                    continue;
                }

                string value = matches.FirstDefault(w => Regex.IsMatch(w, Queries.GETPREFIX, RegexOptions.Compiled));
                if (!string.IsNullOrEmpty(value)) continue;
                pi.SetValue(proxyCollection.Collection, Build(pi.PropertyType, Regex.Match(value, Queries.GETDATA, RegexOptions.Compiled).Value));
            }
            foreach (string item in collectionItems)
            {
                proxyCollection.Push(Build(proxyCollection.ItemType, Regex.Match(item, Queries.GETDATA, RegexOptions.Compiled).Value));
            }
            proxyCollection.CreateProxy();
            return proxyCollection.Collection;
        }
    }
}
