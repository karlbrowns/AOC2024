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

void moveup(int startx, int starty, int[,] map, out int endx, out int endy)
{
    endx = startx; endy = starty;
    int y = starty;
    if (map[startx, y - 1] == -1) return;
    if (map[startx,y - 1] == 0)
    {
        endy = y - 1;
        return;
    }
    int block = 1;
    if (map[startx,y-block]==1) {
        while (map[startx, y - block] == 1) block++;
        if (map[startx, y - block] == -1) return;
        if (map[startx, y - block] == 0)
        {
            endy = y - 1;
            for (int steps = block; steps>0; steps--)
            {
                map[startx, y - steps] = map[startx, y - steps + 1];
            }
            return;
        }
    }
}
void movedown(int startx, int starty, int[,] map, out int endx, out int endy)
{
    endx = startx; endy = starty;
    int y = starty;
    if (map[startx, y + 1] == -1) return;
    if (map[startx, y + 1] == 0)
    {
        endy = y + 1;
        return;
    }
    int block = 1;
    if (map[startx, y + block] == 1)
    {
        while (map[startx, y + block] == 1) block++;
        if (map[startx, y + block] == -1) return;
        if (map[startx, y + block] == 0)
        {
            endy = y + 1;
            for (int steps = block; steps > 0; steps--)
            {
                map[startx, y + steps] = map[startx, y + steps - 1];
            }
            return;
        }
    }
}
void moveright(int startx, int starty, int[,] map, out int endx, out int endy)
{
    endx = startx; endy = starty;
    int x = startx;
    if (map[x+1, starty] == -1) return;
    if (map[x+1, starty] == 0)
    {
        endx = x + 1;
        return;
    }
    int block = 1;
    if (map[x + block, starty] == 1)
    {
        while (map[x + block, starty] == 1) block++;
        if (map[x + block, starty] == -1) return;
        if (map[x + block, starty] == 0)
        {
            endx = x + 1;
            for (int steps = block; steps > 0; steps--)
            {
                map[x + steps, starty] = map[x + steps - 1, starty];
            }
            return;
        }
    }
}
void moveleft(int startx, int starty, int[,] map, out int endx, out int endy)
{
    endx = startx; endy = starty;
    int x = startx;
    if (map[x - 1, starty] == -1) return;
    if (map[x - 1, starty] == 0)
    {
        endx = x - 1;
        return;
    }
    int block = 1;
    if (map[x - block, starty] == 1)
    {
        while (map[x - block, starty] == 1) block++;
        if (map[x - block, starty] == -1) return;
        if (map[x - block, starty] == 0)
        {
            endx = x - 1;
            for (int steps = block; steps > 0; steps--)
            {
                map[x - steps, starty] = map[x - steps + 1, starty];
            }
            return;
        }
    }
}
void P1()
{
    int result = 0;
    int index = 0;
    int width = 50;
    int height = 50;
    String data = "input.txt";
    List<string> input = read_input(data);
    int[,] map = new int[width, height];
    int startx=0, starty=0;
    string instructions = "";
    for (int i=0; i<input.Count; i++)
    {
        if (input[i].Length == 0)
        {
            index = 1;
            continue;
        }
        switch (index)
        {
            case 0:
                for (int x = 0; x < input[i].Length; x++)
                {
                    if (input[i][x] == '#') map[x, i] = -1;
                    if (input[i][x] == '@')
                    {
                        startx = x;
                        starty = i;
                        map[x, i] = 0;
                    }
                    if (input[i][x] == 'O') map[x, i] = 1;
                    if (input[i][x] == '.') map[x, i] = 0;
                }
                break;
            case 1:
                instructions+=input[i];
                break;
        }
    }
    for (int y = 0; y< height; y++)
    {
        for (int x = 0; x<width; x++)
        {
            switch(map[x,y])
            {
                case -1: Console.Write('#'); break;
                case 0: Console.Write('.'); break;
                case 1: Console.Write('O'); break;
                case 10: Console.Write('@'); break;
            }
        }
        Console.WriteLine();
    }
    int endx = startx, endy = starty;
    for (int i=0; i<instructions.Length; i++)
    {
        switch (instructions[i])
        {
            case '^':
                moveup(startx, starty, map, out endx, out endy);
                break;
            case '>':
                moveright(startx, starty, map, out endx, out endy);
                break;
            case 'v':
                movedown(startx, starty, map, out endx, out endy);
                break;
            case '<':
                moveleft(startx, starty, map, out endx, out endy);
                break;
        }
        startx = endx; starty = endy;
    }
    for (int y = 0; y < height; y++)
    {
        for (int x = 0; x < width; x++)
        {
            switch (map[x, y])
            {
                case -1: Console.Write('#'); break;
                case 0: Console.Write('.'); break;
                case 1: Console.Write('O'); result += 100 * y + x; break;
                case 10: Console.Write('@'); break;
            }
        }
        Console.WriteLine();
    }
    Console.WriteLine(result);
    Console.ReadLine();
}

void P2()
{
    int result = 0;
    int index = 0;
    int width = 50;
    int height = 50;
    String data = "input.txt";
    List<string> input = read_input(data);
    int[,] map = new int[width, height];
    int startx = 0, starty = 0;
    string instructions = "";
    for (int i = 0; i < input.Count; i++)
    {
        if (input[i].Length == 0)
        {
            index = 1;
            continue;
        }
        switch (index)
        {
            case 0:
                for (int x = 0; x < input[i].Length; x++)
                {
                    if (input[i][x] == '#') map[x, i] = -1;
                    if (input[i][x] == '@')
                    {
                        startx = x;
                        starty = i;
                        map[x, i] = 0;
                    }
                    if (input[i][x] == 'O') map[x, i] = 1;
                    if (input[i][x] == '.') map[x, i] = 0;
                }
                break;
            case 1:
                instructions += input[i];
                break;
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
