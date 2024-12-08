using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
	class Day8
	{
		public char Value { get; set; }
		public bool PlacedAntinode { get; set; }


		private Day8(char value)
		{
			this.Value = value;
			this.PlacedAntinode = false;
		}

		public static int Part1()
		{
			Day8[,] tab = GetObjectArray();
			char[,] tabChar = ConvertFromTxtToArray();
			int counter = 0;

			for (int i = 0; i < tab.GetLength(0); i++)
			{
				for (int j = 0; j < tab.GetLength(1); j++)
				{
					if (tab[i, j].Value != '.')
					{
						bool firstLoop = true;
						for (int k = i; k < tab.GetLength(0); k++)
						{
							for (int l = 0; l < tab.GetLength(1); l++)
							{
								if (firstLoop)
								{
									if (j + 1 == tab.GetLength(1))
									{
										k++;
									}
									else
									{
										l = j + 1;
									}
									firstLoop = false;
								}
								if (tab[i, j].Value == tab[k, l].Value)
								{
									int differenceI = 0;
									int differenceJ = 0;

									int antinodeFirstI = 0;
									int antinodeFirstJ = 0;

									int antinodeSecondI = 0;
									int antinodeSecondJ = 0;

									if (k > i)
									{
										differenceI = k - i;
										antinodeFirstI = i - differenceI;
										antinodeSecondI = k + differenceI;
									}
									else
									{
										antinodeFirstI = i;
										antinodeSecondI = i;
									}
									if (j > l)
									{
										differenceJ = j - l;
										antinodeFirstJ = j + differenceJ;
										antinodeSecondJ = l - differenceJ;
									}
									else if (l > j)
									{
										differenceJ = l - j;
										antinodeFirstJ = j - differenceJ;
										antinodeSecondJ = l + differenceJ;
									}
									else
									{
										antinodeFirstJ = j;
										antinodeSecondJ = j;
									}

									if (antinodeFirstI >= 0 && antinodeFirstI < tab.GetLength(0) && antinodeFirstJ >= 0 && antinodeFirstJ < tab.GetLength(1))
									{
										if (tab[antinodeFirstI, antinodeFirstJ].PlacedAntinode == false)
										{
											counter++;
											tab[antinodeFirstI, antinodeFirstJ].PlacedAntinode = true;
											tabChar[antinodeFirstI, antinodeFirstJ] = '#';
										}
									}

									if (antinodeSecondI >= 0 && antinodeSecondI < tab.GetLength(0) && antinodeSecondJ >= 0 && antinodeSecondJ < tab.GetLength(1))
									{
										if (tab[antinodeSecondI, antinodeSecondJ].PlacedAntinode == false)
										{
											counter++;
											tab[antinodeSecondI, antinodeSecondJ].PlacedAntinode = true;
											tabChar[antinodeSecondI, antinodeSecondJ] = '#';
										}
									}
								}
							}
						}
					}
				}
			}
			return counter;
		}

		public static int Part2()
		{
			Day8[,] tab = GetObjectArray();
			char[,] tabChar = ConvertFromTxtToArray();
			bool shouldContinue = true;
			int counter = 0;

			while (shouldContinue)
			{
				shouldContinue = false;
				for (int i = 0; i < tab.GetLength(0); i++)
				{
					for (int j = 0; j < tab.GetLength(1); j++)
					{
						if (tab[i, j].Value != '.' || tabChar[i, j] == '#')
						{
							bool firstLoop = true;
							for (int k = i; k < tab.GetLength(0); k++)
							{
								for (int l = 0; l < tab.GetLength(1); l++)
								{
									if (firstLoop)
									{
										if (j + 1 == tab.GetLength(1) && k + 1 < tab.GetLength(0))
										{
											k++;
										}
										else if (j + 1 > tab.GetLength(1))
										{
											l = j + 1;
										}
										firstLoop = false;
									}
									if (tab[i, j].Value == tab[k, l].Value || tabChar[k, l] == '#')
									{
										int differenceI = 0;
										int differenceJ = 0;

										int antinodeFirstI = 0;
										int antinodeFirstJ = 0;

										int antinodeSecondI = 0;
										int antinodeSecondJ = 0;

										if (k > i)
										{
											differenceI = k - i;
											antinodeFirstI = i - differenceI;
											antinodeSecondI = k + differenceI;
										}
										else
										{
											antinodeFirstI = i;
											antinodeSecondI = i;
										}
										if (j > l)
										{
											differenceJ = j - l;
											antinodeFirstJ = j + differenceJ;
											antinodeSecondJ = l - differenceJ;
										}
										else if (l > j)
										{
											differenceJ = l - j;
											antinodeFirstJ = j - differenceJ;
											antinodeSecondJ = l + differenceJ;
										}
										else
										{
											antinodeFirstJ = j;
											antinodeSecondJ = j;
										}

										if (antinodeFirstI >= 0 && antinodeFirstI < tab.GetLength(0) && antinodeFirstJ >= 0 && antinodeFirstJ < tab.GetLength(1))
										{
											if (tab[antinodeFirstI, antinodeFirstJ].PlacedAntinode == false)
											{
												counter++;
												tab[antinodeFirstI, antinodeFirstJ].PlacedAntinode = true;
												tabChar[antinodeFirstI, antinodeFirstJ] = '#';
												shouldContinue = true;
											}
										}

										if (antinodeSecondI >= 0 && antinodeSecondI < tab.GetLength(0) && antinodeSecondJ >= 0 && antinodeSecondJ < tab.GetLength(1))
										{
											if (tab[antinodeSecondI, antinodeSecondJ].PlacedAntinode == false)
											{
												counter++;
												tab[antinodeSecondI, antinodeSecondJ].PlacedAntinode = true;
												tabChar[antinodeSecondI, antinodeSecondJ] = '#';
												shouldContinue = true;
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return counter;
		}

		private static Day8[,] GetObjectArray()
		{
			string basePath = AppDomain.CurrentDomain.BaseDirectory;
			string projectPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\"));
			string dayPath = Path.Combine(projectPath, "Day8");
			string filePath = Path.Combine(dayPath, "Day8test.txt");

			var lines = File.ReadAllLines(filePath);

			Day8[,] tab = new Day8[lines.Length, lines[0].Length];

			for (int i = 0; i < lines.Length; i++)
			{
				for (int j = 0; j < lines[i].Length; j++)
				{
					tab[i, j] = new Day8(lines[i][j]);
				}
			}
			return tab;
		}

		// DEBUGGING
		private static char[,] ConvertFromTxtToArray()
		{
			string basePath = AppDomain.CurrentDomain.BaseDirectory;
			string projectPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\"));
			string dayPath = Path.Combine(projectPath, "Day8");
			string filePath = Path.Combine(dayPath, "Day8test.txt");

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
		private static void PrintArray(char[,] tab)
		{
			for (int i = 0; i < tab.GetLength(0); i++)
			{
				for (int j = 0; j < tab.GetLength(1); j++)
				{
					Console.Write(tab[i, j]);
				}
				Console.WriteLine();
			}
			Console.WriteLine();
		}
	}
}
