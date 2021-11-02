using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
    public class Calculator
    {
        public int Add(string numbers)
        {
            if(string.IsNullOrEmpty(numbers))
                return 0;
            return SplitNumbers(numbers)
                .Select(int.Parse)
                .FilterNegatives()
                .Sum();
        }
        
        static List<string> SplitNumbers(string numbers)
        {
            var stringParts = GetSeparatorAndString(numbers);
            var result = new List<string>();
            var splits = stringParts.Item2.Split(stringParts.Item1);

            foreach (var split in splits)
            {
                result.AddRange(split.Split("\n"));    
            }

            return result;

        }

        static Tuple<string,string> GetSeparatorAndString(string inputText, string defaultSeparator = ",")
        {
            var codeIndex = inputText.IndexOf("//", StringComparison.Ordinal);
            if (codeIndex < 0) return new Tuple<string, string>(defaultSeparator, inputText);
            var parts = inputText.Split("\n");
                
            return new Tuple<string, string>(parts[0].Replace("//", string.Empty), 
                parts[1].Replace("\n", string.Empty));

        }
    }

    public static class CalculatorExtensions
    {
        public static List<int> FilterNegatives(this IEnumerable<int> textNumber)
        {
            var negatives = new List<int>();
            var result = new List<int>();
            
            foreach (var number in textNumber)
            {
                if (number <0)
                    negatives.Add(number);
                else
                    result.Add(number);
            }
            
            if (negatives.Any())
                throw new ArgumentException($"Negative numbers are not allowed: {string.Join(",",negatives)}");
            return result;
        }

    }
}