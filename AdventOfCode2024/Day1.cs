namespace AdventOfCode2024
{
	class Day1
	{
		public static int DistanceSmallestNumbers()
		{
			(int?[] tab1, int?[] tab2) = ConvertFromTxtToArrays();
			int distance = 0;

			for (int i = 0; i < tab1.Length; i++)
			{
				int indexFirst = FindSmallest(tab1);
				int indexSecond = FindSmallest(tab2);
				distance += Math.Abs(tab1[indexFirst].Value - tab2[indexSecond].Value);
				tab1[indexFirst] = null;
				tab2[indexSecond] = null;
			}

			return distance;
		}

		private static int FindSmallest(int?[] tab)
		{
			int index = 0;
			int? value = int.MaxValue;


			for (int i = 0; i < tab.Length; i++)
			{
				if (value > tab[i])
				{
					value = tab[i];
					index = i;
				}
			}
			return index;
		}

		public static int DistanceSimilarity()
		{
			(int?[] tab1, int?[] tab2) = ConvertFromTxtToArrays();
			int distance = 0;
			int numberOfOccurrence = 0;

			for (int i = 0; i < tab1.Length; i++)
			{
				for (int j = 0; j < tab2.Length; j++)
				{
					if (tab1[i] == tab2[j])
					{
						numberOfOccurrence++;
					}
				}
				distance += (tab1[i].Value * numberOfOccurrence);
				numberOfOccurrence = 0;
			}




			return distance;
		}

		static (int?[] tab1, int?[] tab2) ConvertFromTxtToArrays()
		{
			string sciezkaPliku = "C:\\Users\\Jedrulcia\\Desktop\\Jedrzej\\Programowanko\\github\\AdventOfCode2024\\AdventOfCode2024\\Day1.txt";
			var lines = File.ReadAllLines(sciezkaPliku);

			int?[] tab1 = new int?[lines.Length];
			int?[] tab2 = new int?[lines.Length];

			for (int i = 0; i < lines.Length; i++)
			{
				var numbers = lines[i].Split();

				tab1[i] = int.Parse(numbers[0]);
				tab2[i] = int.Parse(numbers[3]);
			}

			return (tab1, tab2);
		}
	}
}
