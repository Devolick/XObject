using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections;
using System.Diagnostics;

namespace XObjectSerializer
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
            return string.Empty;
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
            object o = Activator.CreateInstance(type);
            int count = 0;
            Regex objRx = new Regex(Queries.OBJ, RegexOptions.Compiled);
            Regex valRx = new Regex(Queries.VAL, RegexOptions.Compiled);
            MatchCollection objMatches = objRx.Matches(x);
            MatchCollection valMatches = valRx.Matches(objRx.Replace(x, string.Empty));

            foreach (PropertyInfo pi in EachHelper.EachProps(o))
            {
                string form = string.Format(Queries.PRFO, count);
                if (objMatches.Any(a => Regex.IsMatch(a, form, RegexOptions.Compiled)))
                {
                    string value = objMatches.First(f => Regex.IsMatch(f, form, RegexOptions.Compiled));
                    if (string.IsNullOrEmpty(value)) continue;
                    pi.SetValue(o, Build(pi.PropertyType, Regex.Match(value, Queries.CENO, RegexOptions.Compiled).Value));
                }
                else
                {
                    string value = valMatches.First(f => Regex.IsMatch(f, string.Format(Queries.PRFV, count), RegexOptions.Compiled));
                    if (string.IsNullOrEmpty(value)) continue;
                    pi.SetValue(o, Build(pi.PropertyType, Regex.Match(value, Queries.CENV, RegexOptions.Compiled).Value));
                }
                ++count;
            }
            return o;
        }
        private object CollectionBuilder(Type type, string x)
        {
            int count = 0;
            Regex objRx = new Regex(Queries.OBJ, RegexOptions.Compiled);
            Regex valRx = new Regex(Queries.VAL, RegexOptions.Compiled);
            MatchCollection objMatches = objRx.Matches(x);
            MatchCollection valMatches = valRx.Matches(objRx.Replace(x, string.Empty));
            bool objectItems = false;
            string anyQ = string.Format(Queries.ANY, "I");
            ProxyCollection proxyCollection = null;
            if (objMatches.Any(a => Regex.IsMatch(a, anyQ, RegexOptions.Compiled)))
            {
                objectItems = true;
                proxyCollection = new ProxyCollection(type, objMatches.Where(a => Regex.IsMatch(a, anyQ, RegexOptions.Compiled)).Count());
            }
            else if (valMatches.Any(a => Regex.IsMatch(a, anyQ, RegexOptions.Compiled)))
            {
                objectItems = false;
                proxyCollection = new ProxyCollection(type, valMatches.Where(a => Regex.IsMatch(a, anyQ, RegexOptions.Compiled)).Count());
            }

            foreach (PropertyInfo pi in EachHelper.EachProps(proxyCollection.Collection))
            {
                string form = string.Format(Queries.PRFO, count);
                if (objMatches.Any(a => Regex.IsMatch(a, form, RegexOptions.Compiled)))
                {
                    string value = objMatches.First(f => Regex.IsMatch(f, form, RegexOptions.Compiled));
                    if (string.IsNullOrEmpty(value)) continue;
                    pi.SetValue(proxyCollection.Collection, Build(pi.PropertyType, Regex.Match(value, Queries.CENO, RegexOptions.Compiled).Value));
                }
                else
                {
                    string value = valMatches.First(f => Regex.IsMatch(f, string.Format(Queries.PRFV, count), RegexOptions.Compiled));
                    if (string.IsNullOrEmpty(value)) continue;
                    pi.SetValue(proxyCollection.Collection, Build(pi.PropertyType, Regex.Match(value, Queries.CENV, RegexOptions.Compiled).Value));
                }
                ++count;
            }
            if (objectItems)
            {
                foreach (string item in objMatches.Where(w => Regex.IsMatch(w, anyQ, RegexOptions.Compiled)))
                {
                    if (string.IsNullOrEmpty(item)) continue;
                    proxyCollection.Push(Build(proxyCollection.ItemType, Regex.Match(item, Queries.CENO, RegexOptions.Compiled).Value));
                }
            }
            else
            {
                foreach (string item in valMatches.Where(w => Regex.IsMatch(w, anyQ, RegexOptions.Compiled)))
                {
                    if (string.IsNullOrEmpty(item)) continue;
                    proxyCollection.Push(Build(proxyCollection.ItemType, Regex.Match(item, Queries.CENV, RegexOptions.Compiled).Value));
                }
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
            Regex objRx = new Regex(Queries.OBJ, RegexOptions.Compiled);
            Regex valRx = new Regex(Queries.VAL, RegexOptions.Compiled);
            MatchCollection objMatches = objRx.Matches(x);
            MatchCollection valMatches = valRx.Matches(objRx.Replace(x, string.Empty));
            for (int i = 0; i < 2; i++)
            {
                string form = string.Format(Queries.PRFO, i);
                if (objMatches.Any(a => Regex.IsMatch(a, form, RegexOptions.Compiled)))
                {
                    string v = objMatches.First(f => Regex.IsMatch(f, form, RegexOptions.Compiled));
                    if (i == 0)
                    {
                        keyResult = Build(key.PropertyType, Regex.Match(v, Queries.CENO, RegexOptions.Compiled).Value);
                    }
                    else
                    {
                        valueResult = Build(value.PropertyType, Regex.Match(v, Queries.CENO, RegexOptions.Compiled).Value);
                    }
                }
                else
                {
                    string v = valMatches.First(f => Regex.IsMatch(f, string.Format(Queries.PRFV, i, RegexOptions.Compiled)));
                    if (i == 0)
                    {
                        keyResult = Build(key.PropertyType, Regex.Match(v, Queries.CENV, RegexOptions.Compiled).Value);
                    }
                    else
                    {
                        valueResult = Build(value.PropertyType, Regex.Match(v, Queries.CENV, RegexOptions.Compiled).Value);
                    }
                }
            }
            return ctorKeyValue.Invoke(new object[] { keyResult, valueResult });
        }
    }
}
