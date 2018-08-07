using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace XSerializer
{
    internal class Serialize<TObject> : Builder
    {
        internal Serialize()
            : base() { }

        protected string BooleanBlock(Type type,object o)
        {
            string value = ((bool)o ? 1 : 0).ToString();
            return $"'{value}'";
        }
        protected string DateTimeBlock(Type type, object o)
        {
            string value = AddProtect($"'{((DateTime)o).ToString("yyyy-MM-dd HH:mm:ss")}'");
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
                PropertyInfo k = type.GetProperty("Key");
                PropertyInfo v = type.GetProperty("Value");
                string value = $"\"0{Build(k.GetValue(o))}1{Build(v.GetValue(o))}\"";
                AddReference(o);
                return value;
            }
            else
            {
                return $"\"`{SameObject(o, false)}\"";
            }
        }
        protected string CollectionBlock(Type type, object o)
        {
            if (!ReferenceExists(o))
            {
                StringBuilder complex = new StringBuilder();
                uint count = 0;
                foreach (object op in EachHelper.EachValues(o))
                {
                    if (op == null) continue;
                    string value = Build(op);
                    if (string.IsNullOrEmpty(value)) continue;
                    complex.Append($"{count++}{value}");
                }
                foreach (object item in (o as IEnumerable))
                {
                    if (item == null) continue;
                    string value = Build(item);
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
        protected string ComplexBlock(Type type, object o)
        {
            if (!ReferenceExists(o))
            {
                StringBuilder complex = new StringBuilder();
                uint count = 0;
                foreach (object op in EachHelper.EachValues(o))
                {
                    if (op == null) continue;
                    string value = Build(op);
                    if (string.IsNullOrEmpty(value)) continue;

                    complex.Append($"{count++}{value}");
                }
                AddReference(o);
                return $"\"{complex}\"";
            }
            else
            {
                return $"\"`{SameObject(o, true)}\"";
            }
        }

        internal string Build(object o)
        {
            Type type = o.GetType();
            if (IsBoolType(type))
            {
                return BooleanBlock(type, o);
            }
            else if (IsNumberType(type))
            {
                return NumberBlock(type, o);
            }
            else if (IsStringType(type))
            {
                return StringBlock(type, o);
            }
            else if (IsEnumType(type))
            {
                return EnumBlock(type, o);
            }
            else if (IsDateTimeType(type))
            {
                return DateTimeBlock(type, o);
            }
            else if (IsKeyPairType(type))
            {
                return KeyPairBlock(type, o);
            }
            else if (IsCollectionType(type))
            {
                return CollectionBlock(type, o);
            }
            else //Complex Object
            {
                return ComplexBlock(type, o);
            }
        }

    }
}
