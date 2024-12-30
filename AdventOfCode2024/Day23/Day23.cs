using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
	class Day23
	{
		public static string Part1()
		{
			(List<(string, string)> tConnections, List<(string, string)> connections) = ProcessInput();
			Dictionary<string, HashSet<string>> sets = new Dictionary<string, HashSet<string>>();
			Dictionary<string, HashSet<string>> tSets = new Dictionary<string, HashSet<string>>();

			foreach (var connection in connections)
			{
				if (sets.ContainsKey(connection.Item1))
				{
					sets[connection.Item1].Add(connection.Item2);
				}
				else
				{
					sets.Add(connection.Item1, new HashSet<string>());
					sets[connection.Item1].Add(connection.Item2);
				}
			}

			foreach (var connection in tConnections)
			{
				if (tSets.ContainsKey(connection.Item1))
				{
					tSets[connection.Item1].Add(connection.Item2);
				}
				else
				{
					tSets.Add(connection.Item1, new HashSet<string>());
					tSets[connection.Item1].Add(connection.Item2);
				}
			}

			int counter = 0;

			foreach (var tSet in tSets)
			{
				if (tSet.Value.Count >= 2)
				{
					for (int i = 0; i < tSet.Value.Count; i++)
					{
						for (int j = 0; j < tSet.Value.Count; j++)
						{
							if (i == j)
							{
								continue;
							}
							else
							{
								string elementI = tSet.Value.ElementAt(i);
								string elementJ = tSet.Value.ElementAt(j);

								if (sets.ContainsKey(elementI))
								{
									if (sets[elementI].Contains(elementJ))
									{
										counter++;
										Console.WriteLine($"{tSet.Key}, {elementI}, {elementJ}");
									}
								}
								else if (tSets.ContainsKey(elementI))
								{
									if (tSets[elementI].Contains(elementJ))
									{
										counter++;
										Console.WriteLine($"{tSet.Key}, {elementI}, {elementJ}");
									}
								}
							}
						}
					}
				}
			}


			return counter.ToString();
		}

		private static (List<(string, string)> tConnections, List<(string, string)> connections) ProcessInput()
		{
			List<(string, string)> connections = new List<(string, string)>();
			List<(string, string)> tConnections = new List<(string, string)>();


			string basePath = AppDomain.CurrentDomain.BaseDirectory;
			string projectPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\"));
			string dayPath = Path.Combine(projectPath, "Day23");
			string filePath = Path.Combine(dayPath, "Day23test.txt");
			var lines = File.ReadAllLines(filePath);

			foreach (var line in lines)
			{
				var computers = line.Split('-');

				if (computers[0][0] == 't')
				{
					var connection = (computers[0], computers[1]);
					tConnections.Add(connection);
				}
				else if (computers[1][0] == 't')
				{
					var connection = (computers[1], computers[0]);
					tConnections.Add(connection);
				}
				else
				{
					var connection = (computers[0], computers[1]);
					connections.Add(connection);
				}
			}

			return (tConnections, connections);
		}
	}
}
