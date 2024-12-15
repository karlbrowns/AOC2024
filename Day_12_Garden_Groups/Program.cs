using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Collections;

List<string> read_input(string name)
{
    List<string> strings = new List<string>();
    foreach (string line in System.IO.File.ReadLines(name))
    {
        strings.Add(line);
    }
    return strings;
}

int build_region(List<List<int>> map, List<List<int>> region, int i, int j)
{
    int area = 1;
    int region_type = map[i][j];
    List<int> coords = new List<int>();
    coords.Add(i); coords.Add(j);
    region.Add(coords);
    map[i][j] = -1;
    if ((j + 1 < map[i].Count) && (map[i][j + 1] == region_type))
    {
        area += build_region(map, region, i, j + 1);
    }
    if ((j - 1 >=0 ) && (map[i][j - 1] == region_type))
    {
        area += build_region(map, region, i, j - 1);
    }
    if ((i + 1 < map.Count) && (map[i + 1][j] == region_type))
    {
        area += build_region(map, region, i + 1, j);
    }
    if ((i - 1 >= 0) && (map[i - 1][j]==region_type))
    {
        area += build_region(map, region, i - 1, j);
    }
    return area;
}
void P1()
{
    int result = 0;
    int index = 0;
    String data = "input.txt";
    List<string> input = read_input(data);
    List<List<int>> map = new List<List<int>>();
    for (int i=0; i<input.Count; i++)
    {
        map.Add(new List<int>());
        for (int j=0; j < input[i].Length ; j++)
        {
            map[i].Add(input[i][j]);
        }
    }
    List<List<List<int>>> regions = new List<List<List<int>>>();
    Dictionary<int, int> areas = new Dictionary<int, int>();
    Dictionary<int, int> peris = new Dictionary<int, int>();
    int num_regions = 0;
    int area;
    for (int i=0; i<map.Count; i++)
    {
        for (int j=0; j < map[i].Count; j++)
        {
            if (map[i][j]>0)
            {
                regions.Add(new List<List<int>>());
                area = build_region(map, regions[num_regions], i, j);
                areas.Add(num_regions, area);
                num_regions++;
            }
        }
    }
    for (int i=0; i<regions.Count; i++)
    {
        int height = map.Count+2; int width = map[0].Count+2;
        int[,] perimeter = new int[height+2,width+2];
        for (int j=0; j < regions[i].Count; j++)
        {
            perimeter[regions[i][j][0]+1,regions[i][j][1]+1] = 1;
        }
        int peri = 0;
        for (int y=0; y<height; y++)
        {
            for (int x=0; x<width; x++)
            {
                if (perimeter[y,x]==1)
                {
                    if (perimeter[y - 1, x] != 1)
                    {
                        perimeter[y - 1, x]--;
                        peri++;
                    }
                    if (perimeter[y + 1, x] != 1)
                    {
                        perimeter[y + 1, x]--;
                        peri++;
                    }
                    if (perimeter[y, x - 1] != 1)
                    {
                        perimeter[y, x - 1]--;
                        peri++;
                    }
                    if (perimeter[y, x + 1] != 1)
                    {
                        perimeter[y, x + 1]--;
                        peri++;
                    }
                }
            }
        }
        peris.Add(i, peri);
        //for (int y =0; y<height; y++)
        //{
        //    for (int x = 0; x<width; x++)
        //    {
        //        if (perimeter[y, x] == -1) Console.Write("#");
        //        else if (perimeter[y, x] == 1) Console.Write("A");
        //        else Console.Write('.');
        //    }
        //    Console.WriteLine();
        //}
    }

    for (int i=0; i<areas.Count; i++)
    {
        //Console.WriteLine(areas[i] + ":" + peris[i]);
        result += areas[i] * peris[i];
    }
    Console.WriteLine(result);
    Console.ReadLine();
}


void P2()
{
    int result = 0;
    String data = "input.txt";
    List<string> input = read_input(data);
    List<List<int>> map = new List<List<int>>();
    for (int i = 0; i < input.Count; i++)
    {
        map.Add(new List<int>());
        for (int j = 0; j < input[i].Length; j++)
        {
            map[i].Add(input[i][j]);
        }
    }
    List<List<List<int>>> regions = new List<List<List<int>>>();
    Dictionary<int, int> areas = new Dictionary<int, int>();
    Dictionary<int, int> peris = new Dictionary<int, int>();
    Dictionary<int, int> edges = new Dictionary<int, int>();
    int num_regions = 0;
    int area;
    for (int i = 0; i < map.Count; i++)
    {
        for (int j = 0; j < map[i].Count; j++)
        {
            if (map[i][j] > 0)
            {
                regions.Add(new List<List<int>>());
                area = build_region(map, regions[num_regions], i, j);
                areas.Add(num_regions, area);
                num_regions++;
            }
        }
    }
    for (int i = 0; i < regions.Count; i++)
    {
        int height = map.Count + 2; int width = map[0].Count + 2;
        int[,] perimeter = new int[height + 2, width + 2];
        for (int j = 0; j < regions[i].Count; j++)
        {
            perimeter[regions[i][j][0] + 1, regions[i][j][1] + 1] = 1;
        }
        int corner = 0;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (perimeter[y, x] == 1)
                {
                    if ((perimeter[y - 1, x] == 0) && (perimeter[y, x - 1] == 0)) corner++;
                    if ((perimeter[y, x - 1] == 0) && (perimeter[y + 1, x] == 0)) corner++;
                    if ((perimeter[y + 1, x] == 0) && (perimeter[y, x + 1] == 0)) corner++;
                    if ((perimeter[y, x + 1] == 0) && (perimeter[y - 1, x] == 0)) corner++;

                    if ((perimeter[y - 1, x] == 1) && (perimeter[y, x + 1] == 1) && (perimeter[y - 1, x + 1] == 0)) corner++;
                    if ((perimeter[y, x + 1] == 1) && (perimeter[y + 1, x] == 1) && (perimeter[y + 1, x + 1] == 0)) corner++;
                    if ((perimeter[y + 1, x] == 1) && (perimeter[y, x - 1] == 1) && (perimeter[y + 1, x - 1] == 0)) corner++;
                    if ((perimeter[y, x - 1] == 1) && (perimeter[y - 1, x] == 1) && (perimeter[y - 1, x - 1] == 0)) corner++;

                    //Console.Write('#');
                }
                //else Console.Write('.');
            }
  //          Console.WriteLine();
        }
        edges.Add(i, corner);
        //peris.Add(i, peri);
        //for (int y =0; y<height; y++)
        //{
        //    for (int x = 0; x<width; x++)
        //    {
        //        if (perimeter[y, x] == -1) Console.Write("#");
        //        else if (perimeter[y, x] == 1) Console.Write("A");
        //        else Console.Write('.');
        //    }
        //    Console.WriteLine();
        //}
    }
    for (int i = 0; i < areas.Count; i++)
    {
        Console.WriteLine(areas[i] + ":" + edges[i]);
        result += areas[i] * edges[i];
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
