using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace AdventOfCode2024
{
    public class Coordinates
    {
        public long x { get; set; }
        public long y { get; set; }

        public Coordinates(long x, long y)
        {
            this.x = x;
            this.y = y;
        }
    }

    class Day13
    {
        public Coordinates ButtonA { get; set; }
        public Coordinates ButtonB { get; set; }
        public Coordinates Prize { get; set; }

        public Day13(long aX, long aY, long bX, long bY, long prizeX, long prizeY)
        {
            this.ButtonA = new Coordinates(aX, aY);
            this.ButtonB = new Coordinates(bX, bY);
            this.Prize = new Coordinates(prizeX, prizeY);
        }


        //36870
        public static long Part1()
        {
            long sum = 0;
            List<Day13> list = GetListPart1();

            foreach (var item in list)
            {
                List<long> possibleTokens = new List<long>();

                long currentX = 0;
                long currentY = 0;
                long currentTokens = 0;
                long buttonACounter = 0;

                while (currentX < item.Prize.x && currentY < item.Prize.y)
                {
                    currentX += item.ButtonA.x;
                    currentY += item.ButtonA.y;
                    currentTokens += 3;
                    buttonACounter++;
                }

                if (currentX == item.Prize.x && currentY == item.Prize.y && buttonACounter <= 100)
                {
                    possibleTokens.Add(currentTokens);
                }
                buttonACounter = 0;

                currentX = 0;
                currentY = 0;
                currentTokens = 0;
                int buttonBCounter = 0;

                while (currentX < item.Prize.x && currentY < item.Prize.y)
                {
                    currentX += item.ButtonB.x;
                    currentY += item.ButtonB.y;
                    currentTokens += 1;
                    buttonBCounter++;
                }

                if (currentX == item.Prize.x && currentY == item.Prize.y && buttonBCounter <= 100)
                {
                    possibleTokens.Add(currentTokens);
                }

                while (buttonBCounter > 0)
                {
                    currentX -= item.ButtonB.x;
                    currentY -= item.ButtonB.y;
                    currentTokens -= 1;
                    buttonBCounter--;

                    while (currentX < item.Prize.x && currentY < item.Prize.y)
                    {
                        currentX += item.ButtonA.x;
                        currentY += item.ButtonA.y;
                        currentTokens += 3;
                        buttonACounter++;
                    }

                    if (currentX == item.Prize.x && currentY == item.Prize.y && buttonACounter <= 100 && buttonBCounter <= 100)
                    {
                        possibleTokens.Add(currentTokens);
                    }
                }

                long holder = 0;

                foreach (var token in possibleTokens)
                {
                    if (holder < token)
                    {
                        holder = token;
                    }
                    else if (holder == 0)
                    {
                        holder = token;
                    }
                }
                sum += holder;
            }


            return sum;
        }

        public static List<Day13> GetListPart1()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string projectPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\"));
            string dayPath = Path.Combine(projectPath, "Day13");
            string filePath = Path.Combine(dayPath, "Day13.txt");
            string[] lines = File.ReadAllLines(filePath);

            List<Day13> list = new List<Day13>();

            string pattern = @"Button A: X\+(\d+), Y\+(\d+)\s+Button B: X\+(\d+), Y\+(\d+)\s+Prize: X=(\d+), Y=(\d+)";
            Regex regex = new Regex(pattern, RegexOptions.Multiline);

            for (int i = 0; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                {
                    continue;
                }

                if (i + 2 < lines.Length)
                {
                    string block = string.Join("\n", lines[i], lines[i + 1], lines[i + 2]);
                    Match match = regex.Match(block);
                    if (match.Success)
                    {
                        int aX = int.Parse(match.Groups[1].Value);
                        int aY = int.Parse(match.Groups[2].Value);
                        int bX = int.Parse(match.Groups[3].Value);
                        int bY = int.Parse(match.Groups[4].Value);
                        int prizeX = int.Parse(match.Groups[5].Value);
                        int prizeY = int.Parse(match.Groups[6].Value);

                        list.Add(new Day13(aX, aY, bX, bY, prizeX, prizeY));
                    }

                    i += 2;
                }
            }
            return list;
        }

        // ===================================================== PART2 =====================================================

        // ROZBIEGANIE SIE X I Y - jezeli obydwa przyciski maja rozbiegajace sie wartosci X i Y i obydwa sa w te sama strone, to na pewno nie bedzie tam possible prize
        // Ogolnie jesli ButtonAX > ButtonAY oraz ButtonBX > ButtonBY to mamy pewnosc ze nie bedzie tam prawidlowej odpowiedzi przy tak ogromnej liczbie.

        // 1. ZBLIZYC CURRENTX I CURRENTY laczac wieksze X i Y oraz mniejsze X i Y - wtedy bedziemy mieli dwie zblizone wartosci 
        // 2. Wypuscic obydwie wartosci wypuscic w dwie metody kazda - jedna metoda zwiekszajaca buttonA i zmniejszajaca buttonB do skutku i na odwrot


        public static long Part2()
        {
            long sum = 0;
            List<Day13> list = GetListPart2();

            foreach (var item in list)
            {
                List<long> possibleTokens = new List<long>();
                
                long multiplierXA = item.Prize.x / item.ButtonA.x;
                long multiplierYA = item.Prize.y / item.ButtonA.y;
                long multiplierA = (multiplierXA + multiplierYA) / 2;

                long currentXA = item.ButtonA.x * multiplierA;
                long currentYA = item.ButtonA.y * multiplierA;
                long currentTokensA = 3 * multiplierA;
                long buttonCounterA = multiplierA;


                long multiplierXB = item.Prize.x / item.ButtonB.x;
                long multiplierYB = item.Prize.x / item.ButtonB.y;
                long multiplierB = (multiplierXB + multiplierYB) / 2;

                long currentXB = item.ButtonB.x * multiplierB;
                long currentYB = item.ButtonB.y * multiplierB;
                long currentTokeknsB = multiplierB;
                long buttonCounterB = multiplierB;


                long holder = 0;

                foreach (var token in possibleTokens)
                {
                    if (holder < token)
                    {
                        holder = token;
                    }
                    else if (holder == 0)
                    {
                        holder = token;
                    }
                }
                sum += holder;
            }


            return sum;
        }

        public static List<Day13> GetListPart2()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string projectPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\"));
            string dayPath = Path.Combine(projectPath, "Day13");
            string filePath = Path.Combine(dayPath, "Day13test.txt");
            string[] lines = File.ReadAllLines(filePath);

            List<Day13> list = new List<Day13>();

            string pattern = @"Button A: X\+(\d+), Y\+(\d+)\s+Button B: X\+(\d+), Y\+(\d+)\s+Prize: X=(\d+), Y=(\d+)";
            Regex regex = new Regex(pattern, RegexOptions.Multiline);

            for (int i = 0; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                {
                    continue;
                }

                if (i + 2 < lines.Length)
                {
                    string block = string.Join("\n", lines[i], lines[i + 1], lines[i + 2]);
                    Match match = regex.Match(block);
                    if (match.Success)
                    {
                        long aX = int.Parse(match.Groups[1].Value);
                        long aY = int.Parse(match.Groups[2].Value);
                        long bX = int.Parse(match.Groups[3].Value);
                        long bY = int.Parse(match.Groups[4].Value);
                        long prizeX = int.Parse(match.Groups[5].Value);
                        long prizeY = int.Parse(match.Groups[6].Value);

                        prizeX += 10000000000000;
                        prizeY += 10000000000000;

                        list.Add(new Day13(aX, aY, bX, bY, prizeX, prizeY));
                    }

                    i += 2;
                }
            }
            return list;
        }
    }
}
