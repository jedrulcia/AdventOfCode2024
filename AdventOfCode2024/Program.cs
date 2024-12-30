using AdventOfCode2024;
using System.Diagnostics;

class Program
{
	static void Main()
	{
		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();

		Console.WriteLine(Day23.Part2()); 
		
		stopwatch.Stop();
		Console.WriteLine($"Runtime: {stopwatch.ElapsedMilliseconds} ms");
	}
}