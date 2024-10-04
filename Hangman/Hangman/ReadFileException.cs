using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    class ReadFileException : Exception
    {

        public ReadFileException()
        {
            Console.WriteLine("Le fichier fournit n'est pas bon !");
        }

    }
}
