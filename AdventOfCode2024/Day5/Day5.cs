namespace AdventOfCode2024
{
    class Day5
    {
        public static int Day5Part1()
        {
            int sum = 0;
            bool badFlag = false;

            (int[,] ordering, List<List<int>> updates) = ConvertFromTxtToTabAndList();

            for (int i = 0; i < updates.Count; i++)
            {
                for (int j = 0; j < updates[i].Count - 1; j++)
                {
                    for (int k = j + 1; k < updates[i].Count; k++)
                    {
                        for (int l = 0; l < ordering.GetLength(0); l++)
                        {
                            if (updates[i][j] == ordering[l, 1] && updates[i][k] == ordering[l, 0])
                            {
                                badFlag = true;
                                break;
                            }
                        }
                        if (badFlag == true)
                        {
                            break;
                        }
                    }
                    if (badFlag == true)
                    {
                        break;
                    }
                }
                if (badFlag == false)
                {
                    sum += updates[i][Convert.ToInt32(updates[i].Count() / 2)];
                }
                badFlag = false;
            }

            return sum;
        }

        public static int Day5Part2()
        {
            int sum = 0;
            bool badFlag = false;

            (int[,] ordering, List<List<int>> updates) = ConvertFromTxtToTabAndList();

            for (int i = 0; i < updates.Count; i++)
            {
                for (int j = 0; j < updates[i].Count - 1; j++)
                {
                    for (int k = j + 1; k < updates[i].Count; k++)
                    {
                        for (int l = 0; l < ordering.GetLength(0); l++)
                        {
                            if (updates[i][j] == ordering[l, 1] && updates[i][k] == ordering[l, 0])
                            {
                                badFlag = true;
                                var holder = updates[i][j];
                                updates[i][j] = updates[i][k];
                                updates[i][k] = holder;
                            }
                        }
                    }
                }
                if (badFlag == true)
                {
                    sum += updates[i][Convert.ToInt32(updates[i].Count() / 2)];
                }
                badFlag = false;
            }

            return sum;
        }

        private static (int[,] tabOrdering, List<List<int>> tabUpdates) ConvertFromTxtToTabAndList()
        {

			string basePath = AppDomain.CurrentDomain.BaseDirectory;
			string projectPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\"));
			string dayPath = Path.Combine(projectPath, "Day5");


			string orderingFilePath = Path.Combine(dayPath, "Day5pageOrdering.txt");
			var linesOrdering = File.ReadAllLines(orderingFilePath);
            int[,] ordering = new int[linesOrdering.Length, 2];

            for (int i = 0; i < linesOrdering.Length; i++)
            {
                var numbers = linesOrdering[i].Split("|");

                ordering[i, 0] = int.Parse(numbers[0]);
                ordering[i, 1] = int.Parse(numbers[1]);
            }

			string updatesFilePath = Path.Combine(dayPath, "Day5updates.txt");
			var linesUpdates = File.ReadAllLines(updatesFilePath);
            List<List<int>> updates = new List<List<int>>();

            for (int i = 0; i < linesUpdates.Length; i++)
            {
                var numbers = linesUpdates[i].Split(",");
                List<int> newList = new List<int>();

                for (int j = 0; j < numbers.Length; j++)
                {

                    newList.Add(int.Parse(numbers[j]));

                }
                updates.Add(newList);
            }

            return (ordering, updates);
        }
    }
}
