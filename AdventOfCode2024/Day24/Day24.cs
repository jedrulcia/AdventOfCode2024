using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
	class Day24
	{
		
		// ============================================== PART1 ==============================================

		public static string Part1()
		{
			(Dictionary<string, int> values, List<List<string>> connections, Dictionary<string, int?> zValues) = ProcessInputPart1();

			while (zValues.ContainsValue(null))
			{
				foreach (var connection in connections)
				{
					if (zValues.ContainsKey(connection[3]) && zValues[connection[3]] == null)
					{
						if (values.ContainsKey(connection[0]) && values.ContainsKey(connection[2]))
						{
							switch (connection[1])
							{
								case "AND":
									zValues[connection[3]] = values[connection[0]] & values[connection[2]];
									break;
								case "OR":
									zValues[connection[3]] = values[connection[0]] | values[connection[2]];
									break;
								case "XOR":
									zValues[connection[3]] = values[connection[0]] ^ values[connection[2]];
									break;
							}
						}
					}

					if (!values.ContainsKey(connection[3]))
					{
						if (values.ContainsKey(connection[0]) && values.ContainsKey(connection[2]))
						{
							switch (connection[1])
							{
								case "AND":
									values.Add(connection[3], values[connection[0]] & values[connection[2]]);
									break;
								case "OR":
									values.Add(connection[3], values[connection[0]] | values[connection[2]]);
									break;
								case "XOR":
									values.Add(connection[3], values[connection[0]] ^ values[connection[2]]);
									break;
							}
						}
					}
				}
			}

			var sortedKeys = zValues.Keys
			.OrderByDescending(key => int.Parse(key.Substring(1)))
			.ToList();

			string binary = "";

			foreach (var key in sortedKeys)
			{
				binary += zValues[key].ToString();
			}

			long decimalValue = Convert.ToInt64(binary, 2);

			return decimalValue.ToString();
		}

		public static (Dictionary<string, int> values, List<List<string>> connections, Dictionary<string, int?> zValues) ProcessInputPart1()
		{
			string basePath = AppDomain.CurrentDomain.BaseDirectory;
			string projectPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\"));
			string dayPath = Path.Combine(projectPath, "Day24");

			string valuesPath = Path.Combine(dayPath, "Day24values.txt");
			var valueLines = File.ReadAllLines(valuesPath);
			Dictionary<string, int> values = new Dictionary<string, int>();

			foreach (var line in valueLines)
			{
				var parts = line.Split(' ');
				values.Add(parts[0].Trim(':'), Convert.ToInt32(parts[1]));
			}

			string connectionsPath = Path.Combine(dayPath, "Day24connections.txt");
			var connectionLines = File.ReadAllLines(connectionsPath);
			List<List<string>> connections = new List<List<string>>();
			Dictionary<string, int?> zValues = new Dictionary<string, int?>();

			foreach (var line in connectionLines)
			{
				var parts = line.Split(" ");
				if (parts[4][0] == 'z')
				{
					zValues.Add(parts[4], null);
				}
				connections.Add(new List<string> { parts[0], parts[1], parts[2], parts[4] });
			}

			return (values, connections, zValues);
		}

	}
}
