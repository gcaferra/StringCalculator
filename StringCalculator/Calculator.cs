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
            return SplitNumbers(numbers).Select(int.Parse).Sum();
        }

        static List<string> SplitNumbers(string numbers)
        {
            var result = new List<string>();
            var splits = numbers.Split(",");

            foreach (var split in splits)
            {
                result.AddRange(split.Split("\n"));    
            }

            return result;

        }
    }
}