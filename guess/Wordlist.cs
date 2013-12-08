using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace guess
{
    class WordList : List<string>
    {
        /// <summary>
        /// Parse a text file for words and add them to the dictionary.
        /// </summary>
        /// <param name="fileName">A carriage return deliminted text file.</param>
        public void Parse(string fileName)
        {
            if (File.Exists(fileName)) // If the files does not exist, print an error.
            {
                //Read the entire file and split each word into a string array.
                DateTime Start = DateTime.Now;
                StreamReader reader = new StreamReader(fileName); // Create file reader object.
                string[] delimiters = { Environment.NewLine }; // Holds delimters to split the string by.
                string[] contents = reader.ReadToEnd().Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                //Loop through and add each word to the dictionary.
                foreach (string word in contents)
                {
                    this.Add(word.ToLower()); // Convert to lowercase for simplicity.
                }
                DateTime End = DateTime.Now;
                Console.WriteLine("Added " + this.Count + " words from file " + fileName + " in " + End.Subtract(Start));
            }
            else
            {
                Console.WriteLine(fileName + " file not found, skipped.");
            }
        }
        /// <summary>
        /// Returns a random word from the dictionary.
        /// </summary>
        /// <returns>string</returns>
        public string Random()
        {
            Random rand = new Random(); // Create a random object.
            return this[rand.Next(this.Count)];
        }
    }
}
