using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    class Day16
    {
        public char value { get; set; }

        public Day16(char value)
        {
            this.value = value;
        }


        public static int Part1()
        {
            int lowestScore = int.MaxValue;
            Day16[,] tab = ConvertFromTxtToArray();

            int currentI = 0;
            int currentJ = 0;

            for (int i = 0; i < tab.GetLength(0); i++)
            {
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    if (tab[i,j].value == 'S')
                    {
                        currentI = i;
                        currentJ = j;
                        tab[i, j].value = '.';
                    }
                }
            }

            ShortestPathPart1(tab, currentI, currentJ, ref lowestScore);
            return lowestScore;
        }

		private static void ShortestPathPart1(Day16[,] tab, int startI, int startJ, ref int lowestScore)
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

				if (tab[currentI, currentJ].value == 'E')
				{
					lowestScore = Math.Min(lowestScore, score);
					continue;
				}

				int newI = currentI + directions[direction].dI;
				int newJ = currentJ + directions[direction].dJ;

				if (tab[newI, newJ].value != '#')
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
					int newScore = score + 1000;
					if (newScore < minScores[currentI, currentJ][newDirection])
					{
						minScores[currentI, currentJ][newDirection] = newScore;
						queue.Enqueue((currentI, currentJ, newDirection, newScore));
					}
				}
			}
        }


        // ========================================================== PART2 ==========================================================
        public static int Part2()
        {
            int lowestScore = int.MaxValue;
            HashSet<(int, int)> bestPlaces = new HashSet<(int, int)>();
            Day16[,] tab = ConvertFromTxtToArray();

            int currentI = 0;
            int currentJ = 0;

            for (int i = 0; i < tab.GetLength(0); i++)
            {
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    if (tab[i, j].value == 'S')
                    {
                        currentI = i;
                        currentJ = j;
                        tab[i, j].value = '.';
                    }
                }
            }

            ShortestPathPart2(tab, currentI, currentJ, ref lowestScore, ref bestPlaces);

            return bestPlaces.Count();
        }

        private static void ShortestPathPart2(Day16[,] original, int startI, int startJ, ref int lowestScore, ref HashSet<(int, int)> bestPlaces)
        {
            var directions = new (int dI, int dJ)[] { (0, 1), (1, 0), (0, -1), (-1, 0) };
            int[,][] minScores = new int[original.GetLength(0), original.GetLength(1)][];
            Queue<(int currentI, int currentJ, int direction, int score, HashSet<(int, int)> places)> queue = new();

            for (int i = 0; i < original.GetLength(0); i++)
            {
                for (int j = 0; j < original.GetLength(1); j++)
                {
                    minScores[i, j] = new int[4] { int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue };
                }
            }

            int startDirection = 0;
            minScores[startI, startJ][startDirection] = 0;
            HashSet<(int, int)> hashset = new HashSet<(int, int)>();
            queue.Enqueue((startI, startJ, startDirection, 0, hashset));

            while (queue.Count > 0)
            {
                var (currentI, currentJ, direction, score, places) = queue.Dequeue();

                places.Add((currentI, currentJ));

                if (score > lowestScore) continue;

                if (original[currentI, currentJ].value == 'E')
                {
                    if (score <= lowestScore)
                    {
                        if (score < lowestScore)
                        {
                            bestPlaces.Clear();
                            lowestScore = score;
                        }
                        bestPlaces.UnionWith(places);
                    }
                    continue;
                }

                int newI = currentI + directions[direction].dI;
                int newJ = currentJ + directions[direction].dJ;

                if (original[newI, newJ].value != '#')
                {
                    int newScore = score + 1;
                    if (newScore <= minScores[newI, newJ][direction])
                    {
                        HashSet<(int, int)> newPlaces = new HashSet<(int, int)>(places);
                        minScores[newI, newJ][direction] = newScore;
                        queue.Enqueue((newI, newJ, direction, newScore, newPlaces));
                    }
                }

                for (int rotation = -1; rotation <= 1; rotation += 2)
                {
                    int newDirection = (direction + rotation + 4) % 4;
                    int newScore = score + 1000;
                    if (newScore <= minScores[currentI, currentJ][newDirection])
                    {
                        HashSet<(int, int)> newPlaces = new HashSet<(int, int)>(places);
                        minScores[currentI, currentJ][newDirection] = newScore;
                        queue.Enqueue((currentI, currentJ, newDirection, newScore, newPlaces));
                    }
                }
            }
        }

        private static Day16[,] ConvertFromTxtToArray()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string projectPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\"));
            string dayPath = Path.Combine(projectPath, "Day16");
            string filePath = Path.Combine(dayPath, "Day16.txt");

            var lines = File.ReadAllLines(filePath);

            Day16[,] tab = new Day16[lines.Length, lines[0].Length];

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    tab[i, j]= new Day16(lines[i][j]);
                }
            }

            return tab;
        }


    }
}
