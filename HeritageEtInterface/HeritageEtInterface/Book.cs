using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeritageEtInterface
{
    internal class Book : MediaObject, IBorrow
    {
        private int _nbPages;
        public Book(string name, string synopsis, int stock, int nbPages) : base(name, synopsis, stock)
        {
            _nbPages = nbPages;
        }

        public int NbPages { get => _nbPages; set => _nbPages = value; }

        public void Borrow()
        {
            if (Stock > 0)
            {
                Stock--;
            }
            else
            {
                Console.WriteLine("Le stock est épuisé");
            }
        }

        public void GiveBack()
        {
            Stock++;
        }
    }
}
