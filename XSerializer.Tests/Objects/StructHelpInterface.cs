using System;
using System.Collections.Generic;
using System.Text;
using XObjectSerializer.Interfaces;

namespace XObjectSerializer.Tests
{
    public class StructHelpInterface : IXObject
    {
        public string Value { get; set; }

        public StructHelpInterface() { }

        public void XDeserialize(object o)
        {
            (o as StructHelpInterface).Value = "XDeserialize";
        }

        public void XSerialize(object o)
        {
            (o as StructHelpInterface).Value = "XSerialize";
        }
    }
}
