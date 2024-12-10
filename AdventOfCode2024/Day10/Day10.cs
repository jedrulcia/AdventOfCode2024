using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    class Day10
    {
        public int Id { get; set; }
        public List<int> CountedIds { get; set; } = new List<int>();
        public int Value { get; set; }

        public Day10(int id, int value)
        {
            this.Id = id;
            this.Value = value;
        }
        

        //482
        public static int Part1()
        {
            Day10[,] tab = GetObjectArray();
            int counter = 0;

            for (int i = 0; i < tab.GetLength(0); i++)
            {
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    if (tab[i, j].Value == 0)
                    {
                        TrailheadSearcher(tab, i, j, ref counter, tab[i, j].Id);
                    }
                }
            }

            return counter;
        }

        private static void TrailheadSearcher(Day10[,] tab, int currentI, int currentJ, ref int counter, int startingId)
        {
            if (tab[currentI, currentJ].Value == 9)
            {
                bool shouldCount = true;
                foreach (var item in tab[currentI, currentJ].CountedIds)
                {
                    if (item == startingId)
                    {
                        shouldCount = false; 
                        break;
                    }
                }
                if (shouldCount)
                {
                    counter++;
                    tab[currentI, currentJ].CountedIds.Add(startingId);
                }
                return;
            }
            if (currentI + 1 < tab.GetLength(0) && tab[currentI, currentJ].Value == tab[currentI + 1, currentJ].Value - 1)
            {
                TrailheadSearcher(tab, currentI + 1, currentJ, ref counter, startingId);
            }
            if (currentI - 1 >= 0 && tab[currentI, currentJ].Value == tab[currentI - 1, currentJ].Value - 1)
            {
                TrailheadSearcher(tab, currentI - 1, currentJ, ref counter, startingId);
            }
            if (currentJ + 1 < tab.GetLength(1) && tab[currentI, currentJ].Value == tab[currentI, currentJ + 1].Value - 1)
            {
                TrailheadSearcher(tab, currentI, currentJ + 1, ref counter, startingId);
            }
            if (currentJ - 1 >= 0 && tab[currentI, currentJ].Value == tab[currentI, currentJ - 1].Value - 1)
            {
                TrailheadSearcher(tab, currentI, currentJ - 1, ref counter, startingId);
            }
        }

        private static Day10[,] GetObjectArray()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string projectPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\"));
            string dayPath = Path.Combine(projectPath, "Day10");
            string filePath = Path.Combine(dayPath, "Day10.txt");

            var lines = File.ReadAllLines(filePath);

            Day10[,] tab = new Day10[lines.Length, lines[0].Length];
            int id = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    tab[i, j] = new Day10(id, Convert.ToInt32(lines[i][j] - '0'));
                    id++;
                }
            }
            return tab;
        }

        public static int Part2()
        {
            char[,] tab = ConvertFromTxtToArray();
            int counter = 0;

            for (int i = 0; i < tab.GetLength(0); i++)
            {
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    if (tab[i, j] == '0')
                    {
                        TrailheadSearcherPart2(tab, i, j, ref counter);
                    }
                }
            }

            return counter;
        }
        private static void TrailheadSearcherPart2(char[,] tab, int currentI, int currentJ, ref int counter)
        {
            if (tab[currentI, currentJ] == '9')
            {
                counter++;
                return;
            }
            if (currentI + 1 < tab.GetLength(0) && tab[currentI, currentJ] == tab[currentI + 1, currentJ] - 1)
            {
                TrailheadSearcherPart2(tab, currentI + 1, currentJ, ref counter);
            }
            if (currentI - 1 >= 0 && tab[currentI, currentJ] == tab[currentI - 1, currentJ] - 1)
            {
                TrailheadSearcherPart2(tab, currentI - 1, currentJ, ref counter);
            }
            if (currentJ + 1 < tab.GetLength(1) && tab[currentI, currentJ] == tab[currentI, currentJ + 1] - 1)
            {
                TrailheadSearcherPart2(tab, currentI, currentJ + 1, ref counter);
            }
            if (currentJ - 1 >= 0 && tab[currentI, currentJ] == tab[currentI, currentJ - 1] - 1)
            {
                TrailheadSearcherPart2(tab, currentI, currentJ - 1, ref counter);
            }
        }

        private static char[,] ConvertFromTxtToArray()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string projectPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\"));
            string dayPath = Path.Combine(projectPath, "Day10");
            string filePath = Path.Combine(dayPath, "Day10.txt");

            var lines = File.ReadAllLines(filePath);

            char[,] tab = new char[lines.Length, lines[0].Length];

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    tab[i, j] = lines[i][j];
                }
            }

            return tab;
        }
    }
}
