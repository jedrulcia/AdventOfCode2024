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

		public string input1 { get; set; }
		public string input2 { get; set; }
		public string output {  get; set; }
		public string operation { get; set; }

		public Day24(string input1, string input2, string output, string operation)
		{
			this.input1 = input1;
			this.input2 = input2;
			this.output = output;
			this.operation = operation;
		}

		public static string Part2()
		{
			(Dictionary<string, int> values, List<Day24> connections, Dictionary<string, int?> zValues) = ProcessInputPart2();

			var sorted = values.Keys
			.OrderByDescending(key => int.Parse(key.Substring(1)))
			.ToList();

			string binaryX = "";
			string binaryY = "";

			foreach (var key in sorted)
			{
				if (key[0] == 'x')
				{
					binaryX += values[key];
				}
				else if (key[0] == 'y')
				{
					binaryY += values[key];
				}
			}

			long decimalX = Convert.ToInt64(binaryX, 2);
			long decimalY = Convert.ToInt64(binaryY, 2);
			long decimalSum = decimalX + decimalY;

			string binarySum = Convert.ToString(decimalSum, 2);

			Dictionary<string, int> zValuesProper = new Dictionary<string, int>();


			int number = 45;
			for (int i = 0; i < binarySum.Length; i++)
			{
				string holder = "z";
				if (number >= 10)
				{
					holder += number;
				}
				else
				{
					holder += "0" + number;
				}
				zValuesProper.Add(holder, binarySum[i] - '0');
				number--;
			}


			while (zValues.ContainsValue(null))
			{
				foreach (var connection in connections)
				{
					if (zValues.ContainsKey(connection.output) && zValues[connection.output] == null)
					{
						if (values.ContainsKey(connection.input1) && values.ContainsKey(connection.input2))
						{
							switch (connection.operation)
							{
								case "AND":
									zValues[connection.output] = values[connection.input1] & values[connection.input2];
									break;
								case "OR":
									zValues[connection.output] = values[connection.input1] | values[connection.input2];
									break;
								case "XOR":
									zValues[connection.output] = values[connection.input1] ^ values[connection.input2];
									break;
							}
						}
					}

					if (!values.ContainsKey(connection.output))
					{
						if (values.ContainsKey(connection.input1) && values.ContainsKey(connection.input2))
						{
							switch (connection.operation)
							{
								case "AND":
									values.Add(connection.output, values[connection.input1] & values[connection.input2]);
									break;
								case "OR":
									values.Add(connection.output, values[connection.input1] | values[connection.input2]);
									break;
								case "XOR":
									values.Add(connection.output, values[connection.input1] ^ values[connection.input2]);
									break;
							}
						}
					}
				}
			}
			var sortedKeys = zValues.Keys
			.OrderByDescending(key => int.Parse(key.Substring(1)))
			.ToList();

			foreach (var key in sortedKeys)
			{
				if (zValues[key] != zValuesProper[key])
				{
					Console.WriteLine(key);
				}
			}

			return "";
		}

		public static (Dictionary<string, int> values, List<Day24> connections, Dictionary<string, int?> zValues) ProcessInputPart2()
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
			List<Day24> connections = new List<Day24>();
			Dictionary<string, int?> zValues = new Dictionary<string, int?>();

			foreach (var line in connectionLines)
			{
				var parts = line.Split(" ");
				if (parts[4][0] == 'z')
				{
					zValues.Add(parts[4], null);
				}
				connections.Add(new Day24 (parts[0], parts[2], parts[4], parts[1]));
			}

			return (values, connections, zValues);
		}



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
