using MathNet.Numerics.LinearAlgebra.Solvers;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    class Day17
    {
		public static ulong best = ulong.MaxValue;
		public static string expectedOutput = "2414754114550330";


		public static string Part2()
		{
			Solve(0, expectedOutput.Length - 1);
			return best.ToString();
		}

		private static void Solve(ulong currentA, int index)
		{
			if (index == -1)
			{
				best = Math.Min(best, currentA);
				return;
			}

			var next = expectedOutput[index];
			for (int remainder = 0; remainder < 8; remainder++)
			{
				var nextA = currentA * 8 + (ulong)remainder;
				var result = Execute(nextA);
				if (expectedOutput.EndsWith(result))
				{
					Solve(nextA, index - 1);
				}
			}
		}

		//24 14 75 41 14 55 03 30

		//24 15 75 16 43 55 03 30

        public static string Part1()
        {
            return Execute(25986278);
        }

		private static string Execute(ulong a)
		{
			StringBuilder output = new();
			do
			{
				ulong b = a % 8;
				b = b ^ 4;
				ulong c = a / (ulong)Math.Pow(2, b);
				b = b ^ c;
				b = b ^ 4;
				output.Append((b % 8).ToString());
				a = a / 8;
			} while (a != 0);

			return output.ToString();
		}
    }
}
