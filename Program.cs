using System;
using System.IO;
using System.Threading;

class Program
{
    static int mx1, my1, mx2, my2, mx3, my3, mx4, my4;
    static int flow1 = -1, flow2 = 1, flow3 = -1, flow4 = 1;

    static int x = 1;
    static int y = 1;
    static int score = 0;
    static ConsoleKey directionPacman = ConsoleKey.RightArrow;

    static void Main()
    {

        Console.CursorVisible = false;
        var map = convertMapToArray();

        new Thread(() => {
            while(true) {
                directionPacman = Console.ReadKey().Key;
            }

        }).Start();

        new Thread(()=>{
            while(true) {
                moveMonstrs(ref mx1, ref my1, ref flow1, map);
                moveMonstrs(ref mx2, ref my2, ref flow2, map);
                moveMonstrs(ref mx3, ref my3, ref flow3, map);
                moveMonstrs(ref mx4, ref my4, ref flow4, map);
                drawMap(map);
                Console.SetCursorPosition(map.GetLength(1)+4, 0);
                Console.Write($"Score: {score}");


                if (checkCollision(map)) {
                    Environment.Exit(0);
                };

                Thread.Sleep(400);
            }
            
        }).Start();


        while (true)
        {
            drawPackman(ref x, ref y, map);
            drawMap(map);

            Console.SetCursorPosition(map.GetLength(1)+4, 0);
            Console.Write($"Score: {score}");

            if (score>=22){
                Environment.Exit(0);
            }

            if (checkCollision(map)) {
                Environment.Exit(0);
            };

            Thread.Sleep(200);
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

        mx1 = tempArray.GetLength(1)-2;
        my1 = 1;
        
        mx2 = 1;
        my2 = 3;
        
        mx3 = tempArray.GetLength(1)-2;;
        my3 = 5;

        mx4 = 1;
        my4 = 7;

        
        return tempArray;
    }

    static void drawMap(char[,] map)
    {
        Console.Clear();

        for (var i = 0; i < map.GetLength(0); i++)
        {
            for (var j = 0; j < map.GetLength(1); j++)
            {
                Console.Write(map[i, j]);
            }
            Console.WriteLine();
        }
    }

    static void drawPackman(ref int x, ref int y, char[,] map)
    {
        switch (directionPacman)
        {
            case ConsoleKey.RightArrow:
                if (x + 1 < map.GetLength(1) && map[y, x + 1] != '#')
                {
                    map[y, x] = ' ';
                    x++;
                }
                break;
            case ConsoleKey.DownArrow:
                if (y + 1 < map.GetLength(0) && map[y + 1, x] != '#')
                {
                    map[y, x] = ' ';
                    y++;
                }
                break;
            case ConsoleKey.LeftArrow:
                if (x - 1 >= 0 && map[y, x - 1] != '#')
                {
                    map[y, x] = ' ';
                    x--;
                }
                break;
            case ConsoleKey.UpArrow:
                if (y - 1 >= 0 && map[y - 1, x] != '#')
                {
                    map[y, x] = ' ';
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

        map[y, x] = '@';
    
    }

    static void moveMonstrs(ref int mx, ref int my, ref int flow, char[,] map) {
        var symbl = '0';
        if (map[my,mx] == '9'){
            map[my,mx] = '.';
        } else {
            map[my,mx] = ' ';
        }

        if(mx > 1 && mx < map.GetLength(1)-2) {
            if (flow == -1) {
                mx--;
            } else {
                mx++;
            }
        } else if (mx <=1) {
            flow = 1;
            mx++;
        } else if (mx >= map.GetLength(1)-2) {
            flow = -1;
            mx--;
        }

        if (map[my,mx] == '.') {
            symbl = '9';
        }

        map[my,mx] = symbl;

    }

    static bool checkCollision(char[,] map) {
        return (y == my1 && x == mx1 || y == my2 && x == mx2 || y == my3 && x == mx3 || y == my4 && x == mx4);
    }
}
