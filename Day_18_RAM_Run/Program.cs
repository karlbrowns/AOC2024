using System.Text.RegularExpressions;
using System.Diagnostics;

List<string> read_input(string name)
{
    List<string> strings = new List<string>();
    foreach (string line in System.IO.File.ReadLines(name))
    {
        strings.Add(line);
    }
    return strings;
}

void print_map(int[,] map, int w, int h)
{
    for (int y = 0; y<h; y++)
    {
        for (int x = 0; x<w; x++)
        {
            if (map[x, y] == 0) Console.Write('.');
            if (map[x, y] == -1) Console.Write('#');
            if (map[x, y] > 0) Console.Write('O');
        }
        Console.WriteLine();
    }
}
void find_path(int[,] map, int x, int y, int endx, int endy, int w, int h)
{
    if ((x == endx) && (y == endy)) return;
    if ((x+1 < w) && ((map[x+1,y]==0) || (map[x+1,y] > map[x,y]+1)))
    {
        map[x + 1, y] = map[x, y] + 1;
        find_path(map, x + 1, y, endx, endy, w, h);
    }
    if ((y+1 < h) && ((map[x, y+1]==0) || (map[x,y+1] > map[x,y]+1)))
    {
        map[x, y + 1] = map[x, y] + 1;
        find_path(map, x, y + 1, endx, endy, w, h);
    }
    if ((x - 1 >= 0) && ((map[x - 1, y] == 0) || (map[x - 1, y] > map[x, y] + 1)))
    {
        map[x - 1, y] = map[x, y] + 1;
        find_path(map, x - 1, y, endx, endy, w, h);
    }
    if ((y - 1 >= 0) && ((map[x, y - 1] == 0) || (map[x, y - 1] > map[x, y] + 1)))
    {
        map[x, y - 1] = map[x, y] + 1;
        find_path(map, x, y - 1, endx, endy, w, h);
    }
}

void P1()
{
    int result = 0;
    int index = 0;
    int width = 71;
    int height = 71;
    String data = "input.txt";
    List<string> input = read_input(data);
    int[,] map = new int[width, height];
    for (int i=0; i<1024; i++)
    {
        string[] nums = input[i].Split(',', StringSplitOptions.RemoveEmptyEntries);
        int x = int.Parse(nums[0]);
        int y = int.Parse(nums[1]);
        map[x, y] = -1;
    }
    int endx = width - 1, endy = height - 1 ;
    map[0, 0] = 1;
    find_path(map, 0, 0, endx, endy, width, height);
    print_map(map, width, height);
    result = map[endx, endy] - 1;
    Console.WriteLine(result);
    Console.ReadLine();
}

void P2()
{
    int result = 0;
    int index = 0;
    int width = 71;
    int height = 71;
    String data = "input.txt";
    List<string> input = read_input(data);
    int[,] map = new int[width, height];
    for (int i = 0; i < 1024; i++)
    {
        string[] nums = input[i].Split(',', StringSplitOptions.RemoveEmptyEntries);
        int x = int.Parse(nums[0]);
        int y = int.Parse(nums[1]);
        map[x, y] = -1;
    }
    for (int i = 1024; i < input.Count; i++)
    {
        string[] nums = input[i].Split(',', StringSplitOptions.RemoveEmptyEntries);
        int x = int.Parse(nums[0]);
        int y = int.Parse(nums[1]);
        map[x, y] = -1;
        for (x = 0; x<width; x++)
        {
            for (y = 0; y<height; y++)
            {
                if (map[x, y] > 0) map[x, y] = 0;
            }
        }
        int endx = width - 1, endy = height - 1;
        map[0, 0] = 1;
        find_path(map, 0, 0, endx, endy, width, height);
        if (map[endx, endy] == 0)
        {
            Console.Write(nums[0] + ',' + nums[1]);
            break;
        }
    }
    print_map(map, width, height);
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
