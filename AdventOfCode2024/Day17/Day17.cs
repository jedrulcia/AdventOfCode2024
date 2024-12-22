using MathNet.Numerics.LinearAlgebra.Solvers;
using Microsoft.Win32;
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

		public ulong a { get; set; }
		public ulong b { get; set; } = 0;
		public ulong c { get; set; } = 0;

		public Day17(ulong a)
		{
			this.a = a;
			this.b = 0;
			this.c = 0;
		}

		public static ulong best = ulong.MaxValue;
		public static string expectedOutput = "2414754114550330";

		//public static string expectedOutput = "2415751643550330";

		public static string Part2()
		{
			Solve(0UL, expectedOutput.Length - 1);
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

		public static string Execute(ulong a)
		{
			Day17 register = new Day17(a);
			List<int> program = [2, 4, 1, 4, 7, 5, 4, 1, 1, 4, 5, 5, 0, 3, 3, 0];
			//List<int> program = [2, 4, 1, 5, 7, 5, 1, 6, 4, 3, 5, 5, 0, 3, 3, 0];
			string output = "";

			for (int i = 0; i < program.Count; i += 2)
			{
				ProcessOperation(ref register, program[i], program[i + 1], ref i, ref output);
			}

			return output;
		}

		private static void ProcessOperation(ref Day17 register, int opcode, int operand, ref int i, ref string output)
		{
			ulong result = 0;
			switch (opcode)
			{
				case 0:
					if (operand == 4) result = (ulong)(Math.Pow(2, register.a));
					else if (operand == 5) result = (ulong)(Math.Pow(2, register.b));
					else if (operand == 6) result = (ulong)(Math.Pow(2, register.c));
					else result = (ulong)(Math.Pow(2, operand));
					register.a /= result;
					break;
				case 1:
					register.b = register.b ^ (ulong)operand;
					break;
				case 2:
					if (operand == 4) result = register.a % 8;
					else if (operand == 5) result = register.b % 8;
					else if (operand == 6) result = register.c % 8;
					else result = (ulong)operand % 8;
					register.b = result;
					break;
				case 3:
					if (register.a != 0)
					{
						i = operand - 2;
					}
					break;
				case 4:
					register.b = register.b ^ register.c;
					break;
				case 5:
					if (operand == 4) result = register.a % 8;
					else if (operand == 5) result = register.b % 8;
					else if (operand == 6) result = register.c % 8;
					else result = (ulong)operand % 8;
					output += result.ToString();
					break;
				case 6:
					if (operand == 4) result = (ulong)(Math.Pow(2, register.a));
					else if (operand == 5) result = (ulong)(Math.Pow(2, register.b));
					else if (operand == 6) result = (ulong)(Math.Pow(2, register.c));
					else result = (ulong)(Math.Pow(2, operand));
					register.b = register.a / result;
					break;
				case 7:
					if (operand == 4) result = (ulong)(Math.Pow(2, register.a));
					else if (operand == 5) result = (ulong)(Math.Pow(2, register.b));
					else if (operand == 6) result = (ulong)(Math.Pow(2, register.c));
					else result = (ulong)(Math.Pow(2, operand));
					register.c = register.a / result;
					break;
			}
		}

		// ================================================================= PART 1 =================================================================

		//24 14 75 41 14 55 03 30

		public static string Part1()
		{
			Day17 register = new Day17(25986278);
			List<int> program = [2, 4, 1, 4, 7, 5, 4, 1, 1, 4, 5, 5, 0, 3, 3, 0];
			string output = "";

			for (int i = 0; i < program.Count; i += 2)
			{
				ProcessOperation(ref register, program[i], program[i + 1], ref i, ref output);
			}

			return output;
		}
	}
}
