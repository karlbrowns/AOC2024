using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Runtime.CompilerServices;

List<string> read_input(string name)
{
    List<string> strings = new List<string>();
    foreach (string line in System.IO.File.ReadLines(name))
    {
        strings.Add(line);
    }
    return strings;
}

void print_map(List<List<int>> map)
{
    for (int i=0; i<map.Count; i++)
    {
        for (int j=0; j < map[i].Count; j++)
        {
            if (map[i][j] == -1) Console.Write('#');
            if (map[i][j] == 0) Console.Write('.');
            if (map[i][j] == 10) Console.Write('X');
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}
void print_map2(List<List<int>> map)
{
    for (int i = 0; i < map.Count; i++)
    {
        for (int j = 0; j < map[i].Count; j++)
        {
            if (map[i][j] == -1) Console.Write('#');
            else if (map[i][j] == 0) Console.Write('.');
            else if ((map[i][j] & 15) == 15) Console.Write('+');
            else if ((map[i][j] & 3) == 3) Console.Write("+");
            else if ((map[i][j] & 12) == 12) Console.Write("+");
            else if ((map[i][j] & 6) == 6) Console.Write("+");
            else if ((map[i][j] & 9) == 9) Console.Write("+");
            else if ((map[i][j] & 5) > 0) Console.Write('|');
            else if ((map[i][j] & 10) > 0) Console.Write('-');
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}
void clear_map(List<List<int>> map)
{
    for (int i = 0; i < map.Count; i++)
    {
        for (int j = 0; j < map[i].Count; j++)
        {
            if (map[i][j] > 0) map[i][j] = 0;
        }
    }
}
// not 4579
void P1()
{
    int result = 0;
    int index = 0;
    String data = "input2.txt";
    int gx = 0; int gy = 0;
    List<List<int>> map = new List<List<int>>();
    List<string> input = read_input(data);
    for (int i=0; i<input.Count; i++)
    {
        map.Add(new List<int>());
        for(int j=0; j < input[i].Length; j++)
        {
            switch (input[i][j])
            {
                case '.':
                    map[i].Add(0);
                    break;
                case '#':
                    map[i].Add(-1);
                    break;
                case '^':
                    map[i].Add(1);
                    gx = j;
                    gy = i;
                    break;
                default:
                    map[i].Add(0);
                    break;
            }
        }
    }
    int dirn = 1;   // 1 = up, 2 = right, 3 = down, 4 = left
    while ((gx>=0) && (gy>=0) && (gx < map[0].Count) && (gy < map.Count))
    {
        switch (dirn)
        {
            case 1:
                if ((gy > 0) && (map[gy - 1][gx] < 0))
                {
                    map[gy][gx] = 10;
                    dirn++;
                    //print_map(map);
                }
                else
                {
                    map[gy][gx] = 10;
                    gy--;
                }
                break;
            case 2:
                if ((gx+1 < map[0].Count) && (map[gy][gx+1]<0))
                {
                    map[gy][gx] = 10;
                    dirn++;
                    //print_map(map);
                }
                else
                {
                    map[gy][gx] = 10;
                    gx++;
                }
                break;
            case 3:
                if ((gy+1 < map.Count) && (map[gy + 1][gx]<0))
                {
                    map[gy][gx] = 10;
                    dirn++;
                    //print_map(map);
                }
                else
                {
                    map[gy][gx] = 10;
                    gy++;
                }
                break;
            case 4:
                if ((gx>0) && (map[gy][gx-1]<0))
                {
                    map[gy][gx] = 10;
                    dirn = 1;
                    //print_map(map);
                }
                else
                {
                    map[gy][gx] = 10;
                    gx--;
                }
                break;
                
        }

    }
//    print_map(map);
    Console.WriteLine(gy + "," + gx);
    for (int i=0; i<map.Count; i++)
    {
        for (int j=0; j < map[i].Count; j++)
        {
            if (map[i][j] == 10) result++;
        }
    }
    Console.WriteLine(result);
    Console.ReadLine();
}

void P2()
{
    int result = 0;
    int index = 0;
    String data = "input2.txt";
    int gx = 0; int gy = 0;
    List<List<int>> map = new List<List<int>>();
    List<string> input = read_input(data);
    for (int i = 0; i < input.Count; i++)
    {
        map.Add(new List<int>());
        for (int j = 0; j < input[i].Length; j++)
        {
            switch (input[i][j])
            {
                case '.':
                    map[i].Add(0);
                    break;
                case '#':
                    map[i].Add(-1);
                    break;
                case '^':
                    map[i].Add(0);
                    gx = j;
                    gy = i;
                    break;
                default:
                    map[i].Add(0);
                    break;
            }
        }
    }
    int dirn = 1;   // 1 = up, 2 = right, 4 = down, 8 = left
    int startx = gx, starty = gy;
    while ((gx >= 0) && (gy >= 0) && (gx < map[0].Count) && (gy < map.Count))
    {
        switch (dirn)
        {
            case 1:
                if ((gy > 0) && (map[gy - 1][gx] < 0))
                {
                    map[gy][gx] |= dirn;
                    dirn*=2;
                    //print_map(map);
                }
                else
                {
                    map[gy][gx] |= dirn;
                    gy--;
                }
                break;
            case 2:
                if ((gx + 1 < map[0].Count) && (map[gy][gx + 1] < 0))
                {
                    map[gy][gx] |= dirn;
                    dirn*=2;
                    //print_map(map);
                }
                else
                {
                    map[gy][gx] |= dirn;
                    gx++;
                }
                break;
            case 4:
                if ((gy + 1 < map.Count) && (map[gy + 1][gx] < 0))
                {
                    map[gy][gx] |= dirn;
                    dirn*=2;
                    //print_map(map);
                }
                else
                {
                    map[gy][gx] |= dirn;
                    gy++;
                }
                break;
            case 8:
                if ((gx > 0) && (map[gy][gx - 1] < 0))
                {
                    map[gy][gx] |= dirn;
                    dirn = 1;
                    //print_map(map);
                }
                else
                {
                    map[gy][gx] |= dirn;
                    gx--;
                }
                break;

        }

    }
    print_map2(map);
    List<List<int>> map2 = new List<List<int>>();
    for (int i=0; i<map.Count; i++)
    {
        map2.Add(new List<int>());
        for (int j=0; j < map[0].Count; j++)
        {
            map2[i].Add(map[i][j]);
        }
    }
    int temp;
    for (int i=0; i<map.Count; i++)
    {
        for (int j=0; j < map[0].Count; j++)
        {
            if ((i == starty) && (j == startx)) continue;
            if (map[i][j]>0)
            {
                temp = map[i][j];
                map2[i][j] = -1;
                clear_map(map2);
                gx = startx; gy = starty;
                dirn = 1;
                int nextdirn;
                if (dirn == 1) nextdirn = 2;
                else if (dirn == 2) nextdirn = 4;
                else if (dirn == 4) nextdirn = 8;
                else nextdirn = 1;
                while ((gx >= 0) && (gy >= 0) && (gx < map[0].Count) && (gy < map.Count))
                {
                    //int test = dirn | nextdirn;
                    if ((map[gy][gx] & dirn) == dirn)
                    {
                        //print_map2(map);
                        map2[i][j] = temp;
                        result++;
                        Console.WriteLine(i + "," + j);
                        break;
                    }
                    switch (dirn)
                    {
                        case 1:
                            if ((gy > 0) && (map2[gy - 1][gx] < 0))
                            {
                                map2[gy][gx] |= dirn;
                                dirn *= 2;
                                //print_map(map);
                            }
                            else
                            {
                                map2[gy][gx] |= dirn;
                                gy--;
                            }
                            break;
                        case 2:
                            if ((gx + 1 < map[0].Count) && (map2[gy][gx + 1] < 0))
                            {
                                map2[gy][gx] |= dirn;
                                dirn *= 2;
                                //print_map(map);
                            }
                            else
                            {
                                map2[gy][gx] |= dirn;
                                gx++;
                            }
                            break;
                        case 4:
                            if ((gy + 1 < map.Count) && (map2[gy + 1][gx] < 0))
                            {
                                map2[gy][gx] |= dirn;
                                dirn *= 2;
                                //print_map(map);
                            }
                            else
                            {
                                map2[gy][gx] |= dirn;
                                gy++;
                            }
                            break;
                        case 8:
                            if ((gx > 0) && (map2[gy][gx - 1] < 0))
                            {
                                map2[gy][gx] |= dirn;
                                dirn = 1;
                                //print_map(map);
                            }
                            else
                            {
                                map2[gy][gx] |= dirn;
                                gx--;
                            }
                            break;

                    }

                }

            }
        }
    }
    Console.WriteLine(result);
    //result = 0;
    //for (int i = 0; i < map.Count; i++)
    //{
    //    for (int j = 0; j < map[i].Count; j++)
    //    {
    //        if (map[i][j] > 0) result++;
    //    }
    //}
    //Console.WriteLine(result);
    Console.ReadLine();
}
// not 643 (too low)
// not 417 (also too low)

Stopwatch t = new Stopwatch();
t.Start();
P1();
t.Stop();
Console.WriteLine("P1 took " + t.ElapsedMilliseconds/1000.0 + " seconds");
t.Restart();
P2();
t.Stop();
Console.WriteLine("P2 took " + t.ElapsedMilliseconds / 1000.0 + " seconds");
