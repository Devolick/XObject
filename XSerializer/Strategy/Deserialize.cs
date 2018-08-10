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

namespace XObjectSerializer.Strategy
{
    internal class Deserialize : Builder
    {
        internal Deserialize()
        { }
        internal Deserialize(string dateFormat)
            : base(dateFormat) { }
        internal Deserialize(string dateFormat, IFormatProvider dateFormatProvider)
            : base(dateFormat, dateFormatProvider) { }

        protected object BooleanBlock(Type type, string x)
        {
            return Convert.ChangeType(x == "1", Type.GetTypeCode(type));
        }
        protected object CollectionBlock(Type type, string x)
        {
            if (Regex.IsMatch(x, Queries.IPNT))
            {
                return GetReference(int.Parse(x.Remove(0, 1)), true);
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
            if (Regex.IsMatch(x, Queries.IPNT, RegexOptions.Compiled))
            {
                return GetReference(int.Parse(x.Remove(0, 1)), true);
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
            if (Regex.IsMatch(x, Queries.IPNT, RegexOptions.Compiled))
            {
                return Convert.ChangeType(GetReference(int.Parse(x.Remove(0, 1)), false), Type.GetTypeCode(type));
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
            if (Regex.IsMatch(x, Queries.IPNT, RegexOptions.Compiled))
            {
                return Convert.ChangeType(GetReference(int.Parse(x.Remove(0, 1)), false), Type.GetTypeCode(type));
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
            if (Regex.IsMatch(x, Queries.IPNT, RegexOptions.Compiled))
            {
                return GetReference(int.Parse(x.Remove(0, 1)), true);
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
            if (Regex.IsMatch(x, Queries.IPNT, RegexOptions.Compiled))
            {
                return Convert.ChangeType(GetReference(int.Parse(x.Remove(0, 1)), false), Type.GetTypeCode(type));
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
            if (Regex.IsMatch(x, Queries.IPNT, RegexOptions.Compiled))
            {
                return GetReference(int.Parse(x.Remove(0, 1)), false).ToString();
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

        private object ComplexBuilder(Type type, string x)
        {
            XIgnoreClassAttribute ignoreClass = type.GetCustomAttribute<XIgnoreClassAttribute>(false);
            object o = Activator.CreateInstance(type);
            Regex rx = new Regex($"{Queries.OBJ}|{Queries.VAL}", RegexOptions.Compiled);
            MatchCollection matches = rx.Matches(x);

            int count = -1;
            foreach (PropertyInfo pi in EachHelper.EachProps(o))
            {
                ++count;
                if (pi.GetCustomAttribute(typeof(XIgnorePropertyAttribute), false) != null ||
                    (ignoreClass != null && ignoreClass.Properties.Length > 0 && ignoreClass.Properties.Contains(pi.Name)))
                {
                    continue;
                }

                if (matches.Count <= count) break;
                string value = matches[count].Value;
                if (!Regex.IsMatch(value, string.Format(Queries.PRF, count), RegexOptions.Compiled)) continue;
                pi.SetValue(o, Build(pi.PropertyType, Regex.Match(value, Queries.CENT, RegexOptions.Compiled).Value));
            }
            return o;
        }
        private object CollectionBuilder(Type type, string x)
        {
            XIgnoreClassAttribute ignoreClass = type.GetCustomAttribute<XIgnoreClassAttribute>(false);

            Regex rx = new Regex($"{Queries.OBJ}|{Queries.VAL}", RegexOptions.Compiled);
            MatchCollection matches = rx.Matches(x);
            string anyQ = string.Format(Queries.ANY, "I");
            IEnumerable<string> collectionItems = matches.Where(a => Regex.IsMatch(a, anyQ, RegexOptions.Compiled));
            ProxyCollection proxyCollection = new ProxyCollection(type, collectionItems.Count());
            int count = -1;
            foreach (PropertyInfo pi in EachHelper.EachProps(proxyCollection.Collection))
            {
                ++count;
                if (pi.GetCustomAttribute(typeof(XIgnorePropertyAttribute), false) != null ||
                    (ignoreClass != null && ignoreClass.Properties.Length > 0 && ignoreClass.Properties.Contains(pi.Name)))
                {
                    continue;
                }

                if (matches.Count <= count) break;
                string value = matches[count].Value;
                if (!Regex.IsMatch(value, string.Format(Queries.PRF, count), RegexOptions.Compiled)) continue;
                pi.SetValue(proxyCollection.Collection, Build(pi.PropertyType, Regex.Match(value, Queries.CENT, RegexOptions.Compiled).Value));
            }
            foreach (string item in collectionItems)
            {
                proxyCollection.Push(Build(proxyCollection.ItemType, Regex.Match(item, Queries.CENT, RegexOptions.Compiled).Value));
            }
            proxyCollection.CreateProxy();
            return proxyCollection.Collection;
        }
        private object KeyPairBuilder(Type type, string x)
        {
            PropertyInfo key = type.GetProperty("Key");
            PropertyInfo value = type.GetProperty("Value");
            object keyResult = null;
            object valueResult = null;
            ConstructorInfo ctorKeyValue = type.GetConstructor(new[] { key.PropertyType, value.PropertyType });
            Regex rx = new Regex($"{Queries.OBJ}|{Queries.VAL}", RegexOptions.Compiled);
            MatchCollection matches = rx.Matches(x);
            foreach (Match m in matches)
            {
                if (Regex.IsMatch(m.Value,string.Format(Queries.PRF,0)))
                {
                    keyResult = Build(key.PropertyType, Regex.Match(m.Value, Queries.CENT, RegexOptions.Compiled).Value);
                }
                else
                {
                    valueResult = Build(value.PropertyType, Regex.Match(m.Value, Queries.CENT, RegexOptions.Compiled).Value);
                }
            }
            return ctorKeyValue.Invoke(new object[] { keyResult, valueResult });
        }
    }
}
