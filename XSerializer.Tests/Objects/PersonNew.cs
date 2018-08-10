using System;
using System.Collections.Generic;
using System.Text;

namespace XObjectSerializer.Tests
{
    public class PersonNew:Person
    {
        public string NewClassProperty { get; set; }

        public PersonNew()
            :base()
        {
            
        }
    }
}
