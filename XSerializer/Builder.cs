using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace XSerializer
{
    internal abstract class Builder : IDisposable
    {
        private IList<object> references;
        private bool ignoreRootReference;

        internal Builder()
        {
            references = new List<object>(128);
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
        protected bool IsCollectionType(Type type)
        {
            return typeof(IEnumerable).IsAssignableFrom(type);
        }

        protected bool ReferenceExists(object o)
        {
            if (ignoreRootReference) { ignoreRootReference = false; return false; };
            if (references.Any(a => a.Equals(o)))
            {
                return true;
            }
            return false;
        }
        protected bool SmartReferenceExists(object o)
        {
            if (ignoreRootReference) { ignoreRootReference = false; return false; };
            string value = o.ToString();

            if (references.Any(a => {
                if ((a as string) != null)
                {
                    return (string)a == value;
                }
                return false;
            }))
            {
                return true;
            }
            return false;
        }
        protected void AddReference(object o)
        {
            Debug.WriteLine($"AddReference Index:{references.Count},Type:{o.GetType().FullName},ToString:{o.ToString()}");
            references.Add(o);
        }
        protected void AddSmartReference(string o)
        {
            if (Math.Floor(Math.Log10(references.Count) + 1) >
                Math.Floor(Math.Log10(o.Length) + 1)) return;
            Debug.WriteLine($"AddSmartReference Index:{references.Count},Type:{o.GetType().FullName},ToString:{o.ToString()}");
            references.Add(o);
        }
        protected object GetReference(int id)
        {
            return references[id];
        }
        protected int SameObject(object o, bool referenceType)
        {
            if(referenceType)
            {
                return references.IndexOf(o);
            }
            else
            {
                int index = -1;
                string str = o as string;
                references.First((f) => {
                    ++index;
                    string s = f as string;
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
