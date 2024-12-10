using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Collections;
using System.Security.Cryptography;

List<string> read_input(string name)
{
    List<string> strings = new List<string>();
    foreach (string line in System.IO.File.ReadLines(name))
    {
        strings.Add(line);
    }
    return strings;
}

void find_trail(List<List<int>> map, List<int> start, List<int> ends)
{
    int x = start[1];
    int y = start[0];
    int height = map[y][x];
    int end = 0;
    if (height == 9)
    {
        end = y * map[0].Count + x;
        ends.Add(end);
        return;
    }
    if ((x+1 < map[y].Count) && (map[y][x+1]==height+1))
    {
        List<int> coords = new List<int>();
        coords.Add(y); coords.Add(x + 1);
        find_trail(map, coords, ends);
    }
    if ((y + 1 < map.Count) && (map[y+1][x] == height + 1))
    {
        List<int> coords = new List<int>();
        coords.Add(y+1); coords.Add(x);
        find_trail(map, coords, ends);
    }
    if ((x>0) && (map[y][x - 1] == height + 1))
    {
        List<int> coords = new List<int>();
        coords.Add(y); coords.Add(x - 1);
        find_trail(map, coords, ends);
    }
    if ((y>0) && (map[y-1][x] == height + 1))
    {
        List<int> coords = new List<int>();
        coords.Add(y-1); coords.Add(x);
        find_trail(map, coords, ends);
    }
    return;
}
void P1()
{
    int result = 0;
    int index = 0;
    String data = "input.txt";
    List<List<int>> map = new List<List<int>>();
    HashSet<List<int>> trailheads = new HashSet<List<int>>();
    List<string> input = read_input(data);
    for (int i=0; i<input.Count; i++)
    {
        map.Add(new List<int>());
        for (int j=0; j < input[0].Length; j++)
        {
            map[i].Add(int.Parse(input[i].Substring(j, 1)));
            if (map[i][j]==0)
            {
                List<int> coords = new List<int>();
                coords.Add(i); coords.Add(j);
                trailheads.Add(coords);
            }
        }
    }
    foreach (List<int> trailhead in trailheads)
    {
        HashSet<int> ends = new HashSet<int>();
        List<int> all_ends = new List<int>();
        find_trail(map, trailhead, all_ends);
        for (int i = 0; i < all_ends.Count; i++)
        {
            if (!ends.Contains(all_ends[i]))
            {
                ends.Add(all_ends[i]);
            }
        }
        result += ends.Count;
    }
    Console.WriteLine(result);
    Console.ReadLine();
}

void P2()
{
    int result = 0;
    int index = 0;
    String data = "input.txt";
    List<List<int>> map = new List<List<int>>();
    HashSet<List<int>> trailheads = new HashSet<List<int>>();
    List<string> input = read_input(data);
    for (int i = 0; i < input.Count; i++)
    {
        map.Add(new List<int>());
        for (int j = 0; j < input[0].Length; j++)
        {
            map[i].Add(int.Parse(input[i].Substring(j, 1)));
            if (map[i][j] == 0)
            {
                List<int> coords = new List<int>();
                coords.Add(i); coords.Add(j);
                trailheads.Add(coords);
            }
        }
    }
    foreach (List<int> trailhead in trailheads)
    {
        HashSet<int> ends = new HashSet<int>();
        List<int> all_ends = new List<int>();
        find_trail(map, trailhead, all_ends);
        for (int i = 0; i < all_ends.Count; i++)
        {
            if (!ends.Contains(all_ends[i]))
            {
                ends.Add(all_ends[i]);
            }
        }
        result += all_ends.Count;
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
