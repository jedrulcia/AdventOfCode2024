using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    class Day16
    {
        public bool explored { get; set; } = false;
        public char value { get; set; }

        public Day16(char value)
        {
            this.value = value;
            this.explored = false;
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

            FindShortestPathBFS(tab, currentI, currentJ, ref lowestScore);
            return lowestScore;
        }

		private static void FindShortestPathBFS(Day16[,] tab, int startI, int startJ, ref int lowestScore)
		{
			var directions = new (int dI, int dJ)[] { (0, 1), (1, 0), (0, -1), (-1, 0) };

			int rows = tab.GetLength(0);
			int cols = tab.GetLength(1);

			int[,][] minScores = new int[rows, cols][];
			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < cols; j++)
				{
					minScores[i, j] = new int[4] { int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue };
				}
			}

			Queue<(int currentI, int currentJ, int direction, int score)> queue = new();

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

				if (newI >= 0 && newI < rows && newJ >= 0 && newJ < cols && tab[newI, newJ].value != '#')
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

		public static Day16[,] DeepClone(Day16[,] original)
        {
            int rows = original.GetLength(0);
            int cols = original.GetLength(1);
            Day16[,] clone = new Day16[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    clone[i, j] = new Day16(original[i, j].value);
                    clone[i, j].explored = original[i, j].explored;
                }
            }
            return clone;
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
