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
        void XSerialize(object o);
        void XDeserialize(object o);
    }
}
