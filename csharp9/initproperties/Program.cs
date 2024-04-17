using System;

namespace initproperties
{
    class Program
    {
        static void Main(string[] args)
        {
            Person people1 = new Person("Bob", "Ross");

            people1.SetFirst("Joe");
        }
    }

    class Person
    {
        private string _first { get; init; }
        private string _last { get; init; }

        public Person(string first, string last)
        {
            _first = first;

            _last = last;
        }

        // this will produce a compiler error as the init keyword was used and that makes the _first and _last class variables readonly after they are initialized
        public void SetFirst(string first)
        {
            _first = first;
        }
    }
}
