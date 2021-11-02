using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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
            var (delimiter, stringOfNumber) = GetSeparatorAndString(numbers);
            var result = new List<string>();
            var splits = stringOfNumber.Split(delimiter);

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
                
            return new Tuple<string, string>(GetDelimiter(parts[0]), parts[1]);

        }

        static string GetDelimiter(string part)
        {
            var match = Regex.Match(part, "\\/\\/\\[(.*)\\]");
            return match.Length == 0 ?  part.Replace("//", string.Empty) : match.Groups[1].Value;
        }

        public int GetCalledCount() => _callCount;
    }

    public static class CalculatorExtensions
    {
        public static List<int> Filter(this IEnumerable<int> numbers)
        {
            var exclude = new List<int>();
            var result = new List<int>();
            
            foreach (var number in numbers)
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