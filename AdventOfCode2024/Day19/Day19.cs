using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    class Day19
    {
        public static long Part2()
        {
            (List<string> fragments, List<string> towels) = GetInput();
            Dictionary<string, long> memory = new Dictionary<string, long>();

            long counter = 0;

            foreach (var towel in towels)
            {
                CutThePartsPart2(fragments, memory, towel, ref counter);
            }

            return counter;
        }

        public static void CutThePartsPart2(List<string> fragments, Dictionary<string, long> memory, string towel, ref long counter)
        {
            if (towel.Length == 0)
            {
                counter++;
                return;
            }

            if (memory.ContainsKey(towel))
            {
                counter += memory[towel];
                return;
            }

            long localCount = 0;

            foreach (var fragment in fragments)
            {
                if (towel.StartsWith(fragment))
                {
                    string passTowel = towel.Substring(fragment.Length);
                    CutThePartsPart2(fragments, memory, passTowel, ref localCount);
                }
            }

            memory[towel] = localCount;
            counter += localCount;
        }

        public static int Part1()
        {
            (List<string> fragments, List<string> towels) = GetInput();

            int counter = 0;
            bool shouldContinue = true;

            foreach (var towel in towels)
            {
                CutThePartsPart1(fragments, towel, ref counter, ref shouldContinue);
                shouldContinue = true;
            }

            return counter;
        }

        public static void CutThePartsPart1(List<string> fragments, string towel, ref int counter, ref bool shouldContinue)
        {
            if (towel.Length == 0)
            {
                counter++;
                shouldContinue = false;
            }

            foreach (var fragment in fragments)
            {
                if (!shouldContinue) break;
                if (towel.StartsWith(fragment))
                {
                    string passTowel = towel.Substring(fragment.Length);
                    CutThePartsPart1(fragments, passTowel, ref counter, ref shouldContinue);
                }
            }
        }

        public static (List<string> fragments, List<string> towels) GetInput()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string projectPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\"));
            string dayPath = Path.Combine(projectPath, "Day19");
            string fragmentsPath = Path.Combine(dayPath, "Day19fragments.txt");
            string towelsPath = Path.Combine(dayPath, "Day19towels.txt");

            string[] towelsArray = File.ReadAllLines(towelsPath);
            List<string> towels = new List<string>(towelsArray);

            string fragmentsText = File.ReadAllText(fragmentsPath);

            List<string> fragments = new List<string>();

            string[] parts = fragmentsText.Split(", ");
            foreach (var part in parts)
            {
                fragments.Add(part);
            }

            return (fragments, towels);
        }


    }
}
