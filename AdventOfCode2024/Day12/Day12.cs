using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    class Day12
    {
        public char Value { get; set; }
        public bool WasCounted { get; set; } = false;
        public bool CountedTop { get; set; } = false;
        public bool CountedRight { get; set; } = false;
        public bool CountedBottom { get; set; } = false;
        public bool CountedLeft { get; set; } = false;

        public Day12(char value)
        {
            this.Value = value;
            this.WasCounted = false;
            this.CountedTop = false;
            this.CountedRight = false;
            this.CountedBottom = false;
            this.CountedLeft = false;
        }


        public static int Part1()
        {
            int sum = 0;
            Day12[,] tab = GetObjectArray();

            for (int i = 0; i < tab.GetLength(0); i++)
            {
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    if (tab[i, j].WasCounted == false)
                    {
                        int score = 0;
                        int amount = 0;
                        FieldExplorerPart1(tab, i, j, ref score, ref amount, tab[i, j].Value);
                        int holder = score * amount;
                        Console.WriteLine($"{amount} * {score} = {holder} - {tab[i, j].Value}");
                        sum += holder;
                    }
                }
            }

            return sum;
        }

        public static void FieldExplorerPart1(Day12[,] tab, int currentI, int currentJ, ref int score, ref int amount, char value)
        {
            amount++;
            tab[currentI, currentJ].WasCounted = true;
            if (currentI + 1 < tab.GetLength(0) && tab[currentI + 1, currentJ].Value == value)
            {
                if (tab[currentI + 1, currentJ].WasCounted == false)
                {
                    FieldExplorerPart1(tab, currentI + 1, currentJ, ref score, ref amount, value);
                }
            }
            else
            {
                score++;
            }

            if (currentI - 1 >= 0 && tab[currentI - 1, currentJ].Value == value)
            {
                if (tab[currentI - 1, currentJ].WasCounted == false)
                {
                    FieldExplorerPart1(tab, currentI - 1, currentJ, ref score, ref amount, value);
                }
            }
            else
            {
                score++;
            }

            if (currentJ + 1 < tab.GetLength(1) && tab[currentI, currentJ + 1].Value == value)
            {
                if (tab[currentI, currentJ + 1].WasCounted == false)
                {
                    FieldExplorerPart1(tab, currentI, currentJ + 1, ref score, ref amount, value);
                }
            }
            else
            {
                score++;
            }

            if (currentJ - 1 >= 0 && tab[currentI, currentJ - 1].Value == value)
            {
                if (tab[currentI, currentJ - 1].WasCounted == false)
                {
                    FieldExplorerPart1(tab, currentI, currentJ - 1, ref score, ref amount, value);
                }
            }
            else
            {
                score++;
            }
        }
        public static int Part2()
        {
            int sum = 0;
            Day12[,] tab = GetObjectArray();

            for (int i = 0; i < tab.GetLength(0); i++)
            {
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    if (tab[i, j].WasCounted == false)
                    {
                        int score = 0;
                        int amount = 0;
                        FieldExplorerPart2(tab, i, j, ref score, ref amount, tab[i, j].Value);
                        int holder = score * amount;
                        Console.WriteLine($"{amount} * {score} = {holder} - {tab[i, j].Value}");
                        sum += holder;
                    }
                }
            }
            return sum;
        }

        public static void FieldExplorerPart2(Day12[,] tab, int currentI, int currentJ, ref int score, ref int amount, char value)
        {
            amount++;
            tab[currentI, currentJ].WasCounted = true;

            if (currentI - 1 >= 0 && tab[currentI - 1, currentJ].Value == value)
            {
                if (tab[currentI - 1, currentJ].WasCounted == false)
                {
                    FieldExplorerPart2(tab, currentI - 1, currentJ, ref score, ref amount, value);
                }
            }
            else if (tab[currentI, currentJ].CountedTop == false)
            {
                score++;
                MarkCounted(tab, currentI, currentJ, 0, -1, 0, value);
                MarkCounted(tab, currentI, currentJ, 0, 1, 0, value);
            }

            if (currentJ + 1 < tab.GetLength(1) && tab[currentI, currentJ + 1].Value == value)
            {
                if (tab[currentI, currentJ + 1].WasCounted == false)
                {
                    FieldExplorerPart2(tab, currentI, currentJ + 1, ref score, ref amount, value);
                }
            }
            else if (tab[currentI, currentJ].CountedRight == false)
            {
                score++;
                MarkCounted(tab, currentI, currentJ, -1, 0, 1, value);
                MarkCounted(tab, currentI, currentJ, 1, 0, 1, value);
            }

            if (currentI + 1 < tab.GetLength(0) && tab[currentI + 1, currentJ].Value == value)
            {
                if (tab[currentI + 1, currentJ].WasCounted == false)
                {
                    FieldExplorerPart2(tab, currentI + 1, currentJ, ref score, ref amount, value);
                }
            }
            else if (tab[currentI, currentJ].CountedBottom == false)
            {
                score++;
                MarkCounted(tab, currentI, currentJ, 0, -1, 2, value);
                MarkCounted(tab, currentI, currentJ, 0, 1, 2, value);
            }

            if (currentJ - 1 >= 0 && tab[currentI, currentJ - 1].Value == value)
            {
                if (tab[currentI, currentJ - 1].WasCounted == false)
                {
                    FieldExplorerPart2(tab, currentI, currentJ - 1, ref score, ref amount, value);
                }
            }
            else if (tab[currentI, currentJ].CountedLeft == false)
            {
                score++;
                MarkCounted(tab, currentI, currentJ, -1, 0, 3, value);
                MarkCounted(tab, currentI, currentJ, 1, 0, 3, value);
            }
        }

        private static void MarkCounted(Day12[,] tab, int currentI, int currentJ, int offsetI, int offsetJ, int direction, char value)
        {
            bool shouldContinue = true;
            while (shouldContinue)
            {
                if (currentI >= 0 && currentI < tab.GetLength(0) && currentJ >= 0 && currentJ < tab.GetLength(1))
                {
                    if (tab[currentI, currentJ].Value == value)
                    {
                        switch (direction)
                        {
                            case 0:
                                tab[currentI, currentJ].CountedTop = true; break;
                            case 1:
                                tab[currentI, currentJ].CountedRight = true; break;
                            case 2:
                                tab[currentI, currentJ].CountedBottom = true; break;
                            case 3:
                                tab[currentI, currentJ].CountedLeft = true; break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
                currentI += offsetI;
                currentJ += offsetJ;
            }
        }


        private static Day12[,] GetObjectArray()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string projectPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\"));
            string dayPath = Path.Combine(projectPath, "Day12");
            string filePath = Path.Combine(dayPath, "Day12test.txt");

            var lines = File.ReadAllLines(filePath);

            Day12[,] tab = new Day12[lines.Length, lines[0].Length];

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    tab[i, j] = new Day12(lines[i][j]);
                }
            }
            return tab;
        }
    }
}
