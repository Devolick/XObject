using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using XObjectSerializer.Exceptions;
using XObjectSerializer.Helpers;
using XObjectSerializer.Strategy;
using XObjectSerializer.Tools;

namespace XObjectSerializer
{
    /// <summary>
    /// Serialization class
    /// </summary>
    public static class XObject
    {
        /// <summary>
        /// Serializes the specified object
        /// </summary>
        /// <param name="o">The object to serialize</param>
        /// <param name="mechanism">Indicates how the serialization works</param> 
        /// <returns>Serialized string</returns>
        public static string XSerialize(object o, Mechanism mechanism)
        {
            if (o == null) throw new XObjectException("An empty object can not be serialized.");

            if(mechanism == Mechanism.Strong)
            {
                using (Strategy.Strong.Serialize serialize = new Strategy.Strong.Serialize(mechanism))
                {
                    return serialize.Build(o.GetType(), o);
                }
            }
            else
            {
                return XSerialize(o);
            }
        }
        /// <summary>
        /// Serializes the specified object
        /// </summary>
        /// <param name="o">The object to serialize</param>
        /// <param name="mechanism">Indicates how the serialization works</param>  
        /// <param name="dateFormat">For any DateTime value</param>
        /// <returns>Serialized string</returns>
        public static string XSerialize(object o, Mechanism mechanism, string dateFormat)
        {
            if (o == null) throw new XObjectException("An empty object can not be serialized.");

            if (mechanism == Mechanism.Strong)
            {
                using (Strategy.Strong.Serialize serialize = new Strategy.Strong.Serialize(mechanism, dateFormat))
                {
                    return serialize.Build(o.GetType(), o);
                }
            }
            else
            {
                return XSerialize(o, dateFormat);
            }
        }
        /// <summary>
        /// Serializes the specified object
        /// </summary>
        /// <param name="o">The object to serialize</param>
        /// <param name="mechanism">Indicates how the serialization works</param>  
        /// <param name="dateFormat">Format for any DateTime value</param>
        /// <param name="dateFormatProvider">IFormatProvider for any DateTime value</param>
        /// <returns>Serialized string</returns>
        public static string XSerialize(object o, Mechanism mechanism, string dateFormat, IFormatProvider dateFormatProvider)
        {
            if (o == null) throw new XObjectException("An empty object can not be serialized.");

            if (mechanism == Mechanism.Strong)
            {
                using (Strategy.Strong.Serialize serialize = new Strategy.Strong.Serialize(mechanism, dateFormat, dateFormatProvider))
                {
                    return serialize.Build(o.GetType(), o);
                }
            }
            else
            {
                return XSerialize(o, dateFormat, dateFormatProvider);
            }
        }
        /// <summary>
        /// Serializes the specified object
        /// </summary>
        /// <param name="o">The object to serialize</param>
        /// <returns>Serialized string</returns>
        [Obsolete("Use a similar method with a parameter Machanism")]
        public static string XSerialize(object o)
        {
            if (o == null) throw new XObjectException("An empty object can not be serialized.");

            using (Strategy.Weak.Serialize serialize = new Strategy.Weak.Serialize(Mechanism.Weak))
            {
                return serialize.Build(o.GetType(), o);
            }
        }
        /// <summary>
        /// Serializes the specified object
        /// </summary>
        /// <param name="o">The object to serialize</param>
        /// <param name="dateFormat">For any DateTime value</param>
        /// <returns>Serialized string</returns>
        [Obsolete("Use a similar method with a parameter Machanism")]
        public static string XSerialize(object o, string dateFormat)
        {
            if (o == null) throw new XObjectException("An empty object can not be serialized.");

            using (Strategy.Weak.Serialize serialize = new Strategy.Weak.Serialize(Mechanism.Weak, dateFormat))
            {
                return serialize.Build(o.GetType(), o);
            }
        }
        /// <summary>
        /// Serializes the specified object
        /// </summary>
        /// <param name="o">The object to serialize</param>
        /// <param name="dateFormat">Format for any DateTime value</param>
        /// <param name="dateFormatProvider">IFormatProvider for any DateTime value</param>
        /// <returns>Serialized string</returns>
        [Obsolete("Use a similar method with a parameter Machanism")]
        public static string XSerialize(object o, string dateFormat, IFormatProvider dateFormatProvider)
        {
            if (o == null) throw new XObjectException("An empty object can not be serialized.");

            using (Strategy.Weak.Serialize serialize = new Strategy.Weak.Serialize(Mechanism.Weak, dateFormat, dateFormatProvider))
            {
                return serialize.Build(o.GetType(), o);
            }
        }
        /// <summary>
        /// Deserializes the specified string
        /// </summary>
        /// <param name="x">The string to deserialize</param>
        /// <param name="mechanism">Indicates how the deserialization works</param>   
        /// <returns>Deserialized object</returns>
        public static TObject XDeserialize<TObject>(string x, Mechanism mechanism)
        {
            if (x == null) throw new XObjectException("An empty string can not be deserialized.");

            if(mechanism == Mechanism.Strong)
            {
                using (Strategy.Strong.Deserialize deserialize = new Strategy.Strong.Deserialize(mechanism))
                {
                    return (TObject)deserialize.Build(typeof(TObject), Regex.Match(x, Queries.BODY, RegexOptions.Compiled).Value);
                }
            }
            else
            {
                return XDeserialize<TObject>(x);
            }
        }
        /// <summary>
        /// Deserializes the specified string
        /// </summary>
        /// <param name="x">The string to deserialize</param>
        /// <param name="mechanism">Indicates how the deserialization works</param>   
        /// <param name="dateFormat">Format for any DateTime value</param>
        /// <returns>Deserialized object</returns>
        public static TObject XDeserialize<TObject>(string x, Mechanism mechanism, string dateFormat)
        {
            if (x == null) throw new XObjectException("An empty string can not be deserialized.");

            if (mechanism == Mechanism.Strong)
            {
                using (Strategy.Strong.Deserialize deserialize = new Strategy.Strong.Deserialize(mechanism, dateFormat))
                {
                    return (TObject)deserialize.Build(typeof(TObject), Regex.Match(x, Queries.BODY, RegexOptions.Compiled).Value);
                }
            }
            else
            {
                return XDeserialize<TObject>(x, dateFormat);
            }
        }
        /// <summary>
        /// Deserializes the specified string
        /// </summary>
        /// <param name="x">The string to deserialize</param>
        /// <param name="mechanism">Indicates how the deserialization works</param>   
        /// <param name="dateFormat">Format for any DateTime value</param>
        /// <param name="dateFormatProvider">IFormatProvider for any DateTime value</param>
        /// <returns>Deserialized object</returns>
        public static TObject XDeserialize<TObject>(string x, Mechanism mechanism, string dateFormat, IFormatProvider dateFormatProvider)
        {
            if (x == null) throw new XObjectException("An empty string can not be deserialized.");

            if (mechanism == Mechanism.Strong)
            {
                using (Strategy.Strong.Deserialize deserialize = new Strategy.Strong.Deserialize(mechanism, dateFormat, dateFormatProvider))
                {
                    return (TObject)deserialize.Build(typeof(TObject), Regex.Match(x, Queries.BODY, RegexOptions.Compiled).Value);
                }
            }
            else
            {
                return XDeserialize<TObject>(x, dateFormat, dateFormatProvider);
            }
        }
        /// <summary>
        /// Deserializes the specified string
        /// </summary>
        /// <param name="x">The string to deserialize</param>   
        /// <returns>Deserialized object</returns>
        [Obsolete("Use a similar method with a parameter Machanism")]
        public static TObject XDeserialize<TObject>(string x)
        {
            if (x == null) throw new XObjectException("An empty string can not be deserialized.");

            using (Strategy.Weak.Deserialize deserialize = new Strategy.Weak.Deserialize(Mechanism.Weak))
            {
                return (TObject)deserialize.Build(typeof(TObject), Regex.Match(x, Queries.BODY, RegexOptions.Compiled).Value);
            }
        }
        /// <summary>
        /// Deserializes the specified string
        /// </summary>
        /// <param name="x">The string to deserialize</param>
        /// <param name="dateFormat">Format for any DateTime value</param>
        /// <returns>Deserialized object</returns>
        [Obsolete("Use a similar method with a parameter Machanism")]
        public static TObject XDeserialize<TObject>(string x, string dateFormat)
        {
            if (x == null) throw new XObjectException("An empty string can not be deserialized.");

            using (Strategy.Weak.Deserialize deserialize = new Strategy.Weak.Deserialize(Mechanism.Weak, dateFormat))
            {
                return (TObject)deserialize.Build(typeof(TObject), Regex.Match(x, Queries.BODY, RegexOptions.Compiled).Value);
            }
        }
        /// <summary>
        /// Deserializes the specified string
        /// </summary>
        /// <param name="x">The string to deserialize</param>
        /// <param name="dateFormat">Format for any DateTime value</param>
        /// <param name="dateFormatProvider">IFormatProvider for any DateTime value</param>
        /// <returns>Deserialized object</returns>
        [Obsolete("Use a similar method with a parameter Machanism")]
        public static TObject XDeserialize<TObject>(string x, string dateFormat, IFormatProvider dateFormatProvider)
        {
            if (x == null) throw new XObjectException("An empty string can not be deserialized.");

            using (Strategy.Weak.Deserialize deserialize = new Strategy.Weak.Deserialize(Mechanism.Weak, dateFormat, dateFormatProvider))
            {
                return (TObject)deserialize.Build(typeof(TObject), Regex.Match(x, Queries.BODY, RegexOptions.Compiled).Value);
            }
        }

