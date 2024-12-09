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
			//string text = File.ReadAllText(filePath);
			string text = "2333133121414131402";


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


			for (int j = list.Count - 1; j >= 0; j--)
			{
				if (list[j].Id != ".")
				{
					string idHolder = list[j].Id;
					
					while (j >= 0 && list[j].Id == idHolder)
					{
						idIndexes.Add(j);
						j--;
					}
					j++;

					for (int i = 0; i < j; i++)
					{
						if (list[i].Id == ".")
						{
							while (list[i].Id == ".")
							{
								dotIndexes.Add(i);
								i++;
							}
							i--;
						}

						if (dotIndexes.Count >= idIndexes.Count)
						{
							for (int k = idIndexes.Count - 1; k >= 0; k--)
							{
							}
							for (int k = 0; k < idIndexes.Count - 1; k++)
							{
							}
							idIndexes.Clear();
							dotIndexes.Clear();
						}

					}
					
				}
			}



			/*for (int i = 0; i < list.Count; i++)
			{
				if (list[i].Id == ".")
				{
					while (list[i].Id == ".")
					{
						dotCounter++;
						i++;
					}

					for (int j = holderJ; j >= 0; j--)
					{
						string holderId = "";
						if (list[j].Id != ".")
						{
							holderId = list[j].Id;
							while (list[j].Id == holderId)
							{
								idCounter++;
								j--;
							}

							if (idCounter <= dotCounter)
							{
								while(idCounter > 0)
								{
									list[i - dotCounter].Id = holderId;
									list[j + idCounter].Id = ".";
									foreach (var item in list)
									{
										Console.Write(item.Id);
									}
									Console.WriteLine();
									dotCounter--;
									idCounter--;
									holderJ = j + 1;
								}
								dotCounter = 0;
								idCounter = 0;
							}
							else
							{
								idCounter = 0;
							}
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
			}*/

			long sum = 0;
			int multiplier = 0;

			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].Id != ".")
				{
					sum += Convert.ToInt64(list[i].Id) * multiplier;
					multiplier++;
				}
			}

			return sum;
		}
	}
}
