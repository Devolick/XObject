using System;
using System.Collections.Generic;
using System.Text;

namespace XObjectSerializer.Attributes
{
    /// <summary>
    /// Ignores serialization of class properties
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class XIgnoreClassAttribute: Attribute
    {
        /// <summary>
        /// Property names to be ignored
        /// </summary>
        public string[] Properties { get; private set; }

        private XIgnoreClassAttribute() { }
        /// <summary>
        /// A constructor in which you can ignore class properties
        /// </summary>
        /// <param name="properties">Ignore properties by name</param>
        public XIgnoreClassAttribute(params string[] properties)
        {
            Properties = properties;
        }
    }
}
