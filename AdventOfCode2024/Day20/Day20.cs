using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    class Day20
    {
        public char value { get; set; }
        public int picoseconds { get; set; }
        public bool explored { get; set; }
        public Day20(char value)
        {
            this.value = value;
            this.picoseconds = -1;
            this.explored = false;
        }

		// 1046587
		// 1030705

		public static int Part2()
        {
            Day20[,] array = ConvertFromTxtToArray();

            int startI = 0;
            int startJ = 0;

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j].value == 'S')
                    {
                        startI = i;
                        startJ = j;
                        array[i, j].picoseconds = 0;
                        array[i, j].explored = true;
                    }
                }
            }

            AddPicosecondsToFields(array, startI, startJ);

            int counter = 0;

            FindFasterWaysPart2(array, startI, startJ, ref counter);

            return counter;
        }

        public static void FindFasterWaysPart2(Day20[,] array, int currentI, int currentJ, ref int counter)
        {
            var directions = new (int dI, int dJ)[] { (-1, 0), (0, 1), (1, 0), (0, -1) };
            var cheatDirections = new (int dI, int dJ)[] { (-1, 1), (1, 1), (1, -1), (-1, -1) };
			HashSet<(int, int)> explored = new HashSet<(int, int)>();

			while (array[currentI, currentJ].value != 'E')
            {
				foreach (var cheatDirection in cheatDirections)  
				{
                    MarkExplored(array, currentI, currentJ, cheatDirection, ref explored);
				}

				counter += explored.Count();

				explored.Clear();

				foreach (var direction in directions)
                {
                    if ((array[currentI + direction.dI, currentJ + direction.dJ].value == '.' ||
                    array[currentI + direction.dI, currentJ + direction.dJ].value == 'E') &&
                    array[currentI + direction.dI, currentJ + direction.dJ].explored == false)
                    {
                        array[currentI, currentJ].explored = true;
                        currentI += direction.dI;
                        currentJ += direction.dJ;
                        break;
                    }
                }
            }
        }

        public static void MarkExplored(Day20[,] array, int currentI, int currentJ, (int dI, int dJ) cheatDirections, ref HashSet<(int, int)> explored)
		{
            int picosecondsSaved = 100;

            int startI = currentI;
            int startJ = currentJ;

            for (int i = 0; i < 21; i += 2)
            {
                int offsetI = 0;
                int offsetJ = 0;

				if (currentI < 0 || currentI >= array.GetLength(0) || currentJ < 0 || currentJ >= array.GetLength(1))
				{
					break;
				}


				if (array[currentI, currentJ].value == '.' ||
					array[currentI, currentJ].value == 'E')
				{
					int difference = array[currentI, currentJ].picoseconds - array[startI, startJ].picoseconds - i;
					if (difference >= picosecondsSaved)
					{
                        explored.Add((currentI, currentJ));
					}
				}

				for (int j = i; j < 20; j++)
				{
					offsetI += cheatDirections.dI; 

                    if (currentI + offsetI >= 0 && currentI + offsetI < array.GetLength(0))
                    {
                        if (array[currentI + offsetI, currentJ].value == '.' ||
                            array[currentI + offsetI, currentJ].value == 'E')
						{
							int difference = array[currentI + offsetI, currentJ].picoseconds - array[startI, startJ].picoseconds - j;
							if (difference >= picosecondsSaved)
							{
                                explored.Add((currentI + offsetI, currentJ));
							}
						}
					}

					offsetJ += cheatDirections.dJ;

                    if (currentJ + offsetJ >= 0 && currentJ + offsetJ < array.GetLength(1))
                    {
                        if (array[currentI, currentJ + offsetJ].value == '.' ||
                            array[currentI, currentJ + offsetJ].value == 'E')
                        {
                            int difference = array[currentI, currentJ + offsetJ].picoseconds - array[startI, startJ].picoseconds - j;
                            if (difference >= picosecondsSaved)
                            {
                                explored.Add((currentI, currentJ + offsetJ));
                            }
                        }
                    }

				}
				currentI += cheatDirections.dI;
				currentJ += cheatDirections.dJ;
			}
		}



		// =========================================================
        private static Day20[,] ConvertFromTxtToArray()
		{
			string basePath = AppDomain.CurrentDomain.BaseDirectory;
			string projectPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\"));
			string dayPath = Path.Combine(projectPath, "Day20");
			string filePath = Path.Combine(dayPath, "Day20.txt");

			var lines = File.ReadAllLines(filePath);

			Day20[,] tab = new Day20[lines.Length, lines[0].Length];

			for (int i = 0; i < lines.Length; i++)
			{
				for (int j = 0; j < lines[i].Length; j++)
				{
					tab[i, j] = new Day20(lines[i][j]);
				}
			}

			return tab;
		}

		public static int Part1()
        {
            Day20[,] array = ConvertFromTxtToArray();

            int currentI = 0;
            int currentJ = 0;

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j].value == 'S')
                    {
                        currentI = i;
                        currentJ = j;
                        array[i, j].picoseconds = 0;
                        array[i, j].explored = true;
                    }
                }
            }

            AddPicosecondsToFields(array, currentI, currentJ);

            int counter = 0;

            FindFasterWaysPart1(array, currentI, currentJ, ref counter);

            // 44
            return counter;
        }

        public static void FindFasterWaysPart1(Day20[,] array, int currentI, int currentJ, ref int counter)
        {
            var directions = new (int dI, int dJ)[] { (-1, 0), (0, 1), (1, 0), (0, -1) };
            var cheatDirections = new (int dI, int dJ)[] { (-2, 0), (0, 2), (2, 0), (0, -2) };
            List<int> picosecondList = new List<int>();

            while (array[currentI, currentJ].value != 'E')
            {
                for (int j = 0; j < cheatDirections.Length; j++)
                {
                    if (currentI + cheatDirections[j].dI >= 0 && currentI + cheatDirections[j].dI < array.GetLength(0) &&
                        currentJ + cheatDirections[j].dJ >= 0 && currentJ + cheatDirections[j].dJ < array.GetLength(1))
                    {
                        if (array[currentI + cheatDirections[j].dI, currentJ + cheatDirections[j].dJ].value == '.' ||
                        array[currentI + cheatDirections[j].dI, currentJ + cheatDirections[j].dJ].value == 'E')
                        {
                            int difference = array[currentI + cheatDirections[j].dI, currentJ + cheatDirections[j].dJ].picoseconds - array[currentI, currentJ].picoseconds - 2;
                            if (difference >= 100)
                            {
                                picosecondList.Add(difference);
                                counter++;
                            }
                        }
                    }
                }

                for (int i = 0; i < directions.Length; i++)
                {
                    if ((array[currentI + directions[i].dI, currentJ + directions[i].dJ].value == '.' ||
                    array[currentI + directions[i].dI, currentJ + directions[i].dJ].value == 'E') &&
                    array[currentI + directions[i].dI, currentJ + directions[i].dJ].explored == false)
                    {
                        array[currentI, currentJ].explored = true;
                        currentI += directions[i].dI;
                        currentJ += directions[i].dJ;
                        break;
                    }
                }
            }

            picosecondList.Sort();
            foreach (var item in picosecondList) Console.WriteLine(item);
        }

        public static void AddPicosecondsToFields(Day20[,] array, int currentI, int currentJ)
        {
            var directions = new (int dI, int dJ)[] { (-1, 0), (0, 1), (1, 0), (0, -1) };

            while (array[currentI, currentJ].value != 'E')
            {
                for (int i = 0; i < directions.Length; i++)
                {
                    if ((array[currentI + directions[i].dI, currentJ + directions[i].dJ].value == '.' ||
                        array[currentI + directions[i].dI, currentJ + directions[i].dJ].value == 'E') &&
                        array[currentI + directions[i].dI, currentJ + directions[i].dJ].picoseconds == -1)
                    {
                        array[currentI + directions[i].dI, currentJ + directions[i].dJ].picoseconds = array[currentI, currentJ].picoseconds + 1;
                        currentI += directions[i].dI;
                        currentJ += directions[i].dJ;
                    }
                }
            }
        }

    }
}
