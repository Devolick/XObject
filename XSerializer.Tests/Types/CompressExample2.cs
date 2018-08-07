using System;
using System.Collections.Generic;
using System.Text;

namespace XObjectSerializer.Tests
{
    public class CompressExample2
    {
        public Person Pr1 { get; set; }
        public Person Pr2 { get; set; }

        public CompressExample2()
        {
            Pr1 = new Person();
            Pr2 = new Person();
        }
    }
}
