using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
	class Day23
	{
		// 63
		// 1436
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

			foreach (var set in tSets)
			{
				if (set.Value.Count >= 2)
				{
					for (int i = 0; i < set.Value.Count; i++)
					{
						for (int j = 0; j < set.Value.Count; j++)
						{
							if (i == j)
							{
								continue;
							}
							else
							{
								if (sets.ContainsKey(set.Value.ElementAt(i)))
								{
									if (sets[set.Value.ElementAt(i)].Contains(set.Value.ElementAt(j)))
									{
										counter++;
										Console.WriteLine($"{set.Key}, {set.Value.ElementAt(i)}, {set.Value.ElementAt(j)}");
									}
								}
								else if (tSets.ContainsKey(set.Value.ElementAt(i)))
								{
									if (tSets[set.Value.ElementAt(i)].Contains(set.Value.ElementAt(j)))
									{
										counter++;
										Console.WriteLine($"{set.Key}, {set.Value.ElementAt(i)}, {set.Value.ElementAt(j)}");
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
