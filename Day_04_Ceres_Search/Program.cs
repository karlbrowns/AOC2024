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
    string pattern = "X";
    Regex r = new Regex(pattern);
    int xmax = input[0].Length;
    int ymax = input.Count;
    int x, y;
    for (int i=0; i<input.Count; i++)
    {
        y = i;
        MatchCollection xs = r.Matches(input[i]);
        for (int j=0; j<xs.Count; j++)
        {
            x = xs[j].Index;
            if ((x > 2) && (input[y][x - 1] == 'M') && (input[y][x - 2] == 'A') && (input[y][x - 3] == 'S')) result++;
            if ((x>2) && (y > 2) && (input[y-1][x - 1] == 'M') && (input[y-2][x - 2] == 'A') && (input[y-3][x - 3] == 'S')) result++;
            if ((y > 2) && (input[y-1][x] == 'M') && (input[y-2][x] == 'A') && (input[y-3][x] == 'S')) result++ ;
            if ((x+3<xmax) && (y > 2) && (input[y-1][x + 1] == 'M') && (input[y-2][x + 2] == 'A') && (input[y-3][x + 3] == 'S')) result++;
            if ((x+3 < xmax) && (input[y][x + 1] == 'M') && (input[y][x + 2] == 'A') && (input[y][x + 3] == 'S')) result++;
            if ((x+3 < xmax) && (y+3 < ymax) && (input[y+1][x + 1] == 'M') && (input[y+2][x + 2] == 'A') && (input[y+3][x + 3] == 'S')) result++;
            if ((y+3 < ymax) && (input[y+1][x] == 'M') && (input[y+2][x] == 'A') && (input[y+3][x] == 'S')) result++;
            if ((x > 2) && (y+3<ymax) && (input[y+1][x - 1] == 'M') && (input[y+2][x - 2] == 'A') && (input[y+3][x - 3] == 'S')) result++;

        }
    }
    Console.WriteLine(result);
    Console.ReadLine();
}

void P2()
{
    int result = 0;
    int index = 0;
    String data = "input.txt";
    List<string> input = read_input(data);
    string pattern = "A";
    Regex r = new Regex(pattern);
    int xmax = input[0].Length;
    int ymax = input.Count;
    int x, y;
    for (int i = 0; i < input.Count; i++)
    {
        y = i;
        MatchCollection xs = r.Matches(input[i]);
        for (int j = 0; j < xs.Count; j++)
        {
            x = xs[j].Index;
            if ((x > 0) && (y > 0) && (x + 1 < xmax) && (y + 1 < ymax)
                && (input[y - 1][x - 1] == 'M') && (input[y - 1][x + 1] == 'M')
                && (input[y + 1][x - 1] == 'S') && (input[y + 1][x + 1] == 'S')) { Console.WriteLine(x + " " + y); result++; }
            if ((x > 0) && (y > 0) && (x + 1 < xmax) && (y + 1 < ymax) 
                && (input[y - 1][x - 1] == 'M') && (input[y - 1][x + 1] == 'S')
                && (input[y + 1][x - 1] == 'M') && (input[y + 1][x + 1] == 'S')) { Console.WriteLine(x + " " + y); result++; }
            
            if ((x > 0) && (y > 0) && (x + 1 < xmax) && (y + 1 < ymax) 
                && (input[y - 1][x - 1] == 'S') && (input[y - 1][x + 1] == 'S')
                && (input[y + 1][x - 1] == 'M') && (input[y + 1][x + 1] == 'M')) { Console.WriteLine(x + " " + y); result++; }
            if ((x > 0) && (y > 0) && (x + 1 < xmax) && (y + 1 < ymax) 
                && (input[y - 1][x - 1] == 'S') && (input[y - 1][x + 1] == 'M')
                && (input[y + 1][x - 1] == 'S') && (input[y + 1][x + 1] == 'M')) { Console.WriteLine(x + " " + y); result++; }

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
