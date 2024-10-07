using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeritageEtInterface
{
    internal class MediaObject
    {
        private string _name;
        private string _synopsis;
        private int _stock;

        public MediaObject(string name, string synopsis, int stock)
        {
            Name = name;
            _synopsis = synopsis;
            Stock = stock;
        }

        public string Name { get => _name; set => _name = value; }
        public string synopsis { get => _synopsis; set => _synopsis = value; }
        public int Stock { get => _stock; set => _stock = value; }

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
