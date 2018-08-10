using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using XObjectSerializer.Attributes;
using XObjectSerializer.Exceptions;
using XObjectSerializer.Helpers;
using XObjectSerializer.Interfaces;

namespace XObjectSerializer.Strategy.Strong
{
    internal class Serialize : Weak.Serialize
    {
        internal Serialize(Mechanism machanism)
            :base(machanism)
        {

        }
        internal Serialize(Mechanism machanism, string dateFormat)
            : base(machanism, dateFormat) {

        }
        internal Serialize(Mechanism machanism, string dateFormat, IFormatProvider dateFormatProvider)
            : base(machanism, dateFormat, dateFormatProvider) {

        }

        protected override string CollectionBlock(Type type, object o)
        {
            XIgnoreClassAttribute ignoreClass = type.GetCustomAttribute<XIgnoreClassAttribute>(false);

            if (!ReferenceExists(o))
            {
                (o as IXObject)?.XSerialize(o);
                StringBuilder complex = new StringBuilder(64);
                foreach (PropertyInfo pi in EachHelper.EachProps(o))
                {
                    object piValue = pi.GetValue(o);
                    if (piValue == null ||
                        pi.GetCustomAttribute(typeof(XIgnorePropertyAttribute), false) != null ||
                        (ignoreClass != null && ignoreClass.Properties.Length > 0 && ignoreClass.Properties.Contains(pi.Name)))
                    {
                        continue;
                    }
                    string value = BuildBlocks(pi.PropertyType, piValue);
                    if (!string.IsNullOrEmpty(value))
                    {
                        complex.Append($"{GeneratePropertyKey(pi.Name)}{value}");
                    }
                }
                foreach (object item in (o as IEnumerable))
                {
                    if (item == null) continue;
                    string value = BuildBlocks(item.GetType(), item);
                    if (string.IsNullOrEmpty(value)) continue;
                    complex.Append($"I{value}");
                }
                AddReference(o);
                return $"\"{complex}\"";
            }
            else
            {
                return $"\"`{SameObject(o, true)}\"";
            }
        }
        protected override string ComplexBlock(Type type, object o)
        {
            XIgnoreClassAttribute ignoreClass = type.GetCustomAttribute<XIgnoreClassAttribute>(false);

            if (!ReferenceExists(o))
            {
                (o as IXObject)?.XSerialize(o);
                StringBuilder complex = new StringBuilder(64);
                foreach (PropertyInfo pi in EachHelper.EachProps(o))
                {
                    object piValue = pi.GetValue(o);
                    if (piValue == null ||
                        pi.GetCustomAttribute(typeof(XIgnorePropertyAttribute), false) != null ||
                        (ignoreClass != null && ignoreClass.Properties.Length > 0 && ignoreClass.Properties.Contains(pi.Name)))
                    {
                        continue;
                    }
                    string value = BuildBlocks(pi.PropertyType, piValue);
                    if (!string.IsNullOrEmpty(value))
                    {
                        complex.Append($"{GeneratePropertyKey(pi.Name)}{value}");
                    }
                }
                AddReference(o);
                return $"\"{complex}\"";
            }
            else
            {
                return $"\"`{SameObject(o, true)}\"";
            }
        }
    }
}
