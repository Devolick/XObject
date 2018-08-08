using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

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
        /// <returns>Serialized string</returns>
        public static string XSerialize(object o)
        {
            if (o == null) throw new XObjectException("An empty object can not be serialized.");

            using(Serialize serialize = new Serialize())
            {
                return $"&{serialize.Build(o.GetType(), o)}";
            }
        }
        /// <summary>
        /// Serializes the specified object
        /// </summary>
        /// <param name="o">The object to serialize</param>
        /// <param name="dateFormat">For any DateTime value</param>
        /// <returns>Serialized string</returns>
        public static string XSerialize(object o, string dateFormat)
        {
            if (o == null) throw new XObjectException("An empty object can not be serialized.");

            using (Serialize serialize = new Serialize(dateFormat))
            {
                return $"&{serialize.Build(o.GetType(), o)}";
            }
        }
        /// <summary>
        /// Serializes the specified object
        /// </summary>
        /// <param name="o">The object to serialize</param>
        /// <param name="dateFormat">Format for any DateTime value</param>
        /// <param name="dateFormatProvider">IFormatProvider for any DateTime value</param>
        /// <returns>Serialized string</returns>
        public static string XSerialize(object o, string dateFormat, IFormatProvider dateFormatProvider)
        {
            if (o == null) throw new XObjectException("An empty object can not be serialized.");

            using (Serialize serialize = new Serialize(dateFormat, dateFormatProvider))
            {
                return $"&{serialize.Build(o.GetType(), o)}";
            }
        }
        /// <summary>
        /// Deserializes the specified string
        /// </summary>
        /// <param name="x">The string to deserialize</param>
        /// <returns>Deserialized object</returns>
        public static TObject XDeserialize<TObject>(string x)
        {
            if (x == null) throw new XObjectException("An empty string can not be deserialized.");

            using (Deserialize deserialize = new Deserialize())
            {
                return (TObject)deserialize.Build(typeof(TObject), Regex.Match(x,Queries.BODY, RegexOptions.Compiled).Value);
            }
        }
        /// <summary>
        /// Deserializes the specified string
        /// </summary>
        /// <param name="x">The string to deserialize</param>
        /// <param name="dateFormat">Format for any DateTime value</param>
        /// <returns>Deserialized object</returns>
        public static TObject XDeserialize<TObject>(string x, string dateFormat)
        {
            if (x == null) throw new XObjectException("An empty string can not be deserialized.");

            using (Deserialize deserialize = new Deserialize(dateFormat))
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
        public static TObject XDeserialize<TObject>(string x, string dateFormat, IFormatProvider dateFormatProvider)
        {
            if (x == null) throw new XObjectException("An empty string can not be deserialized.");

            using (Deserialize deserialize = new Deserialize(dateFormat,dateFormatProvider))
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
            return Regex.IsMatch(x, Queries.BODYVALID,RegexOptions.Compiled);
        }
        /// <summary>
        /// Produces cloning of this object. You must follow the serialization rules.
        /// </summary>
        /// <param name="o">Object for clone</param>
        /// <returns>Returns cloned object</returns>
        public static TClone Clone<TClone>(TClone o)
        {
            if (o == null) throw new XObjectException("It is not permissible to clone a null reference.");
            string serialize = XObject.XSerialize(o);
            return XObject.XDeserialize<TClone>(serialize);
        }
    }
}
