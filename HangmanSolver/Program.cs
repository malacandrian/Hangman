using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            Word[] optimalWordList = Word.LoadWords();
            Word[] easyWordList = Word.LoadWords();
            StreamWriter file = new StreamWriter(@"C:\output\words.csv");
            file.WriteLine("Word, Length, #Unique Characters, Optimal Guesses Required, Optimal Incorrect Guesses Required, Optimal Guesses, Optimal options at 10, Easy Guesses Required, Easy Incorrect Guesses Required, Easy options at 10");
            for(int i = 0; i < optimalWordList.Length; i += 1)
            {
                Word oWord = optimalWordList[i];
                Word eWord = easyWordList[i];
                System.Console.WriteLine("Testing " + oWord.RootWord);
                WordTester optimal = new WordTester(oWord, optimalWordList);
                WordTester easy = new WordTester(eWord, easyWordList);
                optimal.GuessUntilUnique(true);
                easy.GuessUntilUnique(false);
                System.Console.WriteLine(oWord.IncorrectGuessess.Length);
                file.WriteLine(String.Format(@"{0}, {1}, {2}, {3}, {4}, {5},{6},{7},{8},{9}",oWord.RootWord, oWord.Length, oWord.UniqueChars.Length, oWord.Guesses.Length, oWord.IncorrectGuessess.Length, String.Join(" ", oWord.Guesses), optimal.optionsAt10, eWord.Guesses.Length, eWord.IncorrectGuessess.Length, easy.optionsAt10));
            }
            file.Close();
        }
    }
}
