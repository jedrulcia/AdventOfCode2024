using System.Text.RegularExpressions;

namespace AdventOfCode2024
{
    class Day3
    {
        static int Day3Part1()
		{
			string basePath = AppDomain.CurrentDomain.BaseDirectory;
			string projectPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\"));
			string dayPath = Path.Combine(projectPath, "Day3");
			string filePath = Path.Combine(dayPath, "Day3.txt");

			string text = File.ReadAllText(filePath);

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

        static int Day3Part2()
        {

			string basePath = AppDomain.CurrentDomain.BaseDirectory;
			string projectPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\"));
			string dayPath = Path.Combine(projectPath, "Day3");
			string filePath = Path.Combine(dayPath, "Day3.txt");

			string text = File.ReadAllText(filePath);

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
