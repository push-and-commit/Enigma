using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeritageEtInterface
{
    internal class Admin : User
    {
        private string _hiredDate;

        public Admin(string lastName, string firstName, int age, string hiredDate) : base(lastName, firstName, age)
        {
            _hiredDate = hiredDate;
        }

        public string HiredDate { get => _hiredDate; set => _hiredDate = value; }
    }
}
