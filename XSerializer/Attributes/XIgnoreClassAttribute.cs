using System;
using System.Collections.Generic;
using System.Text;

namespace XObjectSerializer.Attributes
{
    /// <summary>
    /// Ignores serialization of class properties
    /// </summary>
    public class XIgnoreClassAttribute
    {
        private bool ignored;
        /// <summary>
        /// Property names to be ignored
        /// </summary>
        public string[] Properties { get; set; }

        private XIgnoreClassAttribute() { }
        /// <summary>
        /// The constructor in which you can specify ignoring the class
        /// </summary>
        /// <param name="ignore">Ignore this class</param>
        public XIgnoreClassAttribute(bool ignore)
        {
            ignored = ignore;
        }
        /// <summary>
        /// A constructor in which you can ignore class properties
        /// </summary>
        /// <param name="properties">Ignore properties by name</param>
        public XIgnoreClassAttribute(params string[] properties)
        {
            ignored = false;
            Properties = properties;
        }
    }
}
