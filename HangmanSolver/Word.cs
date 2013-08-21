using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HangmanSolver
{
    public class Word
    {
        public static Word[] LoadWords()
        {
            List<Word> output = new List<Word>();
            string[] file = Properties.Resources.WordList.Split(new char[] { '\n', '\r'}, StringSplitOptions.RemoveEmptyEntries );
            foreach (string word in file)
            {
                output.Add(new Word(word));
            }
            return output.ToArray();
        }

        public readonly string RootWord;
        protected List<char> correctGuesses;
        protected List<char> incorrectGuesses;
        protected List<char> guesses;

        public char[] CorrectGuesses
        {
            get
            {
                return correctGuesses.ToArray();
            }
        }

        public char[] IncorrectGuessess
        {
            get
            {
                return incorrectGuesses.ToArray();
            }
        }

        public char[] Guesses
        {
            get
            {
                return guesses.ToArray();
            }
        }

        public int Length
        {
            get
            {
                return RootWord.Length;
            }
        }

        public string CurrentMatchPattern
        {
            get
            {
                string output = "^";
                string unmatched = (Guesses.Length > 0) ? "[^" + String.Join("", Guesses) + "]" : ".";
                foreach (char c in RootWord)
                {
                    if (correctGuesses.Contains(c))
                    {
                        output += c;
                    }
                    else
                    {
                        output += unmatched;
                    }
                }
                output += "$";
                return output;
            }
        }

        public char[] UniqueChars
        {
            get
            {
                return RootWord.Distinct().ToArray();
            }
        }

        public Word(string w)
        {
            RootWord = w;
            correctGuesses = new List<char>();
            incorrectGuesses = new List<char>();
            guesses = new List<char>();
        }

        public bool Guess(char c)
        {
            guesses.Add(c);
            if (RootWord.Contains(c))
            {
                correctGuesses.Add(c);
                return true;
            }
            else
            {
                incorrectGuesses.Add(c); 
                return false;
            }
        }

        public void Clear()
        {
            correctGuesses = new List<char>();
            incorrectGuesses = new List<char>();
        }

        public bool Matches(string pattern)
        {
            return Regex.IsMatch(RootWord, pattern);
        }

        public bool ContainsAny(char[] matches)
        {
            return RootWord.IndexOfAny(matches) >= 0;
        }
    }
}
