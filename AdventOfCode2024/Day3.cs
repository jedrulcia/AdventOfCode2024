using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;
using System;

namespace AdventOfCode2024
{
    class Day3
    {
        static void Main()
        {
            int all = AddAllMultiplications();
            int doNot = AddAllDontMultiplications();

            Console.WriteLine($"{all} - {doNot} = {all - doNot}");
        }

        static int AddAllMultiplications()
        {
            string text = File.ReadAllText("C:\\Users\\Jedrulcia\\Desktop\\Jedrzej\\Programowanko\\github\\AdventOfCode2024\\AdventOfCode2024\\Day3.txt");
            string template = @"mul\(\d{1,3},\d{1,3}\)";

            int sum = 0;

            MatchCollection matches = Regex.Matches(text, template);

            foreach (Match match in matches)
            {
                string numberTemplate = @"\d+";
                MatchCollection numbers = Regex.Matches(match.Value, numberTemplate);

                sum += Convert.ToInt32(numbers[0].Value) * Convert.ToInt32(numbers[1].Value);
            }

            return sum;
        }

        static int AddAllDontMultiplications()
        {
            string text = File.ReadAllText("C:\\Users\\Jedrulcia\\Desktop\\Jedrzej\\Programowanko\\github\\AdventOfCode2024\\AdventOfCode2024\\Day3.txt");

            string doTemplate = @"do\(\)";
            string doNotTemplate = @"don't\(\)";

            List<string> extractedParts = new List<string>();

            int index = 0;

            while (index < text.Length)
            {
                Match doNotMatch = Regex.Match(text.Substring(index), doNotTemplate);
                if (doNotMatch.Success)
                {
                    index += doNotMatch.Index + doNotMatch.Length;

                    Match doMatch = Regex.Match(text.Substring(index), doTemplate);
                    if (doMatch.Success)
                    {
                        string extracted = text.Substring(index, doMatch.Index);
                        extractedParts.Add(extracted);

                        index += doMatch.Index + doMatch.Length;
                    }
                    else
                    {
                        string extracted = text.Substring(index);
                        extractedParts.Add(extracted);
                    }
                }
                else
                {
                    break;
                }
            }

            int sum = 0;
            string template = @"mul\(\d{1,3},\d{1,3}\)";

            foreach (string part in extractedParts)
            {
                MatchCollection matches = Regex.Matches(part, template);
                foreach (Match match in matches)
                {
                    string numberTemplate = @"\d+";
                    MatchCollection numbers = Regex.Matches(match.Value, numberTemplate);

                    sum += Convert.ToInt32(numbers[0].Value) * Convert.ToInt32(numbers[1].Value);
                }

            }

            return sum;
        }



    }
}
