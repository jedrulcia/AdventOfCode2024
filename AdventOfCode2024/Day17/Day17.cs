using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    class Day17
    {
        public long a { get; set; }
        public long b { get; set; } = 0;
        public long c { get; set; } = 0;

        public Day17(long a)
        {
            this.a = a;
            this.b = 0;
            this.c = 0;
        }

        public static string Part1()
        {
            Day17 register = new Day17(25986278);
            List<int> program = [ 2, 4, 1, 4, 7, 5, 4, 1, 1, 4, 5, 5, 0, 3, 3, 0 ];
            List<int> output = new List<int>();

            for (int i = 0; i < program.Count; i += 2)
            {
                ProcessOperation(ref register, program[i], program[i + 1], ref i, ref output);
            }

            string text = "";

            foreach (var item in output)
            {
                text += (item.ToString() + ",");
            }

            return text.TrimEnd(',');
        }

        private static void ProcessOperation(ref Day17 register, int opcode, int operand, ref int i, ref List<int> output)
        {
            long result = 0;
            switch (opcode)
            {
                case 0:
                    if (operand == 4) result = Convert.ToInt64(Math.Pow(2, register.a));
                    else if (operand == 5) result = Convert.ToInt64(Math.Pow(2, register.b));
                    else if (operand == 6) result = Convert.ToInt64(Math.Pow(2, register.c));
                    else result = Convert.ToInt64(Math.Pow(2, operand));
                    register.a /= result;
                    break;
                case 1:
                    register.b = register.b ^ operand;
                    break;
                case 2:
                    if (operand == 4) result = register.a % 8;
                    else if (operand == 5) result = register.b % 8;
                    else if (operand == 6) result = register.c % 8;
                    else result = operand % 8;
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
                    else result = operand % 8;
                    output.Add(Convert.ToInt32(result));
                    break;
                case 6:
                    if (operand == 4) result = Convert.ToInt64(Math.Pow(2, register.a));
                    else if (operand == 5) result = Convert.ToInt64(Math.Pow(2, register.b));
                    else if (operand == 6) result = Convert.ToInt64(Math.Pow(2, register.c));
                    else result = Convert.ToInt64(Math.Pow(2, operand));
                    register.b = register.a / result;
                    break;
                case 7:
                    if (operand == 4) result = Convert.ToInt64(Math.Pow(2, register.a));
                    else if (operand == 5) result = Convert.ToInt64(Math.Pow(2, register.b));
                    else if (operand == 6) result = Convert.ToInt64(Math.Pow(2, register.c));
                    else result = Convert.ToInt64(Math.Pow(2, operand));
                    register.c = register.a / result;
                    break;

            }
        }
    }
}