        /// <summary>
        /// Validates the integrity of incoming data.
        /// </summary>
        /// <param name="x">serialized string</param>
        /// <returns>Returns true if is valid</returns>
        public static bool IsValid(string x)
        {
            if (string.IsNullOrEmpty(x)) throw new XObjectException("Invalid empty string.");
            return Regex.IsMatch(x, Queries.VALID,RegexOptions.Compiled);
        }
        /// <summary>
        /// Produces cloning of this object. You must follow the serialization rules.
        /// </summary>
        /// <param name="o">Object for clone</param>
        /// <returns>Returns cloned object</returns>
        public static TClone Clone<TClone>(TClone o)
        {
            if (o == null) throw new XObjectException("It is not permissible to clone a null reference.");
            string serialize = XObject.XSerialize(o, Mechanism.Weak);
            return XObject.XDeserialize<TClone>(serialize, Mechanism.Weak);
        }
        /// <summary>
        /// Pass the property value to the object in the specified property path.
        /// </summary>
        /// <param name="target">The object to modify the properties.</param>
        /// <param name="value">Transmit value along the way.</param>
        /// <param name="dotPath">Property search path.</param>
        /// <returns>Indicates the success of the operation.</returns>
        public static bool Partially(object target, object value, string dotPath)
        {
            DeepMaster deepMaster = new DeepMaster(target, value, dotPath);
            return deepMaster.Partially();
        }
        /// <summary>
        /// Passes to the object the property value of another object of the same type along the specified path.
        /// </summary>
        /// <typeparam name="TMerge">Object type</typeparam>
        /// <param name="target">The object to modify the properties.</param>
        /// <param name="from">Object value-giving properties.</param>
        /// <param name="dotPath">Property search path.</param>
        /// <returns></returns>
        public static bool Merge<TMerge>(TMerge target, TMerge from, string dotPath)
        {
            DeepMaster deepMaster = new DeepMaster(target, from, dotPath);
            return deepMaster.Merge();
        }

    }
}
