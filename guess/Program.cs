using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace guess
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] files = { "dictionary.txt", "other.txt" }; // A list of dictionary files to use.
            WordList words = new WordList();

            // Parse the files with error control.
            foreach (string file in files)
            {
                try
                {
                    words.Parse(file);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            // Game Loop
            string[] prefixList = { "st", "nd", "rd", "th" };

            //Console.WriteLine(inCommon("trucks", "staple"));

            while (words.Count>0)
            {
                int currentGuesses = 1;
                int maxGuesses = 0;
                int length = 0;
                string word = words.Random(); // Get a random word.
                string input = "";
                string prefix = "";
                length = word.Length;
                maxGuesses = length + 2; // Player gets length of word in guesses, plus 2.

                Console.WriteLine(new String('=', 50));
                Console.WriteLine("The word is " + new String('*', length) + " (" + length + " letters)");
                //Console.WriteLine("The word is " + new String('*', length) + " (" + length + " letters) " + word);
                Console.WriteLine("You have " + maxGuesses + " guess, good luck!");
                Console.WriteLine(new String('=', 50));

                do
                {
                    if (currentGuesses > 3)
                    {
                        prefix = prefixList[3];
                    }
                    else
                    {
                        prefix = prefixList[currentGuesses - 1];
                    }
                    Console.Write("-> Enter " + currentGuesses + prefix + " guess: ");
                    input = Console.ReadLine().ToLower();

                    // Make sure it is a valid word
                    if (words.Contains(input))
                    {
                        if (input==word)
                        {
                            Console.WriteLine("-> You got it on the " + currentGuesses + prefix + " guess, congratulations!");
                            currentGuesses = 999; // Force game to end by exceeding guesses.
                        }

                        if (input.Length == length)
                        {
                            Console.WriteLine("-> " + input + " -> " + inCommon(input, word) + " letters in common.");
                            currentGuesses++; // Increase current guesses.
                        }
                        else
                        {
                            Console.WriteLine("-> The word you guessed is not " + length + " letters long!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("-> That's not a real word, is it?");
                    }
                } while (currentGuesses <= maxGuesses);
                Console.WriteLine("-> So close!  The word you were looking for was " + word + "."); 
            }
        }

        /// <summary>
        /// Compared two words for common letters, duplicates are discarded.
        /// </summary>
        /// <param name="firstWord">string</param>
        /// <param name="secondWord">string</param>
        /// <returns>int: the number of letters in common</returns>
        public static int inCommon(string firstWord, string secondWord)
        {
            int total = 0; // Total to return.
            char[] firstWordArray = firstWord.ToCharArray(); // Put the firstword in to a char array for looping.
            bool alreadyChecked = false; // Set to true if the current letter has already been checked, takes care of duplicates.
            for (int i = 0; i < firstWord.Length; i++)
            {
                alreadyChecked = firstWord.Substring(0, i).Contains(firstWordArray[i]); // If the current index (letter) is contained in the substring from 0 upto it's position, then it's already been checked.
                if (!alreadyChecked && secondWord.Contains(firstWordArray[i])) // Check to see if second word contains the letter, if so, increase counter.
                {
                    total++;
                }
            }
            return total;
        }
    }
}
