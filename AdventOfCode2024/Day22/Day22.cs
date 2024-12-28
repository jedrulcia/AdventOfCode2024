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
		public static string Part1()
		{
			string basePath = AppDomain.CurrentDomain.BaseDirectory;
			string projectPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\"));
			string dayPath = Path.Combine(projectPath, "Day22");
			string filePath = Path.Combine(dayPath, "Day22.txt");

			var lines = File.ReadAllLines(filePath);

			//List<long> lines = new List<long>([1, 10, 100, 2024]);

			long sum = 0;

			foreach (var line in lines)
			{
				long secretNumber = CalculateSecretNumber(Convert.ToInt64(line));


				sum += secretNumber;
			}



			return sum.ToString();
		}

		public static long CalculateSecretNumber(long secretNumber)
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
