using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    class Day11
    {
        public static int Part1()
        {
            List<long> list = [30, 71441, 3784, 580926, 2, 8122942, 0, 291];

            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < list.Count; j++)
                {
                    if (list[j] == 0)
                    {
                        list[j] = 1;
                    }
                    else if (list[j].ToString().Length % 2 == 0)
                    {
                        string value = list[j].ToString();
                        int middleIndex = value.Length / 2;

                        list[j] = Convert.ToInt64(value.Substring(0, middleIndex));
                        list.Insert(j + 1, Convert.ToInt64(value.Substring(middleIndex)));
                        j++;
                    }
                    else
                    {
                        list[j] *= 2024;
                    }
                }
            }
            return list.Count;
        }

		//228651922369703
		public static long Part2()
        {
            List<long> numbers = [30, 71441, 3784, 580926, 2, 8122942, 0, 291];
            Dictionary<long, long> currentState = new();
            numbers.GroupBy(x => x).ToList().ForEach(x => currentState.Add(x.Key, x.Count()));

            for (int i = 0; i < 75; i++)
			{
				var newState = new Dictionary<long, long>();

                foreach (var (number, count) in currentState)
                {
                    newState = CheckRules(number, count, newState);
                }

                currentState = newState;
            }

            return currentState.Sum(n => n.Value);
        }

        private static Dictionary<long, long> CheckRules(long number, long count, Dictionary<long, long> newState)
        {
            if (number == 0)
            {
                newState = AddOrUpdate(1, count, newState);
            }
            else if (number.ToString().Length % 2 == 0)
			{
				string value = number.ToString();
				int middleIndex = value.Length / 2;

				long valueFirst = Convert.ToInt64(value.Substring(0, middleIndex));
				long valueSecond = Convert.ToInt64(value.Substring(middleIndex));

                newState = AddOrUpdate(valueFirst, count, newState);
                newState = AddOrUpdate(valueSecond, count, newState);
			}
            else
            {
                newState = AddOrUpdate(number * 2024, count, newState);
            }

            return newState;
        }

        private static Dictionary<long, long> AddOrUpdate(long number, long count, Dictionary<long, long> newState)
        {
			if (newState.ContainsKey(number))
			{
				newState[number] += count;
			}
			else
			{
				newState[number] = count;
			}
            return newState;
		}
    }
}
