using System;
using System.Linq;

class Program
{
    static void Main()
    {

          string[] words = {"hello", "wonderful", "LINQ", "beautiful", "world"};

          //Get only short words
          var shortWords = from word in words where word.Length <= 5 select word;      

          //Print each word out
          foreach (var word in shortWords) {
             Console.WriteLine(word);
          }	 

          //Console.ReadLine();



        ////////////
        ////////////
        int[] fibNum = { 1, 1, 2, 3, 5, 8, 13, 21, 34 };
        double averageValue = fibNum.Where(n => n % 2 == 1).Average();
        Console.WriteLine(averageValue);
        ////////////
        ////////////
    }
}