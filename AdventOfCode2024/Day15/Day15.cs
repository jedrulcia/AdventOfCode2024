using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text;
using System.ComponentModel;

namespace AdventOfCode2024
{
	class Day15
	{
		private static int width;
		private static int height;
		private static char[,] map;
		private static List<char> instructions;
		private static Point robotPosition;

		public static int Part2()
		{
			ProcessInputPart2();


			for (int i = 0; i < map.GetLength(0); i++)
			{
				for (int j = 0; j < map.GetLength(1); j++)
				{
					if (map[i, j] == '@')
					{
						robotPosition = new Point(i, j);
					}
				}
			}

			OutputMap();

			foreach (var instruction in instructions)
			{
				MoveRobot(instruction);
			}

			var total = 0;
			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					if (map[x, y] == '[')
					{
						total += y * 100 + x;
					}
				}
			}

			return total;
		}
		private static void OutputMap()
		{
			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					Console.Write(map[x, y]);
				}
				Console.WriteLine("");
			}
		}

		private static void MoveRobot(char instruction)
		{
			var direction = instruction switch
			{
				'^' => new Point(0, -1),
				'>' => new Point(1, 0),
				'v' => new Point(0, 1),
				'<' => new Point(-1, 0),
				_ => throw new InvalidOperationException()
			};

			if (CanMove(robotPosition, direction))
			{
				Move(robotPosition, direction);
				robotPosition.X += direction.X;
				robotPosition.Y += direction.Y;
			}
		}

		private static bool CanMove(Point position, Point direction)
		{
			var newPosition = new Point(position.X + direction.X, position.Y + direction.Y);
			var currentTile = map[position.X, position.Y];

			if (currentTile == '.')
			{
				return true;
			}

			if (currentTile == '#')
			{
				return false;
			}

			var newTile = map[newPosition.X, newPosition.Y];

			if (currentTile == '[' || currentTile == ']')
			{
				if ((currentTile == ']' && direction.X == -1 && direction.Y == 0) ||
					(currentTile == '[' && direction.X == 1 && direction.Y == 0))
				{
					return CanMove(newPosition, direction);
				}

				if (currentTile == ']' && ((direction.X == 0 && direction.Y == -1) || (direction.X == 0 && direction.Y == 1)))
				{
					var holder = new Point(newPosition.X + (-1), newPosition.Y);
					return CanMove(newPosition, direction) && CanMove(holder, direction);
				}

				if (currentTile == '[' && ((direction.X == 0 && direction.Y == -1) || (direction.X == 0 && direction.Y == 1)))
				{
					var holder = new Point(newPosition.X + 1, newPosition.Y);
					return CanMove(newPosition, direction) && CanMove(holder, direction);
				}

				return CanMove(newPosition, direction);
			}

			if (currentTile == '@')
			{
				return CanMove(newPosition, direction);
			}

			throw new InvalidOperationException();
		}

		private static void Move(Point position, Point direction)
		{
			var newPosition = new Point(position.X + direction.X, position.Y + direction.Y);

			var currentTile = map[position.X, position.Y];
			var newTile = map[newPosition.X, newPosition.Y];

			if ((direction.X == -1 && direction.Y == 0) || (direction.X == 1 && direction.Y == 0))
			{
				if (newTile != '.')
				{
					Move(newPosition, direction);
				}
			}
			else
			{
				if (newTile == '[')
				{
					var holder = new Point(newPosition.X + 1, newPosition.Y);
					Move(holder, direction);
					Move(newPosition, direction);
				}
				else if (newTile == ']')
				{
					var holder = new Point(newPosition.X + (-1), newPosition.Y);
					Move(holder, direction);
					Move(newPosition, direction);
				}
				else if (newTile != '.')
				{
					Move(newPosition, direction);
				}
			}

			map[newPosition.X, newPosition.Y] = map[position.X, position.Y];
			map[position.X, position.Y] = '.';
		}

		public static void ProcessInputPart2()
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

			instructions = arrowList;

			width = mapLines[0].Length * 2;
			height = mapLines.Count;
			map = new char[width, height];


			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < mapLines[0].Length; x++)
				{
					var character = mapLines[y][x];

					if (character == '@')
					{
						robotPosition = new Point(x * 2, y);
						map[x * 2, y] = '@';
						map[x * 2 + 1, y] = '.';
					}
					else if (character == '#')
					{
						map[x * 2, y] = '#';
						map[x * 2 + 1, y] = '#';
					}
					else if (character == '.')
					{
						map[x * 2, y] = '.';
						map[x * 2 + 1, y] = '.';
					}
					else if (character == 'O')
					{
						map[x * 2, y] = '[';
						map[x * 2 + 1, y] = ']';
					}
				}
			}
		}

		// ===================================================

		public static int Part1()
		{
			(List<char> arrowList, char[,] tab) = ProcessInput();
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

		public static (List<char> arrowList, char[,] tab) ProcessInput()
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
