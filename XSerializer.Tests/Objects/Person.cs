using System;
using System.Collections.Generic;
using System.Text;

namespace XObjectSerializer.Tests
{
    public class PersonInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int RealAge { get; set; }


        public PersonInfo() { }
    }
    public class Person
    {
        public Person Schizophrenia { get; set; } // null
        public PersonInfo Info { get; set; }
        public string Secret { get; set; }
        public string Empty { get; set; }

        public Person()
        {
            Info = new PersonInfo();
            Info.FirstName = "Dzmitry";
            Info.LastName = "Dym";
            Info.Age = 27;
            Info.RealAge = 27;

            Secret = "1234pin";
        }
    }
}
