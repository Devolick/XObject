using System;
using System.Collections.Generic;
using System.Text;

namespace XSerializer.Tests
{
    public class PersonInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int RealAge { get; set; }


        public PersonInfo() { }
    }

    public class PortfolioList : List<string>
    {
        public bool IsPrivateInfo { get; set; }
        public PortfolioList() { }
    }

    public class Person
    {
        public KeyValuePair<int, string> Pair { get; set; }
        public KeyValuePair<int, string> Pair2 { get; set; }
        public PersonInfo Info { get; set; }
        public PortfolioList Portfolio { get; set; }
        public string Secret { get; set; }
        public string Empty { get; set; }

        public Person()
        {
            Pair = new KeyValuePair<int, string>(123, "Dzmitry Dym");
            Pair2 = Pair;
            Portfolio = new PortfolioList();
            Portfolio.Add("3Ds models");
            Portfolio.Add("C++ Programms");
            Portfolio.Add("C# Standard");

            Info = new PersonInfo();
            Info.FirstName = "Dzmitry";
            Info.LastName = "Dym";
            Info.Age = 27;
            Info.RealAge = 27;

            Secret = "1234pin";
        }
    }
}
