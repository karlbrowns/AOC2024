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

void P1()
{
    int result = 0;
    int index = 0;
    String data = "input.txt";
    List<string> input = read_input(data);
    char[] seps = { '=', ',', ' ' };
    int width = 101;
    int height = 103;
    List<List<int>> robots= new List<List<int>>();
    for (int i=0; i<input.Count; i++)
    {
        string[] nums = input[i].Split(seps, StringSplitOptions.RemoveEmptyEntries);
        robots.Add(new List<int>());
        robots[i].Add(int.Parse(nums[1]));
        robots[i].Add(int.Parse(nums[2]));
        robots[i].Add(int.Parse(nums[4]));
        robots[i].Add(int.Parse(nums[5]));
    }
    int[,] map = new int[width, height];
    for (int i=0; i<robots.Count; i++)
    {
        int x, y;
        x = robots[i][0];
        y = robots[i][1];
        int vx, vy;
        vx = robots[i][2];
        vy = robots[i][3];
        x += 100 * vx;
        x %= width;
        y += 100 * vy;
        y %= height;
        if (x<0) x+=width;
        if (y < 0) y += height;
        map[x, y] += 1;
    }
    for (int y = 0; y<height; y++)
    {
        for (int x = 0; x<width; x++)
        {
            if (map[x, y] == 0) Console.Write('.');
            else Console.Write(map[x, y]);
        }
        Console.WriteLine();
    }
    int[] regions = new int[4];
    int hx = (width >> 1) + 1;
    int hy = (height >> 1) + 1;
    for (int y=0; y<height>>1; y++)
    {
        for (int x = 0; x<width>>1; x++)
        {
            regions[0] += map[x, y];
            regions[1] += map[x + hx, y];
            regions[2] += map[x, y + hy];
            regions[3] += map[x + hx, y + hy];
        }
    }
    result = regions[0] * regions[1] * regions[2] * regions[3];
    Console.WriteLine(result);
    Console.ReadLine();
}

void P2()
{
    int result = 0;
    int index = 0;
    String data = "input.txt";
    List<string> input = read_input(data);
    char[] seps = { '=', ',', ' ' };
    int width = 101;
    int height = 103;
    List<List<int>> robots = new List<List<int>>();
    for (int i = 0; i < input.Count; i++)
    {
        string[] nums = input[i].Split(seps, StringSplitOptions.RemoveEmptyEntries);
        robots.Add(new List<int>());
        robots[i].Add(int.Parse(nums[1]));
        robots[i].Add(int.Parse(nums[2]));
        robots[i].Add(int.Parse(nums[4]));
        robots[i].Add(int.Parse(nums[5]));
    }
    while (true)
    {
        int max = 0;
        bool pause = false;
        int[,] map = new int[width, height];
        for (int i = 0; i < robots.Count; i++)
        {
            int x, y;
            x = robots[i][0];
            y = robots[i][1];
            int vx, vy;
            vx = robots[i][2];
            vy = robots[i][3];
            x += 1 * vx;
            x %= width;
            y += 1 * vy;
            y %= height;
            if (x < 0) x += width;
            if (y < 0) y += height;
            map[x, y] += 1;
            if (map[x, y] > max) max = map[x, y];
            robots[i][0] = x; robots[i][1] = y;
        }
        if (max == 1) pause = true;
        result++;
        if (pause)
        {
            for (int yy = 0; yy < height; yy++)
            {
                for (int xx = 0; xx < width; xx++)
                {
                    if (map[xx, yy] == 0) Console.Write('.');
                    else Console.Write(map[xx, yy]);
                }
                Console.WriteLine();
            }

            Console.WriteLine(result);
            Console.ReadLine();
        }
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
