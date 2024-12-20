using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Runtime.InteropServices.JavaScript;

List<string> read_input(string name)
{
    List<string> strings = new List<string>();
    foreach (string line in System.IO.File.ReadLines(name))
    {
        strings.Add(line);
    }
    return strings;
}

void move(int x, int y, int[,] map, int dirn, int[,,] cost, int endx, int endy)
{
    if ((x==endx) && (y==endy)) return;
    //Console.WriteLine(x + "," + y);
    //print_map(map, cost, 15 , 15);

    int endscore = int.MaxValue;
    if ((cost[endx, endy, 0]>0) && (endscore > cost[endx, endy, 0])) endscore = cost[endx, endy, 0];
    if ((cost[endx, endy, 1]>0) && (endscore > cost[endx, endy, 1])) endscore = cost[endx, endy, 1];
    if ((cost[endx, endy, 2]>0) && (endscore > cost[endx, endy, 2])) endscore = cost[endx, endy, 2];
    if ((cost[endx, endy, 3]>0) && (endscore > cost[endx, endy, 3])) endscore = cost[endx, endy, 3];
    if (cost[x, y, dirn] > endscore) return;
    switch (dirn)
    {
        case 0:
            if (map[x, y - 1] == 0)
            {
                if (((cost[x, y - 1, 0] > 0) && (cost[x, y - 1, 0] > cost[x, y, dirn] + 1)) || (cost[x, y - 1, 0] == 0))
                {
                    cost[x, y - 1, 0] = cost[x, y, dirn] + 1;
                    move(x, y - 1, map, 0, cost, endx, endy);
                }
            }
            if (map[x + 1, y] == 0)
            {
                if (((cost[x + 1, y, 1] > 0) && (cost[x + 1, y, 1] > cost[x, y, dirn] + 1001)) || (cost[x + 1, y, 1] == 0))
                {
                    cost[x + 1, y, 1] = cost[x, y, dirn] + 1001;
                    move(x + 1, y, map, 1, cost, endx, endy);
                }
            }
            if (map[x, y + 1] == 0)
            {
            }
            if (map[x - 1, y] == 0)
            {
                if (((cost[x - 1, y, 3] > 0) && (cost[x - 1, y, 3] > cost[x, y, dirn] + 1001)) || (cost[x - 1, y, 3] == 0))
                {
                    cost[x - 1, y, 3] = cost[x, y, dirn] + 1001;
                    move(x - 1, y, map, 3, cost, endx, endy);
                }
            }
            break;
        case 1:
            if (map[x, y - 1] == 0)
            {
                if (((cost[x, y - 1, 0] > 0) && (cost[x, y - 1, 0] > cost[x, y, dirn] + 1001)) || (cost[x, y - 1, 0] == 0))
                {
                    cost[x, y - 1, 0] = cost[x, y, dirn] + 1001;
                    move(x, y - 1, map, 0, cost, endx, endy);
                }
            }
            if (map[x + 1, y] == 0)
            {
                if (((cost[x + 1, y, 1] > 0) && (cost[x + 1, y, 1] > cost[x, y, dirn] + 1)) || (cost[x + 1, y, 1] == 0))
                {
                    cost[x + 1, y, 1] = cost[x, y, dirn] + 1;
                    move(x + 1, y, map, 1, cost, endx, endy);
                }
            }
            if (map[x, y + 1] == 0)
            {
                if (((cost[x, y + 1, 2] > 0) && (cost[x, y + 1, 2] > cost[x, y, dirn] + 1001)) || (cost[x, y + 1, 2] == 0))
                {
                    cost[x, y + 1, 2] = cost[x, y, dirn] + 1001;
                    move(x, y + 1, map, 2, cost, endx, endy);
                }
            }
            if (map[x - 1, y] == 0)
            {
            }
            break;
        case 2:
            if (map[x, y - 1] == 0)
            {
            }
            if (map[x + 1, y] == 0)
            {
                if (((cost[x + 1, y, 1] > 0) && (cost[x + 1, y, 1] > cost[x, y, dirn] + 1001)) || (cost[x + 1, y, 1] == 0))
                {
                    cost[x + 1, y, 1] = cost[x, y, dirn] + 1001;
                    move(x + 1, y, map, 1, cost, endx, endy);
                }
            }
            if (map[x, y + 1] == 0)
            {
                if (((cost[x, y + 1, 2] > 0) && (cost[x, y + 1, 2] > cost[x, y, dirn] + 1)) || (cost[x, y + 1, 2] == 0))
                {
                    cost[x, y + 1, 2] = cost[x, y, dirn] + 1;
                    move(x, y + 1, map, 2, cost, endx, endy);
                }
            }
            if (map[x - 1, y] == 0)
            {
                if (((cost[x - 1, y, 3] > 0) && (cost[x - 1, y, 3] > cost[x, y, dirn] + 1001)) || (cost[x - 1, y, 3] == 0))
                {
                    cost[x - 1, y, 3] = cost[x, y, dirn] + 1001;
                    move(x - 1, y, map, 3, cost, endx, endy);
                }
            }
            break;
        case 3:
            if (map[x, y - 1] == 0)
            {
                if (((cost[x, y - 1, 0] > 0) && (cost[x, y - 1, 0] > cost[x, y, dirn] + 1001)) || (cost[x, y - 1, 0] == 0))
                {
                    cost[x, y - 1, 0] = cost[x, y, dirn] + 1001;
                    move(x, y - 1, map, 0, cost, endx, endy);
                }
            }
            if (map[x + 1, y] == 0)
            {
            }
            if (map[x, y + 1] == 0)
            {
                if (((cost[x, y + 1, 2] > 0) && (cost[x, y + 1, 2] > cost[x, y, dirn] + 1001)) || (cost[x, y + 1, 2] == 0))
                {
                    cost[x, y + 1, 2] = cost[x, y, dirn] + 1001;
                    move(x, y + 1, map, 2, cost, endx, endy);
                }
            }
            if (map[x - 1, y] == 0)
            {
                if (((cost[x - 1, y, 0] > 0) && (cost[x - 1, y, 3] > cost[x, y, dirn] + 1)) || (cost[x, y + 1, 3] == 0))
                {
                    cost[x - 1, y, 3] = cost[x, y, dirn] + 1;
                    move(x - 1, y, map, 3, cost, endx, endy);
                }
            }
            break;

    }
    return;
}
void print_map(int[,] map, int[,,] cost, int width, int height)
{
    
    for (int j = 0; j < height; j++)
    {
        for (int i = 0; i < width; i++)
        {
            if (map[i, j] == -1) Console.Write("#####");
            else
            {
                int c = int.MaxValue;
                if (cost[i, j, 0] > 0) c = cost[i, j, 0];
                if ((cost[i, j, 1] > 0) && (c > cost[i, j, 1])) c = cost[i, j, 1];
                if ((cost[i, j, 2] > 0) && (c > cost[i, j, 2])) c = cost[i, j, 2];
                if ((cost[i, j, 3] > 0) && (c > cost[i, j, 3])) c = cost[i, j, 3];
                if (c == int.MaxValue)
                {
                    Console.Write(".....");
                }
                else 
                    Console.Write(c.ToString().PadLeft(5, '0'));
            }
        }
        Console.WriteLine();
    }
    //Console.ReadLine();

}
//429324 too high
//94468 too high
//94467 too high
void P1()
{
    int result = 0;
    int index = 0;
    int width = 141;
    int height = 141;
    int startx=0, starty=0, endx=0, endy=0;
    String data = "input.txt";
    List<string> input = read_input(data);
    int[,] map = new int[width, height];
    for (int i=0; i<input.Count; i++)
    {
        for (int j=0; j < input[i].Length; j++)
        {
            if (input[i][j] == '#') map[j, i] = -1;
            if (input[i][j] == '.') map[j, i] = 0;
            if (input[i][j] == 'E')
            {
                map[j, i] = 0;
                endx = j; endy = i;
            }
            if (input[i][j] == 'S')
            {
                map[j, i] = 0;
                startx = j; starty = i;
            }
        }
    }
    int x = startx;
    int y = starty;
    int dirn = 1;   // dirn 0=N, 1=E, 2=S, 3=W
    int[,,] cost = new int[width, height, 4];
    for (int i=0; i<width; i++)
    {
        for (int j=0; j<height; j++) { 
            for (int k=0; k<4; k++)
            {
                cost[i, j, k] = 0;
            }
        }
    }
    move(x, y, map, dirn, cost, endx, endy);
    result = int.MaxValue;
    print_map(map, cost, width, height);
    for (int i=0; i<3; i++)
    {
        if ((cost[endx, endy, i]>0) && (cost[endx, endy, i] < result)) result = cost[endx, endy, i];
    }
    Console.WriteLine(result);
    Console.ReadLine();
}
//11048
void P2()
{
    int result = 0;
    int index = 0;
    String data = "input.txt";
    List<string> input = read_input(data);
    for (int i = 0; i < input.Count; i++)
    {
    }
    Console.WriteLine(result);
    Console.ReadLine();
}

Stopwatch t = new Stopwatch();
t.Start();
P1();
t.Stop();
Console.WriteLine("P1 took " + t.ElapsedMilliseconds/1000.0 + " seconds");
t.Restart();
P2();
t.Stop();
Console.WriteLine("P2 took " + t.ElapsedMilliseconds / 1000.0 + " seconds");
