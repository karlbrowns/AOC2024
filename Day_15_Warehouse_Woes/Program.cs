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

int print_map(int[,] map, int width, int height)
{
    int result = 0;
    for (int y = 0; y < height; y++)
    {
        for (int x = 0; x < width; x++)
        {
            switch (map[x, y])
            {
                case -1: Console.Write('#'); break;
                case 0: Console.Write('.'); break;
                case 1: Console.Write('['); result += 100 * y + x; break;
                case 2: Console.Write(']'); break;
                case 10: Console.Write('@'); break;
            }
        }
        Console.WriteLine();
    }
    return result;
}
bool testup(int startx, int starty, int[,] map)
{
    bool result = true;
    if (map[startx, starty-1] == -1) return false;
    if (map[startx, starty - 1] == 0)
    {
        if (map[startx, starty] <= 2) map[startx, starty] += 4;
        return true;
    }
    if ((map[startx, starty-1] & 3) == 1)
    {
        result = testup(startx, starty - 1, map);
        if (result) result = testup(startx + 1, starty - 1, map);
        if (result)
        {
            if (map[startx, starty] <= 2) map[startx, starty] += 4;
        }
    }
    if ((map[startx, starty-1] & 3) == 2)
    {
        result = testup(startx, starty - 1, map);
        if (result) result = testup(startx - 1, starty - 1, map);
        if (result)
        {
            if (map[startx, starty] <= 2) map[startx, starty] += 4;
        }
    }
    return result;
}
bool testdown(int startx, int starty, int[,] map)
{
    bool result = true;
    if (map[startx, starty + 1] == -1) return false;
    if (map[startx, starty + 1] == 0)
    {
        if (map[startx, starty] <= 2) map[startx, starty] += 4;
        return true;
    }
    if ((map[startx, starty + 1] & 3) == 1)
    {
        result = testdown(startx, starty + 1, map);
        if (result) result = testdown(startx + 1, starty + 1, map);
        if (result)
        {
            if (map[startx,starty]<=2) map[startx, starty] += 4;
        }
    }
    if ((map[startx, starty + 1] & 3) == 2)
    {
        result = testdown(startx, starty + 1, map);
        if (result) result = testdown(startx - 1, starty + 1, map);
        if (result)
        {
            if (map[startx, starty] <= 2) map[startx, starty] += 4;
        }
    }
    return result;
}
bool moveup2(int startx, int starty, int[,] map, int width, int height)
{
    int y = starty;
    if (map[startx, y - 1] == -1) return false;
    if (map[startx, y - 1] == 0)
    {
        return true;
    }
    int block = 1;
    bool can_move;
    can_move = testup(startx, y - block, map);
    if (can_move)
    {
        if ((map[startx, y - 1] & 3) == 1) can_move = testup(startx + 1, y - block, map);
        if ((map[startx, y - 1] & 3) == 2) can_move = testup(startx - 1, y - block, map);
    }
    return can_move;
    
}
bool movedown2(int startx, int starty, int[,] map, int width, int height)
{
    int y = starty;
    if (map[startx, y + 1] == -1) return false;
    if (map[startx, y + 1] == 0)
    {
        return true;
    }
    int block = 1;
    bool can_move;
    can_move = testdown(startx, y + block, map);
    if (can_move)
    {
        if ((map[startx, y + 1] & 3) == 1) can_move = testdown(startx + 1, y + block, map);
        if ((map[startx, y + 1] & 3) == 2) can_move = testdown(startx - 1, y + block, map);
    }
    return can_move;
}
void moveright2(int startx, int starty, int[,] map, out int endx, out int endy)
{
    endx = startx; endy = starty;
    int x = startx;
    if (map[x + 1, starty] == -1) return;
    if (map[x + 1, starty] == 0)
    {
        endx = x + 1;
        return;
    }
    int block = 1;
    if (map[x + block, starty] >= 1)
    {
        while (map[x + block, starty] >= 1) block++;
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
void moveleft2(int startx, int starty, int[,] map, out int endx, out int endy)
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
    if (map[x - block, starty] >= 1)
    {
        while (map[x - block, starty] >= 1) block++;
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
void P2()
{
    int result = 0;
    int index = 0;
    int width = 50;
    int height = 50;
    String data = "input.txt";
    List<string> input = read_input(data);
    int[,] map = new int[width*2, height];
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
                    if (input[i][x] == '#') { map[x<<1, i] = -1; map[(x<<1) + 1, i] = -1; }
                    if (input[i][x] == '@')
                    {
                        startx = x<<1;
                        starty = i;
                        map[(x<<1), i] = 0;
                        map[(x<<1) + 1, i] = 0;
                    }
                    if (input[i][x] == 'O') { map[x<<1, i] = 1; map[(x<<1) + 1, i] = 2; }
                    if (input[i][x] == '.') { map[x<<1, i] = 0; map[(x<<1) + 1, i] = 0; }
                }
                break;
            case 1:
                instructions += input[i];
                break;
        }
    }
    bool test;
    int endx, endy;
    width *= 2;
    for (int i=0; i<instructions.Length; i++)
    {
        switch (instructions[i])
        {
            case '^':
                Console.WriteLine("Up");
                test = moveup2(startx, starty, map, width, height);
                if (test)
                {
                    for (int k = 0; k < height; k++)
                    {
                        for (int j = 0; j < width; j++)
                        {
                            if (map[j, k] > 2)
                            {
                                map[j, k - 1] = map[j, k] - 4;
                                map[j, k] = 0;
                            }
                        }
                    }
                }
                if (!test)
                {
                    for (int k = 0; k < height; k++)
                    {
                        for (int j = 0; j < width; j++)
                        {
                            if (map[j, k] > 2) map[j, k] -= 4;
                        }
                    }
                }
                if (test) starty--;
                break;
            case '>':
                Console.WriteLine("Right");
                moveright2(startx, starty, map, out endx, out endy);
                startx = endx; starty = endy;
                break;
            case 'v':
                Console.WriteLine("Down");
                test = movedown2(startx, starty, map, width, height);
                if (test)
                {
                    for (int k = height - 1; k >= 0; k--)
                    {
                        for (int j = 0; j < width; j++)
                        {
                            if (map[j, k] > 2)
                            {
                                map[j, k + 1] = map[j, k] - 4;
                                map[j, k] = 0;
                            }
                        }
                    }
                }
                if (!test)
                {
                    for (int k = 0; k<height; k++)
                    {
                        for (int j=0; j<width; j++) {
                            if (map[j, k] > 2) map[j, k] -= 4;
                        }
                    }
                }

                if (test) starty++;
                break;
            case '<':
                Console.WriteLine("Left");
                moveleft2(startx, starty, map, out endx, out endy);
                startx = endx; starty = endy;
                break;
        }
        //result = print_map(map, width, height);
        //Console.ReadLine();
    }
    result = print_map(map, width, height);
    Console.WriteLine(result);
    Console.ReadLine();
}

Stopwatch t = new Stopwatch();
t.Start();
//P1();
t.Stop();
Console.WriteLine("P1 took " + t.ElapsedMilliseconds/1000.0 + " seconds");
t.Restart();
P2();
t.Stop();
Console.WriteLine("P2 took " + t.ElapsedMilliseconds / 1000.0 + " seconds");
