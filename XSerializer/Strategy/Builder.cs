﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using XObjectSerializer.Helpers;

namespace XObjectSerializer.Strategy
{
    internal abstract class Builder : IDisposable
    { 
        private IList<object> references;
        protected int stringPointer;
        private bool ignoreRootReference;
        protected string dateFormat;
        protected IFormatProvider dateFormatProvider;
        protected Mechanism machanism;

        internal Builder()
        {
            stringPointer = 0;
            references = new List<object>(128);
            ignoreRootReference = true;
            machanism = Mechanism.Weak;
        }
        internal Builder(Mechanism machanism)
            :this()
        {
            this.machanism = machanism;
        }
        internal Builder(Mechanism machanism, string dateFormat)
            :this(machanism)
        {
            this.dateFormat = dateFormat;
        }
        internal Builder(Mechanism machanism, string dateFormat, IFormatProvider dateFormatProvider)
            :this(machanism, dateFormat)
        {
            this.dateFormatProvider = dateFormatProvider;
        }

        #region Types
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
        #endregion

        #region References
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
            if (o == null) return;

            references.Add(o);
        }
        protected void AddSmartReference(string o)
        {
            if (string.IsNullOrEmpty(o)) return;

            if (Math.Floor(Math.Log10(references.Count) + 1) >
                Math.Floor(Math.Log10(o.Length) + 1)) return;
            references.Add(o);
        }
        protected object GetReference(int id)
        {
            return references[id];
        }
        protected int SameObject(object o, bool referenceType)
        {
            if (referenceType)
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
        protected IEnumerable<string> EachStringReferences()
        {
            foreach (var item in references)
            {
                string value = (item as string);
                if (!string.IsNullOrEmpty(value))
                    yield return value;
            }
        }
        #endregion

        #region String Protect
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
                    value = Regex.Replace(value, re, $"{re}{re}", RegexOptions.Compiled);
                }
            }
            else
            {
                foreach (string re in replacement)
                {
                    value = Regex.Replace(value, $"{re}{re}", re, RegexOptions.Compiled);
                }
            }
            return value;
        }
        #endregion

        #region Strong Type
        protected string GeneratePropertyKey(string propertyName)
        {
            int key = 0;
            unchecked
            {
                int hash1 = 5381;
                int hash2 = hash1;

                for (int i = 0; i < propertyName.Length && propertyName[i] != '\0'; i += 2)
                {
                    hash1 = ((hash1 << 5) + hash1) ^ propertyName[i];
                    if (i == propertyName.Length - 1 || propertyName[i + 1] == '\0')
                        break;
                    hash2 = ((hash2 << 5) + hash2) ^ propertyName[i + 1];
                }

                key = hash1 + (hash2 * 1566083941);
            }
            return key.ToString("X");
        }
        protected bool EqualsPropertyKey(string hexKey, string propertyName)
        {
            return GeneratePropertyKey(propertyName) == hexKey;
        }
        #endregion

        internal object Clone(Type type, object o)
        {
            using (Strategy.Weak.Serialize serialize = new Strategy.Weak.Serialize(Mechanism.Weak))
            {
                using (Strategy.Weak.Deserialize deserialize = new Strategy.Weak.Deserialize(Mechanism.Weak))
                {
                    return deserialize.Build(type, serialize.Build(type, o));
                }
            }
        }

        public void Dispose()
        {
            
        }
    }
}
