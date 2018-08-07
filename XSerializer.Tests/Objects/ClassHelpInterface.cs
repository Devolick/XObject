using System;
using System.Collections.Generic;
using System.Text;

namespace XObjectSerializer.Tests
{
    public class ClassHelpInterface : IXObject
    {
        public string Value { get; set; }

        public ClassHelpInterface() { }

        public void XDeserialize(object o)
        {
            (o as ClassHelpInterface).Value = "XDeserialize";
        }

        public void XSerialize(object o)
        {
            (o as ClassHelpInterface).Value = "XSerialize";
        }
    }
}
