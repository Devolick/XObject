using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace XSerializer
{
    internal abstract class Builder : IDisposable
    {
        private IDictionary<int, object> references;
        private bool ignoreRootReference;

        internal Builder() {
            references = new Dictionary<int, object>(128);
            ignoreRootReference = true;
        }

        protected bool IsBoolType(Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Boolean:
                    return true;
            }
            return false;
        }
        protected bool IsKeyPairType(Type type)
        {
            if (type.IsGenericType)
            {
                return type.GetGenericTypeDefinition() != null ? type.GetGenericTypeDefinition() == typeof(KeyValuePair<,>) : false;
            }
            return false;
        }
        protected bool IsNumberType(Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Char:
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Double:
                case TypeCode.Single:
                case TypeCode.Decimal:
                    return true;
            }
            return false;
        }
        protected bool IsEnumType(Type type)
        { 
            return type.IsEnum;
        }
        protected bool IsNullableType(Type type)
        {
            return Nullable.GetUnderlyingType(type) != null;
        }
        protected bool IsStringType(Type type)
        {
            return Type.GetTypeCode(type) == TypeCode.String;
        }
        protected bool IsDateTimeType(Type type)
        {
            return Type.GetTypeCode(type) == TypeCode.DateTime;
        }
        protected bool IsDBNullType(Type type)
        {
            return Type.GetTypeCode(type) == TypeCode.DBNull;
        }
        protected bool IsEnumerableType(Type type)
        {
            return typeof(IEnumerable).IsAssignableFrom(type);
        }

        protected bool AddReference(object o, out int index)
        {
            index = -1;
            if (ignoreRootReference) { ignoreRootReference = false; return true; };
            if (!references.Any(a => a.Equals(o)))
            {
                references.Add(references.Count,o);
                index = references.Count - 1;
                return true;
            }
            return false;
        }
        protected bool AddReference(object o)
        {
            if (ignoreRootReference) { ignoreRootReference = false; return true; };
            if (!references.Any(a => a.Equals(o)))
            {
                references.Add(references.Count, o);
                return true;
            }
            return false;
        }
        protected bool AddSmartReference(object o, int length, out int index)
        {
            index = -1;
            if (ignoreRootReference) { ignoreRootReference = false; return true; };
            if (Math.Floor(Math.Log10(references.Count) + 1) >
                Math.Floor(Math.Log10(length) + 1) + 8) return true;
            if (!references.Any(a => a.Equals(o)))
            {
                references.Add(references.Count, o);
                index = references.Count - 1;
                return true;
            }
            return false;
        }
        protected void PassReference(int id, object o)
        {
            references.Add(id, o);
        }
        protected object FindReference(int id)
        {
            return references[id];
        }
        protected int SameReference(object o, bool referenceType)
        {
            if(referenceType)
            {
                int indexOf = -1;
                foreach (var item in references)
                {
                    ++indexOf;
                    if (item.Key.Equals(o)) return indexOf;
                }
                return indexOf;
            }
            else
            {
                int index = -1;
                string str = o as string;
                references.First((f) => {
                    ++index;
                    string s = f.Value as string;
                    if (str == s)
                        return true;
                    return false;
                });
                return index;
            }
        }

        protected string AddProtect(string value)
        {
            return ProtectString(value, Queries.RSTRINGS, true);
        }
        protected string RemoveProtect(string value)
        {
            return ProtectString(value, Queries.RSTRINGS, false);
        }
        private string ProtectString(string value, string[] replacement, bool protect)
        {
            if (protect)
            {
                foreach (string re in replacement)
                {
                    value = Regex.Replace(value, re, $"{re}{re}");
                }
            }
            else
            {
                foreach (string re in replacement)
                {
                    value = Regex.Replace(value, $"{re}{re}", re);
                }
            }
            return value;
        }

        public void Dispose()
        {
            
        }
    }
}
