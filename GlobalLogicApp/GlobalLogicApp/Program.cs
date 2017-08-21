using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GlobalLogicApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello GlobalLogic!");
            Console.WriteLine("Please enter file path:");

            string path = Console.ReadLine();

            if (File.Exists(path) == false)
            {
                Console.WriteLine($"File {path} not exist");
                return;
            }

            var wordSet = GetWordSet(path);

            if (wordSet != null)
            {
                var countWordsResult = GetWordsCount(wordSet);
                ShowResut(countWordsResult);
            }

            Console.ReadLine();
        }

        static List<string> GetWordSet(string path)
        {
            var wordsSet = new List<string>();

            try
            {
                using (StreamReader _strr = new StreamReader(path))
                {
                    string line;
                    string[] words;

                    while ((line = _strr.ReadLine()) != null)
                    {
                        var charsToRemove = new string[] { "@", ",", ".", ";", "'" };

                        foreach (var item in charsToRemove)
                        {
                            line = line.Replace(item, string.Empty);
                        }


                        words = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        wordsSet.AddRange(words);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return wordsSet;
        }

        static Dictionary<string, int> GetWordsCount(List<string> wordSet)
        {
            var countWords = new Dictionary<string, int>();

            foreach (var item in wordSet)
            {
                if (!countWords.ContainsKey(item))
                {
                    countWords.Add(item, 1);
                }
                countWords[item]++;
            }

            return countWords;
        }

        static void ShowResut(Dictionary<string, int> result)
        {
            Console.WriteLine("Result:\n");
            foreach (var item in result)
            {
                Console.WriteLine($"\t{item.Key} {item.Value}");
            }
        }
    }
}
