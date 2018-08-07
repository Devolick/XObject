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

        internal string Build(object o)
        {
            Type type = o.GetType();
            if (IsBoolType(type))
            {
                string value = ((bool)o ? 1 : 0).ToString();
                int index;
                if (AddSmartReference(value, value.Length, out index))
                {
                    return index > -1 ? $"`{index}'{value}'" : $"'{value}'";
                }
                else
                {
                    return $"'`{SameReference(value, false)}'";
                }
            }
            else if (IsNumberType(type))
            {
                string value = AddProtect(o.ToString());
                int index;
                if (AddSmartReference(value, value.Length, out index))
                {
                    return index > -1 ? $"`{index}'{value}'" : $"'{value}'";
                }
                else
                {
                    return $"'`{SameReference(value,false)}'";
                }
            }
            else if (IsStringType(type))
            {
                string value = AddProtect((string)o);
                int index;
                if (AddSmartReference(value, value.Length, out index))
                {
                    return index > -1 ? $"`{index}'{value}'" : $"'{value}'";
                }
                else
                {
                    return $"'`{SameReference(value, false)}'";
                }
            }
            else if (IsEnumType(type))
            {
                string value = AddProtect(((int)o).ToString());
                int index;
                if (AddSmartReference(value, value.Length, out index))
                {
                    return index > -1 ? $"`{index}'{value}'" : $"'{value}'";
                }
                else
                {
                    return $"'`{SameReference(value, false)}'";
                }
            }
            else if (IsDateTimeType(type))
            {
                int index;
                string value = AddProtect($"'{((DateTime)o).ToString("yyyy-MM-dd HH:mm:ss")}'");
                if (AddSmartReference(value, value.Length, out index))
                {
                    return index > -1 ? $"`{index}'{value}'" : $"'{value}'";
                }
                else
                {
                    return $"'`{SameReference(value, false)}'";
                }
            }
            else if (IsKeyPairType(type))
            {
                int index;
                if (AddReference(o, out index))
                {
                    PropertyInfo key = type.GetProperty("Key");
                    PropertyInfo value = type.GetProperty("Value");
                    return index > -1 ? 
                        $"`{index}\"0{Build(key.GetValue(o))}1{Build(value.GetValue(o))}\"" :
                        $"\"0{Build(key.GetValue(o))}1{Build(value.GetValue(o))}\"";
                }
                else
                {
                    return $"\"`{SameReference(o, false)}\"";
                }
            }
            //else if (IsDBNullType(type))
            //{

            //}
            //else if (IsNullableType(type))
            //{

            //}
            else if (IsEnumerableType(type))
            {
                int index;
                if (AddReference(o, out index))
                {
                    StringBuilder complex = new StringBuilder();
                    uint count = 0;
                    foreach (object op in EachHelper.EachValues(o))
                    {
                        complex.Append($"{count++}{Build(op)}");
                    }
                    foreach (object item in (o as IEnumerable))
                    {
                        complex.Append($"I{Build(item)}");
                    }
                    return index > -1 ? $"`{index}\"{complex}\"" : $"\"{complex}\"";
                }
                else
                {
                    return $"\"`{SameReference(o, true)}\"";
                }
            }
            else //Complex Object
            {
                int index;
                if (AddReference(o, out index))
                {
                    StringBuilder complex = new StringBuilder();
                    uint count = 0;
                    foreach (object op in EachHelper.EachValues(o))
                    {
                        complex.Append($"{count++}{Build(op)}");
                    }
                    return index > -1 ? $"`{index}\"{complex}\"" : $"\"{complex}\"";
                }
                else
                {
                    return $"\"`{SameReference(o, true)}\"";
                }
            }
        }
    }
}
