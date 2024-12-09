using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AdventOfCode2024
{
	class Day7
	{
		public static long Part1()
		{
			Dictionary<long, List<int>> dict = ConvertTxtToDictionary();
			long sum = 0;
			foreach (var item in dict)
			{
				var list = item.Value as List<int>;

				List<string> sequences = GenerateSequencePart1(list.Count - 1);

				foreach (var sequence in sequences)
				{
					long currentValue = 0;
					for (int j = 0; j < sequence.Length; j++)
					{
						if (sequence[j] == '*')
						{
							if (j == 0)
							{
								currentValue = list[j] * list[j + 1];
							}
							else
							{
								currentValue *= list[j + 1];
							}
						}
						else if (sequence[j] == '+')
						{
							if (j == 0)
							{
								currentValue = list[j] + list[j + 1];
							}
							else
							{
								currentValue += list[j + 1];
							}
						}
					}
					if (currentValue == item.Key)
					{
						sum += currentValue;
						break;
					}
				}
			}
			return sum;
		}

		public static List<string> GenerateSequencePart1(int n)
		{
			List<string> sequences = new List<string>();
			char adding = '+';
			char multiplying = '*';
			char stringAdding = '|';

			int totalCombinations = (int)Math.Pow(2, n);

			for (int i = 0; i < totalCombinations; i++)
			{
				string combination = "";
				for (int j = 0; j < n; j++)
				{
					if ((i & (1 << j)) != 0)
					{
						combination += multiplying;
					}
					else
					{
						combination += adding;
					}
				}
				sequences.Add(combination);
			}


			return sequences;
		}

		/* 116094961956019
			Runtime: 10417 ms
		 */


		public static long Part2()
		{

			Dictionary<long, List<int>> dict = ConvertTxtToDictionary();
			Dictionary<int, List<string>> sequenceDictionary = new Dictionary<int, List<string>>();


			int max = 2;
			foreach (var item in dict)
			{
				max = Math.Max(max, item.Value.Count);
			}

			for (int i = 2; i <= max; i++)
			{
				sequenceDictionary[i] = (GenerateSequencePart2(i - 1));
			}

			long sum = 0;

			foreach (var item in dict)
			{
				var list = item.Value as List<int>;

				List<string> sequences = sequenceDictionary[list.Count];

				foreach (var sequence in sequences)
				{
					long currentValue = 0;
					for (int j = 0; j < sequence.Length; j++)
					{
						if (sequence[j] == '*')
						{
							if (j == 0)
							{
								currentValue = list[j] * list[j + 1];
							}
							else
							{
								currentValue *= list[j + 1];
							}
						}
						else if (sequence[j] == '+')
						{
							if (j == 0)
							{
								currentValue = list[j] + list[j + 1];
							}
							else
							{
								currentValue += list[j + 1];
							}
						}
						else if (sequence[j] == '|')
						{
							if (j == 0)
							{
								string holder = Convert.ToString(list[j]) + Convert.ToString(list[j + 1]);
								currentValue = Convert.ToInt64(holder);
							}
							else
							{
								string holder = Convert.ToString(currentValue) + Convert.ToString(list[j + 1]);
								currentValue = Convert.ToInt64(holder);
							}
						}
					}
					if (currentValue == item.Key)
					{
						sum += currentValue;
						break;
					}
				}
			}
			return sum;
		}

		public static List<string> GenerateSequencePart2(int n)
		{
			List<string> sequences = new List<string>();
			char adding = '+';
			char multiplying = '*';
			char stringAdding = '|';

			int totalCombinations = (int)Math.Pow(3, n);

			for (int i = 0; i < totalCombinations; i++)
			{
				string combination = "";
				int current = i;

				for (int j = 0; j < n; j++)
				{
					int remainder = current % 3;
					current /= 3;

					if (remainder == 0)
					{
						combination += adding;
					}
					else if (remainder == 1)
					{
						combination += multiplying;
					}
					else if (remainder == 2)
					{
						combination += stringAdding;
					}
				}

				sequences.Add(combination);
			}

			return sequences;
		}

		public static Dictionary<long, List<int>> ConvertTxtToDictionary()
		{
			Dictionary<long, List<int>> dict = new Dictionary<long, List<int>>();

			string basePath = AppDomain.CurrentDomain.BaseDirectory;
			string projectPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\"));
			string dayPath = Path.Combine(projectPath, "Day7");
			string filePath = Path.Combine(dayPath, "Day7.txt");

			var lines = File.ReadAllLines(filePath);

			for (int i = 0; i < lines.Count(); i++)
			{ 
				var numbers = lines[i].Split();
				string text = numbers[0];
				long key = Convert.ToInt64(text.Remove(text.Length - 1));

				dict[key] = new List<int>();

				for(int j = 1; j < numbers.Count(); j++)
				{
					var list = dict[key];
					int number = Convert.ToInt32(numbers[j]);
					list.Add(number);

				}
			}
			return dict;
		}
	}
}
