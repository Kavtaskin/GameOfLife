using System;
using System.Threading;

namespace GameOfLife
{
    class GameOfLife
    {
        private const bool debug = false;
        static void Main()
        {
            var field = new Field();
            var map = field.Generate(debug);
            while (true)
            {
                map = field.Change(map);
                Thread.Sleep(50);
                field.Display(map);
            };
        }
    }

    class Field
    {
        public const int height = 75;
        public const int width = height;
        public const int seed = 1;
        private int[,] map = new int[height, width];

        public int[,] Generate(bool debug)
        {
            if (debug)
            {
                map[0, 0] = 1;
                map[0, width - 1] = 1;
                map[height - 1, 0] = 1;
                map[height - 1, width - 1] = 1;

                map[10, 10] = 1;
                map[10, 11] = 1;
                map[10, 12] = 1;

                map[15, 15] = 1;
                map[15, 16] = 1;
                map[16, 15] = 1;
                map[16, 16] = 1;

                map[30, 0] = 1;
                map[31, 0] = 1;
                map[32, 0] = 1;

                map[50, 10] = 1;
                map[50, 11] = 1;
                map[50, 12] = 1;
                map[49, 12] = 1;
                map[48, 11] = 1;
            }
            else
            {
                Random rnd = new Random(seed);
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        map[i, j] = rnd.Next(0, 2);
                    }
                }
            }
            return map;
        }

        public void Display(int[,] map)
        {
            Console.Clear();
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (map[i, j] == 1)
                        Console.Write(".");
                    else
                        Console.Write(" ");
                }
                Console.Write("\n");
            }
        }

        public int[,] Change(int[,] map)
        {
            var buffer = (int[,])map.Clone();

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    var neigbors = checkNeighbors(buffer, i, j);

                    if (buffer[i, j] == 0)
                    {
                        if (neigbors == 3)
                            map[i, j] = 1;
                    }
                    else
                    {
                        if ((neigbors < 2) || (neigbors > 3))
                            map[i, j] = 0;
                    }
                }
            }
            return map;
        }

        private int checkNeighbors(int[,] map, int i, int j)
        {
            var summ = 0;
            var i_top = i - 1;
            var i_bottom = i + 1;
            var i_left = j - 1;
            var j_right = j + 1;

            if (i == 0)
                i_top = height - 1;
            else if (i == height - 1)
                i_bottom = 0;
            if (j == 0)
                i_left = width - 1;
            else if (j == width - 1)
                j_right = 0;

            summ = map[i_top, i_left] + map[i_top, j] + map[i_top, j_right] + map[i, i_left] + map[i, j_right] + map[i_bottom, i_left] + map[i_bottom, j] + map[i_bottom, j_right];
            return summ;
        }
    }
}

