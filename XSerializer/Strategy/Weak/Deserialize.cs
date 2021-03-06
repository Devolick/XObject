﻿using System;
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

namespace XObjectSerializer.Strategy.Weak
{
    internal class Deserialize : Builder
    {
        internal Deserialize()
        { }
        internal Deserialize(Mechanism machanism)
            : base(machanism)
        {

        }
        internal Deserialize(Mechanism machanism, string dateFormat)
            : base(machanism,dateFormat) { }
        internal Deserialize(Mechanism machanism, string dateFormat, IFormatProvider dateFormatProvider)
            : base(machanism,dateFormat, dateFormatProvider) { }

        protected object BooleanBlock(Type type, string x)
        {
            return Convert.ChangeType(x == "1", Type.GetTypeCode(type));
        }
        protected object CollectionBlock(Type type, string x)
        {
            if (Regex.IsMatch(x, Queries.INNERPOINTER))
            {
                return GetReference(int.Parse(x.Remove(0, 1)));
            }
            else
            {
                var value = CollectionBuilder(type, x);
                AddReference(value);
                (value as IXObject)?.XDeserialize(value);
                return value;
            }
        }
        protected object ComplexBlock(Type type, string x)
        {
            if (Regex.IsMatch(x, Queries.INNERPOINTER, RegexOptions.Compiled))
            {
                return GetReference(int.Parse(x.Remove(0, 1)));
            }
            else
            {
                var value = ComplexBuilder(type, x);
                AddReference(value);
                (value as IXObject)?.XDeserialize(value);
                return value;
            }
        }
        protected object DateTimeBlock(Type type, string x)
        {
            x = RemoveProtect(x);
            if (Regex.IsMatch(x, Queries.INNERPOINTER, RegexOptions.Compiled))
            {
                return Convert.ChangeType(GetReference(int.Parse(x.Remove(0, 1))), Type.GetTypeCode(type));
            }
            else
            {
                object value = null;
                if (string.IsNullOrEmpty(dateFormat))
                {
                    value = DateTime.Parse(x, dateFormatProvider);
                }
                else
                {
                    value = DateTime.ParseExact(x, dateFormat, dateFormatProvider);
                }
                AddSmartReference(x);
                return value;
            }
        }
        protected object EnumBlock(Type type, string x)
        {
            if (Regex.IsMatch(x, Queries.INNERPOINTER, RegexOptions.Compiled))
            {
                return Convert.ChangeType(GetReference(int.Parse(x.Remove(0, 1))), Type.GetTypeCode(type));
            }
            else
            {
                int en = int.Parse(x);
                AddSmartReference(x);
                return Enum.ToObject(type, en);
            }
        }
        protected object KeyPairBlock(Type type, string x)
        {
            if (Regex.IsMatch(x, Queries.INNERPOINTER, RegexOptions.Compiled))
            {
                return GetReference(int.Parse(x.Remove(0, 1)));
            }
            else
            {
                var value = KeyPairBuilder(type, x);
                AddReference(value);
                (value as IXObject)?.XDeserialize(value);
                return value;
            }
        }
        protected object NumberBlock(Type type, string x)
        {
            if (Regex.IsMatch(x, Queries.INNERPOINTER, RegexOptions.Compiled))
            {
                return Convert.ChangeType(GetReference(int.Parse(x.Remove(0, 1))), Type.GetTypeCode(type));
            }
            else
            {
                var value = Convert.ChangeType(x, Type.GetTypeCode(type));
                AddSmartReference(x);
                return value;
            }
        }
        protected object StringBlock(Type type, string x)
        {
            if (string.IsNullOrEmpty(x)) return null;
            x = RemoveProtect(x);
            if (Regex.IsMatch(x, Queries.INNERPOINTER, RegexOptions.Compiled))
            {
                return GetReference(int.Parse(x.Remove(0, 1))).ToString();
            }
            else
            {
                AddSmartReference(x);
                return x;
            }
        }
        protected object NullableBlock(Type type, string x)
        {
            if (type.IsGenericType &&
                type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return Build(type.GetGenericArguments()[0], x);
            }
            return null;
        }
        protected object DBNullBlock(Type type, string x)
        {
            return DBNull.Value;
        }

