using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
	class Day9
	{
		public string Id { get; set; }

		public Day9(string id)
		{
			this.Id = id;
		}


		//6330095022244
		public static long Part1()
		{
			string basePath = AppDomain.CurrentDomain.BaseDirectory;
			string projectPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\"));
			string dayPath = Path.Combine(projectPath, "Day9");
			string filePath = Path.Combine(dayPath, "Day9.txt");
			string text = File.ReadAllText(filePath);
			//string text = "2333133121414131402";


			List<Day9> list = new List<Day9>();
			int counter = 1;
			int id = 0;

			foreach (var character in text)
			{
				int n = Convert.ToInt32(character - '0');
				if (counter % 2 == 0)
				{
					for (int i = 0; i < n; i++)
					{
						list.Add(new Day9("."));
					}
				}
				if (counter % 2 != 0)
				{
					for (int i = 0; i < n; i++)
					{
						list.Add(new Day9(id.ToString()));
					}
					id++;
				}
				counter++;
			}

			bool shouldEndFlag = false;
			int holderJ = list.Count - 1;

			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].Id == ".")
				{
					for (int j = holderJ; j >= 0; j--)
					{
						if (list[j].Id != ".")
						{
							list[i].Id = list[j].Id;
							list[j].Id = ".";
							holderJ = j;
							break;
						}
						if (j <= i)
						{
							shouldEndFlag = true;
							break;
						}
					}
				}
				if (shouldEndFlag)
				{
					break;
				}
			}

			long sum = 0;

			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].Id != ".")
				{
					sum += Convert.ToInt64(list[i].Id) * i;
				}
				else
				{
					break;
				}
			}

			return sum;
		}

		public static long Part2()
		{
			string basePath = AppDomain.CurrentDomain.BaseDirectory;
			string projectPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\"));
			string dayPath = Path.Combine(projectPath, "Day9");
			string filePath = Path.Combine(dayPath, "Day9.txt");
			string text = File.ReadAllText(filePath);
			//string text = "2333133121414131402";


			List<Day9> list = new List<Day9>();
			int counter = 1;
			int id = 0;

			foreach (var character in text)
			{
				int n = Convert.ToInt32(character - '0');
				if (counter % 2 == 0)
				{
					for (int i = 0; i < n; i++)
					{
						list.Add(new Day9("."));
					}
				}
				if (counter % 2 != 0)
				{
					for (int i = 0; i < n; i++)
					{
						list.Add(new Day9(id.ToString()));
					}
					id++;
				}
				counter++;
			}

			bool shouldEndFlag = false;
			int holderJ = list.Count - 1;

			List<int> dotIndexes = new List<int>();
			List<int> idIndexes = new List<int>();

			for (int i = list.Count - 1; i >= 0; i--)
			{
				if (list[i].Id != ".")
				{
					string idHolder = list[i].Id;
					int currentI = i;

					while (currentI >= 0 && list[currentI].Id == idHolder)
					{
						idIndexes.Add(currentI);
						currentI--;
					}

					for (int j = 0; j < i; j++)
					{
						if (list[j].Id == ".")
						{
							int currentJ = j;

							while (list[currentJ].Id == "." && j < list.Count())
							{
								dotIndexes.Add(currentJ);
								currentJ++;
							}

                            if (dotIndexes.Count >= idIndexes.Count)
                            {
                                for (int k = idIndexes.Count - 1; k >= 0; k--)
                                {
                                    list[dotIndexes[k]].Id = idHolder;
                                    list[idIndexes[k]].Id = ".";
                                }
                                idIndexes.Clear();
                                dotIndexes.Clear();
                                idHolder = "";
                                break;
                            }
                            dotIndexes.Clear();
                        }
                    }
                    idIndexes.Clear();
                    i = currentI + 1;
                }
            }

            long sum = 0;
			int multiplier = 0;

			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].Id != ".")
				{
					sum += Convert.ToInt64(list[i].Id) * multiplier;
                }
                multiplier++;
            }

			return sum;
		}
	}
}
