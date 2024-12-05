namespace AdventOfCode2024
{
    class Day2
    {
        public static int Day2Part1()
        {
            var listOfLists = ConvertFromTxtToLists();

            int safeCounter = 0;
            bool safeFlag = true;

            bool decreasingFlag = false;
            bool increasingFlag = false;

            foreach (var list in listOfLists)
            {
                for (int i = 0; i < list.Count - 1; i++)
                {
                    if (decreasingFlag == false && increasingFlag == false)
                    {
                        if (list[i] > list[i + 1])
                        {
                            decreasingFlag = true;
                        }
                        else if (list[i] < list[i + 1])
                        {
                            increasingFlag = true;
                        }
                        else
                        {
                            safeFlag = false;
                            break;
                        }
                    }

                    if (list[i] > list[i + 1] && increasingFlag == true)
                    {
                        safeFlag = false;
                        break;
                    }
                    else if (list[i] < list[i + 1] && decreasingFlag == true)
                    {
                        safeFlag = false;
                        break;
                    }
                    else if (list[i] == list[i + 1])
                    {
                        safeFlag = false;
                        break;
                    }
                    else if (Math.Abs(list[i] - list[i + 1]) > 3 || Math.Abs(list[i] - list[i + 1]) < 1)
                    {
                        safeFlag = false;
                        break;
                    }
                }

                if (safeFlag)
                {
                    safeCounter++;
                }

                safeFlag = true;
                increasingFlag = false;
                decreasingFlag = false;
            }

            return safeCounter;
        }

        public static int Day2Part2()
        {
            var listOfLists = ConvertFromTxtToLists();

            int safeCounter = 0;

            bool safeFlag = true;

            bool decreasingFlag = false;
            bool increasingFlag = false;

            foreach (var list in listOfLists)
            {
                for (int j = -1; j < list.Count; j++)
                {
                    var newList = new List<int>(list);
                    if (j != -1)
                    {
                        newList.RemoveAt(j);
                    }

                    for (int i = 0; i < newList.Count - 1; i++)
                    {
                        if (decreasingFlag == false && increasingFlag == false)
                        {
                            if (newList[i] > newList[i + 1])
                            {
                                decreasingFlag = true;
                            }
                            else if (newList[i] < newList[i + 1])
                            {
                                increasingFlag = true;
                            }
                            else
                            {
                                safeFlag = false;
                                break;
                            }
                        }

                        if (newList[i] > newList[i + 1] && increasingFlag == true)
                        {
                            safeFlag = false;
                            break;
                        }
                        else if (newList[i] < newList[i + 1] && decreasingFlag == true)
                        {
                            safeFlag = false;
                            break;
                        }
                        else if (newList[i] == newList[i + 1])
                        {
                            safeFlag = false;
                            break;
                        }
                        else if (Math.Abs(newList[i] - newList[i + 1]) > 3 || Math.Abs(newList[i] - newList[i + 1]) < 1)
                        {
                            safeFlag = false;
                            break;
                        }
                    }

                    if (safeFlag)
                    {
                        safeCounter++;
                        break;
                    }

                    safeFlag = true;
                    increasingFlag = false;
                    decreasingFlag = false;
                }
            }

            return safeCounter;
        }

        private static List<List<int>> ConvertFromTxtToLists()
		{
			string basePath = AppDomain.CurrentDomain.BaseDirectory;
			string projectPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\"));
			string dayPath = Path.Combine(projectPath, "Day2");
			string filePath = Path.Combine(dayPath, "Day2.txt");

			var lines = File.ReadAllLines(filePath);
            var list = new List<List<int>>();

            for (int i = 0; i < lines.Length; i++)
            {
                var appendingList = new List<int>();
                var numbers = lines[i].Split();
                foreach (var number in numbers)
                {
                    appendingList.Add(Convert.ToInt32(number));
                }
                list.Add(appendingList);
            }
            return list;
        }
    }
}
