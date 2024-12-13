using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using MathNet.Numerics.LinearAlgebra;

namespace AdventOfCode2024
{
	public class Coordinates
	{
		public long x { get; set; }
		public long y { get; set; }

		public Coordinates(long x, long y)
		{
			this.x = x;
			this.y = y;
		}
	}

	class Day13
	{
		public Coordinates ButtonA { get; set; }
		public Coordinates ButtonB { get; set; }
		public Coordinates Prize { get; set; }

		public Day13(long aX, long aY, long bX, long bY, long prizeX, long prizeY)
		{
			this.ButtonA = new Coordinates(aX, aY);
			this.ButtonB = new Coordinates(bX, bY);
			this.Prize = new Coordinates(prizeX, prizeY);
		}

		public static List<Day13> GetList()
		{
			string basePath = AppDomain.CurrentDomain.BaseDirectory;
			string projectPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\"));
			string dayPath = Path.Combine(projectPath, "Day13");
			string filePath = Path.Combine(dayPath, "Day13.txt");
			string[] lines = File.ReadAllLines(filePath);

			List<Day13> list = new List<Day13>();

			string pattern = @"Button A: X\+(\d+), Y\+(\d+)\s+Button B: X\+(\d+), Y\+(\d+)\s+Prize: X=(\d+), Y=(\d+)";
			Regex regex = new Regex(pattern, RegexOptions.Multiline);

			for (int i = 0; i < lines.Length; i++)
			{
				if (string.IsNullOrWhiteSpace(lines[i]))
				{
					continue;
				}

				if (i + 2 < lines.Length)
				{
					string block = string.Join("\n", lines[i], lines[i + 1], lines[i + 2]);
					Match match = regex.Match(block);
					if (match.Success)
					{
						int aX = int.Parse(match.Groups[1].Value);
						int aY = int.Parse(match.Groups[2].Value);
						int bX = int.Parse(match.Groups[3].Value);
						int bY = int.Parse(match.Groups[4].Value);
						int prizeX = int.Parse(match.Groups[5].Value);
						int prizeY = int.Parse(match.Groups[6].Value);

						list.Add(new Day13(aX, aY, bX, bY, prizeX, prizeY));
					}

					i += 2;
				}
			}
			return list;
		}

		//36870
		public static long Part1()
		{
			long sum = 0;
			List<Day13> list = GetList();

			foreach (var item in list)
			{
				List<long> possibleTokens = new List<long>();

				long currentX = 0;
				long currentY = 0;
				long currentTokens = 0;
				long buttonACounter = 0;

				while (currentX < item.Prize.x && currentY < item.Prize.y)
				{
					currentX += item.ButtonA.x;
					currentY += item.ButtonA.y;
					currentTokens += 3;
					buttonACounter++;
				}

				if (currentX == item.Prize.x && currentY == item.Prize.y && buttonACounter <= 100)
				{
					possibleTokens.Add(currentTokens);
				}
				buttonACounter = 0;

				currentX = 0;
				currentY = 0;
				currentTokens = 0;
				int buttonBCounter = 0;

				while (currentX < item.Prize.x && currentY < item.Prize.y)
				{
					currentX += item.ButtonB.x;
					currentY += item.ButtonB.y;
					currentTokens += 1;
					buttonBCounter++;
				}

				if (currentX == item.Prize.x && currentY == item.Prize.y && buttonBCounter <= 100)
				{
					possibleTokens.Add(currentTokens);
				}

				while (buttonBCounter > 0)
				{
					currentX -= item.ButtonB.x;
					currentY -= item.ButtonB.y;
					currentTokens -= 1;
					buttonBCounter--;

					while (currentX < item.Prize.x && currentY < item.Prize.y)
					{
						currentX += item.ButtonA.x;
						currentY += item.ButtonA.y;
						currentTokens += 3;
						buttonACounter++;
					}

					if (currentX == item.Prize.x && currentY == item.Prize.y && buttonACounter <= 100 && buttonBCounter <= 100)
					{
						possibleTokens.Add(currentTokens);
					}
				}

				long holder = 0;

				foreach (var token in possibleTokens)
				{
					if (holder < token)
					{
						holder = token;
					}
					else if (holder == 0)
					{
						holder = token;
					}
				}
				sum += holder;
			}


			return sum;
		}

		// ===================================================== PART2 =====================================================


		//53731008640291
		//78101482023732
		public static long Part2()
		{
			long sum = 0;
			List<Day13> list = GetList();

			foreach (var item in list)
			{
				List<long> possibleTokens = new List<long>();

				if ((item.ButtonA.x > item.ButtonA.y && item.ButtonB.x > item.ButtonB.y) || (item.ButtonA.y > item.ButtonA.x && item.ButtonB.y > item.ButtonB.x))
				{
					continue;
				}
				else
				{
					item.Prize.x += 10000000000000;
					item.Prize.y += 10000000000000;

					MathNet.Numerics.LinearAlgebra.Vector<double> solution = null;

					var A = Matrix<double>.Build.DenseOfArray(new double[,]
					{
						{item.ButtonA.x, item.ButtonB.x },
						{item.ButtonA.y, item.ButtonB.y }
					});

					var b = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(new double[] { item.Prize.x, item.Prize.y });

					try
					{
						solution = A.Solve(b);
					}
					catch (Exception ex)
					{
						continue;
					}

					Console.WriteLine($"{solution[0]} | {solution[1]}");


					if (IsEffectivelyInteger(solution[0]) && IsEffectivelyInteger(solution[1]))
					{
						long token = Convert.ToInt64((solution[0] * 3) + solution[1]);

						sum += token;
					}
					else
					{
						continue;
					}
				}
			}
			return sum;
		}

		private static bool IsEffectivelyInteger(double value, double tolerance = 1e-2)
		{
			return Math.Abs(value - Math.Round(value)) < tolerance;
		}
	}
}
