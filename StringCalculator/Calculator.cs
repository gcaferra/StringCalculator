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
        
        static string[] SplitNumbers(string numbers)
        {
            var (delimiters, stringOfNumber) = GetSeparatorAndString(numbers);
            delimiters.Add("\n");
            
            return stringOfNumber.Split(delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries);

        }

        static Tuple<List<string>, string> GetSeparatorAndString(string inputText, string defaultSeparator = ",")
        {
            var codeIndex = inputText.IndexOf("//", StringComparison.Ordinal);
            if (codeIndex < 0) return new Tuple<List<string>, string>(new List<string> {defaultSeparator}, inputText);
            var parts = inputText.Split("\n");
                
            return new Tuple<List<string>, string>(GetDelimiter(parts[0]), parts[1]);

        }

        static List<string> GetDelimiter(string part)
        {
            var matches = Regex.Matches(part, @"\[([^\[\]]+)\]*");
            
            return matches.Count == 0
                ? new List<string> {part.Replace("//", string.Empty)} 
                : matches.Select(x => x.Groups[1].Value).ToList();
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