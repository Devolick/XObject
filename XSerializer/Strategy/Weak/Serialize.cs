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

namespace XObjectSerializer.Strategy.Weak
{
    internal class Serialize : Builder
    {
        internal Serialize()
        {

        }
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

        protected string BooleanBlock(Type type, object o)
        {
            string value = ((bool)o ? 1 : 0).ToString();
            return $"'{value}'";
        }
        protected string DateTimeBlock(Type type, object o)
        {
            string value = AddProtect($"{((DateTime)o).ToString(dateFormat, dateFormatProvider)}");
            if (!SmartReferenceExists(value))
            {
                AddSmartReference(value);
                return $"'{value}'";
            }
            else
            {
                return $"'`{SameObject(value, false)}'";
            }
        }
        protected string EnumBlock(Type type, object o)
        {
            string value = ((int)o).ToString();
            if (!SmartReferenceExists(value))
            {
                AddSmartReference(value);
                return $"'{value}'";
            }
            else
            {
                return $"'`{SameObject(value, false)}'";
            }
        }
        protected string NumberBlock(Type type, object o)
        {
            string value = o.ToString();
            if (!SmartReferenceExists(value))
            {
                AddSmartReference(value);
                return $"'{value}'";
            }
            else
            {
                return $"'`{SameObject(value, false)}'";
            }
        }
        protected string StringBlock(Type type, object o)
        {
            string value = (string)o;
            if (string.IsNullOrEmpty(value)) return null;

            value = AddProtect(value);
            if (!SmartReferenceExists(value))
            {
                AddSmartReference(value);
                return $"'{value}'";
            }
            else
            {
                return $"'`{SameObject(value, false)}'";
            }
        }
        protected string KeyPairBlock(Type type, object o)
        {
            if (!ReferenceExists(o))
            {
                (o as IXObject)?.XSerialize(o);
                PropertyInfo k = type.GetProperty("Key");
                PropertyInfo v = type.GetProperty("Value");
                string value = $"\"0{BuildBlocks(k.PropertyType, k.GetValue(o))}1{BuildBlocks(v.PropertyType, v.GetValue(o))}\"";
                AddReference(o);
                return value;
            }
            else
            {
                return $"\"`{SameObject(o, false)}\"";
            }
        }
        protected string NullableBlock(Type type, object o)
        {
            if (o != null)
            {
                return BuildBlocks(type.GetGenericArguments()[0], o);
            }
            return null;
        }
        protected string DBNullBlock(Type type, object o)
        {
            return string.Empty;
        }

        protected virtual string CollectionBlock(Type type, object o)
        {
            XIgnoreClassAttribute ignoreClass = type.GetCustomAttribute<XIgnoreClassAttribute>(false);

            if (!ReferenceExists(o))
            {
                (o as IXObject)?.XSerialize(o);
                StringBuilder complex = new StringBuilder(64);
                int count = -1;
                foreach (PropertyInfo pi in ReflectionHelper.EachProps(o))
                {
                    ++count;
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
                        complex.Append($"{count}{value}");
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
        protected virtual string ComplexBlock(Type type, object o)
        {
            XIgnoreClassAttribute ignoreClass = type.GetCustomAttribute<XIgnoreClassAttribute>(false);

            if (!ReferenceExists(o))
            {
                (o as IXObject)?.XSerialize(o);
                StringBuilder complex = new StringBuilder(64);
                int count = -1;
                foreach (PropertyInfo pi in ReflectionHelper.EachProps(o))
                {
                    ++count;
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
                        complex.Append($"{count}{value}");
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

        protected string BuildBlocks(Type type, object o)
        {
            if (IsNullableType(type))
            {
                try { return NullableBlock(type, o); }
                catch (Exception ex) { throw new XObjectException("An error occurred while serializing Nullable type.", ex); }
            }
            else if (IsDBNullType(type))
            {
                try { return DBNullBlock(type, o); }
                catch (Exception ex) { throw new XObjectException("An error occurred while serializing DBNull type.", ex); }
            }
            else if (IsBoolType(type))
            {
                try { return BooleanBlock(type, o); }
                catch (Exception ex) { throw new XObjectException("An error occurred while serializing Boolean type.", ex); }
            }
            else if (IsNumberType(type))
            {
                try { return NumberBlock(type, o); }
                catch (Exception ex) { throw new XObjectException("An error occurred while serializing Number type.", ex); }
            }
            else if (IsStringType(type))
            {
                try { return StringBlock(type, o); }
                catch (Exception ex) { throw new XObjectException("An error occurred while serializing String type.", ex); }
            }
            else if (IsEnumType(type))
            {
                try { return EnumBlock(type, o); }
                catch (Exception ex) { throw new XObjectException("An error occurred while serializing Enum type.", ex); }
            }
            else if (IsDateTimeType(type))
            {
                try { return DateTimeBlock(type, o); }
                catch (Exception ex) { throw new XObjectException("An error occurred while serializing DateTime type.", ex); }
            }
            else if (IsKeyPairType(type))
            {
                try { return KeyPairBlock(type, o); }
                catch (Exception ex) { throw new XObjectException("An error occurred while serializing KeyPair type.", ex); }
            }
            else if (IsCollectionType(type))
            {
                try { return CollectionBlock(type, o); }
                catch (Exception ex) { throw new XObjectException("An error occurred while serializing Collection type.", ex); }
            }
            else //Complex Object
            {
                try { return ComplexBlock(type, o); }
                catch (Exception ex) { throw new XObjectException("An error occurred while serializing Object type.", ex); }
            }
        }
        internal string Build(Type type, object o)
        {
            return $"&{BuildBlocks(type, o)}";
        }
    }
}
