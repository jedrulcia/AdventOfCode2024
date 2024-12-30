using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
	class Day22
	{
		public static string Part2()
		{
			string basePath = AppDomain.CurrentDomain.BaseDirectory;
			string projectPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\"));
			string dayPath = Path.Combine(projectPath, "Day22");
			string filePath = Path.Combine(dayPath, "Day22.txt");

			var lines = File.ReadAllLines(filePath);

			Dictionary<(long, long, long, long), long> sequences = new Dictionary<(long, long, long, long), long>();

			foreach (var line in lines)
			{
				CalculateBestSequencePart2(Convert.ToInt64(line), ref sequences);
			}

			long mostBananas = sequences.Values.Max();

			return mostBananas.ToString();
		}

		public static void CalculateBestSequencePart2(long secretNumber, ref Dictionary<(long, long, long, long), long> sequences)
		{
			List<long> differences = new List<long>();
			List<long> secretNumbers = new List<long>();
			HashSet<(long, long, long, long)> usedSequences = new HashSet<(long, long, long, long)>();

			for (int i = 0; i < 2000; i++)
			{
				secretNumber = (secretNumber * 64) ^ secretNumber;
				secretNumber = secretNumber % 16777216;

				secretNumber = (long)(secretNumber / 32) ^ secretNumber;
				secretNumber = secretNumber % 16777216;

				secretNumber = (secretNumber * 2048) ^ secretNumber;
				secretNumber = secretNumber % 16777216;

				secretNumbers.Add(secretNumber);
				 
				if (i == 0)
				{
					differences.Add(0);
				}
				else
				{
					long previousNumber = secretNumbers[i - 1] % 10;
					long currentNumber = secretNumbers[i] % 10;

					differences.Add(currentNumber - previousNumber);
				}

				if (i >= 4)
				{
					long bananas = secretNumbers[i] % 10;
					var tuple = (differences[i - 3], differences[i - 2], differences[i - 1], differences[i]);

					if (!usedSequences.Contains(tuple))
					{
						usedSequences.Add(tuple);
						if (sequences.ContainsKey(tuple))
						{
							sequences[tuple] += bananas;
						}
						else
						{
							sequences[tuple] = bananas;
						}
					}
				}
			}
		}

		public static string Part1()
		{
			string basePath = AppDomain.CurrentDomain.BaseDirectory;
			string projectPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\"));
			string dayPath = Path.Combine(projectPath, "Day22");
			string filePath = Path.Combine(dayPath, "Day22.txt");

			var lines = File.ReadAllLines(filePath);

			long sum = 0;

			foreach (var line in lines)
			{
				long secretNumber = CalculateSecretNumberPart1(Convert.ToInt64(line));

				sum += secretNumber;
			}


			return sum.ToString();
		}

		public static long CalculateSecretNumberPart1(long secretNumber)
		{
			for (int i = 0; i < 2000; i++)
			{
				secretNumber = (secretNumber * 64) ^ secretNumber;
				secretNumber = secretNumber % 16777216;

				secretNumber = (long)(secretNumber / 32) ^ secretNumber;
				secretNumber = secretNumber % 16777216;

				secretNumber = (secretNumber * 2048) ^ secretNumber;
				secretNumber = secretNumber % 16777216;
			}

			return secretNumber;
		}

	}
}
