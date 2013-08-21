using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanSolver
{
    public class WordTester
    {
        public Word[] Words { get; protected set; }
        public readonly Word CurWord;
        public int optionsAt10 { get; protected set; }

        public int NumGuesses
        {
            get
            {
                return CurWord.Guesses.Count();
            }
        }

        public int NumMatches
        {
            get
            {
                return Words.Count();
            }
        }

        public WordTester(Word word, Word[] words)
        {
            Words = words;
            CurWord = word;
            optionsAt10 = 0;
            ReduceToMatches();
        }

        public Word[] GetMatches()
        {
            return Words.Where(a => a.Matches(CurWord.CurrentMatchPattern)).ToArray();
        }

        protected Word[] ReduceToMatches()
        {
            Words = GetMatches();
            return Words;
        }

        protected Dictionary<char, int> GetChars()
        {
            Dictionary<char, int> output = new Dictionary<char, int>();
            foreach (Word word in Words)
            {
                char[] chars = word.UniqueChars;
                foreach (char c in chars)
                {
                    if (output.Keys.Contains(c))
                    {
                        output[c] += 1;
                    }
                    else
                    {
                        output.Add(c, 1);
                    }
                }
            }
            return output;
        }

        protected char NextChar(bool optimal)
        {
            if (optimal)
            {
                Dictionary<char, int> chars = GetChars();
                foreach (char c in CurWord.Guesses)
                {
                    chars.Remove(c);
                }

                //Return the item with the highest number of hits
                return chars.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
            }
            else
            {
                char[] letters = { 'e', 't', 'a', 'o', 'i', 'n', 's', 'r', 'h', 'l', 'd', 'c', 'u', 'm', 'f', 'p', 'g', 'w', 'y', 'b', 'v', 'k', 'x', 'j', 'q', 'z' };
                return letters[CurWord.Guesses.Count()];
            }
        }

        protected int Next(bool optimal)
        {
            CurWord.Guess(NextChar(optimal));
            ReduceToMatches();
            if (optionsAt10 == 0 && CurWord.IncorrectGuessess.Length == 9)
            {
                optionsAt10 = Words.Length;
            }
            return Words.Count();
        }

        public int GuessUntilUnique(bool optimal)
        {
            while (Next(optimal) > 1) { };
            return CurWord.Guesses.Count();
        }
    }
}
