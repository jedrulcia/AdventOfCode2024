using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
	class Day15
	{
		public static int Part2()
		{
			(List<char> arrowList, char[,] tab) = ProcessTxtFilePart2();
			int sum = 0;

			int currentRobotI = 0;
			int currentRobotJ = 0;

			for (int i = 0; i < tab.GetLength(0); i++)
			{
				for (int j = 0; j < tab.GetLength(1); j++)
				{
					if (tab[i, j] == '@')
					{
						currentRobotI = i;
						currentRobotJ = j;
					}
				}
			}

			foreach (var item in arrowList)
			{
				switch (item)
				{
					case '^': PushPart2(tab, ref currentRobotI, ref currentRobotJ, -1, 0); break;
					case '>': PushPart2(tab, ref currentRobotI, ref currentRobotJ, 0, 1); break;
					case 'v': PushPart2(tab, ref currentRobotI, ref currentRobotJ, 1, 0); break;
					case '<': PushPart2(tab, ref currentRobotI, ref currentRobotJ, 0, -1); break;
				}
			}

			for (int i = 0; i < tab.GetLength(0); i++)
			{
				for (int j = 0; j < tab.GetLength(1); j++)
				{
					Console.Write(tab[i, j]);
					if (tab[i, j] == '[')
					{
						sum += (100 * i) + j;
					}
				}
				Console.WriteLine();
			}

			return sum;
		}

		public static bool PushPart2(char[,] tab, ref int currentRobotI, ref int currentRobotJ, int offsetI, int offsetJ)
		{

			if (tab[currentRobotI + offsetI, currentRobotJ + offsetJ] == '#')
			{
				return false;
			}
			else if (tab[currentRobotI + offsetI, currentRobotJ + offsetJ] == '.')
			{
				int holderOffsetI = offsetI;
				int holderOffsetJ = offsetJ;

				if (holderOffsetI > 0) holderOffsetI--;
				else if (holderOffsetI < 0) holderOffsetI++;
				else if (holderOffsetJ > 0) holderOffsetJ--;
				else if (holderOffsetJ < 0) holderOffsetJ++;

				char holder = tab[currentRobotI + holderOffsetI, currentRobotJ + holderOffsetJ];
				tab[currentRobotI + holderOffsetI, currentRobotJ + holderOffsetJ] = tab[currentRobotI + offsetI, currentRobotJ + offsetJ];
				tab[currentRobotI + offsetI, currentRobotJ + offsetJ] = holder;

				if (tab[currentRobotI + offsetI, currentRobotJ + offsetJ] == '@')
				{
					currentRobotI += offsetI;
					currentRobotJ += offsetJ;
				}

				return true;
			}
			else if (tab[currentRobotI + offsetI, currentRobotJ + offsetJ] == '[')
			{
				int passOffsetI = offsetI;
				int passOffsetJ = offsetJ;

				if (passOffsetI > 0) passOffsetI++;
				else if (passOffsetI < 0) passOffsetI--;
				else if (passOffsetJ > 0) passOffsetJ++;
				else if (passOffsetJ < 0) passOffsetJ--;

				if (PushPart2(tab, ref currentRobotI, ref currentRobotJ, passOffsetI, passOffsetJ) && 
					PushPart2(tab, ref currentRobotI, ref currentRobotJ, passOffsetI, passOffsetJ))
				{
					int holderOffsetI = offsetI;
					int holderOffsetJ = offsetJ;

					if (holderOffsetI > 0) holderOffsetI--;
					else if (holderOffsetI < 0) holderOffsetI++;
					else if (holderOffsetJ > 0) holderOffsetJ--;
					else if (holderOffsetJ < 0) holderOffsetJ++;

					char holder = tab[currentRobotI + holderOffsetI, currentRobotJ + holderOffsetJ];
					tab[currentRobotI + holderOffsetI, currentRobotJ + holderOffsetJ] = tab[currentRobotI + offsetI, currentRobotJ + offsetJ];
					tab[currentRobotI + offsetI, currentRobotJ + offsetJ] = holder;

					if (tab[currentRobotI + offsetI, currentRobotJ + offsetJ] == '@')
					{
						currentRobotI += offsetI;
						currentRobotJ += offsetJ;
					}

					return true;
				}

				else
				{
					return false;
				}
			}
			else if (tab[currentRobotI + offsetI, currentRobotJ + offsetJ] == ']')
			{
				int passOffsetI = offsetI;
				int passOffsetJ = offsetJ;

				if (passOffsetI > 0) passOffsetI++;
				else if (passOffsetI < 0) passOffsetI--;
				else if (passOffsetJ > 0) passOffsetJ++;
				else if (passOffsetJ < 0) passOffsetJ--;

				if (PushPart2(tab, ref currentRobotI, ref currentRobotJ, passOffsetI, passOffsetJ) &&
					PushPart2(tab, ref currentRobotI, ref currentRobotJ, passOffsetI, passOffsetJ - 1))
				{
					int holderOffsetI = offsetI;
					int holderOffsetJ = offsetJ;

					if (holderOffsetI > 0) holderOffsetI--;
					else if (holderOffsetI < 0) holderOffsetI++;
					else if (holderOffsetJ > 0) holderOffsetJ--;
					else if (holderOffsetJ < 0) holderOffsetJ++;

					char holder = tab[currentRobotI + holderOffsetI, currentRobotJ + holderOffsetJ];
					tab[currentRobotI + holderOffsetI, currentRobotJ + holderOffsetJ] = tab[currentRobotI + offsetI, currentRobotJ + offsetJ];
					tab[currentRobotI + offsetI, currentRobotJ + offsetJ] = holder;

					holder = tab[currentRobotI + holderOffsetI, currentRobotJ + holderOffsetJ];
					tab[currentRobotI + holderOffsetI, currentRobotJ + holderOffsetJ] = tab[currentRobotI + offsetI, currentRobotJ + offsetJ];
					tab[currentRobotI + offsetI, currentRobotJ + offsetJ] = holder;

					if (tab[currentRobotI + offsetI, currentRobotJ + offsetJ] == '@')
					{
						currentRobotI += offsetI;
						currentRobotJ += offsetJ;
					}

					return true;
				}

				else
				{
					return false;
				}
			}
			return false;
		}

		public static (List<char> arrowList, char[,] tab) ProcessTxtFilePart2()
		{
			string basePath = AppDomain.CurrentDomain.BaseDirectory;
			string projectPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\"));
			string dayPath = Path.Combine(projectPath, "Day15");
			string filePath = Path.Combine(dayPath, "Day15test.txt");
			string[] lines = File.ReadAllLines(filePath);

			List<string> mapLines = new List<string>();
			List<char> arrowList = new List<char>();

			bool readingArrows = false;

			foreach (string line in lines)
			{
				if (line.TrimStart('<', '>', '^', 'v').Length == 0)
				{
					readingArrows = true;
				}

				if (readingArrows)
				{
					foreach (char c in line)
					{
						arrowList.Add(c);
					}
				}
				else
				{
					mapLines.Add(line);
				}
			}

			int numRows = mapLines.Count;
			int numCols = mapLines[0].Length;
			char[,] tab = new char[numRows, numCols * 2];

			for (int i = 0; i < numRows; i++)
			{
				for (int j = 0; j < numCols; j++)
				{
					if (mapLines[i][j] == 'O')
					{
						tab[i, j * 2] = '[';
						tab[i, j * 2 + 1] = ']';
					}
					else if (mapLines[i][j] == '#')
					{
						tab[i, j * 2] = '#';
						tab[i, j * 2 + 1] = '#';
					}
					else
					{
						tab[i, j * 2] = mapLines[i][j];
						tab[i, j * 2 + 1] = '.';
					}
				}
			}


			return (arrowList, tab);
		}


		// ===================================================






		public static int Part1()
		{
			(List<char> arrowList, char[,] tab) = ProcessTxtFilePart1();
			int sum = 0;

			int currentRobotI = 0;
			int currentRobotJ = 0;

			for (int i = 0; i < tab.GetLength(0); i++)
			{
				for (int j = 0; j < tab.GetLength(1); j++)
				{
					if (tab[i, j] == '@')
					{
						currentRobotI = i;
						currentRobotJ = j;
					}
				}
			}

			foreach (var item in arrowList)
			{
				switch (item)
				{
					case '^': PushPart1(tab, ref currentRobotI, ref currentRobotJ, -1, 0); break;
					case '>': PushPart1(tab, ref currentRobotI, ref currentRobotJ, 0, 1); break;
					case 'v': PushPart1(tab, ref currentRobotI, ref currentRobotJ, 1, 0); break;
					case '<': PushPart1(tab, ref currentRobotI, ref currentRobotJ, 0, -1); break;
				}
			}

			for (int i = 0; i < tab.GetLength(0); i++)
			{
				for (int j = 0; j < tab.GetLength(1); j++)
				{
					Console.Write(tab[i, j]);
					if (tab[i, j] == 'O')
					{
						sum += (100 * i) + j;
					}
				}
				Console.WriteLine();
			}

			return sum;
		}

		public static bool PushPart1(char[,] tab, ref int currentRobotI, ref int currentRobotJ, int offsetI, int offsetJ)
		{
			if (tab[currentRobotI + offsetI, currentRobotJ + offsetJ] == '#')
			{
				return false;
			}
			else if (tab[currentRobotI + offsetI, currentRobotJ + offsetJ] == '.')
			{
				int holderOffsetI = offsetI;
				int holderOffsetJ = offsetJ;

				if (holderOffsetI > 0) holderOffsetI--;
				else if (holderOffsetI < 0) holderOffsetI++;
				else if (holderOffsetJ > 0) holderOffsetJ--;
				else if (holderOffsetJ < 0) holderOffsetJ++;

				char holder = tab[currentRobotI + holderOffsetI, currentRobotJ + holderOffsetJ];
				tab[currentRobotI + holderOffsetI, currentRobotJ + holderOffsetJ] = tab[currentRobotI + offsetI, currentRobotJ + offsetJ];
				tab[currentRobotI + offsetI, currentRobotJ + offsetJ] = holder;

				if (tab[currentRobotI + offsetI, currentRobotJ + offsetJ] == '@')
				{
					currentRobotI += offsetI;
					currentRobotJ += offsetJ;
				}

				return true;
			}
			else if (tab[currentRobotI + offsetI, currentRobotJ + offsetJ] == 'O')
			{
				int passOffsetI = offsetI;
				int passOffsetJ = offsetJ;

				if (passOffsetI > 0) passOffsetI++;
				else if (passOffsetI < 0) passOffsetI--;
				else if (passOffsetJ > 0) passOffsetJ++;
				else if (passOffsetJ < 0) passOffsetJ--;

				if (PushPart1(tab, ref currentRobotI, ref currentRobotJ, passOffsetI, passOffsetJ))
				{
					int holderOffsetI = offsetI;
					int holderOffsetJ = offsetJ;

					if (holderOffsetI > 0) holderOffsetI--;
					else if (holderOffsetI < 0) holderOffsetI++;
					else if (holderOffsetJ > 0) holderOffsetJ--;
					else if (holderOffsetJ < 0) holderOffsetJ++;

					char holder = tab[currentRobotI + holderOffsetI, currentRobotJ + holderOffsetJ];
					tab[currentRobotI + holderOffsetI, currentRobotJ + holderOffsetJ] = tab[currentRobotI + offsetI, currentRobotJ + offsetJ];
					tab[currentRobotI + offsetI, currentRobotJ + offsetJ] = holder;

					if (tab[currentRobotI + offsetI, currentRobotJ + offsetJ] == '@')
					{
						currentRobotI += offsetI;
						currentRobotJ += offsetJ;
					}

					return true;
				}
				else
				{
					return false;
				}
			}
			return false;
		}

		public static (List<char> arrowList, char[,] tab) ProcessTxtFilePart1()
		{
			string basePath = AppDomain.CurrentDomain.BaseDirectory;
			string projectPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\"));
			string dayPath = Path.Combine(projectPath, "Day15");
			string filePath = Path.Combine(dayPath, "Day15.txt");
			string[] lines = File.ReadAllLines(filePath);

			List<string> mapLines = new List<string>();
			List<char> arrowList = new List<char>();

			bool readingArrows = false;

			foreach (string line in lines)
			{
				if (line.TrimStart('<', '>', '^', 'v').Length == 0)
				{
					readingArrows = true;
				}

				if (readingArrows)
				{
					foreach (char c in line)
					{
						arrowList.Add(c);
					}
				}
				else
				{
					mapLines.Add(line);
				}
			}

			int numRows = mapLines.Count;
			int numCols = mapLines[0].Length;
			char[,] tab = new char[numRows, numCols];

			for (int i = 0; i < numRows; i++)
			{
				for (int j = 0; j < numCols; j++)
				{
					tab[i, j] = mapLines[i][j];
				}
			}

			return (arrowList, tab);
		}
	}
}