        internal object Build(Type type, string x)
        {
            if (IsNullableType(type))
            {
                try { return NullableBlock(type, x); }
                catch (Exception ex) { throw new XObjectException("An error occurred while deserializing Nullable type.", ex); }
            }
            else if (IsDBNullType(type))
            {
                try { return DBNullBlock(type, x); }
                catch (Exception ex) { throw new XObjectException("An error occurred while deserializing DBNull type.", ex); }
            }
            else if (IsBoolType(type))
            {
                try { return BooleanBlock(type, x); }
                catch (Exception ex) { throw new XObjectException("An error occurred while deserializing Boolean type.", ex); }
            }
            else if (IsNumberType(type))
            {
                try { return NumberBlock(type, x); }
                catch (Exception ex) { throw new XObjectException("An error occurred while deserializing Number type.", ex); }
            }
            else if (IsStringType(type))
            {
                try { return StringBlock(type, x); }
                catch (Exception ex) { throw new XObjectException("An error occurred while deserializing String type.", ex); }
            }
            else if (IsEnumType(type))
            {
                try { return EnumBlock(type, x); }
                catch (Exception ex) { throw new XObjectException("An error occurred while deserializing Enum type.", ex); }
            }
            else if (IsDateTimeType(type))
            {
                try { return DateTimeBlock(type, x); }
                catch (Exception ex) { throw new XObjectException("An error occurred while deserializing DateTime type.", ex); }
            }
            else if (IsKeyPairType(type))
            {
                try { return KeyPairBlock(type, x); }
                catch (Exception ex) { throw new XObjectException("An error occurred while deserializing KeyPair type.", ex); }
            }
            else if (IsCollectionType(type))
            {
                try { return CollectionBlock(type, x); }
                catch (Exception ex) { throw new XObjectException("An error occurred while deserializing Collection type.", ex); }
            }
            else //Complex Object
            {
                try { return ComplexBlock(type, x); }
                catch (Exception ex) { throw new XObjectException("An error occurred while deserializing Object type.", ex); }
            }
        }

        protected virtual object ComplexBuilder(Type type, string x)
        {
            XIgnoreClassAttribute ignoreClass = type.GetCustomAttribute<XIgnoreClassAttribute>(false);
            object o = Activator.CreateInstance(type);
            Regex rx = new Regex($"{Queries.OBJECT}|{Queries.VALUE}", RegexOptions.Compiled);
            MatchCollection matches = rx.Matches(x);

            int count = -1;
            foreach (PropertyInfo pi in ReflectionHelper.EachProps(o))
            {
                ++count;
                if (pi.GetCustomAttribute(typeof(XIgnorePropertyAttribute), false) != null ||
                    (ignoreClass != null && ignoreClass.Properties.Length > 0 && ignoreClass.Properties.Contains(pi.Name)))
                {
                    continue;
                }
                
                string value = matches.FirstDefault(w => Regex.IsMatch(w, string.Format(Queries.GETPREFIX,count), RegexOptions.Compiled));
                if (string.IsNullOrEmpty(value)) continue;
                pi.SetValue(o, Build(pi.PropertyType, Regex.Match(value, Queries.GETDATA, RegexOptions.Compiled).Value));
            }
            return o;
        }
        protected virtual object CollectionBuilder(Type type, string x)
        {
            XIgnoreClassAttribute ignoreClass = type.GetCustomAttribute<XIgnoreClassAttribute>(false);

            Regex rx = new Regex($"{Queries.OBJECT}|{Queries.VALUE}", RegexOptions.Compiled);
            MatchCollection matches = rx.Matches(x);
            string anyQ = string.Format(Queries.PREFIX, "I");
            IEnumerable<string> collectionItems = matches.Where(a => Regex.IsMatch(a, anyQ, RegexOptions.Compiled));
            ProxyCollection proxyCollection = new ProxyCollection(type, collectionItems.Count());
            int count = -1;
            foreach (PropertyInfo pi in ReflectionHelper.EachProps(proxyCollection.CollectionObject))
            {
                ++count;
                if (pi.GetCustomAttribute(typeof(XIgnorePropertyAttribute), false) != null ||
                    (ignoreClass != null && ignoreClass.Properties.Length > 0 && ignoreClass.Properties.Contains(pi.Name)))
                {
                    continue;
                }

                string value = matches.FirstDefault(w => Regex.IsMatch(w, string.Format(Queries.GETPREFIX, count), RegexOptions.Compiled));
                if (string.IsNullOrEmpty(value)) continue;
                pi.SetValue(proxyCollection.CollectionObject, Build(pi.PropertyType, Regex.Match(value, Queries.GETDATA, RegexOptions.Compiled).Value));
            }
            foreach (string item in collectionItems)
            {
                proxyCollection.Push(Build(proxyCollection.ItemType, Regex.Match(item, Queries.GETDATA, RegexOptions.Compiled).Value));
            }
            proxyCollection.CreateProxy();
            return proxyCollection.CollectionObject;
        }
        private object KeyPairBuilder(Type type, string x)
        {
            PropertyInfo key = type.GetProperty("Key");
            PropertyInfo value = type.GetProperty("Value");
            object keyResult = null;
            object valueResult = null;
            ConstructorInfo ctorKeyValue = type.GetConstructor(new[] { key.PropertyType, value.PropertyType });
            Regex rx = new Regex($"{Queries.OBJECT}|{Queries.VALUE}", RegexOptions.Compiled);
            MatchCollection matches = rx.Matches(x);
            foreach (Match m in matches)
            {
                if (Regex.IsMatch(m.Value,string.Format(Queries.PREFIX,0)))
                {
                    keyResult = Build(key.PropertyType, Regex.Match(m.Value, Queries.GETDATA, RegexOptions.Compiled).Value);
                }
                else
                {
                    valueResult = Build(value.PropertyType, Regex.Match(m.Value, Queries.GETDATA, RegexOptions.Compiled).Value);
                }
            }
            return ctorKeyValue.Invoke(new object[] { keyResult, valueResult });
        }
    }
}
