using System;
using System.Collections.Generic;
using System.Text;

namespace XObjectSerializer.Attributes
{
    /// <summary>
    /// Ignores property serialization
    /// </summary>
    public class XIgnorePropertyAttribute: Attribute
    {
        public XIgnorePropertyAttribute() { }
    }
}
