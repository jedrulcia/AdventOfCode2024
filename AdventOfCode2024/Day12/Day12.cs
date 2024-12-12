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
		public int? BlockId { get; set; } = null;

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


		// ================================================================================== PART2 ==================================================================================

		//802820 - too low
		public static int Part2()
		{
			int sum = 0;
			int id = 0;
			Day12[,] tab = GetObjectArray();

			for (int i = 0; i < tab.GetLength(0); i++)
			{
				for (int j = 0; j < tab.GetLength(1); j++)
				{
					if (tab[i, j].WasCounted == false)
					{
						int score = 0;
						int amount = 0;
						FieldExplorerPart2(tab, i, j, ref score, ref amount, tab[i, j].Value, id);
						ScoreCounter(tab, i, j, ref score, tab[i, j].Value, id);
						int holder = score * amount;
						Console.WriteLine($"{amount} * {score} = {holder} - {tab[i, j].Value}");
						sum += holder;
						id++;
					}
				}
			}
			return sum;
		}

		public static void FieldExplorerPart2(Day12[,] tab, int currentI, int currentJ, ref int score, ref int amount, char value, int id)
		{
			amount++;
			tab[currentI, currentJ].WasCounted = true;
			tab[currentI, currentJ].BlockId = id;

			if (currentI - 1 >= 0 && tab[currentI - 1, currentJ].Value == value)
			{
				if (tab[currentI - 1, currentJ].WasCounted == false)
				{
					FieldExplorerPart2(tab, currentI - 1, currentJ, ref score, ref amount, value, id);
				}
			}

			if (currentJ + 1 < tab.GetLength(1) && tab[currentI, currentJ + 1].Value == value)
			{
				if (tab[currentI, currentJ + 1].WasCounted == false)
				{
					FieldExplorerPart2(tab, currentI, currentJ + 1, ref score, ref amount, value, id);
				}
			}

			if (currentI + 1 < tab.GetLength(0) && tab[currentI + 1, currentJ].Value == value)
			{
				if (tab[currentI + 1, currentJ].WasCounted == false)
				{
					FieldExplorerPart2(tab, currentI + 1, currentJ, ref score, ref amount, value, id);
				}
			}

			if (currentJ - 1 >= 0 && tab[currentI, currentJ - 1].Value == value)
			{
				if (tab[currentI, currentJ - 1].WasCounted == false)
				{
					FieldExplorerPart2(tab, currentI, currentJ - 1, ref score, ref amount, value, id);
				}
			}
		}

		private static void ScoreCounter(Day12[,] tab, int currentI, int currentJ, ref int score, char value, int id)
		{
			for (int i = 0; i < tab.GetLength(0); i++)
			{
				for (int j = 0; j < tab.GetLength(1); j++)
				{
					if (tab[i, j].Value == value && tab[i, j].BlockId == id)
					{
						if (i - 1 < 0 || tab[i - 1, j].Value != value)
						{
							if (tab[i, j].CountedTop == false)
							{
								score++;
								MarkTop(tab, i, j, value, 5);
							}
						}
						if (j + 1 >= tab.GetLength(1) || tab[i, j + 1].Value != value)
						{
							if (tab[i, j].CountedRight == false)
							{
								score++;
								MarkRight(tab, i, j, value, 5);
							}
						}
						if (i + 1 >= tab.GetLength(0) || tab[i + 1, j].Value != value)
						{
							if (tab[i, j].CountedBottom == false)
							{
								score++;
								MarkBottom(tab, i, j, value, 5);
							}
						}
						if (j - 1 < 0 || tab[i, j - 1].Value != value)
						{
							if (tab[i, j].CountedLeft == false)
							{
								score++;
								MarkLeft(tab, i, j, value, 5);
							}
						}
					}
				}
			}
		}

		private static void MarkTop(Day12[,] tab, int i, int j, char value, int direction)
		{
			tab[i, j].CountedTop = true;
			if (j - 1 >= 0 && tab[i, j - 1].Value == value && (i - 1 < 0 || tab[i - 1, j].Value != value) && (direction == 5 || direction == 3))
			{
				MarkTop(tab, i, j - 1, value, 3);
			}
			if (j + 1 < tab.GetLength(1) && tab[i, j + 1].Value == value && (i - 1 < 0 || tab[i - 1, j].Value != value) && (direction == 5 || direction == 1))
			{
				MarkTop(tab, i, j + 1, value, 1);
			}
		}

		private static void MarkRight(Day12[,] tab, int i, int j, char value, int direction)
		{
			tab[i, j].CountedRight = true;
			if (i - 1 >= 0 && tab[i - 1, j].Value == value && (j + 1 >= tab.GetLength(1) || tab[i, j + 1].Value != value) && (direction == 5 || direction == 0))
			{
				MarkRight(tab, i - 1, j, value, 0);
			}
			if (i + 1 < tab.GetLength(0) && tab[i + 1, j].Value == value && (j + 1 >= tab.GetLength(1) || tab[i, j + 1].Value != value) && (direction == 5 || direction == 2))
			{
				MarkRight(tab, i + 1, j, value, 2);
			}
		}

		private static void MarkBottom(Day12[,] tab, int i, int j, char value, int direction)
		{
			tab[i, j].CountedBottom = true;
			if (j - 1 >= 0 && tab[i, j - 1].Value == value && (i + 1 >= tab.GetLength(0) || tab[i + 1, j].Value != value) && (direction == 5 || direction == 3))
			{
				MarkBottom(tab, i, j - 1, value, 3);
			}
			if (j + 1 < tab.GetLength(1) && tab[i, j + 1].Value == value && (i + 1 >= tab.GetLength(0) || tab[i + 1, j].Value != value) && (direction == 5 || direction == 1))
			{
				MarkBottom(tab, i, j + 1, value, 1);
			}
		}

		private static void MarkLeft(Day12[,] tab, int i, int j, char value, int direction)
		{
			tab[i, j].CountedLeft = true;
			if (i - 1 >= 0 && tab[i - 1, j].Value == value && (j - 1 < 0 || tab[i, j - 1].Value != value) && (direction == 5 || direction == 0))
			{
				MarkLeft(tab, i - 1, j, value, 0);
			}
			if (i + 1 < tab.GetLength(0) && tab[i + 1, j].Value == value && (j - 1 < 0 || tab[i, j - 1].Value != value) && (direction == 5 || direction == 2))
			{
				MarkLeft(tab, i + 1, j, value, 2);
			}
		}



		private static Day12[,] GetObjectArray()
		{
			string basePath = AppDomain.CurrentDomain.BaseDirectory;
			string projectPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\"));
			string dayPath = Path.Combine(projectPath, "Day12");
			string filePath = Path.Combine(dayPath, "Day12.txt");

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
