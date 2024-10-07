using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeritageEtInterface
{
    internal class DVD : MediaObject, IBorrow
    {
        private int _duration; // minutes
        public DVD(string name, string synopsis, int stock, int duration) : base(name, synopsis, stock)
        {
            _duration = duration;
        }

        public int Duration { get => _duration; set => _duration = value; }

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
