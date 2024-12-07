using System.Diagnostics.Metrics;
using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode2024
{
	class Day6
	{
		public static int Part1()
		{
			char[,] tab = ConvertFromTxtToArray();
			int counter = 0;

			int currentPositionI = 0;
			int currentPositionJ = 0;

			// FINDING STARTING POSITION OF GUARDIAN
			for (int i = 0; i < tab.GetLength(0); i++)
			{
				for (int j = 0; j < tab.GetLength(1); j++)
				{
					if (tab[i, j] == '^')
					{
						currentPositionI = i;
						currentPositionJ = j;
					}
				}
			}

			int movingDirection = 0;
			bool shouldContinue = true;

			while (shouldContinue == true)
			{
				if (movingDirection == 0)
				{
					while (tab[currentPositionI - 1, currentPositionJ] != '#')
					{
						if (tab[currentPositionI, currentPositionJ] != 'X')
						{
							counter++;
							tab[currentPositionI, currentPositionJ] = 'X';
						}
						currentPositionI--;
						if (currentPositionI - 1 < 0)
						{
							if (tab[currentPositionI, currentPositionJ] != 'X')
							{
								counter++;
								tab[currentPositionI, currentPositionJ] = 'X';
							}
							shouldContinue = false;
							break;
						}
					}
					movingDirection = 1;
				}
				else if (movingDirection == 1)
				{
					while (tab[currentPositionI, currentPositionJ + 1] != '#')
					{
						if (tab[currentPositionI, currentPositionJ] != 'X')
						{
							counter++;
							tab[currentPositionI, currentPositionJ] = 'X';
						}
						currentPositionJ++;
						if (currentPositionJ + 1 >= tab.GetLength(1))
						{
							if (tab[currentPositionI, currentPositionJ] != 'X')
							{
								counter++;
								tab[currentPositionI, currentPositionJ] = 'X';
							}
							shouldContinue = false;
							break;
						}
					}
					movingDirection = 2;
				}
				else if (movingDirection == 2)
				{
					while (tab[currentPositionI + 1, currentPositionJ] != '#')
					{
						if (tab[currentPositionI, currentPositionJ] != 'X')
						{
							counter++;
							tab[currentPositionI, currentPositionJ] = 'X';
						}
						currentPositionI++;
						if (currentPositionI + 1 >= tab.GetLength(0))
						{
							if (tab[currentPositionI, currentPositionJ] != 'X')
							{
								counter++;
								tab[currentPositionI, currentPositionJ] = 'X';
							}
							shouldContinue = false;
							break;
						}
					}
					movingDirection = 3;
				}
				else if (movingDirection == 3)
				{
					while (tab[currentPositionI, currentPositionJ - 1] != '#')
					{
						if (tab[currentPositionI, currentPositionJ] != 'X')
						{
							counter++;
							tab[currentPositionI, currentPositionJ] = 'X';
						}
						currentPositionJ--;
						if (currentPositionJ - 1 < 0)
						{
							if (tab[currentPositionI, currentPositionJ] != 'X')
							{
								counter++;
								tab[currentPositionI, currentPositionJ] = 'X';
							}
							shouldContinue = false;
							break;
						}
					}
					movingDirection = 0;
				}
			}

			return counter;
		}

		public bool MovedUp { get; set; }
		public bool MovedRight { get; set; }
		public bool MovedDown { get; set; }
		public bool MovedLeft { get; set; }
		public bool StartingPosition { get; set; }
		public bool PlacedObstalce { get; set; }
		public char Value { get; set; }

		public Day6(char value)
		{
			this.MovedUp = false;
			this.MovedRight = false;
			this.MovedDown = false;
			this.MovedLeft = false;
			this.StartingPosition = false;
			this.PlacedObstalce = false;
			this.Value = value;
		}
		//510
		//508
		//507
		//506
		//706

		//2449

		//5147
		//need to continue tab[currentPositionI, j].Value == '#'

		public static int Part2()
		{
			Day6[,] tab = GetObjectArray();
			int counter = 0;

			int startingPositionI = 0;
			int startingPositionJ = 0;

			// FINDING STARTING POSITION OF GUARDIAN
			for (int i = 0; i < tab.GetLength(0); i++)
			{
				for (int j = 0; j < tab.GetLength(1); j++)
				{
					if (tab[i, j].Value == '^')
					{
						startingPositionI = i;
						startingPositionJ = j;
					}
				}
			}

			int currentPositionI = startingPositionI;
			int currentPositionJ = startingPositionJ;

			tab[startingPositionI, startingPositionJ].StartingPosition = true;
			tab[startingPositionI, startingPositionJ].MovedUp = true;

			int movingDirection = 0;
			bool shouldContinue = true;

			while (shouldContinue == true)
			{
				//============================================MOVING UP===========================================
				if (movingDirection == 0)
				{
					while (tab[currentPositionI - 1, currentPositionJ].Value != '#')
					{
						if (tab[currentPositionI - 1, currentPositionJ].StartingPosition == false &&
							tab[currentPositionI - 1, currentPositionJ].PlacedObstalce == false &&
							tab[currentPositionI - 1, currentPositionJ].MovedUp == false &&
							tab[currentPositionI - 1, currentPositionJ].MovedRight == false &&
							tab[currentPositionI - 1, currentPositionJ].MovedDown == false &&
							tab[currentPositionI - 1, currentPositionJ].MovedLeft == false)
						{
							Day6[,] newTab = DeepClone(tab);
							if (LoopSearcherRight(newTab, currentPositionI, currentPositionJ))
							{
								tab[currentPositionI - 1, currentPositionJ].PlacedObstalce = true;
								counter++;
								tab[currentPositionI - 1, currentPositionJ].Value = 'O';
							}
						}

						if (tab[currentPositionI, currentPositionJ].MovedUp != true)
						{
							tab[currentPositionI, currentPositionJ].MovedUp = true;
						}
						tab[currentPositionI, currentPositionJ].Value = 'X';
						PrintObjectArray(tab);
						currentPositionI--;
						if (currentPositionI - 1 < 0)
						{
							shouldContinue = false;
							break;
						}
					}
					tab[currentPositionI, currentPositionJ].MovedUp = true;
					movingDirection = 1;
				}
				//============================================MOVING RIGHT===========================================
				else if (movingDirection == 1)
				{
					while (tab[currentPositionI, currentPositionJ + 1].Value != '#')
					{
						if (tab[currentPositionI, currentPositionJ + 1].StartingPosition == false &&
							tab[currentPositionI, currentPositionJ + 1].PlacedObstalce == false &&
							tab[currentPositionI, currentPositionJ + 1].MovedUp == false &&
							tab[currentPositionI, currentPositionJ + 1].MovedRight == false &&
							tab[currentPositionI, currentPositionJ + 1].MovedDown == false &&
							tab[currentPositionI, currentPositionJ + 1].MovedLeft == false)
						{
							Day6[,] newTab = DeepClone(tab);
							if (LoopSearcherDown(newTab, currentPositionI, currentPositionJ))
							{
								tab[currentPositionI, currentPositionJ + 1].PlacedObstalce = true;
								counter++;
								tab[currentPositionI, currentPositionJ + 1].Value = 'O';
							}
						}

						if (tab[currentPositionI, currentPositionJ].MovedRight != true)
						{
							tab[currentPositionI, currentPositionJ].MovedRight = true;
						}
						tab[currentPositionI, currentPositionJ].Value = 'X';
						PrintObjectArray(tab);
						currentPositionJ++;
						if (currentPositionJ + 1 >= tab.GetLength(1))
						{
							shouldContinue = false;
							break;
						}
					}
					tab[currentPositionI, currentPositionJ].MovedRight = true;
					movingDirection = 2;
				}
				//============================================MOVING DOWN===========================================
				else if (movingDirection == 2)
				{

					while (tab[currentPositionI + 1, currentPositionJ].Value != '#')
					{
						if (tab[currentPositionI + 1, currentPositionJ].StartingPosition == false &&
							tab[currentPositionI + 1, currentPositionJ].PlacedObstalce == false &&
							tab[currentPositionI + 1, currentPositionJ].MovedUp == false &&
							tab[currentPositionI + 1, currentPositionJ].MovedRight == false &&
							tab[currentPositionI + 1, currentPositionJ].MovedDown == false &&
							tab[currentPositionI + 1, currentPositionJ].MovedLeft == false)
						{
							Day6[,] newTab = DeepClone(tab);
							if (LoopSearcherLeft(newTab, currentPositionI, currentPositionJ))
							{
								tab[currentPositionI + 1, currentPositionJ].PlacedObstalce = true;
								counter++;
								tab[currentPositionI + 1, currentPositionJ].Value = 'O';
							}
						}

						if (tab[currentPositionI, currentPositionJ].MovedDown != true)
						{
							tab[currentPositionI, currentPositionJ].MovedDown = true;
						}
						tab[currentPositionI, currentPositionJ].Value = 'X';
						PrintObjectArray(tab);
						currentPositionI++;
						if (currentPositionI + 1 >= tab.GetLength(0))
						{
							shouldContinue = false;
							break;
						}
					}
					tab[currentPositionI, currentPositionJ].MovedDown = true;
					movingDirection = 3;
				}
				//============================================MOVING LEFT===========================================
				else if (movingDirection == 3)
				{

					while (tab[currentPositionI, currentPositionJ - 1].Value != '#')
					{
						if (tab[currentPositionI, currentPositionJ - 1].StartingPosition == false &&
							tab[currentPositionI, currentPositionJ - 1].PlacedObstalce == false &&
							tab[currentPositionI, currentPositionJ - 1].MovedUp == false &&
							tab[currentPositionI, currentPositionJ - 1].MovedRight == false &&
							tab[currentPositionI, currentPositionJ - 1].MovedDown == false &&
							tab[currentPositionI, currentPositionJ - 1].MovedLeft == false)
						{
							Day6[,] newTab = DeepClone(tab);
							if (LoopSearcherUp(newTab, currentPositionI, currentPositionJ))
							{
								tab[currentPositionI, currentPositionJ - 1].PlacedObstalce = true;
								counter++;
								tab[currentPositionI, currentPositionJ - 1].Value = 'O';
							}
						}

						if (tab[currentPositionI, currentPositionJ].MovedLeft != true)
						{
							tab[currentPositionI, currentPositionJ].MovedLeft = true;
						}
						tab[currentPositionI, currentPositionJ].Value = 'X';
						PrintObjectArray(tab);
						currentPositionJ--;
						if (currentPositionJ - 1 < 0)
						{
							shouldContinue = false;
							break;
						}
					}
					tab[currentPositionI, currentPositionJ].MovedLeft = true;
					movingDirection = 0;
				}
			}
			return counter;
		}

		// MOVING RIGHT AFTER HITTING NEW OBSTACLE
		private static bool LoopSearcherRight(Day6[,] tab, int currentPositionI, int currentPositionJ)
		{
			for (int j = currentPositionJ; j < tab.GetLength(1); j++)
			{
				if (tab[currentPositionI, j].MovedRight == true)
				{
					return true;
				}

				tab[currentPositionI, j].MovedRight = true;

				if (j + 1 >= tab.GetLength(1))
				{
					return false;
				}
				else if (tab[currentPositionI, j].Value == '#')
				{
					return LoopSearcherDown(tab, currentPositionI, j);
				}
			}

			return false;
		}

		// MOVING DOWN AFTER HITTING NEW OBSTALCE
		private static bool LoopSearcherDown(Day6[,] tab, int currentPositionI, int currentPositionJ)
		{
			for (int i = currentPositionI; i < tab.GetLength(0); i++)
			{
				if (tab[i, currentPositionJ].MovedDown == true)
				{
					return true;
				}

				tab[i, currentPositionJ].MovedDown = true;


				if (i + 1 >= tab.GetLength(0))
				{
					return false;
				}
				else if (tab[i + 1, currentPositionJ].Value == '#')
				{
					return LoopSearcherLeft(tab, i, currentPositionJ);
				}
			}
			return false;
		}

		// MOVING LEFT AFTER HITTING NEW OBSTALCE
		private static bool LoopSearcherLeft(Day6[,] tab, int currentPositionI, int currentPositionJ)
		{
			for (int j = currentPositionJ; j >= 0; j--)
			{
				if (tab[currentPositionI, j].MovedLeft == true)
				{
					return true;
				}

				tab[currentPositionI, j].MovedLeft = true;

				if (j - 1 < 0)
				{
					return false;
				}
				else if (tab[currentPositionI, j - 1].Value == '#')
				{
					return LoopSearcherUp(tab, currentPositionI, j);
				}
			}
			return false;
		}

		// MOVING UP AFTER HITTING NEW OBSTACLE
		private static bool LoopSearcherUp(Day6[,] tab, int currentPositionI, int currentPositionJ)
		{
			for (int i = currentPositionI; i >= 0; i--)
			{
				if (tab[i, currentPositionJ].MovedUp == true)
				{
					return true;
				}

				tab[i, currentPositionJ].MovedUp = true;

				if (i - 1 < 0)
				{
					return false;
				}
				else if (tab[i - 1, currentPositionJ].Value == '#')
				{
					return LoopSearcherRight(tab, i, currentPositionJ);
				}
			}
			return false;
		}
		public static Day6[,] DeepClone(Day6[,] original)
		{
			int rows = original.GetLength(0);
			int cols = original.GetLength(1);
			Day6[,] clone = new Day6[rows, cols];

			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < cols; j++)
				{
					clone[i, j] = new Day6(original[i, j].Value);
					clone[i, j].MovedUp = original[i, j].MovedUp;
					clone[i, j].MovedRight = original[i, j].MovedRight;
					clone[i, j].MovedDown = original[i, j].MovedDown;
					clone[i, j].MovedLeft = original[i, j].MovedLeft;
					clone[i, j].StartingPosition = original[i, j].StartingPosition;
					clone[i, j].PlacedObstalce = original[i, j].PlacedObstalce;
				}
			}
			return clone;
		}

		private static char[,] ConvertFromTxtToArray()
		{
			string basePath = AppDomain.CurrentDomain.BaseDirectory;
			string projectPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\"));
			string dayPath = Path.Combine(projectPath, "Day6");
			string filePath = Path.Combine(dayPath, "Day6.txt");

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

		private static Day6[,] GetObjectArray()
		{
			string basePath = AppDomain.CurrentDomain.BaseDirectory;
			string projectPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\"));
			string dayPath = Path.Combine(projectPath, "Day6");
			string filePath = Path.Combine(dayPath, "Day6.txt");

			var lines = File.ReadAllLines(filePath);

			Day6[,] tab = new Day6[lines.Length, lines[0].Length];

			for (int i = 0; i < lines.Length; i++)
			{
				for (int j = 0; j < lines[i].Length; j++)
				{
					tab[i, j] = new Day6(lines[i][j]);
				}
			}
			return tab;
		}

		private static void PrintObjectArray(Day6[,] array)
		{
			/*for (int i = 0; i < array.GetLength(0); i++)
			{
				for (int j = 0; j < array.GetLength(1); j++)
				{
					Console.Write(array[i, j].Value);
				}
				Console.WriteLine();
			}
			Console.WriteLine();*/
		}


	}
}
