using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
    public class Calculator
    {
        int _callCount;
        public event Action<string, int> AddOccurred;
        public int Add(string numbers)
        {
            _callCount++;
            
            if(string.IsNullOrEmpty(numbers))
                return 0;
            var result = SplitNumbers(numbers)
                .Select(int.Parse)
                .Filter()
                .Sum();
            AddOccurred?.Invoke(numbers, result);
            return result;
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

        public int GetCalledCount() => _callCount;
    }

    public static class CalculatorExtensions
    {
        public static List<int> Filter(this IEnumerable<int> textNumber)
        {
            var exclude = new List<int>();
            var result = new List<int>();
            
            foreach (var number in textNumber)
            {
                if (number is < 0 or > 1000)
                    exclude.Add(number);
                else
                    result.Add(number);
            }
            
            if (exclude.Any(x=> x < 0))
                throw new ArgumentException($"Negative numbers are not allowed: {string.Join(",",exclude.Where(x => x < 0))}");
            return result;
        }

    }
}