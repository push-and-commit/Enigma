using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeritageEtInterface
{
    internal class User
    {
        private string _lastName;
        private string _firstName;
        private int _age;

        public User(string lastName, string firstName, int age)
        {
            _lastName = lastName;
            _firstName = firstName;
            _age = age;
        }

        public string LastName { get => _lastName; set => _lastName = value; }
        public string FirstName { get => _firstName; set => _firstName = value; }
        public int Age { get => _age; set => _age = value; }
    }
}
