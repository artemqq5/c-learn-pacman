using System;
using System.IO;
using System.Threading;

class Program
{
    static int x = 1;
    static int y = 1;
    static int score = 0;

    static void Main()
    {
        var map = convertMapToArray();
        x = 1;
        y = 1;

        while (true)
        {
            Console.Clear();
            drawMap(map);

            Console.SetCursorPosition(map.GetLength(1)+4, 0);
            Console.Write($"Score: {score}");

            var pressedKey = Console.ReadKey();

            drawPackman(ref x, ref y, pressedKey, map);
        }
    }

    static char[,] convertMapToArray()
    {
        string[] map = File.ReadAllLines("map.txt");
        char[,] tempArray = new char[map.Length, map[0].Length];

        for (var i = 0; i < map.Length; i++)
        {
            for (var j = 0; j < map[i].Length; j++)
            {
                tempArray[i, j] = map[i][j];
            }
        }

        return tempArray;
    }

    static void drawMap(char[,] map)
    {
        for (var i = 0; i < map.GetLength(0); i++)
        {
            for (var j = 0; j < map.GetLength(1); j++)
            {
                if (i == y && j == x)
                {
                    Console.Write("@"); 
                }
                else
                {
                    Console.Write(map[i, j]);
                }
            }
            Console.WriteLine();
        }
    }

    static void drawPackman(ref int x, ref int y, ConsoleKeyInfo pressedKey, char[,] map)
    {
        switch (pressedKey.Key)
        {
            case ConsoleKey.RightArrow:
                if (x + 1 < map.GetLength(1) && map[y, x + 1] != '#')
                {
                    x++;
                }
                break;
            case ConsoleKey.DownArrow:
                if (y + 1 < map.GetLength(0) && map[y + 1, x] != '#')
                {
                    y++;
                }
                break;
            case ConsoleKey.LeftArrow:
                if (x - 1 >= 0 && map[y, x - 1] != '#')
                {
                    x--;
                }
                break;
            case ConsoleKey.UpArrow:
                if (y - 1 >= 0 && map[y - 1, x] != '#')
                {
                    y--;
                }
                break;
            default:
                break;
        }

        if (map[y, x] == '.') {
            map[y, x] = ' ';
            score++;
        }

        Console.SetCursorPosition(x, y);
        Console.Write("@");
    }
}
