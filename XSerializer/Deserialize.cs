using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections;
using System.Diagnostics;

namespace XSerializer
{
    internal class Deserialize<TObject> : Builder
    {
        internal Deserialize()
             : base() {
        }

        protected object BooleanBlock(Type type, string x)
        {
            x = RemoveProtect(x);
            if (Regex.IsMatch(x, Queries.IPNT))
            {
                return Convert.ChangeType(GetReference(int.Parse(x.Remove(0, 1))).ToString() == "1", Type.GetTypeCode(type));
            }
            else
            {
                return Convert.ChangeType(x == "1", Type.GetTypeCode(type));
            }
        }
        protected object CollectionBlock(Type type, string x)
        {
            if (Regex.IsMatch(x, Queries.IPNT))
            {
                return GetReference(int.Parse(x.Remove(0, 1)));
            }
            else
            {
                var value = CollectionBuilder(type, x);
                return value;
            }
        }
        protected object ComplexBlock(Type type, string x)
        {
            if (Regex.IsMatch(x, Queries.IPNT))
            {
                return GetReference(int.Parse(x.Remove(0, 1)));
            }
            else
            {
                var value = ComplexBuilder(type, x);
                return value;
            }
        }
        protected object DateTimeBlock(Type type, string x)
        {
            x = RemoveProtect(x);
            if (Regex.IsMatch(x, Queries.IPNT))
            {
                return Convert.ChangeType(GetReference(int.Parse(x.Remove(0, 1))), Type.GetTypeCode(type));
            }
            else
            {
                object value = Convert.ChangeType(x, Type.GetTypeCode(type));
                AddSmartReference(x);
                return value;
            }
        }
        protected object EnumBlock(Type type, string x)
        {
            x = RemoveProtect(x);
            if (Regex.IsMatch(x, Queries.IPNT))
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
            if (Regex.IsMatch(x, Queries.IPNT))
            {
                return GetReference(int.Parse(x.Remove(0, 1)));
            }
            else
            {
                var value = KeyPairBuilder(type, x);
                return value;
            }
        }
        protected object NumberBlock(Type type, string x)
        {
            x = RemoveProtect(x);
            if (Regex.IsMatch(x, Queries.IPNT))
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
            x = RemoveProtect(x);
            if (Regex.IsMatch(x, Queries.IPNT))
            {
                return Convert.ChangeType(GetReference(int.Parse(x.Remove(0, 1))), Type.GetTypeCode(type));
            }
            else
            {
                AddSmartReference(x);
                return x;
            }
        }
        
        internal object Build(Type type, string x)
        {
            if (IsBoolType(type))
            {
                return BooleanBlock(type, x);
            }
            else if (IsNumberType(type))
            {
                return NumberBlock(type, x);
            }
            else if (IsStringType(type))
            {
                return StringBlock(type, x);
            }
            else if (IsEnumType(type))
            {
                return EnumBlock(type, x);
            }
            else if (IsDateTimeType(type))
            {
                return DateTimeBlock(type, x);
            }
            else if (IsKeyPairType(type))
            {
                return KeyPairBlock(type, x);
            }
            else if (IsCollectionType(type))
            {
                return CollectionBlock(type, x);
            }
            else //Complex Object
            {
                return ComplexBlock(type, x);
            }
        }

        private object ComplexBuilder(Type type, string x)
        {
            object o = Activator.CreateInstance(type);
            int count = 0;
            Regex objRx = new Regex(Queries.OBJ);
            Regex valRx = new Regex(Queries.VAL);
            MatchCollection objMatches = objRx.Matches(x);
            MatchCollection valMatches = valRx.Matches(objRx.Replace(x, string.Empty));

