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

        internal object Build(Type type, string x)
        {

            if (IsBoolType(type))
            {
                x = RemoveProtect(x);
                if (Regex.IsMatch(x, Queries.IPNT))
                {
                    return Convert.ChangeType(FindReference(int.Parse(x.Remove(0, 1))).ToString() == "1", Type.GetTypeCode(type));
                }
                else
                {
                    if (Regex.IsMatch(x, Queries.OPNT))
                    {
                        string refX = Regex.Match(x, Queries.OPNTVV).Value;
                        PassReference(int.Parse(Regex.Match(x, Queries.OPNTV).Value), refX);
                        return Convert.ChangeType(refX == "1", Type.GetTypeCode(type));
                    }
                    else
                    {
                        return Convert.ChangeType(x == "1", Type.GetTypeCode(type));
                    }
                }
            }
            else if (IsNumberType(type))
            {
                x = RemoveProtect(x);
                if(Regex.IsMatch(x, Queries.IPNT))
                {
                    return Convert.ChangeType(FindReference(int.Parse(x.Remove(0, 1))), Type.GetTypeCode(type));
                }
                else
                {
                    if (Regex.IsMatch(x, Queries.OPNT))
                    {
                        string refX = Regex.Match(x, Queries.OPNTVV).Value;
                        PassReference(int.Parse(Regex.Match(x, Queries.OPNTV).Value), refX);
                        return Convert.ChangeType(refX, Type.GetTypeCode(type));
                    }
                    else
                    {
                        return Convert.ChangeType(x, Type.GetTypeCode(type));
                    }
                }
            }
            else if (IsStringType(type))
            {
                x = RemoveProtect(x);
                if (Regex.IsMatch(x, Queries.IPNT))
                {
                    return Convert.ChangeType(FindReference(int.Parse(x.Remove(0, 1))), Type.GetTypeCode(type));
                }
                else
                {
                    if (Regex.IsMatch(x, Queries.OPNT))
                    {
                        string refX = Regex.Match(x, Queries.OPNTVV).Value;
                        PassReference(int.Parse(Regex.Match(x, Queries.OPNTV).Value), refX);
                        return refX;
                    }
                    else
                    {
                        return x;
                    }
                }
            }
            else if (IsEnumType(type))
            {
                x = RemoveProtect(x);
                if (Regex.IsMatch(x, Queries.IPNT))
                {
                    return Convert.ChangeType(FindReference(int.Parse(x.Remove(0, 1))), Type.GetTypeCode(type));
                }
                else
                {
                    
                    if (Regex.IsMatch(x, Queries.OPNT))
                    {
                        string refX = Regex.Match(x, Queries.OPNTVV).Value;
                        PassReference(int.Parse(Regex.Match(x, Queries.OPNTV).Value), refX);
                        int en = int.Parse(refX);
                        return Enum.ToObject(type, en);
                    }
                    else
                    {
                        int en = int.Parse(x);
                        return Enum.ToObject(type, en);
                    }
                }
            }
            else if (IsDateTimeType(type))
            {
                x = RemoveProtect(x);
                if (Regex.IsMatch(x, Queries.IPNT))
                {
                    return Convert.ChangeType(FindReference(int.Parse(x.Remove(0, 1))), Type.GetTypeCode(type));
                }
                else
                {
                    if (Regex.IsMatch(x, Queries.OPNT))
                    {
                        string refX = Regex.Match(x, Queries.OPNTVV).Value;
                        PassReference(int.Parse(Regex.Match(x, Queries.OPNTV).Value), refX);
                        object value = Convert.ChangeType(refX, Type.GetTypeCode(type));
                        return value;
                    }
                    else
                    {
                        object value = Convert.ChangeType(x, Type.GetTypeCode(type));
                        return value;
                    }
                }
            }
            else if (IsKeyPairType(type))
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
                        if(i == 0)
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
                return ctorKeyValue.Invoke(new object[] { keyResult, valueResult });
            }
            //else if (IsDBNullType(type))
            //{

            //}
            //else if (IsNullableType(type))
            //{

            //}
            else if (IsEnumerableType(type))
            {
                if (Regex.IsMatch(x, Queries.IPNT))
                {
                    return FindReference(int.Parse(x.Remove(0, 1)));
                }
                else
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
                    return proxyCollection.Collection;
                }
            }
            else //Complex Object
            {
                if (Regex.IsMatch(x, Queries.IPNT))
                {
                    return FindReference(int.Parse(x.Remove(0, 1)));
                }
                else
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
                            pi.SetValue(o, Build(pi.PropertyType, Regex.Match(value,Queries.CENO).Value));
                        }
                        else
                        {
                            string value = valMatches.First(f => Regex.IsMatch(f, string.Format(Queries.PRFV, count)));
                            pi.SetValue(o, Build(pi.PropertyType, Regex.Match(value, Queries.CENV).Value));
                        }
                        ++count;
                    }
                    return o;
                }
            }
        }
    }
}
