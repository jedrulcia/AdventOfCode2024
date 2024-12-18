using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    class Day18
    {
        public static int Part2()
        {
            List<(int, int)> coordinates = GetList();
            char[,] tab = GetArray();

            for (int i = 0; i < coordinates.Count; i++)
            {
                int currentI = (coordinates[i]).Item1;
                int currentJ = (coordinates[i]).Item2;
                tab[currentI, currentJ] = '#';

                int lowestScore = int.MaxValue;
                ShortestPathPart2(tab, 1, 1, ref lowestScore);

                if (lowestScore == int.MaxValue)
                {
                    Console.WriteLine($"{coordinates[i].Item2 - 1},{coordinates[i].Item1 - 1}");
                    return i + 1;
                }
            }
            return 0;
        }

        private static void ShortestPathPart2(char[,] tab, int startI, int startJ, ref int lowestScore)
        {
            var directions = new (int dI, int dJ)[] { (0, 1), (1, 0), (0, -1), (-1, 0) };
            int[,][] minScores = new int[tab.GetLength(0), tab.GetLength(1)][];
            Queue<(int currentI, int currentJ, int direction, int score)> queue = new();

            for (int i = 0; i < tab.GetLength(0); i++)
            {
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    minScores[i, j] = new int[4] { int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue };
                }
            }

            int startDirection = 0;
            minScores[startI, startJ][startDirection] = 0;
            queue.Enqueue((startI, startJ, startDirection, 0));

            while (queue.Count > 0)
            {
                var (currentI, currentJ, direction, score) = queue.Dequeue();

                if (score >= lowestScore) continue;

                if (currentI == tab.GetLength(0) - 2 && currentJ == tab.GetLength(1) - 2)
                {
                    lowestScore = Math.Min(lowestScore, score);
                    continue;
                }

                int newI = currentI + directions[direction].dI;
                int newJ = currentJ + directions[direction].dJ;

                if (tab[newI, newJ] != '#')
                {
                    int newScore = score + 1;
                    if (newScore < minScores[newI, newJ][direction])
                    {
                        minScores[newI, newJ][direction] = newScore;
                        queue.Enqueue((newI, newJ, direction, newScore));
                    }
                }

                for (int rotation = -1; rotation <= 1; rotation += 2)
                {
                    int newDirection = (direction + rotation + 4) % 4;
                    if (score < minScores[currentI, currentJ][newDirection])
                    {
                        minScores[currentI, currentJ][newDirection] = score;
                        queue.Enqueue((currentI, currentJ, newDirection, score));
                    }
                }
            }
        }

        public static char[,] GetArray()
        {
            char[,] tab = new char[73, 73];

            for (int i = 0; i < tab.GetLength(0); i++)
            {
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    if (i == 0 || j == 0 || i + 1 == tab.GetLength(0) || j + 1 == tab.GetLength(1))
                    {
                        tab[i, j] = '#';
                    }
                    else
                    {
                        tab[i, j] = '.';
                    }
                }
            }
            return tab;
        }

        public static int Part1()
        {
            List<(int, int)> coordinates = GetList();
            char[,] tab = GetArray();

            for (int i = 0; i < 1024; i++)
            {
                int currentI = (coordinates[i]).Item1;
                int currentJ = (coordinates[i]).Item2;

                tab[currentI, currentJ] = '#';
            }

            int lowestScore = int.MaxValue;

            ShortestPathPart1(tab, 1, 1, ref lowestScore);


            return lowestScore;
        }

        private static void ShortestPathPart1(char[,] tab, int startI, int startJ, ref int lowestScore)
        {
            var directions = new (int dI, int dJ)[] { (0, 1), (1, 0), (0, -1), (-1, 0) };
            int[,][] minScores = new int[tab.GetLength(0), tab.GetLength(1)][];
            Queue<(int currentI, int currentJ, int direction, int score)> queue = new();

            for (int i = 0; i < tab.GetLength(0); i++)
            {
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    minScores[i, j] = new int[4] { int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue };
                }
            }

            int startDirection = 0;
            minScores[startI, startJ][startDirection] = 0;
            queue.Enqueue((startI, startJ, startDirection, 0));

            while (queue.Count > 0)
            {
                var (currentI, currentJ, direction, score) = queue.Dequeue();

                if (score >= lowestScore) continue;

                if (currentI == 71 && currentJ == 71)
                {
                    lowestScore = Math.Min(lowestScore, score);
                    continue;
                }

                int newI = currentI + directions[direction].dI;
                int newJ = currentJ + directions[direction].dJ;

                if (tab[newI, newJ] != '#')
                {
                    int newScore = score + 1;
                    if (newScore < minScores[newI, newJ][direction])
                    {
                        minScores[newI, newJ][direction] = newScore;
                        queue.Enqueue((newI, newJ, direction, newScore));
                    }
                }

                for (int rotation = -1; rotation <= 1; rotation += 2)
                {
                    int newDirection = (direction + rotation + 4) % 4;
                    if (score < minScores[currentI, currentJ][newDirection])
                    {
                        minScores[currentI, currentJ][newDirection] = score;
                        queue.Enqueue((currentI, currentJ, newDirection, score));
                    }
                }
            }
        }

        public static List<(int, int)> GetList()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string projectPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\"));
            string dayPath = Path.Combine(projectPath, "Day18");
            string filePath = Path.Combine(dayPath, "Day18.txt");
            string[] lines = File.ReadAllLines(filePath);

            List<(int, int)> coordinates = new List<(int, int)>();

            foreach (var item in lines)
            {
                string[] parts = item.Split(',');
                int part1 = Convert.ToInt32(parts[1]) + 1;
                int part2 = Convert.ToInt32(parts[0]) + 1;

                coordinates.Add((part1, part2));

            }
            return coordinates;
        }
    }


}