            foreach (PropertyInfo pi in EachHelper.EachProps(o))
            {
                string form = string.Format(Queries.PRFO, count);
                if (objMatches.Any(a => Regex.IsMatch(a, form)))
                {
                    string value = objMatches.First(f => Regex.IsMatch(f, form));
                    pi.SetValue(o, Build(pi.PropertyType, Regex.Match(value, Queries.CENO).Value));
                }
                else
                {
                    string value = valMatches.First(f => Regex.IsMatch(f, string.Format(Queries.PRFV, count)));
                    pi.SetValue(o, Build(pi.PropertyType, Regex.Match(value, Queries.CENV).Value));
                }
                ++count;
            }
            AddReference(o);
            return o;
        }
        private object CollectionBuilder(Type type, string x)
        {
            int count = 0;
            Regex objRx = new Regex(Queries.OBJ);
            Regex valRx = new Regex(Queries.VAL);
            MatchCollection objMatches = objRx.Matches(x);
            MatchCollection valMatches = valRx.Matches(objRx.Replace(x, string.Empty));
            bool objectItems = false;
            string anyQ = string.Format(Queries.ANY, "I");
            ProxyCollection proxyCollection = null;
            if (objMatches.Any(a => Regex.IsMatch(a, anyQ)))
            {
                objectItems = true;
                proxyCollection = new ProxyCollection(type, objMatches.Where(a => Regex.IsMatch(a, anyQ)).Count());
            }
            else if (valMatches.Any(a => Regex.IsMatch(a, anyQ)))
            {
                objectItems = false;
                proxyCollection = new ProxyCollection(type, valMatches.Where(a => Regex.IsMatch(a, anyQ)).Count());
            }

            foreach (PropertyInfo pi in EachHelper.EachProps(proxyCollection.Collection))
            {
                string form = string.Format(Queries.PRFO, count);
                if (objMatches.Any(a => Regex.IsMatch(a, form)))
                {
                    string value = objMatches.First(f => Regex.IsMatch(f, form));
                    pi.SetValue(proxyCollection.Collection, Build(pi.PropertyType, Regex.Match(value, Queries.CENO).Value));
                }
                else
                {
                    string value = valMatches.First(f => Regex.IsMatch(f, string.Format(Queries.PRFV, count)));
                    pi.SetValue(proxyCollection.Collection, Build(pi.PropertyType, Regex.Match(value, Queries.CENV).Value));
                }
                ++count;
            }
            if (objectItems)
            {
                foreach (string item in objMatches.Where(w => Regex.IsMatch(w, anyQ)))
                {
                    proxyCollection.Push(Build(proxyCollection.ItemType, Regex.Match(item, Queries.CENO).Value));
                }
            }
            else
            {
                foreach (string item in valMatches.Where(w => Regex.IsMatch(w, anyQ)))
                {
                    proxyCollection.Push(Build(proxyCollection.ItemType, Regex.Match(item, Queries.CENV).Value));
                }
            }
            proxyCollection.CreateProxy();
            AddReference(proxyCollection.Collection);
            return proxyCollection.Collection;
        }
        private object KeyPairBuilder(Type type, string x)
        {
            PropertyInfo key = type.GetProperty("Key");
            PropertyInfo value = type.GetProperty("Value");
            object keyResult = null;
            object valueResult = null;
            ConstructorInfo ctorKeyValue = type.GetConstructor(new[] { key.PropertyType, value.PropertyType });
            Regex objRx = new Regex(Queries.OBJ);
            Regex valRx = new Regex(Queries.VAL);
            MatchCollection objMatches = objRx.Matches(x);
            MatchCollection valMatches = valRx.Matches(objRx.Replace(x, string.Empty));
            for (int i = 0; i < 2; i++)
            {
                string form = string.Format(Queries.PRFO, i);
                if (objMatches.Any(a => Regex.IsMatch(a, form)))
                {
                    string v = objMatches.First(f => Regex.IsMatch(f, form));
                    if (i == 0)
                    {
                        keyResult = Build(key.PropertyType, Regex.Match(v, Queries.CENO).Value);
                    }
                    else
                    {
                        valueResult = Build(value.PropertyType, Regex.Match(v, Queries.CENO).Value);
                    }
                }
                else
                {
                    string v = valMatches.First(f => Regex.IsMatch(f, string.Format(Queries.PRFV, i)));
                    if (i == 0)
                    {
                        keyResult = Build(key.PropertyType, Regex.Match(v, Queries.CENV).Value);
                    }
                    else
                    {
                        valueResult = Build(value.PropertyType, Regex.Match(v, Queries.CENV).Value);
                    }
                }
            }
            var keyPair = ctorKeyValue.Invoke(new object[] { keyResult, valueResult });
            AddReference(keyPair);
            return keyPair;
        }
    }
}
