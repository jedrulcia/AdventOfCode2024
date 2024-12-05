namespace AdventOfCode2024
{
    class Day4
    {
        public static int Day4Part1()
        {
            int counter = 0;
            char[,] tab = ConvertFromTxtToArray();


            for (int i = 0; i < tab.GetLength(0); i++)
            {
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    if (tab[i, j] == 'X')
                    {
                        if (i + 3 < tab.GetLength(0))
                        {
                            // VERTICAL DOWN
                            if (tab[i + 1, j] == 'M' && tab[i + 2, j] == 'A' && tab[i + 3, j] == 'S')
                            {
                                counter++;
                            }
                            if (j + 3 < tab.GetLength(1))
                            {
                                // DIAGONAL RIGHT-DOWN
                                if (tab[i + 1, j + 1] == 'M' && tab[i + 2, j + 2] == 'A' && tab[i + 3, j + 3] == 'S')
                                {
                                    counter++;
                                }
                            }
                        }
                        if (i - 3 >= 0)
                        {
                            // VERTICAL UP
                            if (tab[i - 1, j] == 'M' && tab[i - 2, j] == 'A' && tab[i - 3, j] == 'S')
                            {
                                counter++;
                            }
                            // DIAGONAL LEFT-UP
                            if (j - 3 >= 0)
                            {
                                if (tab[i - 1, j - 1] == 'M' && tab[i - 2, j - 2] == 'A' && tab[i - 3, j - 3] == 'S')
                                {
                                    counter++;
                                }
                            }
                        }
                        if (j + 3 < tab.GetLength(1))
                        {
                            // HORIZONTAL RIGHT
                            if (tab[i, j + 1] == 'M' && tab[i, j + 2] == 'A' && tab[i, j + 3] == 'S')
                            {
                                counter++;
                            }
                            // DIAGONAL RIGHT-UP
                            if (i - 3 >= 0)
                            {
                                if (tab[i - 1, j + 1] == 'M' && tab[i - 2, j + 2] == 'A' && tab[i - 3, j + 3] == 'S')
                                {
                                    counter++;
                                }
                            }
                        }
                        if (j - 3 >= 0)
                        {
                            // HORIZONTAL LEFT
                            if (tab[i, j - 1] == 'M' && tab[i, j - 2] == 'A' && tab[i, j - 3] == 'S')
                            {
                                counter++;
                            }
                            // DIAGONAL LEFT-DOWN
                            if (i + 3 < tab.GetLength(0))
                            {
                                if (tab[i + 1, j - 1] == 'M' && tab[i + 2, j - 2] == 'A' && tab[i + 3, j - 3] == 'S')
                                {
                                    counter++;
                                }
                            }
                        }
                    }
                }
            }


            return counter;
        }

        public static int Day4Part2()
        {
            int counter = 0;
            char[,] tab = ConvertFromTxtToArray();

            for (int i = 0; i < tab.GetLength(0); i++)
            {
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    if (tab[i, j] == 'A')
                    {
                        if (i + 1 < tab.GetLength(0) && j + 1 < tab.GetLength(1) && i - 1 >= 0 && j - 1 >= 0)
                        {
                            if (tab[i - 1, j - 1] == 'M' && tab[i + 1, j + 1] == 'S' || tab[i - 1, j - 1] == 'S' && tab[i + 1, j + 1] == 'M')
                            {
                                if (tab[i - 1, j + 1] == 'M' && tab[i + 1, j - 1] == 'S' || tab[i - 1, j + 1] == 'S' && tab[i + 1, j - 1] == 'M')
                                {
                                    counter++;
                                }
                            }
                        }
                    }
                }
            }

            return counter;
        }

        public static char[,] ConvertFromTxtToArray()
		{
			string basePath = AppDomain.CurrentDomain.BaseDirectory;
			string projectPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\"));
			string dayPath = Path.Combine(projectPath, "Day4");
			string filePath = Path.Combine(dayPath, "Day4.txt");

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
    }
}
