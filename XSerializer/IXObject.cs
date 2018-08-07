using System;
using System.Collections.Generic;
using System.Text;

namespace XObjectSerializer
{
    /// <summary>
    /// 
    /// </summary>
    public interface IXObject
    {
        /// <summary>
        /// It will be called before serialized the object.
        /// </summary>
        /// <param name="o">Current target object</param>
        void XSerialize(object o);
        /// <summary>
        /// It will be called after deserialized the object.
        /// </summary>
        /// <param name="o">Current target object</param>
        void XDeserialize(object o);
    }
}
