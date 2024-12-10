using System.Diagnostics.Metrics;
using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode2024
{
    class Day6
	{
		// ==============================================PART1===========================================
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



		// ==============================================PART2===========================================
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
            this.PlacedObstalce = false;
            this.Value = value;
        }

		//510
		//508
		//507
		//506
		//706
		//5147
		//2449

        //2090

		public static int Part2()
        {
            Day6[,] tab = GetObjectArray();

            int currentPositionI = 0;
            int currentPositionJ = 0;
            // FINDING STARTING POSITION OF GUARDIAN
            for (int i = 0; i < tab.GetLength(0); i++)
            {
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    if (tab[i, j].Value == '^')
                    {
                        tab[i, j].MovedUp = true;
                        currentPositionI = i;
                        currentPositionJ = j;
                        break;
                    }
                }
            }

            int counter = 0;
            int movingDirection = 0;
            bool shouldContinue = true;

            while (shouldContinue == true)
            {
                int iOffset = 0;
                int jOffset = 0;

                switch (movingDirection % 4)
                {
                    case 0: iOffset = -1; jOffset = 0; break;
                    case 1: iOffset = 0; jOffset = 1; break;
                    case 2: iOffset = 1; jOffset = 0; break;
                    case 3: iOffset = 0; jOffset = -1; break;
                }

                while (tab[currentPositionI + iOffset, currentPositionJ + jOffset].Value != '#')
                {
                    if (tab[currentPositionI + iOffset, currentPositionJ + jOffset].PlacedObstalce == false &&
                        tab[currentPositionI + iOffset, currentPositionJ + jOffset].MovedUp == false &&
                        tab[currentPositionI + iOffset, currentPositionJ + jOffset].MovedRight == false &&
                        tab[currentPositionI + iOffset, currentPositionJ + jOffset].MovedDown == false &&
                        tab[currentPositionI + iOffset, currentPositionJ + jOffset].MovedLeft == false)
                    {
                        bool wasFound = false;

                        switch (movingDirection % 4)
                        {
                            case 0: wasFound = LoopSearcherRight(DeepClone(tab), currentPositionI, currentPositionJ); break;
                            case 1: wasFound = LoopSearcherDown(DeepClone(tab), currentPositionI, currentPositionJ); break;
                            case 2: wasFound = LoopSearcherLeft(DeepClone(tab), currentPositionI, currentPositionJ); break;
                            case 3: wasFound = LoopSearcherUp(DeepClone(tab), currentPositionI, currentPositionJ); break;
                        }

                        if (wasFound)
                        {
                            tab[currentPositionI + iOffset, currentPositionJ + jOffset].PlacedObstalce = true;
                            counter++;
                        }
                    }

                    switch (movingDirection % 4)
                    {
                        case 0: tab[currentPositionI, currentPositionJ].MovedUp = true; break;
                        case 1: tab[currentPositionI, currentPositionJ].MovedRight = true; break;
                        case 2: tab[currentPositionI, currentPositionJ].MovedDown = true; break;
                        case 3: tab[currentPositionI, currentPositionJ].MovedLeft = true; break;
                    }

                    currentPositionI += iOffset;
                    currentPositionJ += jOffset;
                    if (currentPositionI + iOffset < 0 || currentPositionI + iOffset >= tab.GetLength(0) || currentPositionJ + jOffset < 0 || currentPositionJ + jOffset >= tab.GetLength(1))
                    {
                        shouldContinue = false;
                        break;
                    }
                }
                switch (movingDirection % 4)
                {
                    case 0: tab[currentPositionI, currentPositionJ].MovedUp = true; break;
                    case 1: tab[currentPositionI, currentPositionJ].MovedRight = true; break;
                    case 2: tab[currentPositionI, currentPositionJ].MovedDown = true; break;
                    case 3: tab[currentPositionI, currentPositionJ].MovedUp = true; break;
                }
                movingDirection++;
            }
			PrintObjectArray(tab);
            return counter;
        }

        // MOVING RIGHT AFTER HITTING NEW OBSTACLE
        private static bool LoopSearcherRight(Day6[,] tab, int currentPositionI, int currentPositionJ)
        {
			while (currentPositionJ < tab.GetLength(1) - 1)
			{
				if (tab[currentPositionI, currentPositionJ].MovedRight == true)
				{
					return true;
				}

				tab[currentPositionI, currentPositionJ].MovedRight = true;
				currentPositionJ++;

				if (tab[currentPositionI, currentPositionJ].Value == '#')
				{
					return LoopSearcherDown(tab, currentPositionI, currentPositionJ - 1);
				}
			}

            return false;
        }

        // MOVING DOWN AFTER HITTING NEW OBSTALCE
        private static bool LoopSearcherDown(Day6[,] tab, int currentPositionI, int currentPositionJ)
        {
			while (currentPositionI < tab.GetLength(0) - 1)
            {
                if (tab[currentPositionI, currentPositionJ].MovedDown == true)
                {
                    return true;
                }

                tab[currentPositionI, currentPositionJ].MovedDown = true;
				currentPositionI++;

                if (tab[currentPositionI, currentPositionJ].Value == '#')
                {
                    return LoopSearcherLeft(tab, currentPositionI - 1, currentPositionJ);
                }
            }
            return false;
        }

        // MOVING LEFT AFTER HITTING NEW OBSTALCE
        private static bool LoopSearcherLeft(Day6[,] tab, int currentPositionI, int currentPositionJ)
        {
            while (currentPositionJ > 0)
            {
                if (tab[currentPositionI, currentPositionJ].MovedLeft == true)
                {
                    return true;
                }

                tab[currentPositionI, currentPositionJ].MovedLeft = true;
				currentPositionJ--;

                if (tab[currentPositionI, currentPositionJ].Value == '#')
                {
                    return LoopSearcherUp(tab, currentPositionI, currentPositionJ + 1);
                }
            }
            return false;
        }

        // MOVING UP AFTER HITTING NEW OBSTACLE
        private static bool LoopSearcherUp(Day6[,] tab, int currentPositionI, int currentPositionJ)
        {
            while(currentPositionI > 0)
            {
                if (tab[currentPositionI, currentPositionJ].MovedUp == true)
                {
                    return true;
                }

                tab[currentPositionI, currentPositionJ].MovedUp = true;
				currentPositionI--;


				if (tab[currentPositionI, currentPositionJ].Value == '#')
                {
                    return LoopSearcherRight(tab, currentPositionI + 1, currentPositionJ);
                }
            }
            return false;
		}

		private static Day6[,] GetObjectArray()
		{
			string basePath = AppDomain.CurrentDomain.BaseDirectory;
			string projectPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\"));
			string dayPath = Path.Combine(projectPath, "Day6");
			string filePath = Path.Combine(dayPath, "Day6test.txt");

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
			for (int i = 0; i < array.GetLength(0); i++)
			{
				for (int j = 0; j < array.GetLength(1); j++)
				{
					Console.Write(array[i, j].Value);
				}
				Console.WriteLine();
			}
			Console.WriteLine();
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
					clone[i, j].PlacedObstalce = original[i, j].PlacedObstalce;
				}
			}
			return clone;
		}


    }
}
