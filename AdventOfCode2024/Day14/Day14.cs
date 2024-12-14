using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
	internal class Day14
	{
		public int pX { get; set; }
		public int pY { get; set; }
		public int vX { get; set; }
		public int vY { get; set; }
		public Day14(int pX, int pY, int vX, int vY)
		{
			this.pX = pX;
			this.pY = pY;
			this.vX = vX;
			this.vY = vY;
		}

		public static List<Day14> GetList()
		{
			string basePath = AppDomain.CurrentDomain.BaseDirectory;
			string projectPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\"));
			string dayPath = Path.Combine(projectPath, "Day14");
			string filePath = Path.Combine(dayPath, "Day14.txt");
			string[] lines = File.ReadAllLines(filePath);


			List<Day14> list = new List<Day14>();

			for (int i = 0; i < lines.Length; i++)
			{
				string line = lines[i];

				string[] parts = line.Split(' ');

				string[] pParts = parts[0].Substring(2).Split(',');
				string[] vParts = parts[1].Substring(2).Split(',');

				int pX = int.Parse(pParts[0]);
				int pY = int.Parse(pParts[1]);
				int vX = int.Parse(vParts[0]);
				int vY = int.Parse(vParts[1]);

				list.Add(new Day14(pX, pY, vX, vY));
			}

			return list;
		}

		public static int Part1()
		{
			List<Day14> list = GetList();

			int wide = 101;
			int tall = 103;

			for (int i = 0; i < 100; i++)
			{
				char[,] tab = new char[tall, wide];
				foreach (var item in list)
				{
					tab[item.pY, item.pX] = '0';

					int currentX = item.pX + item.vX;
					int currentY = item.pY + item.vY;

					if (currentX < 0)
					{
						currentX = wide + currentX;
					}
					else if (currentX >= wide)
					{
						currentX = currentX - wide;
					}

					if (currentY < 0)
					{
						currentY = tall + currentY;
					}
					else if (currentY >= tall)
					{
						currentY = currentY - tall;
					}

					item.pX = currentX;
					item.pY = currentY;

					tab[item.pY, item.pX] = '1';
				}
			}

			int firstQuadrant = 0;
			int secondQuadrant = 0;
			int thirdQuadrant = 0;
			int fourthQuadrant = 0;

			foreach (var item in list)
			{
				if (item.pX < wide / 2 && item.pY < tall / 2)
				{
					firstQuadrant++;
				}
				else if (item.pX > wide / 2 && item.pY < tall / 2)
				{
					secondQuadrant++;
				}
				else if (item.pX < wide / 2 && item.pY > tall / 2)
				{
					thirdQuadrant++;
				}
				else if (item.pX > wide / 2 && item.pY > tall / 2)
				{
					fourthQuadrant++;
				}

			}

			int result = firstQuadrant * secondQuadrant * thirdQuadrant * fourthQuadrant;
			return result; 
		}

		public static int Part2()
		{
			List<Day14> list = GetList();

			int wide = 101;
			int tall = 103;

			for (int i = 0; i < 10000000; i++)
			{
				char[,] tab = new char[tall, wide];
				foreach (var item in list)
				{
					tab[item.pY, item.pX] = '0';

					int currentX = item.pX + item.vX;
					int currentY = item.pY + item.vY;

					if (currentX < 0)
					{
						currentX = wide + currentX;
					}
					else if (currentX >= wide)
					{
						currentX = currentX - wide;
					}

					if (currentY < 0)
					{
						currentY = tall + currentY;
					}
					else if (currentY >= tall)
					{
						currentY = currentY - tall;
					}

					item.pX = currentX;
					item.pY = currentY;

					tab[item.pY, item.pX] = '1';
				}

				Dictionary<int, int> dictX = new Dictionary<int, int>();
				Dictionary<int, int> dictY = new Dictionary<int, int>();
				foreach (var item in list)
				{
					if (dictX.ContainsKey(item.pX))
					{
						dictX[item.pX]++;
					}
					else
					{
						dictX[item.pX] = 1;
					}

					if (dictY.ContainsKey(item.pY))
					{
						dictY[item.pY]++;
					}
					else
					{
						dictY[item.pY] = 1;
					}
				}

				bool hasXGreaterThan35 = dictX.Values.Any(value => value > 30);
				bool hasYGreaterThan35 = dictY.Values.Any(value => value > 30);

				if (hasXGreaterThan35 && hasYGreaterThan35)
				{

					for (int j = 0; j < tab.GetLength(0); j++)
					{
						for (int k = 0; k < tab.GetLength(1); k++)
						{
							if (tab[j, k] == '1')
							{
								Console.Write('1');
							}
							else
							{
								Console.Write('.');
							}
						}
						Console.WriteLine();
					}
					Console.WriteLine();
					return i + 1;
				}
			}
			return 0;
		}
	}
}
