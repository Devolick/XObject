using System;
using System.Collections.Generic;
using System.Text;

namespace XObjectSerializer.Attributes
{
    /// <summary>
    /// Ignores property serialization
    /// </summary>
    [AttributeUsage(AttributeTargets.Property,AllowMultiple = false, Inherited = false)]
    public class XIgnorePropertyAttribute: Attribute
    {
        public XIgnorePropertyAttribute() { }
    }
}
