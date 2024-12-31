using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
	class Day25
	{
		public static string Part1()
		{
			var (locks, keys) = ProcessInput();
			int counter = 0;

			foreach (var locck in locks)
			{
				foreach (var key in keys)
				{
					bool shouldAdd = true;
					if (locck[0] + key[0] <= 7)
					{
						for (int i = 1; i < 5; i++)
						{
							if (locck[i] + key[i] > 7)
							{
								shouldAdd = false;
								break;
							}
						}
						if (shouldAdd)
						{
							counter++;
						}
					}
				}
			}

			return counter.ToString();
		}
		private static (List<List<int>> locks, List<List<int>> keys) ProcessInput()
		{
			string basePath = AppDomain.CurrentDomain.BaseDirectory;
			string projectPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\"));
			string dayPath = Path.Combine(projectPath, "Day25");
			string filePath = Path.Combine(dayPath, "Day25.txt");
			var lines = File.ReadAllLines(filePath);

			var locks = new List<List<int>>();
			var keys = new List<List<int>>();
			var currentBlock = new List<string>();

			foreach (var line in lines)
			{
				if (string.IsNullOrWhiteSpace(line))
				{
					if (currentBlock.Count > 0)
					{
						var columnHeights = GetColumnHeights(currentBlock);

						if (IsLock(currentBlock))
						{
							locks.Add(columnHeights);
						}
						else if (IsKey(currentBlock))
						{
							keys.Add(columnHeights);
						}

						currentBlock.Clear();
					}
				}
				else
				{
					currentBlock.Add(line);
				}
			}

			if (currentBlock.Count > 0)
			{
				var columnHeights = GetColumnHeights(currentBlock);

				if (IsLock(currentBlock))
				{
					locks.Add(columnHeights);
				}
				else if (IsKey(currentBlock))
				{
					keys.Add(columnHeights);
				}
			}

			return (locks, keys);
		}

		static List<int> GetColumnHeights(List<string> block)
		{
			var height = block.Count;
			var width = block[0].Length;
			var columnHeights = new List<int>();

			for (int col = 0; col < width; col++)
			{
				int columnHeight = 0;

				for (int row = 0; row < height; row++)
				{
					if (block[row][col] == '#')
					{
						columnHeight++;
					}
				}

				columnHeights.Add(columnHeight);
			}

			return columnHeights;
		}

		static bool IsLock(List<string> block)
		{
			return block.First().All(c => c == '#') && block.Last().All(c => c == '.');
		}

		static bool IsKey(List<string> block)
		{
			return block.First().All(c => c == '.') && block.Last().All(c => c == '#');
		}
	}
}
