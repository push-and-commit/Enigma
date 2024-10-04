using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    class Hangman
    {
        private string _word;
        private int _countLives;
        private string _hiddenWord;
        private string _lettersUsed;

        public Hangman(string word, int countLives)
        {
            _word = word;
            _countLives = countLives;
            _hiddenWord = initializeHiddenWord();
            _lettersUsed = "";
        }

        public string word
        {
            get { return _word; }
            set
            {
                _word = value;
            }
        }

        public int countLives
        {
            get { return _countLives; }
            set
            {
                _countLives = value;
            }
        }

        public string hiddenWord
        {
            get { return _hiddenWord; }
        }


        public string lettersUsed
        {
            get { return _lettersUsed; }
            set
            {
                _lettersUsed = value;
            }
        }


        private string initializeHiddenWord()
        {
            for (int i = 0; i < _word.Length; i++)
            {
                _hiddenWord += "_";
            }
            return _hiddenWord;
        }

        public int isAlreadyUsed(string value)
        {
            return _lettersUsed.IndexOf(value);
        }

        public int isInWord(string value)
        {
            return _word.IndexOf(value);
        }

        public void addLetterToHiddenWord(string value)
        {
            char[] hiddenChosenWord = hiddenWord.ToCharArray();
            char valueToTest = value.ToCharArray()[0]; // Double sécurité, input seulement d'un seul caractère
            for (int i = 0; i < _word.Length; i++)
            {
                if (valueToTest == _word[i])
                {
                    hiddenChosenWord[i] = valueToTest;
                }
            }

            _hiddenWord = "";
            for (int i = 0; i < _word.Length; i++)
            {
                _hiddenWord += hiddenChosenWord[i];
            }
        }

        public bool isHiddenValid()
        {
            return _word == _hiddenWord;
        }

        public bool isLost(int gameLives)
        {
            return gameLives > 0 ? false : true;
        }
    }
}
